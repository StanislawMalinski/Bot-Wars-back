using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;


namespace Engine.BusinessLogic.BackgroundWorkers;

public class TournamentWorker: IInvocable 
{
    private readonly IScheduler _scheduler; 
    private readonly TournamentRepository _tournamentRepository;
    private readonly IAchievementsRepository _achievementsRepository;
    private readonly MatchRepository _matchRepository;
    private readonly TaskRepository _taskRepository;
    private long TaskId { get; set; }
    private long TourId { get; set; }
    private Dictionary<int,GameInfo> _games;
    public TournamentWorker(IScheduler scheduler ,TournamentRepository tournamentRepository,TaskRepository taskRepository, MatchRepository matchRepository , IAchievementsRepository achievementsRepository,long task)
    {
        _scheduler = scheduler;
        _tournamentRepository = tournamentRepository;
        TaskId = task;
        _matchRepository = matchRepository;
        _achievementsRepository = achievementsRepository;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        
        Console.WriteLine("jaki tunie działa");
        _Task? task = (await _taskRepository.GetTask(TaskId)).Match(x=>x.Data,x=>null);
        if(task == null) return;
        TourId = task.OperatingOn;
        Game? gameBot = (await _tournamentRepository.TournamentGame(TourId)).Match(x=>x.Data,x=>null);
        if ((await _matchRepository.AreAny(TourId)).IsError)
        {
            await _tournamentRepository.TournamentPlaying(TourId);
            var botList = await _tournamentRepository.TournamentBotsToPlay(TourId);
            //var tournament = (await _tournamentRepository.GetTournamentAsync(TourId)).Match(x=>x.Data,x=>null);
            List<Bot?> bots = botList.Match(x => x.Data, x => new List<Bot>());
        
            if (bots.Count() < 2)
            {
                if (bots.Count == 1)
                {
                    await _tournamentRepository.TournamentEnded(TourId,bots.First().Id,TaskId);
                }
                else
                {
                    await _tournamentRepository.TournamentEnded(TourId,TaskId);
                }
                
            }
            MadeHeap(bots);
            
            await _matchRepository.CreateAllLadder([.._games.Values],TourId);
            
            var startGames =  (await _matchRepository.GetAllReadyToPlay(TourId)).Match(x=>x.Data,x=>new List<long>());
            foreach (var p in startGames)
            {
                await _matchRepository.PlayMatch(p);
            }
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }

        var playedGames =  (await _matchRepository.GetAllPlayed(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in playedGames)
        {
            await _matchRepository.ResolveMatch(p);
        }
        var readyGames =  (await _matchRepository.GetAllReadyToPlay(TourId)).Match(x=>x.Data,x=>new List<long>());
        foreach (var p in readyGames)
        {
            await _matchRepository.PlayMatch(p);
        }

        var tournamentWinner = await _matchRepository.IsResolve(TourId, "0");
        if (tournamentWinner.IsError)
        {
            _scheduler.ScheduleWithParams<TournamentWorker>(TaskId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TaskId);
            return;
        }
        

        var end = await _tournamentRepository.TournamentEnded(TourId,TaskId);
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