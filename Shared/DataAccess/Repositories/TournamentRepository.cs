using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Mappers;
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

        public async Task<HandlerResult<Success, IErrorResult>> CreateTournamentAsync(long userId,
            TournamentRequest tournamentRequest)
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
            var tournament = _mapper
                .TournamentRequestToTournament(tournamentRequest);
            tournament.CreatorId = userId;
            await _dataContext
                .Tournaments
                .AddAsync(tournament);
            await _dataContext
                .SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> DeleteTournamentAsync(long id)
        {
            var tournament = await _dataContext.Tournaments.FindAsync(id);
            if (tournament == null)
                return new EntityNotFoundErrorResult()
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = $"Tournament of id {id} does not exist"
                };
            _dataContext
                .Tournaments
                .Remove(tournament);
            await _dataContext
                .SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournamentAsync(long id)
        {
            var tournament = await _dataContext.Tournaments
                .Include(tournament => tournament.Matches)
                .Include(tournament => tournament.TournamentReference)
                .SingleOrDefaultAsync(tournament => tournament.Id == id);

            if (tournament == null)
                return new EntityNotFoundErrorResult()
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = $"No tournament with given id could have been found"
                };
            return new SuccessData<TournamentResponse>()
            {
                Data = _mapper.TournamentToTournamentResponse(tournament)
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> UpdateTournamentAsync(long id,
            TournamentRequest tournamentRequest)
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
                .FindAsync(id);

            if (tournamentToEdit == null)
                return new EntityNotFoundErrorResult()
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = $"Tournament of id {id} does not exist"
                };
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

        public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetTournamentsAsync()
        {
            var tournamentResponses = await _dataContext
                .Tournaments
                .Include(tournament => tournament.Matches)
                .Include(tournament => tournament.TournamentReference)
                .Select(tournament => _mapper.TournamentToTournamentResponse(tournament))
                .ToListAsync();

            return new SuccessData<List<TournamentResponse>>()
            {
                Data = tournamentResponses
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
        {
            var tournament = await _dataContext
                .Tournaments
                .FindAsync(tournamentId);
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
                .FindAsync(botId);
            if (bot == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No bot with such id has been found"
                };
            }

            var result = await _dataContext
                .TournamentReferences
                .FirstOrDefaultAsync(x => x.tournamentId == tournamentId && x.botId == botId);
            if (result != null)
            {
                return new AlreadyRegisterForTournamentError()
                {
                    Title = "AlreadyRegisterForTournamentError 400",
                    Message = "You are already registered for tournament"
                };
            }

            var tournamentReference = new TournamentReference
            {
                tournamentId = tournamentId,
                botId = botId,
                LastModification = new DateTime()
            };

            if (tournament.Status is not (TournamentStatus.SCHEDULED
                or TournamentStatus.NOTSCHEDULED))
            {
                return new TournamentIsBeingPlayedError()
                {
                    Title = "TournamentIsBeingPlayedError 400",
                    Message = "You cannot register a bot while the tournament is being played or has been finished"
                };
            }

            await _dataContext
                .TournamentReferences
                .AddAsync(tournamentReference);
            await _dataContext
                .SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId,
            long botId)
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

        public async Task<HandlerResult<SuccessData<List<Bot>>, IErrorResult>> TournamentBotsToPlay(long tournamentId)
        {
            var result = await _dataContext.TournamentReferences.Where(x => x.tournamentId == tournamentId)
                .Include(x => x.Bot).Select(x => x.Bot).ToListAsync();
            return new SuccessData<List<Bot>>()
            {
                Data = result!
            };
        }

        public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> TournamentGame(long tournamentId)
        {
            var result = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (result == null) return new EntityNotFoundErrorResult();
            var gameResult = await _dataContext.Games.FindAsync(result.GameId);
            return new SuccessData<Game>()
            {
                Data = gameResult
            };
        }

        public async Task<HandlerResult<Success, IErrorResult>> TournamentEnded(long tournamentId, long winner,
            long taskId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            var taskRes = await _dataContext.Tasks.FindAsync(taskId);
            if (res == null || taskRes == null) return new EntityNotFoundErrorResult();
            res.Status = TournamentStatus.PLAYED;
            taskRes.Status = TaskStatus.Done;
            await _achievementsRepository.UpDateProgressNoSave(AchievementsTypes.TournamentsWon, winner);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> TournamentEnded(long tournamentId, long taskId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            var taskRes = await _dataContext.Tasks.FindAsync(taskId);
            if (res == null || taskRes == null) return new EntityNotFoundErrorResult();
            res.Status = TournamentStatus.PLAYED;
            taskRes.Status = TaskStatus.Done;
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> TournamentPlaying(long tournamentId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (res == null) return new EntityNotFoundErrorResult();
            res.Status = TournamentStatus.INPLAY;
            await _dataContext.SaveChangesAsync();
            return new Success();
        }

        public async Task<HandlerResult<Success, IErrorResult>> ScheduleTournament(long tournamentId)
        {
            var res = await _dataContext.Tournaments.FindAsync(tournamentId);
            if (res == null) return new EntityNotFoundErrorResult();
            if (res.Status != TournamentStatus.NOTSCHEDULED) return new IncorrectOperation();
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
            return new Success();
        }

        public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>>
            GetFilteredTournamentsAsync(TournamentFilterRequest tournamentFilterRequest)
        {
            var unfilteredTournaments = _dataContext
                .Tournaments.Include(tournament => tournament.Creator)
                .Where(tournament => true);

            if (tournamentFilterRequest.MaxPlayOutDate != null)
            {
                unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                    tournament.TournamentsDate <= tournamentFilterRequest.MaxPlayOutDate);
            }

            if (tournamentFilterRequest.MinPlayOutDate != null)
            {
                unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                    tournament.TournamentsDate >= tournamentFilterRequest.MinPlayOutDate);
            }

            if (tournamentFilterRequest.Creator != null)
            {
                unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                    tournamentFilterRequest.Creator == null
                    || tournamentFilterRequest.Creator.Equals(tournament.Creator.Login));
            }

            if (tournamentFilterRequest.TournamentTitle != null)
            {
                unfilteredTournaments = unfilteredTournaments.Where(tournament =>
                    tournament.TournamentTitle.Contains(tournamentFilterRequest.TournamentTitle));
            }

            var filteredTournaments = await unfilteredTournaments
                .Select(tournament => _mapper.TournamentToTournamentResponse(tournament))
                .ToListAsync();

            if (tournamentFilterRequest.UserParticipation == null)
            {
                return new SuccessData<List<TournamentResponse>>()
                {
                    Data = filteredTournaments
                };
            }

            var filteredTournamentList = new List<TournamentResponse>();
            foreach (var tournamentResponse in filteredTournaments)
            {
                if ((await PlayerParticipate(tournamentResponse.Id, tournamentFilterRequest.UserParticipation))
                    .IsSuccess) filteredTournamentList.Add(tournamentResponse);
            }

            return new SuccessData<List<TournamentResponse>>()
            {
                Data = filteredTournamentList
            };
        }

        private async Task<HandlerResult<Success, IErrorResult>> PlayerParticipate(long tourId, string? playerUsername)
        {
            if (playerUsername == null) return new Success();
            var res = await _dataContext.TournamentReferences
                .Where(reference => reference.tournamentId == tourId)
                .Include(tournamentReference => tournamentReference.Bot)
                .ThenInclude(bot => bot.Player)
                .Select(x => x.Bot.Player.Login)
                .ToListAsync();
            foreach (var p in res)
            {
                if (playerUsername.Equals(p)) return new Success();
            }

            return new EntityNotFoundErrorResult();
        }
    }
}