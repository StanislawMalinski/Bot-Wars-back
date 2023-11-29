namespace Communication.APIs.DTOs
{
    public class GameDto
    {
        public int NumbersOfPlayer { get; set; }
        public DateTime LastModification { get; set; }
        public string? GameFile { get; set; }
        public string? GameInstructions { get; set; }
        public string? InterfaceDefinition { get; set; }
        public bool IsAvaiableForPlay { get; set; }

    }
}
