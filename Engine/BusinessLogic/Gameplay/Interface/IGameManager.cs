using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay.Interface;

public interface IGameManager
{
     Task<GameResult> PlayGame(Game game, List<Bot> bots,int memoryLimit, int timeLimit);
}