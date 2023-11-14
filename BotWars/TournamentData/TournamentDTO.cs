using BotWars.Gry;

namespace BotWars.TournamentData
{
    public class TournamentDTO
    {
        public long Id { get; set; }
        public string TournamentsTitle { get; set; }
        public string Description { get; set; }
        public long GameId { get; set; }
        public int PlayersLimit { get; set; }
        public DateTime TournamentsDate { get; set; }
        public DateTime PostedDate { get; set; }
        public bool WasPlayedOut { get; set; }
        public string? Contrains { get; set; }
        public string? Image { get; set; }
    }
}
