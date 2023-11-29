using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Repositories;

public class PointHistoryRepository : IPointHistoryRepository
{
    private DataContext _dataContext;
    public PointHistoryRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<ServiceResponse<List<PointHistory>>> GetPointHistoryAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<PointHistory>> GetPointHistoryAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<PointHistory>> UpdatePointHistoryAsync(PointHistory product)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<PointHistory>> DeletePointHistoryAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<PointHistory>> CreatePointHistoryAsync(PointHistory product)
    {
        throw new NotImplementedException();
    }
}