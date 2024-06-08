using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointHistoryRepository
{
    public Task<List<PointHistory>> GetHistoryForPlayer(long playerId);
}