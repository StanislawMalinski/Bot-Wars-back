namespace Engine.BusinessLogic.Gameplay.Interface;

public abstract class GameResult
{
    DateTime DateEnded { get; set; }
    public string gameLog { get; set; }
    public GameResult()
    {
        DateEnded = DateTime.Now;
    }
}