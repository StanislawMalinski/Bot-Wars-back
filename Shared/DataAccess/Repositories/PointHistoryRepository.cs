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
    public async Task<HandlerResult<SuccessData<List<PointHistory>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        
        var result = from entity in _dataContext.PointHistories
            where entity.PlayerId == playerId
            select entity;
        
        List<PointHistory> pointHistories = result.ToList();
        return new  SuccessData<List<PointHistory>>()
        {
            Data = pointHistories
        };
        
    }
}
