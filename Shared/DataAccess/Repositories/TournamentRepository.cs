using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Pagination;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories
{
    public class TournamentRepository
    {
        private readonly DataContext _dataContext;
        private readonly ITournamentMapper _mapper;
        private readonly IAchievementsRepository _achievementsRepository;

        public TournamentRepository(DataContext dataContext, ITournamentMapper mapper,
            IAchievementsRepository achievementsRepository)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _achievementsRepository = achievementsRepository;
        }

        public async Task<bool> DeleteTournamentAsync(long id)
        {
            var res = await _dataContext.Tournaments.FindAsync(id);
            if (res == null) return false;
            _dataContext.Tournaments.Remove(res);
            return true;
        }


        public async Task<Tournament?> GetTournamentExtended(long tournamentId)
        {
            return await _dataContext.Tournaments.Include(tournament => tournament.Matches)
                .Include(tournament => tournament.TournamentReference)
                .SingleOrDefaultAsync(tournament => tournament.Id == tournamentId);;
        }
        public async Task<Tournament?> GetTournament(long tournamentId)
        {
            return await _dataContext.Tournaments.FindAsync(tournamentId);
        }
        public async Task<EntityEntry<TournamentReference>> AddTournamentReference(TournamentReference tournamentReference)
        {
            return await _dataContext.TournamentReferences.AddAsync(tournamentReference);
        }

        public async Task<bool> TournamentStarted(long tourId)
        {
            return ((await _dataContext.Tournaments.FindAsync(tourId))!).Status is (TournamentStatus.SCHEDULED
                or TournamentStatus.NOTSCHEDULED);
        }

        public async Task<bool> IsRegisteredForTournament(long botId,long tournamentId)
        {
            return  await _dataContext
                .TournamentReferences
                .FirstOrDefaultAsync(x => x.tournamentId == tournamentId && x.botId == botId) != null;
        }
        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
        

        public async Task<EntityEntry<Tournament>> AddTournament(Tournament tournament)
        {
            return await _dataContext.Tournaments.AddAsync(tournament);
        }

        public async Task<bool> TournamentCreator(long tourId, long creatorId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tourId);
            if (res == null) return false;
            return res.CreatorId == creatorId;
        }

        public async Task<List<Tournament>> GetPlayerScheduledTournaments(long playerId)
        {
            return await _dataContext.Tournaments.Where(x => x.CreatorId == playerId && x.Status != TournamentStatus.DONE &&
                                                             x.Status != TournamentStatus.INPLAY)
                .ToListAsync();
        }

        public async Task<List< _Task>> GetScheduledTournamentTask(long tourId)
        {
            return await _dataContext
                .Tasks.Where(x => x.Status == TaskStatus.ToDo && x.Type == TaskTypes.PlayTournament &&
                                  x.OperatingOn == tourId)
                .ToListAsync();
        }

        public async Task<bool> DeleteTask(_Task task)
        {
            _dataContext.Tasks.Remove(task);
            return true;
        }

       

        public async Task<HandlerResult<Success, IErrorResult>> UpdateTournamentAsync(long id,
            TournamentRequest tournamentRequest, long playerId)
        {
            var game = await _dataContext
                .Games
                .FindAsync(tournamentRequest.GameId);

            if (game is null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "Game with given id could not have been found"
                };
            }

            var tournamentToEdit = await _dataContext
                .Tournaments
                .Include(t => t.Creator)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (tournamentToEdit == null)
                return new EntityNotFoundErrorResult()
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = $"Tournament of id {id} does not exist"
                };

            var authorizationResult = await _dataContext.Players.FindAsync(playerId);

            if (tournamentToEdit.Creator.Id != playerId && authorizationResult.RoleId != 2)
            {
                return new NotTournamentCreatorError()
                {
                    Title = "NotTournamentCreatorError 400",
                    Message = $"You cannot update tournament, that you did not create and while you are not an admin"
                };
            }

            var tournament = _mapper
                .TournamentRequestToTournament(tournamentRequest);

            tournamentToEdit.TournamentTitle = tournament.TournamentTitle;
            tournamentToEdit.Description = tournament.Description;
            tournamentToEdit.GameId = tournament.GameId;
            tournamentToEdit.PlayersLimit = tournament.PlayersLimit;
            tournamentToEdit.TournamentsDate = tournament.TournamentsDate;
            tournamentToEdit.Constraints = tournament.Constraints;
            tournamentToEdit.Image = tournament.Image;

            await _dataContext
                .SaveChangesAsync();
            return new Success();
        }

       

        public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId,
            long botId, long playerId)
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

            var bot = await _dataContext
                .Bots
                .Include(b => b.Player)
                .Where(b => b.Id == botId)
                .FirstOrDefaultAsync();

            if (bot == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No bot with such id has been found"
                };
            }

            if (bot.Player.Id != playerId)
            {
                return new NotBotCreatorError()
                {
                    Title = "NotBotCreatorError 400",
                    Message = $"You cannot register a bot, that you did not create"
                };
            }

            var result = await _dataContext
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

            if (tournament.Status is not (TournamentStatus.SCHEDULED or TournamentStatus.NOTSCHEDULED))
            {
                return new TournamentIsBeingPlayedError()
                {
                    Title = "TournamentIsBeingPlayedError 400",
                    Message = "You cannot unregister a bot while the tournament is being played or has been finished"
                };
            }

            _dataContext
                .TournamentReferences
                .Remove(result);
            await _dataContext
                .SaveChangesAsync();
            return new Success();
        }

        public async Task<List<Bot>> TournamentBotsToPlay(long tournamentId)
        {
            return (await _dataContext.TournamentReferences.Where(x => x.tournamentId == tournamentId)
                .Include(x => x.Bot).Select(x => x.Bot).ToListAsync())!;
            
        }

        public async Task<Game?> TournamentGame(long tournamentId)
        {
            var result = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (result == null) return null;
            return (await _dataContext.Games.FindAsync(result.GameId))!;
            
        }

        
        public async Task<bool> TournamentPlaying(long tournamentId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (res == null) return false;
            res.Status = TournamentStatus.INPLAY;
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ScheduleTournament(long tournamentId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (res == null) return false;
            if (res.Status != TournamentStatus.NOTSCHEDULED) return false;
            res.Status = TournamentStatus.SCHEDULED;
            _Task task = new _Task
            {
                OperatingOn = tournamentId,
                ScheduledOn = res.TournamentsDate,
                Status = TaskStatus.ToDo,
                Type = TaskTypes.PlayTournament
            };
            await _dataContext.Tasks.AddAsync(task);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public  IQueryable<Tournament> GetTournamentsQuery()
        {
            return _dataContext
                .Tournaments.Include(tournament => tournament.Creator)
                .Where(tournament => true);    
        }

        public async Task<List<string>> GetParticipantName(long tourId)
        {
            return await _dataContext.TournamentReferences
                .Where(reference => reference.tournamentId == tourId)
                .Include(tournamentReference => tournamentReference.Bot)
                .ThenInclude(bot => bot.Player)
                .Select(x => x.Bot.Player.Login)
                .ToListAsync();
        }

        
    }
}