using BotWars.Gry;
using BotWars.Models;
using BotWars.Repository;
using BotWars.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly TournamentRepository _tournamentRepository;
        public TournamentService(TournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ServiceResponse<Tournament>> AddTournament(Tournament tournament)
        {
            return await _tournamentRepository.CreateTournamentAsync(tournament);
        }

        

        public async Task<ServiceResponse<Tournament>> DeleteTournament(long id)
        {
            return await _tournamentRepository.DeleteTournamentAsync(id);
        }


        public async Task<ServiceResponse<List<Tournament>>> GetListOfTournaments()
        {
            return await _tournamentRepository.GetTournamentsAsync();
        }

        public async Task<ServiceResponse<List<Tournament>>> GetListOfTournamentsFiltered()
        {
            var tourlist = await _tournamentRepository.GetTournamentsAsync();
            if(tourlist.Success)
            {
                //filter;

            }

            return tourlist;
        }

        public async Task<ServiceResponse<Tournament>> GetTournament(long id)
        {
            return await _tournamentRepository.GetTournamentAsync(id);
        }


        public async Task<ServiceResponse<Tournament>> RegisterSelfForTournament(long tournamentId, long playerId)
        {
            return await _tournamentRepository.RegisterSelfForTournament(tournamentId, playerId);  
        }

        public async Task<ServiceResponse<Tournament>> UnregisterSelfForTournament(long tournamentId, long playerId)
        {
            return await _tournamentRepository.UnregisterSelfForTournament(tournamentId,playerId);
        }

        public async Task<ServiceResponse<Tournament>> UpdateTournament(long id, Tournament tournament)
        {
            tournament.Id = id;
            return await _tournamentRepository.UpdateTournamentAsync(tournament);
        }

        
    }
}
