using BotWars.TournamentData;

namespace Shared.DataAccess.DataBaseEntities
{
    public class ArchivedMatches
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public Game? Game { get; set; }  
        public long TournamentsId { get; set; }
        public Tournament? Tournament { get; set; }
        public DateTime Played { get; set; }
        public String? Match;

        public List<ArchivedMatchPlayers>? ArchivedMatchPlayers { get; set; }
    }
}
