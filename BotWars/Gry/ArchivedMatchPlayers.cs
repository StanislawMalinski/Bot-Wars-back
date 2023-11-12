namespace BotWars.Gry
{
    public class ArchivedMatchPlayers
    {
        public long Id { get; set; }
       
        public long PlayerId { get; set; }
        public Player? Player { get; set; }
        public long TournamentId { get; set; }
        public Tournament? Tournament { get; set; }
        public long MatchId { get; set; }
        public ArchivedMatches? archivedMatches { get; set; }


    }
}
