namespace Shared.DataAccess.DataBaseEntities
{
    public class Bot
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public Player? Player { get; set; }
        public long GameId { get; set; }   
        public Game? Games { get; set; }
        public string BotFile { get; set; }

       public List<TournamentReference>? TournamentReference { get; set; }
    }
}
