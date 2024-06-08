using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.BackgroundWorkers.Data;

public class GameData
{
    public Game Game;
    public string Id { get; set; }
    public int Number { get; set; }
    public List<Bot> BotsId { get; set; }
}