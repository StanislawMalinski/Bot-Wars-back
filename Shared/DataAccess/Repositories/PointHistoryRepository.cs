using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class PointHistoryRepository : IPointHistoryRepository
{
    private readonly DataContext _dataContext;

    public PointHistoryRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<List<PointHistory>> GetHistoryForPlayer(long playerId)
    {
        return await _dataContext.PointHistories.Where(x => x.PlayerId == playerId).ToListAsync();
    }
}
