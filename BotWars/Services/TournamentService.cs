using BotWars.Repository;
using BotWars.Services.IServices;
using BotWars.TournamentData;

namespace BotWars.Services
{
    public class TournamentService : ITournamentService
    {
        
        private readonly TournamentRepository _tournamentRepository;

        public TournamentService(TournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ServiceResponse<TournamentDTO>> AddTournament(TournamentDTO tournament)
        {
            return await _tournamentRepository.CreateTournamentAsync(tournament);
        }

        public async Task<ServiceResponse<TournamentDTO>> DeleteTournament(long id)
        {
            return await _tournamentRepository.DeleteTournamentAsync(id);
        }

        public async Task<ServiceResponse<List<TournamentDTO>>> GetListOfTournaments()
        {
            return await _tournamentRepository.GetTournamentsAsync();
        }

        public async Task<ServiceResponse<List<TournamentDTO>>> GetListOfTournamentsFiltered()
        {
            var tourlist = await _tournamentRepository.GetTournamentsAsync();
            if (tourlist.Success)
            {
                //filter;
            }

            return tourlist;
        }

        public async Task<ServiceResponse<TournamentDTO>> GetTournament(long id)
        {
            return await _tournamentRepository.GetTournamentAsync(id);
        }

        public async Task<ServiceResponse<TournamentDTO>> RegisterSelfForTournament(long tournamentId, long playerId)
        {
            return await _tournamentRepository.RegisterSelfForTournament(tournamentId, playerId);
        }

        public async Task<ServiceResponse<TournamentDTO>> UnregisterSelfForTournament(long tournamentId, long playerId)
        {
            return await _tournamentRepository.UnregisterSelfForTournament(tournamentId, playerId);
        }

        public async Task<ServiceResponse<TournamentDTO>> UpdateTournament(long id, TournamentDTO tournament)
        {
            tournament.Id = id;
            return await _tournamentRepository.UpdateTournamentAsync(tournament);
        }
    }
}