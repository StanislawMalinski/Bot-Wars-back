namespace Shared.DataAccess.DataBaseEntities
{
    public class Matches
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game? Game { get; set; }  
        public long TournamentsId { get; set; }
        public Tournament? Tournament { get; set; }
        public DateTime Played { get; set; }
        public string Match;

        public List<MatchPlayers>? MatchPlayers { get; set; }
    }
}
