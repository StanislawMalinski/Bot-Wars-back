using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Repositories
{
	public class TournamentRepository
    {
        private readonly DataContext _dataContext;
        private readonly ITournamentMapper _mapper;
        public TournamentRepository(DataContext dataContext, ITournamentMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TournamentDto>> CreateTournamentAsync(TournamentDto dto)
        {
            try
            {
                Tournament tournament = _mapper.DtoToTournament(dto);
                tournament.PostedDate = DateTime.Now;
                await _dataContext.Tournaments.AddAsync(tournament);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<TournamentDto>() 
                { 
                    Data = _mapper.TournamentToDTO(tournament),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDto>()
                {
                    Data = dto,
                    Success = false,
                    Message = "Cannot create Tournament"
                };
            }
        }

        public async Task<ServiceResponse<TournamentDto>> DeleteTournamentAsync(long id)
        {
            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<TournamentDto>() 
                { 
                    Data = null,
                    Success = false,
                    Message = $"Tournament of id {id} dont exits" 
                };
                 _dataContext.Tournaments.Remove(tournament);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<TournamentDto>()
                {
                    Data = _mapper.TournamentToDTO(tournament),
                    Message = "Tournament was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDto>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<TournamentDto>> GetTournamentAsync(long id)
        {
            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<TournamentDto>() 
                { 
                    Data = null, 
                    Success = false, 
                    Message = $"Tournament of id {id} dont exits" 
                };
                return new ServiceResponse<TournamentDto>() 
                { 
                    Data = _mapper.TournamentToDTO(tournament),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDto>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }

        public async Task<ServiceResponse<TournamentDto>> UpdateTournamentAsync(TournamentDto dto)
        {
            try
            {
                Tournament tournament = _mapper.DtoToTournament(dto);
                var TournamentToEdit = new Tournament() { Id = tournament.Id };
                _dataContext.Tournaments.Attach(TournamentToEdit);

                TournamentToEdit.TournamentTitle = tournament.TournamentTitle;
                TournamentToEdit.Description = tournament.Description;
                TournamentToEdit.GameId = tournament.GameId;
                TournamentToEdit.PlayersLimit = tournament.PlayersLimit;
                TournamentToEdit.TournamentsDate = tournament.TournamentsDate;
                TournamentToEdit.WasPlayedOut = tournament.WasPlayedOut;
                TournamentToEdit.Constrains = tournament.Constrains;
                TournamentToEdit.Image = tournament.Image;

                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<TournamentDto> 
                { 
                    Data = _mapper.TournamentToDTO(TournamentToEdit),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDto>
                {
                    Data = dto,
                    Success = false,
                    Message = "An error occured while updating Tournament"
                };
            }
        }


        public async Task<ServiceResponse<List<TournamentDto>>> GetTournamentsAsync()
        {

            var tournaments = await _dataContext.Tournaments.ToListAsync();
            try
            {
                var dtos = tournaments.Select(x => _mapper.TournamentToDTO(x)).ToList();
                var response = new ServiceResponse<List<TournamentDto>>()
                {
                    Data = dtos,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<TournamentDto>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

    }

  
}
