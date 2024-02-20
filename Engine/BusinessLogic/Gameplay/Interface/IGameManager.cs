using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay.Interface;

public interface IGameManager
{
     GameResult PlayGame(Game game, List<Bot> bots);
}