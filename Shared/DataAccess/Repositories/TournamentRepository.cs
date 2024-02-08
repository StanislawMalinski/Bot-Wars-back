using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

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
        
        public async Task<HandlerResult<Success, IErrorResult>> CreateTournamentAsync(TournamentDto dto)
        {
            
            Tournament tournament = _mapper.DtoToTournament(dto);
            tournament.PostedDate = DateTime.Now;
            await _dataContext.Tournaments.AddAsync(tournament);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> DeleteTournamentAsync(long id)
        {
            
            Tournament tournament = await _dataContext.Tournaments.FindAsync(id);
            if (tournament == null) return new EntityNotFoundErrorResult()
            { 
                Title = "result null",
                Message = $"Tournament of id {id} dont exits" 
            };
             _dataContext.Tournaments.Remove(tournament);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<TournamentDto>, IErrorResult>> GetTournamentAsync(long id)
        {
            
            Tournament tournament = await _dataContext.Tournaments.FindAsync(id);
            if (tournament == null) return new EntityNotFoundErrorResult() 
            { 
                Title = "return null",
                Message = $"Tournament of id {id} dont exits" 
            };
            return new SuccessData<TournamentDto>()
            { 
                Data = _mapper.TournamentToDTO(tournament)
            };
            
        }

        public async Task<HandlerResult<Success, IErrorResult>> UpdateTournamentAsync(TournamentDto dto)
        {
            Tournament TournamentToEdit = await _dataContext.Tournaments.FindAsync(dto.Id);
            if (TournamentToEdit == null) return new EntityNotFoundErrorResult() 
            { 
                Title = "return null",
                Message = $"Tournament of id {dto.Id} dont exits" 
            };
            Tournament tournament = _mapper.DtoToTournament(dto);

            TournamentToEdit.TournamentTitle = tournament.TournamentTitle;
            TournamentToEdit.Description = tournament.Description;
            TournamentToEdit.GameId = tournament.GameId;
            TournamentToEdit.PlayersLimit = tournament.PlayersLimit;
            TournamentToEdit.TournamentsDate = tournament.TournamentsDate;
            TournamentToEdit.WasPlayedOut = tournament.WasPlayedOut;
            TournamentToEdit.Constraints = tournament.Constraints;
            TournamentToEdit.Image = tournament.Image;

            await _dataContext.SaveChangesAsync();
            return new Success();

        }


        public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetTournamentsAsync()
        {
            
                var dtos = _dataContext.Tournaments.Select(x => _mapper.TournamentToDTO(x)).ToList();
                return new SuccessData<List<TournamentDto>>()
                {
                    Data = dtos
                };

        }

        public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
        {
            var tournament = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No tournament with such id has been found"
                };
            }

            var bot = await _dataContext.Bots.FindAsync(botId);
            if (bot == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No bot with such id has been found"
                };
            }
            var result =await _dataContext
                .TournamentReferences
                .FirstOrDefaultAsync(x => x.tournamentId == tournamentId && x.botId == botId);
            if (result != null)
            {
                return new AlreadyRegisterForTournamentError()
                {
                    Title = "Tournament Registration",
                    Message = "You are already registered for tournament"
                };
            }
            var tournamentReference = new TournamentReference
            {
                tournamentId = tournamentId,
                botId = botId,
                LastModification = new DateTime()
            };
            await _dataContext.TournamentReferences.AddAsync(tournamentReference);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
        {
            var tournament = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No tournament with such id has been found"
                };
            }

            var bot = await _dataContext.Bots.FindAsync(botId);
            if (bot == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No bot with such id has been found"
                };
            }
            
            var result =await _dataContext
                .TournamentReferences
                .FirstOrDefaultAsync(x => x.tournamentId == tournamentId && x.botId == botId);
            if (result == null)
            {
                return new EntityNotFoundErrorResult()
                {
                    Title = "Tournament Registration",
                    Message = "You were not registered for tournament"
                };
            }

            _dataContext.TournamentReferences.Remove(result);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<List<Bot>>, IErrorResult>> TournamentBotsToPlay(long tournamentId)
        {
            var result =  await _dataContext.TournamentReferences.Where(x => x.tournamentId == tournamentId).Include(x=>x.Bot).Select(x=>x.Bot).ToListAsync();
            return new SuccessData<List<Bot>>()
            {
                Data = result
            };
        }
        public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> TournamentGame(long tournamentId)
        {
            var result = await _dataContext.Tournaments.FindAsync(tournamentId);
            var gameResult = await _dataContext.Games.FindAsync(result.GameId);
            return new SuccessData<Game>()
            {
                Data = gameResult
            };
        }

    }
    
    
}
