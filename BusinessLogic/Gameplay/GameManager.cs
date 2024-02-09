using BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class GameManager : IGameManager
{
    public GameResult PlayGame(Game gameData, List<Bot> botsData)
    {
        return new SuccessfullGameResult()
        {
            BotWinner = botsData[0],
            BotLoser = botsData[1],
        };
    }
}