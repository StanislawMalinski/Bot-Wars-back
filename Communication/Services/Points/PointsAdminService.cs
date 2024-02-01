using Communication.ServiceInterfaces;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsAdminService : PointsIdentifiedPlayerService
{
    
    public PointsAdminService(PointsServiceProvider pointsServiceProvider) : base(pointsServiceProvider)
    {
    }
    
    public override async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
    {
        return await _pointsServiceProvider.SetPointsForPlayer(playerId, points);
    }
}