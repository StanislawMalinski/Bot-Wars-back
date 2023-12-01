namespace Shared.DataAccess.DataBaseEntities
{
    public class Game //: IFileData
    {
        public long Id { get; set; }
        public int NumbersOfPlayer { get; set; }
        public DateTime LastModification { get; set; }
        public string? GameFile { get; set; }
        public string? GameInstructions { get; set; }
        public string? InterfaceDefinition { get; set; }
        public bool IsAvaiableForPlay { get; set; }


        public List<Bot>? Bot { get; set; }
        public List<Tournament>? Tournaments { get; set; }
        public List<ArchivedMatches>? ArchivedMatches { get; set; }

        

    }
}
