namespace Shared.DataAccess.DTO;

public class TournamentFilterRequest
{
    public string? TournamentTitle { get; set; }
    public DateTime? MinPlayOutDate { get; set; }
    public DateTime? MaxPlayOutDate { get; set; }
    public string? Creator { get; set; }
    public string? UserParticipation { get; set; }
}