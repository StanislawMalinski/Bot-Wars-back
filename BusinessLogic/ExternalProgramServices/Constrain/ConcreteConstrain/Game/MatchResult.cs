namespace BotWars.Models
{
    public class MatchResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
    }
}
