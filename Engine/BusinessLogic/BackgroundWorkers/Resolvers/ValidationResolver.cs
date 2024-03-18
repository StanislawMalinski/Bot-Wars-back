using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public class ValidationResolver : Resolver
{
    private readonly TaskRepository _taskRepository;
    private readonly BotRepository _botRepository;

    public ValidationResolver(TaskRepository taskRepository, BotRepository botRepository)
    {
        _taskRepository = taskRepository;
        _botRepository = botRepository;
    }
    public override async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    { 
        return await _taskRepository.GetTask(taskId);
    }
    public async Task<HandlerResult<SuccessData<Bot>,IErrorResult>> GetBot(long botId)
    {
        return await _botRepository.GetBot(botId);
    }
    public async Task<HandlerResult<SuccessData<Game>,IErrorResult>> GetGame(long botId)
    {
        return await _botRepository.GetGame(botId);
    }

    public async Task<HandlerResult<Success,IErrorResult>> ValidateBot(long taskId, bool result)
    {
        return await _botRepository.ValidationResult(taskId,result);
    }
}