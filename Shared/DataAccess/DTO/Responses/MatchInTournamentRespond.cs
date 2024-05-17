namespace Shared.DataAccess.DTO.Responses;

public record class MatchInTournamentRespond(
    List<long> BotId,
    string Status,
    long Winner,
    int Position,
    DateTime PlayedOutDate);
    
