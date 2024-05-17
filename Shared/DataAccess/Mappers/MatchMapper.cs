using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public class MatchMapper
{
    public MatchResponse MapEntityToResponse(Matches match)
    {
        MatchResponse matchResponse = new(match.Id, match.GameId, match.Game?
                .GameFile , match.TournamentsId, match.Tournament?.TournamentTitle
            ,match.MatchPlayers.Select(x=>x.BotId).ToList(),match.Status.ToString(),match.Winner
        , match.Played);

        return matchResponse;
    }
    
    public MatchInTournamentRespond MapEntityToMatcInTournamentResponse(Matches match)
    {
        MatchInTournamentRespond matchResponse = new(match.MatchPlayers.Select(x=>x.BotId).ToList()
            ,match.Status.ToString(),match.Winner, 
             Int32.Parse( match.Data ) , match.Played);

        return matchResponse;
    }
}