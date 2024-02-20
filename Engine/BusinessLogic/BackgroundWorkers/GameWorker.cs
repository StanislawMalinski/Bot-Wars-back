using Coravel.Cache.Interfaces;
using Coravel.Invocable;
using Engine.BusinessLogic.BackgroundWorkers.Data;
using Engine.BusinessLogic.Gameplay;
using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class GameWorker: IInvocable, IInvocableWithPayload<GameData>
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
        GameResult result =  gameManager.PlayGame(Payload.Game, Payload.BotsId);
        if (result is SuccessfullGameResult successfullResult)
        {
            Console.WriteLine("fight bot " + successfullResult.BotWinner.Id + " bot " + successfullResult.BotLoser.Id);
            Console.WriteLine("bot winner " + successfullResult.BotWinner.Id);
            return result;
        }
        else
        {
            throw new NotImplementedException("Game not successfull");
        }
    }

    public async Task Invoke()
    {
        GameManager gameManager = new GameManager();
        GameResult result =  gameManager.PlayGame(Payload.Game,Payload.BotsId);
        _cache.Remember(Payload.Id, BigDataLocalFunction, TimeSpan.FromHours(2));
    }
}