namespace BotWars.Gry
{
    public class Player
    {
        public long Id { get; set; }
        public String? email { get; set; }
        public String? login { get; set; }

        public List<Bot>? Bot { get; set; }
        public List<ArchivedMatchPlayers>? ArchivedMatchPlayers { get; set; }
    }
}
