using System.Diagnostics;
using Microsoft.AspNetCore.Http;
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
    private readonly HttpClient _httpClient;
    // move to config
    private readonly string _gathererEndpoint = "http://host.docker.internal:7002/api/Gatherer";

    public BotRepository(DataContext dataContext, IBotMapper botMapper, HttpClient httpClient)
    {
        _dataContext = dataContext;
        _botMapper = botMapper;
        _httpClient = httpClient;
    }

    //public async Task<HandlerResult<SuccessData<long>, IErrorResult>> AddBot(BotFileDto bot)
    //{
    //    Bot newbot = new Bot()
    //    {
    //        PlayerId = bot.PlayerId,
    //        GameId = bot.GameId,
    //        BotFile = bot.BotName
    //    };
        
    //    var res =  await _dataContext.Bots.AddAsync(newbot);
    //    await _dataContext.SaveChangesAsync();
        
    //    return new SuccessData<long>()
    //    {
    //        Data = res.Entity.Id
    //    };
    //}

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

    public async Task<HandlerResult<SuccessData<List<BotResponse>>, IErrorResult>> GetBotsForPlayer(long playerId)
    {
        var player = await _dataContext
            .Players
            .FindAsync(playerId);

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
            .Where(bot => bot.PlayerId == playerId)
            .Select(bot => _botMapper.MapBotToResponse(bot))
            .ToListAsync();

        return new SuccessData<List<BotResponse>>
        {
            Data = bots
        };
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> AddBot(BotRequest botRequest)
    {
        try
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(botRequest.BotFile.OpenReadStream());
            content.Add(streamContent, "file", botRequest.BotFile.FileName);
            var res = await _httpClient.PutAsync(_gathererEndpoint, content);
            long botFileId;
            if (res.IsSuccessStatusCode)
            {
                string cont = await res.Content.ReadAsStringAsync();
                botFileId = Convert.ToInt32(cont);
            }
            else
            {
                return new IncorrectOperation
                {
                    Title = "IncorrectOperation 404",
                    Message = "Faild to upload file to FileGatherer"
                };
            }
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
            bot.FileId = botFileId;
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