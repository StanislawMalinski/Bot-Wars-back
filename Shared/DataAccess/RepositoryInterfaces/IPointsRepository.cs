using BotWars.Services;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsRepository
{
    public Task<ServiceResponse<bool>> setPointsForPlayer(long PlayerId, long Points);
    public Task<ServiceResponse<bool>> addPoints(long PlayerId, long Points);
    public Task<ServiceResponse<bool>> subtracPoints(long PlayerId, long Points);
    public Task<ServiceResponse<long>> getCurrentPointForPlayer(long PlayerId);
    public Task<ServiceResponse<List<Player>>> getLeadboards();
}