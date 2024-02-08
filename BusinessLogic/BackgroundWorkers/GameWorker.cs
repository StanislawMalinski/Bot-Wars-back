using BusinessLogic.BackgroundWorkers.Data;
using BusinessLogic.Gameplay;
using Coravel.Invocable;

namespace BusinessLogic.BackgroundWorkers;

public class GameWorker : IInvocable, IInvocableWithPayload<GameData>
{
    public GameWorker()
    {
    }

    public GameData Payload { get; set; }
    public async Task Invoke()
    {
        GameManager gameManager = new GameManager();
        gameManager.PlayGame(Payload.Game,Payload.BotsId);
    }

   
}