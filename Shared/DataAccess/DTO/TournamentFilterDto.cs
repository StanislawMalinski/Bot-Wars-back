namespace Shared.DataAccess.DTO;

public class TournamentFilterDto
{
    public DateTime MinPlayOutDate { get; set; }
    public DateTime MaxPlayOutDate { get; set; }
    public string? Creator { get; set; }
    public string? UserParticipation { get; set; }
}