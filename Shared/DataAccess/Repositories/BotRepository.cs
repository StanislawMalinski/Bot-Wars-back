using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
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

namespace Shared.DataAccess.Repositories;

public class BotRepository : IBotRepository
{
    private readonly DataContext _dataContext;
    private readonly IBotMapper _botMapper;
    private string BotFilePath = "FileSystem/Bots";

    public BotRepository(DataContext dataContext, IBotMapper botMapper)
    {
        _dataContext = dataContext;
        _botMapper = botMapper;
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> AddBot(BotFileDto bot)
    {
        Bot newbot = new Bot()
        {
            PlayerId = bot.PlayerId,
            GameId = bot.GameId,
            BotFile = bot.BotName
        };
        
        var res =  await _dataContext.Bots.AddAsync(newbot);
        await _dataContext.SaveChangesAsync();
        
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetAllBots()
    {
        var bots = await _dataContext
            .Bots
            .Select(x => _botMapper.MapBotToResponse(x))
            .ToListAsync();
        return new SuccessData<List<BotResponse>>
        {
            Data = bots
        };
    }

    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> GetBot(long botId)
    {
        var res = await _dataContext.Bots.FindAsync(botId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Bot>() { Data = res };
    }
    
    public async Task<HandlerResult<SuccessData<BotResponse>, IErrorResult>> GetBotResponse(long botId)
    {
        var bot = await _dataContext.Bots.FindAsync(botId);
        if (bot == null) return new EntityNotFoundErrorResult();
        return new SuccessData<BotResponse>() { Data = _botMapper.MapBotToResponse(bot) };
    }

    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        //TODO obsługa plików, jak będzie system plików
        var player = await _dataContext.Players.FindAsync(botRequest.PlayerId);
        if (player == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No user with given id exists"
            };

        var game = await _dataContext.Games.FindAsync(botRequest.GameId);
        if (game == null)
            return new EntityNotFoundErrorResult
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No game with given id exists"
            };
        
        var bot = _botMapper.MapRequestToBot(botRequest);
        await _dataContext.AddAsync(bot);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteBot(long botId)
    {
        var bot = await _dataContext.Bots.FindAsync(botId);
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

    public async Task<HandlerResult<Success, IErrorResult>> ValidationResult(long taskId, bool result)
    {
        var resTask = await _dataContext.Tasks.FindAsync(taskId);
        if (resTask == null) return new EntityNotFoundErrorResult();
        var resBot = await _dataContext.Bots.FindAsync(resTask.OperatingOn);
        if (resBot == null) return new EntityNotFoundErrorResult();
        resTask.Status = TaskStatus.Done;
        resBot.Validation = result ? BotStatus.ValidationSucceed : BotStatus.ValidationFailed;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    
}