namespace Shared.DataAccess.DataBaseEntities
{
    public class Game
    {
        public long Id { get; set; }
        public int NumbersOfPlayer { get; set; }
        public DateTime LastModification { get; set; }
        public string? GameFile { get; set; }
        public string? GameInstructions { get; set; }
        public string? InterfaceDefinition { get; set; }
        public bool IsAvailableForPlay { get; set; }
        public long FileId { get; set; }
        public Player Creator { get; set; }
        public long CreatorId { get; set; }

        public List<Bot>? Bot { get; set; }
        public List<Tournament>? Tournaments { get; set; }
        public List<Matches>? Matches { get; set; }
    }
}
