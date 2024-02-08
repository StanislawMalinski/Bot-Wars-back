﻿using BusinessLogic.BackgroundWorkers.Data;
using BusinessLogic.Gameplay;
using Coravel.Cache.Interfaces;
using Coravel.Invocable;

namespace BusinessLogic.BackgroundWorkers;

public class GameWorker : IInvocable, IInvocableWithPayload<GameData>
{
    private ICache _cache;
    public GameWorker(ICache cache)
    {
        _cache = cache;
    }

    public GameData Payload { get; set; }
    GameResult BigDataLocalFunction() 
    {
        GameManager gameManager = new GameManager();
        GameResult result =  gameManager.PlayGame(Payload.Game,Payload.BotsId);
        Console.WriteLine("fight bot " +result.botWinner.Id + " bot " + result.botLoser.Id);
        Console.WriteLine("bot winner "+ result.botWinner.Id);
        return result;
    }
    public async Task Invoke()
    {
        
        GameManager gameManager = new GameManager();
        GameResult result =  gameManager.PlayGame(Payload.Game,Payload.BotsId);
        _cache.Remember(Payload.Id, BigDataLocalFunction, TimeSpan.FromHours(2));
    }

   
}