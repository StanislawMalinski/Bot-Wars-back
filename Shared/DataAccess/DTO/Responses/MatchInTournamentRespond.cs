using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DTO.Responses;

public record class MatchInTournamentRespond(
    long matchId,
    List<long> BotId,
    string Status,
    long Winner,
    int Position,
    DateTime PlayedOutDate,MatchResult MatchResult
    );
    
