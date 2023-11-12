namespace BotWars.Gry
{
    public class Tournament
    {
        public long Id { get; set; }
        public string TournamentTitles { get; set; }
        public string Description { get; set; }
        public long GameId { get; set; }
        public Game? Game { get; set; }
        public int PlayersLimit { get; set; }
        public DateTime TournamentsDate { get; set; }
        public DateTime PostedDate { get; set; }
        public bool WasPlayedOut { get; set; }
        public String? Contrains { get; set; }
        public String? Image {  get; set; }

        public ArchivedMatchPlayers? ArchivedMatchPlayers { get; set; }
        public List<ArchivedMatches>? ArchivedMatches { get; set; }
        public List<TournamentReference>? TournamentReference { get; set; }
    }
}
