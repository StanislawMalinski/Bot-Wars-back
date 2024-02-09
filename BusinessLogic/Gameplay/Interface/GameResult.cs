namespace BusinessLogic.Gameplay.Interface
{
    public abstract class GameResult
    {
        DateTime DateEnded { get; set; }
        public GameResult()
        {
            DateEnded = DateTime.Now;
        }
    }
}
