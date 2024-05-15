using Engine.Services;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class ValidationResolver : Resolver
{
    private readonly TaskService _taskService;
    private readonly IBotRepository _botRepository;

    public ValidationResolver(TaskService taskService, IBotRepository botRepository)
    {
        _taskService = taskService;
        _botRepository = botRepository;
    }
    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        return await _taskService.GetTask(taskId);
    }
    public async Task<HandlerResult<SuccessData<Bot>,IErrorResult>> GetBot(long botId)
    {
        var res = await _botRepository.GetBot(botId);
        if (res == null)
        {
            return new EntityNotFoundErrorResult();
        }

        return new SuccessData<Bot>()
        {
            Data = res
        };
    }
    public async Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetGame(long botId)
    {
        var res= await _botRepository.GetGame(botId);
        if (res == null)
        {
            return new EntityNotFoundErrorResult();
        }

        return new SuccessData<Game>()
        {
            Data = res
        };
    }

    public async Task<HandlerResult<Success,IErrorResult>> ValidateBot(long taskId, bool result,int memoryUsed,int timeUsed)
    {
        
        var resTask = await _taskService.GetTask(taskId);
        if (resTask.IsError) return new EntityNotFoundErrorResult();
        var task = resTask.Match(x => x.Data, null!);
        var resBot = await _botRepository.GetBot(task!.OperatingOn);
        if (resBot == null) return new EntityNotFoundErrorResult();
        task.Status = TaskStatus.Done;
        resBot.Validation = result ? BotStatus.ValidationSucceed : BotStatus.ValidationFailed;
        resBot.MemoryUsed = memoryUsed;
        resBot.TimeUsed = timeUsed;
        await _botRepository.SaveChangeAsync();
        return new Success();
        
    }
}