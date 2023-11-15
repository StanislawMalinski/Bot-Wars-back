using AutoMapper;
using BotWars.Models;
using BotWars.Services;
using BotWars.TournamentData;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Repository
{
    public class TournamentRepository // this code was refactored by upgrading to AutoMapper
    {
        private readonly DataContext _dataContext;
        private readonly ITournamentMapper _mapper;
        private readonly IMapper _mapper1;

        public TournamentRepository(DataContext dataContext, ITournamentMapper mapper, IMapper mapper1)
        {
            //_mapper1 = mapper1; //Automapper
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TournamentDTO>> CreateTournamentAsync(TournamentDTO dto)
        {
            try
            {
                Tournament tournament = _mapper.DtoToTournament(dto);
                //_mapper1.Map<Tournament>(dto); //does the same but with 1 external line of code 
                tournament.PostedDate = DateTime.Now;
                await _dataContext.Tournaments.AddAsync(tournament);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<TournamentDTO>() 
                { 
                    Data = _mapper.TournamentToDTO(tournament),
                    //Data = _mapper1.Map<TournamentDTO>(tournament),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDTO>()
                {
                    Data = dto,
                    Success = false,
                    Message = "Cannot create Tournament"
                };
            }
        }

        public async Task<ServiceResponse<TournamentDTO>> DeleteTournamentAsync(long id)
        {
            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<TournamentDTO>() 
                { 
                    Data = null,
                    Success = false,
                    Message = $"Tournament of id {id} dont exits" 
                };
                 _dataContext.Tournaments.Remove(tournament);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<TournamentDTO>()
                {
                    Data = _mapper.TournamentToDTO(tournament),
                    //Data = _mapper1.Map<TournamentDTO>(tournament),
                    Message = "Tournament was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDTO>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<TournamentDTO>> GetTournamentAsync(long id)
        {
            try
            {
                Tournament tournament = _dataContext.Tournaments.Find(id);
                if (tournament == null) return new ServiceResponse<TournamentDTO>() 
                { 
                    Data = null, 
                    Success = false, 
                    Message = $"Tournament of id {id} dont exits" 
                };
                return new ServiceResponse<TournamentDTO>() 
                { 
                    Data = _mapper.TournamentToDTO(tournament),
                    //Data = _mapper1.Map<TournamentDTO>(tournament),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDTO>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }

        public async Task<ServiceResponse<TournamentDTO>> UpdateTournamentAsync(TournamentDTO dto)
        {
            try
            {
                Tournament tournament = _mapper.DtoToTournament(dto);
                //Data = _mapper1.Map<TournamentDTO>(tournament),
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
                return new ServiceResponse<TournamentDTO> 
                { 
                    Data = _mapper.TournamentToDTO(TournamentToEdit),
                    //Data = _mapper1.Map<TournamentDTO>(TournamentToEdit),
                    Success = true 
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<TournamentDTO>
                {
                    Data = dto,
                    Success = false,
                    Message = "An error occured while updating Tournament"
                };
            }
        }


        public async Task<ServiceResponse<List<TournamentDTO>>> GetTournamentsAsync()
        {

            var tournaments = await _dataContext.Tournaments.ToListAsync();
            try
            {
                var dtos = tournaments.Select(x => _mapper.TournamentToDTO(x)).ToList();
                //var dtos = tournaments.Select(x => _mapper1.Map<TournamentDTO>(x)).ToList();
                var response = new ServiceResponse<List<TournamentDTO>>()
                {
                    Data = dtos,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<TournamentDTO>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public Task<ServiceResponse<TournamentDTO>> RegisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TournamentDTO>> UnregisterSelfForTournament(long tournamentId, long playerId)
        {
            throw new NotImplementedException();
        }

    }
}
