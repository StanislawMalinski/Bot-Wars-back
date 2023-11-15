using BotWars.Gry;

namespace BotWars.TournamentData
{
    public class TournamentReference
    {
        public long Id { get; set; }
        public long tournamentId { get; set; }
        public Tournament? Tournament { get; set; }
        public long bodId { get; set; }
        public Bot? Bot { get; set; }
    }
}
