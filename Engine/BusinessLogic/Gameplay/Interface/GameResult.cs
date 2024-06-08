namespace Engine.BusinessLogic.Gameplay.Interface;

public abstract class GameResult
{
    public GameResult()
    {
        DateEnded = DateTime.Now;
    }

    private DateTime DateEnded { get; set; }
    public string gameLog { get; set; }
}