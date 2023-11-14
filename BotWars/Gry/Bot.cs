using BotWars.TournamentData;

namespace BotWars.Gry
{
    public class Bot
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public Player? Players { get; set; }
        public long GameId { get; set; }   
        public Game? Games { get; set; }
        public String? BotFile { get; set; }

        public List<TournamentReference>? TournamentReference { get; set; }
    }
}
