using BotWars.TournamentData;

namespace BotWars.Services.IServices
{
    public interface ITournamentService
    {

        public Task<ServiceResponse<List<TournamentDTO>>> GetListOfTournaments();

        public Task<ServiceResponse<List<TournamentDTO>>> GetListOfTournamentsFiltered();

        public Task<ServiceResponse<TournamentDTO>> GetTournament(long id);

        public Task<ServiceResponse<TournamentDTO>> UpdateTournament(long id,TournamentDTO tournament);

        public Task<ServiceResponse<TournamentDTO>> DeleteTournament(long id);

        public Task<ServiceResponse<TournamentDTO>> AddTournament(TournamentDTO tournament);

        public Task<ServiceResponse<TournamentDTO>> UnregisterSelfForTournament(long tournamentId,long playerId);

        public Task<ServiceResponse<TournamentDTO>> RegisterSelfForTournament(long tournamentId, long playerId);
    }
}
