using Communication.APIs.DTOs;

namespace Communication.APIs.Mappers
{
    public interface ITournamentMapper
    {
        public Tournament DtoToTournament(TournamentDto dto);
        public TournamentDto TournamentToDTO(Tournament tournament);
    }
}
