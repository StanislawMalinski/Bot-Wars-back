using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public class MatchMapper
{
    public MatchResponse MapEntityToResponse(Matches match)
    {
        MatchResponse matchResponse = new(match.Id, match.GameId, match.Game?
                .GameFile , match.TournamentsId, match.Tournament?.TournamentTitle
        , match.Played);

        return matchResponse;
    }
}