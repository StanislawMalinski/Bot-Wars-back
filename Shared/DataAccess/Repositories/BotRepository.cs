using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
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

namespace Shared.DataAccess.Repositories;

public class BotRepository : IBotRepository
{
    private readonly DataContext _dataContext;
    private readonly IBotMapper _botMapper;
    private readonly IFileRepository _fileRepository;

    // move to config
    //private readonly IAuthorizationService _authorizationService;
    //private readonly IUserContextRepository _userContextRepository;

    public BotRepository(DataContext dataContext,
        IBotMapper botMapper,
        IFileRepository fileRepository
        //IAuthorizationService authorizationService,
        //IUserContextRepository userContextRepository
    )
    {
        //_userContextRepository = userContextRepository;
        //_authorizationService = authorizationService;
        _fileRepository = fileRepository;
        _dataContext = dataContext;
        _botMapper = botMapper;
    }

    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> GetBot(long botId)
    {
        var res = await _dataContext
            .Bots
            .FindAsync(botId);

        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Bot>() { Data = res };
    }

    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        var bot = await _dataContext.Bots.FindAsync(botId);
        if (bot == null) return new EntityNotFoundErrorResult();
        return new SuccessData<BotResponse>() { Data = _botMapper.MapBotToResponse(bot) };
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(string? playerName,
        PageParameters pageParameters)
    {
        var player = await _dataContext
            .Players
            .FirstOrDefaultAsync(p => p.Login.Equals(playerName));
        
        
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Message = "EntityNotFound 404",
                Title = "Player with given id does not exist"
            };
        }

        var bots = await _dataContext
            .Bots
            .Where(bot => bot.Player == player)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(bot => _botMapper.MapBotToResponse(bot))
            .ToListAsync();

        return new SuccessData<List<BotResponse>>
        {
            Data = bots
        };
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetBotFileForPlayer(long botId)
    {
        var bot = await _dataContext
            .Bots
            .FirstOrDefaultAsync(bot => bot.Id == botId);

        if (bot == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Message = "EntityNotFound 404",
                Title = "Bot with given id does not exist"
            };
        }

        return await _fileRepository.GetFile(bot.FileId, bot.BotFile);
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForTournament(
        long tournamentId, PageParameters pageParameters)
    {
        var tournament = await _dataContext
            .Tournaments
            .FindAsync(tournamentId);

        if (tournament == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Message = "Tournament could not have been found",
                Title = "EntityNotFoundErrorResult 404"
            };
        }

        var references = await _dataContext.TournamentReferences
            .Where(tr => tr.Tournament == tournament)
            .ToListAsync();

        var responses = new List<BotResponse>();
        foreach (var reference in references)
        {
            var returnedBot = await _dataContext.Bots.FindAsync(reference.botId);
            responses.Add(_botMapper.MapBotToResponse(returnedBot));
        }

        return new SuccessData<List<BotResponse>>
        {
            Data = responses.Skip(pageParameters.PageNumber * pageParameters.PageSize)
                .Take(pageParameters.PageSize)
                .ToList()
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest, long playerId)

    {
        try
        {
            long botFileId;
            var res = await _fileRepository.UploadFile(botRequest.BotFile);
            if (res.IsSuccess)
            {
                botFileId = res.Match(x => x.Data, x => 0);
            }
            else
            {
                return new IncorrectOperation
                {
                    Title = "IncorrectOperation 404",
                    Message = "Faild to upload file to FileGatherer"
                };
            }

            var game = await _dataContext.Games.FindAsync(botRequest.GameId);
            if (game == null)
            {
                return new EntityNotFoundErrorResult
                {
                    Title = "EntityNotFoundErrorResult 404",
                    Message = "No game with given id exists"
                };
            }
            var bot = _botMapper.MapRequestToBot(botRequest);
            bot.FileId = botFileId;
            bot.PlayerId = playerId;
            await _dataContext.AddAsync(bot);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }
        catch (Exception ex)
        {
            return new IncorrectOperation
            {
                Title = "IncorrectOperation 505",
                Message = ex.Message
            };
        }
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        var bot = await _dataContext.Bots.FindAsync(botId);
        /*var authorizationResult = _authorizationService.AuthorizeAsync(_userContextRepository.GetUser(),
            bot,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        if (!authorizationResult.Succeeded)
        {
            return new UnauthorizedError();
        }*/
        if (bot == null) return new EntityNotFoundErrorResult();
        _dataContext.Bots.Remove(bot);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> GetGame(long botId)
    {
        var res = await _dataContext.Bots.FindAsync(botId);
        if (res == null) return new EntityNotFoundErrorResult();
        var resGame = await _dataContext.Games.FindAsync(res.GameId);
        if (resGame == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>() { Data = resGame };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ValidationResult(long taskId, bool result, int memoryUsed,
        int timeUsed)
    {
        var resTask = await _dataContext.Tasks.FindAsync(taskId);
        if (resTask == null) return new EntityNotFoundErrorResult();
        var resBot = await _dataContext.Bots.FindAsync(resTask.OperatingOn);
        if (resBot == null) return new EntityNotFoundErrorResult();
        resTask.Status = TaskStatus.Done;
        resBot.Validation = result ? BotStatus.ValidationSucceed : BotStatus.ValidationFailed;
        resBot.MemoryUsed = memoryUsed;
        resBot.TimeUsed = timeUsed;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}