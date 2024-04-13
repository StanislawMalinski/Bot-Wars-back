using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DTO.Responses;

public class TournamentResponse
{
    public long Id { get; set; }
    public string TournamentTitle { get; set; }
    public string Description { get; set; }
    public int PlayersLimit { get; set; }
    public DateTime TournamentsDate { get; set; }
    public DateTime PostedDate { get; set; }
    public RankingTypes RankingType { get; set; }
    public string? Constraints { get; set; }
    public string Image { get; set; }
    public bool WasPlayedOut { get; set; }
    public int MemoryLimit { get; set; }
    public int TimeLimit { get; set; }
    
    public List<long>? MatchIds { get; set; }
    public List<long>? BotIds { get; set; }
}