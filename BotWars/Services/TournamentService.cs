using BotWars.Gry;
using BotWars.Models;
using BotWars.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly DataContext _dataContext;
        public TournamentService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<ServiceResponse<Tournament>> AddTournament(Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Tournament>> CreateTournamentAsync(Tournament Tournament)
        {
            try
            {
                await _dataContext.Tournaments.AddAsync(Tournament);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Tournament>() { Data = Tournament, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>()
                {
                    Data = Tournament,
                    Success = false,
                    Message = "Cannot create Tournament"
                };
            }
        }

        public Task<ServiceResponse<Tournament>> DeleteTournament(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Tournament>> DeleteTournamentAsync(long id)
        {


            try
            {
                Tournament book = _dataContext.Tournaments.Find(id);
                if (book == null) return new ServiceResponse<Tournament>() { Data = book, Success = false, Message = $"Tournament of id {id} dont exits" };
                _dataContext.Tournaments.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Tournament>()
                {
                    Data = book,
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

        public Task<ServiceResponse<List<Game>>> GetGamesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Tournament>>> GetListOfTournaments()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<Tournament>>> GetListOfTournamentsFiltered()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Tournament>> GetTournament(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Tournament>> GetTournamentAsync(long id)
        {
            try
            {
                Tournament Tournament = _dataContext.Tournaments.Find(id);
                if (Tournament == null) return new ServiceResponse<Tournament>() { Data = Tournament, Success = false, Message = $"Tournament of id {id} dont exits" };

                return new ServiceResponse<Tournament>() { Data = Tournament, Success = true };
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

        public Task<ServiceResponse<Tournament>> UpdateTournament(long id, Tournament tournament)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Tournament>> UpdateTournamentAsync(Tournament Tournament)
        {
            try
            {
                var productToEdit = new Tournament() { Id = Tournament.Id };
                _dataContext.Tournaments.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Tournament> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Tournament>
                {
                    Data = Tournament,
                    Success = false,
                    Message = "An error occured while updating Tournament"
                };
            }
        }
    }
}
