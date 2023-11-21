using BotWars.Services;
using Communication.APIs.DTOs;

namespace Shared.DataAccess.RepositoryInterfaces
{
    public interface ITournamentService
    {

        public Task<ServiceResponse<List<TournamentDto>>> GetListOfTournaments();

        public Task<ServiceResponse<List<TournamentDto>>> GetListOfTournamentsFiltered();

        public Task<ServiceResponse<TournamentDto>> GetTournament(long id);

        public Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament);

        public Task<ServiceResponse<TournamentDto>> DeleteTournament(long id);

        public Task<ServiceResponse<TournamentDto>> AddTournament(TournamentDto tournament);

        public Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId);

        public Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId);
    }
}
