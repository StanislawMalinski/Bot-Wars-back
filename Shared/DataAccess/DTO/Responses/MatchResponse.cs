namespace Shared.DataAccess.DTO.Responses;

public record MatchResponse(long Id,
    long GameId,
    string GameName,
    long TournamentId,
    string TournamentName,
    List<long> BotId,
    string Status,
    long Winner,
    DateTime PlayedOutDate);