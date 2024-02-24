﻿using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay;
using Shared.DataAccess.DataBaseEntities;
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
    public long TourId { get; set; }
    public TournamentWorker(ICache cache, IQueue queue,TournamentRepository tournamentRepository,MatchRepository matchRepository , IAchievementsRepository achievementsRepository,long tournamentId)
    {
        _tournamentRepository = tournamentRepository;
        _queue = queue;
        TourId = tournamentId;
        _matchRepository = matchRepository;
        _achievementsRepository = achievementsRepository;
    }

    public async Task Invoke()
    {
        
        string identifier = "Tournament "+ TourId+" ";
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
                        var playedGame = await _matchRepository.IsMatchPlayed(TourId, Games[key].Identifier);
                        if (playedGame.IsSuccess)
                        {
                            var botWinner = playedGame.Match(x => x.Data, x=>null);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, botWinner.PlayerId);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, botWinner.PlayerId);
                            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, botLoser.PlayerId);
                            GameInfo gameInfo = Games[key];
                            gameInfo.Played = true;
                            gameInfo.Bot = botWinner;
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
            
            var playedGame = await _matchRepository.IsMatchPlayed(TourId, Games[0].Identifier);
            while (!playedGame.IsSuccess)
            {
                await Task.Delay(2000);
                playedGame = await _matchRepository.IsMatchPlayed(TourId, Games[0].Identifier);
            }
            var botWinner = playedGame.Match(x => x.Data, x=>null);
            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotWinner.PlayerId);
            //await _achievementsRepository.UpDateProgress(AchievementsTypes.WinGames, res.BotWinner.PlayerId);
            //await _achievementsRepository.UpDateProgress(AchievementsTypes.GamePlayed, res.BotLoser.PlayerId);
            await _achievementsRepository.UpDateProgress(AchievementsTypes.TournamentsWon, botWinner.PlayerId);
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