using BotWars.Gry;
using BotWars.Models;
using BotWars.Services;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Repository
{
    public class TournamentRepository
    {
        private readonly DataContext _dataContext;
        public TournamentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Tournament>> CreateTournamentAsync(Tournament tournament)
        {
            try
            {
                tournament.PostedDate = DateTime.Now;
                await _dataContext.Tournaments.AddAsync(tournament);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Tournament>() { Data = tournament, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>()
                {
                    Data = tournament,
                    Success = false,
                    Message = "Cannot create Tournament"
                };
            }
        }


        public async Task<ServiceResponse<Tournament>> DeleteTournamentAsync(long id)
        {


            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<Tournament>() { Data = tournament, Success = false, Message = $"Tournament of id {id} dont exits" };
                 _dataContext.Tournaments.Remove(tournament);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Tournament>()
                {
                    Data = tournament,
                    Message = "Tournament was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }


        public async Task<ServiceResponse<Tournament>> GetTournamentAsync(long id)
        {
            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<Tournament>() { Data = tournament, Success = false, Message = $"Tournament of id {id} dont exits" };

                return new ServiceResponse<Tournament>() { Data = tournament, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }

        public async Task<ServiceResponse<Tournament>> UpdateTournamentAsync(Tournament tournament)
        {
            try
            {
                var TournamentToEdit = new Tournament() { Id = tournament.Id };
                _dataContext.Tournaments.Attach(TournamentToEdit);

                TournamentToEdit.TournamentTitles = tournament.TournamentTitles;
                TournamentToEdit.Description = tournament.Description;
                TournamentToEdit.GameId = tournament.GameId;
                TournamentToEdit.PlayersLimit = tournament.PlayersLimit;
                TournamentToEdit.TournamentsDate = tournament.TournamentsDate;
                TournamentToEdit.WasPlayedOut = tournament.WasPlayedOut;
                TournamentToEdit.Contrains = tournament.Contrains;
                TournamentToEdit.Image = tournament.Image;



                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Tournament> { Data = TournamentToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>
                {
                    Data = tournament,
                    Success = false,
                    Message = "An error occured while updating Tournament"
                };
            }
        }


        public async Task<ServiceResponse<List<Tournament>>> GetTournamentsAsync()
        {

            var Tournaments = await _dataContext.Tournaments.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Tournament>>()
                {
                    Data = Tournaments,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Tournament>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }

        }


        public Task<ServiceResponse<Tournament>> RegisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Tournament>> UnregisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

    }
}
