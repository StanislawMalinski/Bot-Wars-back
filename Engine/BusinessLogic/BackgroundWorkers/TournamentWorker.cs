using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;


namespace Engine.BusinessLogic.BackgroundWorkers;

public class TournamentWorker: IInvocable 
{
    private readonly IScheduler _scheduler;
    private readonly TournamentResolver _resolver;
    private long TaskId { get; set; }
    private long TourId { get; set; }
    private Dictionary<int,GameInfo> _games;
    public TournamentWorker(IScheduler scheduler ,TournamentResolver resolver,long task)
    {
        _scheduler = scheduler;
        _resolver = resolver;
        TaskId = task;
      
    }

    public async Task Invoke()
    {
        
        Console.WriteLine($"Turnie się rozpoczol {TaskId}");
        
        _Task? task = (await _resolver.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        
        if(task == null) return;
        Console.WriteLine($"Operuje na turnieju {task.Id}");
        TourId = task.OperatingOn;
        Game? gameBot = (await _resolver.GetGame(TourId)).Match(x=>x.Data,x=>null);
        
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
                {
                    await _resolver.EndTournament(TourId,bots.First().Id,TaskId);
                }
                else
                {
                    await _resolver.EndTournament(TourId,TaskId);
                }
                
            }
            MadeHeap(bots);
            
            await _resolver.CreateLadder([.._games.Values],TourId);
            
            var startGames =  (await _resolver.GetMatchesReadyToPlay(TourId) ).Match(x=>x.Data,x=>new List<long>());
            foreach (var p in startGames)
            {
                await _resolver.PlayMatch(p);
            }
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }
        Console.WriteLine($"Sa już macze początkwowe {TaskId}");
        // Web socety
        await _resolver.GetTournamentMatchStatus(TourId);
        
        
        
        Console.WriteLine("koniec wib sokiety");
        var playedGames =  (await _resolver.GetPlayedMatches(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in playedGames)
        {
            
            await _resolver.ResolveMatch(p);
        }
        var readyGames =  (await _resolver.GetMatchesReadyToPlay(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in readyGames)
        {
            
            await _resolver.PlayMatch(p);
        }
        
        var tournamentWinner = await _resolver.IsMatchResolved(TourId, "0");
        if (tournamentWinner.IsError)
        {
            
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }
        
        var end = await _resolver.EndTournament(TourId,tournamentWinner.Match(x=>x.Data,x=>0), TaskId);
        if (end.IsError)
        {
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
        }
        Console.WriteLine("finished tournament "+ TaskId + " " + TourId);
        
    }
    

    private void MadeHeap(List<Bot?> bots)
    {
        _games = new Dictionary<int, GameInfo>();
        int i = 0;
        foreach (var bot in bots)
        {
            if (i == 0)
            {
                _games.Add(0,new GameInfo(true,0,bot,null));
                i++;
            }
            else
            {
                int p = (i - 1) / 2;
                GameInfo gameInfo = _games[p];
                gameInfo.Key = i;
                _games.Add(i,gameInfo);
                _games.Remove(p);
                _games.Add(i+1,new GameInfo(true,i+1,bot,null));
                i += 2;
            }
        }
        List<int> keyList = [.._games.Keys];
        foreach (int key in keyList)
        {
            int pkey = ((key - 1) / 2);
            if (!_games.ContainsKey(pkey))
            {
                _games.Add(pkey, new GameInfo(false,pkey,null,new List<Bot>() {
                    _games[key].Bot,
                }));
                _games.Remove(key);
            }
            else
            {
                GameInfo gameInfo = _games[pkey];
                gameInfo.ReadyToPlay = true;
                gameInfo.Bots.Add(_games[key].Bot);
                _games[pkey] = gameInfo;
                _games.Remove(key);
            }
        }

    }


    
}