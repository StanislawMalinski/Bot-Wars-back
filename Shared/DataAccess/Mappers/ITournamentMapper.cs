using Communication.APIs.DTOs;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers
{
    public interface ITournamentMapper
    {
        public Tournament DtoToTournament(TournamentDto dto);
        public TournamentDto TournamentToDTO(Tournament tournament);
    }
}
