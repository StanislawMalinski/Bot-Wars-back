namespace Shared.DataAccess.DataBaseEntities
{
    public class MatchPlayers
    {
        public long Id { get; set; }
       
        //public long PlayerId { get; set; }
        //public Player? Player { get; set; }
        public long BotId { get; set; }
        public Bot? Bot { get; set; }
        //public long TournamentId { get; set; }
        //public Tournament? Tournament { get; set; }
        public long MatchId { get; set; }
        public Matches? Matches { get; set; }


    }
}
