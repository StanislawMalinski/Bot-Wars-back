using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Repositories;

public class PointHistoryRepository : IPointHistoryRepository
{
    private readonly DataContext _dataContext;

    public PointHistoryRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<List<PointHistory>>> GetHistoryForPlayer(long playerId)
    {
        try
        {
            var result = from entity in _dataContext.PointHistories
                where entity.PlayerId == playerId
                select entity;

            List<PointHistory> pointHistories = result.ToList();
            return new ServiceResponse<List<PointHistory>>()
            {
                Data = pointHistories,
                Message = "It works!",
                Success = true
            };
        }
        catch (Exception)
        {
            return new ServiceResponse<List<PointHistory>>()
            {
                Data = null,
                Message = "Database Failure",
                Success = false
            };
        }
    }

    public Task<ServiceResponse<PointHistory>> LogNewPointsHistoryPoint()
    {
        throw new NotImplementedException();
    }
}