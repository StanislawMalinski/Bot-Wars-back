using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Engine.BusinessLogic.BackgroundWorkers.Resolvers;

public abstract class Resolver
{
    public abstract Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId);
}