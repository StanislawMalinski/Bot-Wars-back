namespace Shared.DataAccess.DTO.Requests;

public record MatchFilterRequest(string? GameName,
    //string? Username,
    string? TournamentName,
    DateTime? MinPlayOutDate,
    DateTime? MaxPlayOutDate);