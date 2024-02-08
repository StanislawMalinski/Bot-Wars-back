using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class GameResult
{
    public int Winner {  get; set; }
    public Bot bot { get; }
}