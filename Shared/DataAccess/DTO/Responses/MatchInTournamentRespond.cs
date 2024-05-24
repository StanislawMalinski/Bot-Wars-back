using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DTO.Responses;

public record class MatchInTournamentRespond(
    long matchId,
    List<BotPlayer> PlayersBots,
    string Status,
    long Winner,
    int Position,
    DateTime PlayedOutDate,string MatchResult
    );
    
