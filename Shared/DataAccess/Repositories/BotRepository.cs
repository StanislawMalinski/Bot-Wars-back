using System.Diagnostics;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories;

public class BotRepository
{
    private DataContext _dataContext;
    private string BotFilePath = "FileSystem/Bots";

    public BotRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
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
    
    public async Task<HandlerResult<SuccessData<Bot>, IErrorResult>> GetBot(long botId)
    {
        var res = await _dataContext.Bots.FindAsync(botId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Bot>() { Data = res };
    }
    
    public async Task<HandlerResult<SuccessData<Game>, IErrorResult>> GetGame(long botId)
    {
        var res = await _dataContext.Bots.FindAsync(botId);
        if (res == null) return new EntityNotFoundErrorResult();
        var resGame = await _dataContext.Games.FindAsync(res.GameId);
        if (resGame == null) return new EntityNotFoundErrorResult();
        return new SuccessData<Game>() { Data = resGame };
    }

    public async Task<HandlerResult<Success, IErrorResult>> ValidationResult(long taskId,bool result)
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