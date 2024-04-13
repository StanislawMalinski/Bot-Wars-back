using Engine.BusinessLogic.Gameplay.Interface;

namespace Engine.BusinessLogic.Gameplay;

 public class ErrorGameResult : GameResult
{
    public ErrorGameStatus Status { get; set; }
    public bool BotError  { get; set; }
    public bool GameError  { get; set; }
    public long BotErrorId { get; set; }
    public ErrorGameStatus ErrorGameStatus { get; set; }
}