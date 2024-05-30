using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsRepository
{
    Task<List<PointHistoryDto>> GetHistoryForPlayer(long playerId);
    Task<long> GetCurrentPointsForPlayer(long playerId);
    Task<List<PlayerResponse>> GetLeaderboards(PageParameters pageParameters);
    Task<long> GetPlayerPoint(long playerId);
    Task UpdatePointsForPlayerNoSave(long playerId, long points,long tourId);
    Task<int> NumberOfLeaderBoard();
}