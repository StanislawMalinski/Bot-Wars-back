using System.Text.Json;
using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.Services;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class TournamentWorker: IInvocable 
{
    private readonly TournamentResolver _resolver;
    private readonly IScheduler _scheduler;
    private readonly WebSocketService _websocketService;
    private Dictionary<int,GameInfo> _games;

    public TournamentWorker(IScheduler scheduler, TournamentResolver resolver, long task, WebSocketService webSocketService)
    {
        _scheduler = scheduler;
        _resolver = resolver;
        TaskId = task;
        _websocketService = webSocketService;
    }

    private long TaskId { get; }
    private long TourId { get; set; }

    public async Task Invoke()
    {
        
        Console.WriteLine($"Turnie się rozpoczol {TaskId}");
        
        var task = (await _resolver.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        
        if(task == null) return;
        Console.WriteLine($"Operuje na turnieju {task.Id}");
        TourId = task.OperatingOn;
        var gameBot = (await _resolver.GetGame(TourId)).Match(x=>x.Data,x=>null);
        
        if ((await _resolver.AreAnyMatchesPlayed(TourId)).IsError)
        {
            Console.WriteLine("Truniej katywcja brak jeszcze meczów");
            await _resolver.StartPlaying(TourId);
            var botList = await _resolver.GetRegisterBots(TourId);
            //var tournament = (await _tournamentRepository.GetTournamentAsync(TourId)).Match(x=>x.Data,x=>null);
            List<Bot?> bots = botList.Match(x => x.Data, x => new List<Bot>());
            Console.WriteLine($"LIE BOTÓW {bots.Count}");
            if (bots.Count() < 2)
            {
                if (bots.Count == 1)
                    await _resolver.EndTournament(TourId,bots.First().Id,TaskId);
                else
                    await _resolver.EndTournament(TourId,TaskId);
            }
            MadeHeap(bots);
            
            await _resolver.CreateLadder([.._games.Values],TourId);
            
            var startGames =  (await _resolver.GetMatchesReadyToPlay(TourId) ).Match(x=>x.Data,x=>new List<long>());
            foreach (var p in startGames) await _resolver.PlayMatch(p);

            await WebSockets();
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }
        
        var flag = false;

        var playedGames =  (await _resolver.GetPlayedMatches(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in playedGames)
        {
            flag = true;
            await _resolver.ResolveMatch(p);
        }
        var readyGames =  (await _resolver.GetMatchesReadyToPlay(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in readyGames)
        {
            flag = true;
            await _resolver.PlayMatch(p);
        }

        if (flag) await WebSockets();
        var tournamentWinner = await _resolver.IsMatchResolved(TourId, "0");
        if (tournamentWinner.IsError)
        {
            
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }
   
        var end = await _resolver.EndTournament(TourId,tournamentWinner.Match(x=>x.Data,x=>0), TaskId);
        if (end.IsError)
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
        Console.WriteLine("finished tournament "+ TaskId + " " + TourId);
        
    }

    private void MadeHeap(List<Bot?> bots)
    {
        _games = new Dictionary<int, GameInfo>();
        var i = 0;
        foreach (var bot in bots)
            if (i == 0)
            {
                _games.Add(0,new GameInfo(true,0,bot,null));
                i++;
            }
            else
            {
                var p = (i - 1) / 2;
                var gameInfo = _games[p];
                gameInfo.Key = i;
                _games.Add(i,gameInfo);
                _games.Remove(p);
                _games.Add(i+1,new GameInfo(true,i+1,bot,null));
                i += 2;
            }

        List<int> keyList = [.._games.Keys];
        foreach (var key in keyList)
        {
            var pkey = (key - 1) / 2;
            if (!_games.ContainsKey(pkey))
            {
                _games.Add(pkey, new GameInfo(false,pkey,null,new List<Bot>
                {
                    _games[key].Bot
                }));
                _games.Remove(key);
            }
            else
            {
                var gameInfo = _games[pkey];
                gameInfo.ReadyToPlay = true;
                gameInfo.Bots.Add(_games[key].Bot);
                _games[pkey] = gameInfo;
                _games.Remove(key);
            }
        }

    }

    private async Task WebSockets()
    {
        var status = (await _resolver.GetTournamentMatchStatus(TourId)).Match(x => x.Data, null);
        if (status != null)
        {
            var jsonStatus = JsonSerializer.Serialize(status);
            Console.WriteLine(jsonStatus);
            await _websocketService.SendUpdateToAllClients(jsonStatus);
            Console.WriteLine("koniec wib sokiety");
        } 
        else
        {
            Console.WriteLine("Status cannot be obtained!");
        }
    }
}