using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.Gameplay;

 public class ErrorGameResult : GameResult
{
    public ErrorGameStatus Status { get; set; }
}