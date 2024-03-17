using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers
{
	public interface ITournamentMapper
    {
        Tournament DtoToTournament(TournamentDto dto);
        TournamentDto TournamentToDTO(Tournament tournament);
        TournamentResponse TournamentToTournamentResponse(Tournament tournament);
        Tournament TournamentRequestToTournament(TournamentRequest tournamentRequest);
    }
}
