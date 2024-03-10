namespace Shared.DataAccess.DTO.Responses;

public class GameResponse
{
    public long Id { get; set; }
    public int NumbersOfPlayer { get; set; }
    public DateTime LastModification { get; set; }
    public string? GameFileName { get; set; }
    public string? GameInstructions { get; set; }
    public string? InterfaceDefinition { get; set; }
    public bool IsAvailableForPlay { get; set; }
    
        
    public List<long>? BotIds { get; set; }
    public List<long>? TournamentsIds { get; set; }
    public List<long>? MatchesIds { get; set; }
    
}