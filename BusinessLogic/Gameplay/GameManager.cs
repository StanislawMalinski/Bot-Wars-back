using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class GameManager
{
    public GameResult PlayGame(Game game, List<Bot> bots)
    {
        var res = bots.ToArray();
        return new GameResult()
        {
            botWinner = res[0],
            botLoser = res[1],
            Winner = 1
        };


    }
}