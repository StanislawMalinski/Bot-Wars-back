using Shared.DataAccess.DataBaseEntities;

namespace BusinessLogic.Gameplay;

public class GameResult
{
    public int Winner {  get; set; }
    public Bot botWinner { get; set; }
    public Bot botLoser { get; set; }
}