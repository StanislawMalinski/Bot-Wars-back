using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DataBaseEntities
{
    public class Matches
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public long TournamentsId { get; set; }
        public long LogId { get; set; }
        public DateTime Played { get; set; }
        public GameStatus Status;
        public long Winner { get; set; }
        public string? Data { get; set; }

        public List<MatchPlayers>? MatchPlayers { get; set; }
        public Tournament? Tournament { get; set; }
        public Game? Game { get; set; } 
    }
}
