using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class GameManager
{
    public GameResult PlayGame(Game game, List<Bot> bots)
    {
        Random random = new Random();
        return new GameResult()
        {
            Winner = random.Next(1, 2)
        };
    }
}