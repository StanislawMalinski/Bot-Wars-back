using BusinessLogic.BackgroundWorkers.Data;
using BusinessLogic.Gameplay;
using Coravel.Cache.Interfaces;
using Coravel.Events.Interfaces;
using Coravel.Invocable;
using Coravel.Queuing.Broadcast;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;

namespace BusinessLogic.BackgroundWorkers;

public class TournamentWorker : IInvocable, IInvocableWithPayload<int>
{
    private Scheduler _scheduler; 
    private ICache _cache;
    private IQueue _queue;
    private TournamentRepository _tournamentRepository;
    public int Payload { get; set; }
    private Guid da;
    public TournamentWorker(ICache cache, IQueue queue,TournamentRepository tournamentRepository)
    {
        _tournamentRepository = tournamentRepository;
        _cache = cache;
        _queue = queue;
    }

    public async Task Invoke()
    {
        
        string identifier = this.ToString() + DateTime.Now;
        var botList = await _tournamentRepository.TournamentBotsToPlay(Payload);
        int steps =(int) Math.Ceiling(Math.Log2(botList.Match((data => data.Data.Count()),(result => 0))));
        if (steps == 0)
        {
            return;
        }
        int begin = 0;
        for (int i = 0; i < steps; i++)
        {
            begin *= 2;
            begin++;
        }

        List<Bot> bots = botList.Match(x => x.Data, x => new List<Bot>());
        
        MadeHeap(bots,identifier);
        Game gamebot = (await _tournamentRepository.TournamentGame(Payload)).Match(x=>x.Data,x=>null);
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
                                Id = identifier + identifier + pkey
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
                            GameResult res = await _cache.GetAsync<GameResult>(Games[key].Identifier);
                           
                            GameInfo gameInfo = Games[key];
                            gameInfo.Played = true;
                            gameInfo.Bot = res.bot;
                            Games[key] = gameInfo;

                        }
                    }
                }
            }

            await Task.Delay(1000);
        }
        
        
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
        int p;
        foreach (var bot in bots)
        {
            if (i == 0)
            {
                Games.Add(0,new GameInfo(identifier + '0',true,0,bot));
                i++;
            }
            else
            {
                p = (i - 1) / 2;
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