using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class TournamentWorker: IInvocable
{
    private Scheduler _scheduler; 
    //private ICache _cache;
    private IQueue _queue;
    private TournamentRepository _tournamentRepository;
    private IAchievementsRepository _achievementsRepository;
    private MatchRepository _matchRepository;
    private TaskRepository _taskRepository;
    public long TourId { get; set; }
    public TournamentWorker(ICache cache, IQueue queue,TournamentRepository tournamentRepository,TaskRepository taskRepository, MatchRepository matchRepository , IAchievementsRepository achievementsRepository,long tournamentId)
    {
        _tournamentRepository = tournamentRepository;
        _queue = queue;
        TourId = tournamentId;
        _matchRepository = matchRepository;
        _achievementsRepository = achievementsRepository;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        
        string identifier = "Tournament "+ TourId+" ";
        Console.WriteLine("Ropoczy się turniej "+ TourId);
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
        MadeHeap(bots,identifier);
        
        await _matchRepository.CreateAllLadder([.._games.Values],TourId);
        Game gamebot = (await _tournamentRepository.TournamentGame(TourId)).Match(x=>x.Data,x=>null);
        List<int> keylist = [.._games.Keys];
        foreach (int key in keylist)
        {
            if (_games[key].ReadyToPlay)
            {
                /*
                Console.WriteLine("aaa");
                var matchId = (await _matchRepository.UpdateMatch(TourId, gameInfo,new List<Bot>(){_games[key].Bot})).Match(x=>x.Data,x=>0);
                Console.WriteLine("aaavccx");
                var taskId =  (await _taskRepository.CreateTask(TaskTypes.PlayGame, matchId, DateTime.Now)).Match(x=>x.Data,x=>0);
                Console.WriteLine("aaaagfg");
                _queue.QueueInvocableWithPayload<GameWorker, long>(matchId);
                Console.WriteLine("aaalkdfnsd");*/
            }
        }

        while (_games.Count() > 1)
        {
            keylist = [.._games.Keys];
            foreach (int key in keylist)
            {
                if (_games.ContainsKey(key))
                {
                    if (_games[key].Played)
                    {
                        int pkey = ((key - 1) / 2);
                        if (!_games.ContainsKey(pkey))
                        {
                            var tgi = new GameInfo(false, pkey, null, new List<Bot>()
                            {
                                _games[key].Bot,
                            });
                            _games.Add(pkey, tgi);
                            var matchId = (await _matchRepository.CreateMatch(TourId, tgi)).Match(x=>x.Data,x=>0);
                            _games.Remove(key);
                        }
                        else
                        {
                            GameInfo gameInfo = _games[pkey];
                            gameInfo.ReadyToPlay = true;
                            gameInfo.Bots.Add(_games[key].Bot);
                            _games[pkey] = gameInfo;
                            _games.Remove(key);
                            Console.WriteLine("aaa");
                            var matchId = (await _matchRepository.UpdateMatch(TourId, gameInfo,new List<Bot>(){_games[key].Bot})).Match(x=>x.Data,x=>0);
                            Console.WriteLine("aaavccx");
                            var taskId =  (await _taskRepository.CreateTask(TaskTypes.PlayGame, matchId, DateTime.Now)).Match(x=>x.Data,x=>0);
                            Console.WriteLine("aaaagfg");
                            _queue.QueueInvocableWithPayload<GameWorker, long>(matchId);
                            Console.WriteLine("aaalkdfnsd");
                        }
                    }
                    else
                    {
                        var playedGame = await _matchRepository.IsMatchPlayed(TourId,  key.ToString());
                        if (playedGame.IsSuccess)
                        {
                            var botWinner = playedGame.Match(x => x.Data, x=>null);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, botWinner.PlayerId);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, botWinner.PlayerId);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, botLoser.PlayerId);
                            GameInfo gameInfo = _games[key];
                            gameInfo.Played = true;
                            gameInfo.Bot = botWinner;
                            _games[key] = gameInfo;

                        }
                    }
                }
            }

            await Task.Delay(2000);
        }

       
        
        var lastGame = await _matchRepository.IsMatchPlayed(TourId, _games[0].Key.ToString());
        while (!lastGame.IsSuccess)
        {
            await Task.Delay(2000);
            lastGame = await _matchRepository.IsMatchPlayed(TourId, _games[0].Key.ToString());
        }
        var botTWinner = lastGame.Match(x => x.Data, x=>null);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotWinner.PlayerId);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, res.BotWinner.PlayerId);
        //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotLoser.PlayerId);
        await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, botTWinner.PlayerId);
        

        await _tournamentRepository.TournamentEnded(TourId);
        Console.WriteLine("finished tournament");
        
    }

   
    

    private Dictionary<int,GameInfo> _games;

    private void MadeHeap(List<Bot?> bots,string identifier)
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