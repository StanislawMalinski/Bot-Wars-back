using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointHistoryRepository
{
    public Task<ServiceResponse<List<PointHistory>>> GetHistoryForPlayer(long playerId);
    public Task<ServiceResponse<PointHistory>> LogNewPointsHistoryPoint();

}