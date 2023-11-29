using BotWars.Services;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointHistoryRepository
{
    public Task<ServiceResponse<List<PointHistory>>> GetPointHistoryAsync();

    public Task<ServiceResponse<PointHistory>> GetPointHistoryAsync(long id);

    public Task<ServiceResponse<PointHistory>> UpdatePointHistoryAsync(PointHistory product);

    public Task<ServiceResponse<PointHistory>> DeletePointHistoryAsync(long id);

    public Task<ServiceResponse<PointHistory>> CreatePointHistoryAsync(PointHistory product);
}