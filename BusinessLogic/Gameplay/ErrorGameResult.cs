using BusinessLogic.Gameplay.Interface;

namespace BusinessLogic.Gameplay
{
    public class ErrorGameResult : GameResult
    {
        public ErrorGameStatus Status { get; set; }
    }
}
