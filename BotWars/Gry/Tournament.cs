namespace BotWars.Gry
{
    public class Tournament
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game? Game { get; set; }
        public int PlayersLimi { get; set; }
        public DateTime TournamentsDate { get; set; }
        public DateTime PostedDate { get; set; }
        public bool WasPlayedOut { get; set; }
        public String? Contrains { get; set; }

        public ArchivedMatchPlayers? ArchivedMatchPlayers { get; set; }
        public ArchivedMatches? ArchivedMatches { get; set; }
    }
}
