using Engine.BusinessLogic.Gameplay.Interface;
using Shared.DataAccess.DataBaseEntities;

namespace Engine.BusinessLogic.Gameplay;

public class DisqualificationGameResult : GameResult
{
    public Bot DisqualifiedBot { get; set; }
    public DisqualificationGameStatus Status { get; set; } 
}