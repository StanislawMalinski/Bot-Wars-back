using BusinessLogic.BackgroundWorkers.Data;
using BusinessLogic.Gameplay;
using Coravel.Cache.Interfaces;
using Coravel.Events.Interfaces;
using Coravel.Invocable;
using Coravel.Queuing.Broadcast;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BusinessLogic.BackgroundWorkers;

public class TournamentWorker : IInvocable
{
    private Scheduler _scheduler; 
    private ICache _cache;
    private IQueue _queue;
    private TournamentRepository _tournamentRepository;
    private IAchievementsRepository _achievementsRepository;
    public long TourId { get; set; }
    public TournamentWorker(ICache cache, IQueue queue,TournamentRepository tournamentRepository, IAchievementsRepository achievementsRepository,long tournamentId)
    {
        _tournamentRepository = tournamentRepository;
        _cache = cache;
        _queue = queue;
        TourId = tournamentId;
        _achievementsRepository = achievementsRepository;
    }

    public async Task Invoke()
    {
        
        string identifier = this.ToString() +" "+ TourId+ " "+ DateTime.Now;
        Console.WriteLine("Ropoczy się turniej "+ TourId);
        var botList = await _tournamentRepository.TournamentBotsToPlay(TourId);

        List<Bot> bots = botList.Match(x => x.Data, x => new List<Bot>());
        
        MadeHeap(bots,identifier);
        Game gamebot = (await _tournamentRepository.TournamentGame(TourId)).Match(x=>x.Data,x=>null);
      
        while (Games.Count() > 1)
        {
            List<int> keylist = [..Games.Keys];
            foreach (int key in keylist)
            {
                if (Games.ContainsKey(key))
                {
                    if (Games[key].Played)
                    {
                        int key2 = key + (key % 2) * 2 - 1;
                        int pkey = ((key - 1) / 2);
                        if (Games.ContainsKey(key2) && Games[key2].Played)
                        {
                            _queue.QueueInvocableWithPayload<GameWorker, GameData>(new GameData()
                            {
                                BotsId = new List<Bot>()
                                {
                                    Games[key].Bot,
                                    Games[key2].Bot
                                },
                                Game = gamebot,
                                Id = identifier + pkey
                            });
                            Games.Add(pkey,new GameInfo(identifier + pkey,false,pkey,null ));
                            Games.Remove(key);
                            Games.Remove(key2);
                        }
                    }
                    else
                    {
                        if (await _cache.HasAsync(Games[key].Identifier))
                        {
                            SuccessfullGameResult res = await _cache.GetAsync<SuccessfullGameResult>(Games[key].Identifier);
                            _cache.Forget(Games[key].Identifier);
                            await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotWinner.PlayerId);
                            await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, res.BotWinner.PlayerId);
                            await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotLoser.PlayerId);
                            GameInfo gameInfo = Games[key];
                            gameInfo.Played = true;
                            gameInfo.Bot = res.BotWinner;
                            Games[key] = gameInfo;

                        }
                    }
                }
            }

            await Task.Delay(2000);
        }

        if (Games.Count() == 0)
        {
            
        }else if (Games[0].Played)
        {
            await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, Games[0].Bot.PlayerId);
        }
        else
        {
            
            
            while (!(await _cache.HasAsync(identifier+0)))
            {
                await Task.Delay(2000);
            }
            SuccessfullGameResult res = await _cache.GetAsync<SuccessfullGameResult>(Games[0].Identifier);
            _cache.Forget(Games[0].Identifier);
            await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotWinner.PlayerId);
            await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, res.BotWinner.PlayerId);
            await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotLoser.PlayerId);
            await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, res.BotWinner.PlayerId);
        }

        await _tournamentRepository.TournamentEnded(TourId);
        Console.WriteLine("finished tournament");
        
    }

    private struct GameInfo(string identifier, bool played, int key, Bot bot)
    {
        public int Key  = key;
        public string Identifier  = identifier;
        public bool Played { get; set; } = played;
        public Bot Bot = bot;
    }

    private Dictionary<int,GameInfo> Games;

    private void MadeHeap(List<Bot> bots,string identifier)
    {
        Games = new Dictionary<int, GameInfo>();
        int i = 0;
        foreach (var bot in bots)
        {
            if (i == 0)
            {
                Games.Add(0,new GameInfo(identifier + '0',true,0,bot));
                i++;
            }
            else
            {
                int p = (i - 1) / 2;
                GameInfo gameInfo = Games[p];
                gameInfo.Key = i;
                gameInfo.Identifier = identifier + i;
                Games.Add(i,gameInfo);
                Games.Remove(p);
                Games.Add(i+1,new GameInfo(identifier + (i+1),true,i+1,bot));
                i += 2;
            }
        }
    }

    
}