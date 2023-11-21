namespace BotWars.Gry
{
    public class GameModel
    {
        public long Id { get; set; }
        public int NumberOfPlayers { get; set; }
        public DateTime LastModification { get; set; }
        public bool IsAvailableForPlay { get; set; }
        public String InterfaceDefinition { get; set; }
        public String GameInstruction { get; set; }
        public String GameFile { get; set; }

    }
}
