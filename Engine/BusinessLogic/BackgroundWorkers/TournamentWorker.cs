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
    private IScheduler _scheduler; 
    //private ICache _cache;
    
    private TournamentRepository _tournamentRepository;
    private IAchievementsRepository _achievementsRepository;
    private MatchRepository _matchRepository;
    private TaskRepository _taskRepository;
    public long TourId { get; set; }
    public TournamentWorker(IScheduler scheduler ,TournamentRepository tournamentRepository,TaskRepository taskRepository, MatchRepository matchRepository , IAchievementsRepository achievementsRepository,long tournamentId)
    {
        _scheduler = scheduler;
        _tournamentRepository = tournamentRepository;
        TourId = tournamentId;
        _matchRepository = matchRepository;
        _achievementsRepository = achievementsRepository;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        
        Console.WriteLine("jaki tunie działa");
       
        Game gamebot = (await _tournamentRepository.TournamentGame(TourId)).Match(x=>x.Data,x=>null);
        List<int> keylist;
        if ((await _matchRepository.AreAny(TourId)).IsError)
        {
            
            var botList = await _tournamentRepository.TournamentBotsToPlay(TourId);
            //var tournament = (await _tournamentRepository.GetTournamentAsync(TourId)).Match(x=>x.Data,x=>null);
            List<Bot?> bots = botList.Match(x => x.Data, x => new List<Bot>());
        
            if (bots.Count() < 2)
            {
                if (bots.Count == 1)
                {
                    await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, bots.First().PlayerId);
                }
                await _tournamentRepository.TournamentEnded(TourId);
            }
            MadeHeap(bots);
            
            await _matchRepository.CreateAllLadder([.._games.Values],TourId);
            
            var startGames =  (await _matchRepository.GetAllReadyToPlay(TourId)).Match(x=>x.Data,x=>new List<long>());
            foreach (var p in startGames)
            {
                await _matchRepository.PlayMatch(p);
            }
            _scheduler.ScheduleWithParams<TournamentWorker>(TourId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TourId);
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
            _scheduler.ScheduleWithParams<TournamentWorker>(TourId)
                .EverySeconds(8).Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ TourId);
            return;
        }
        
        //var botTWinner = lastGame.Match(x => x.Data, x=>null);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotWinner.PlayerId);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, res.BotWinner.PlayerId);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotLoser.PlayerId);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, botTWinner.PlayerId);
        

        await _tournamentRepository.TournamentEnded(TourId);
        Console.WriteLine("finished tournament");
        
       
        
    }

   
    

    private Dictionary<int,GameInfo> _games;

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