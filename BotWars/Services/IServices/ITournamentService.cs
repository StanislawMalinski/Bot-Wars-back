using BotWars.Gry;

namespace BotWars.Services.IServices
{
    public interface ITournamentService
    {
        public Task<ServiceResponse<List<Game>>> GetGamesAsync();

        public Task<ServiceResponse<List<Tournament>>> GetListOfTournaments();

        public Task<ServiceResponse<List<Tournament>>> GetListOfTournamentsFiltered();

        public Task<ServiceResponse<Tournament>> GetTournament(long id);

        public Task<ServiceResponse<Tournament>> UpdateTournament(long id,Tournament tournament);

        public Task<ServiceResponse<Tournament>> DeleteTournament(long id);

        public Task<ServiceResponse<Tournament>> AddTournament(Tournament tournament);

        public Task<ServiceResponse<Tournament>> UnregisterSelfForTournament(long tournamentId,long playerId);

        public Task<ServiceResponse<Tournament>> RegisterSelfForTournament(long tournamentId, long playerId);
    }
}
