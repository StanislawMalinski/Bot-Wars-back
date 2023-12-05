using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsRepository
{
    public Task<HandlerResult<Success,IErrorResult>> setPointsForPlayer(long PlayerId, long Points);
    public Task<HandlerResult<Success,IErrorResult>> addPoints(long PlayerId, long Points);
    public Task<HandlerResult<Success,IErrorResult>> subtracPoints(long PlayerId, long Points);
    public Task<HandlerResult<SuccessData<long>,IErrorResult>> getCurrentPointForPlayer(long PlayerId);
    public Task<HandlerResult<SuccessData<List<Player>>,IErrorResult>> getLeadboards();
}