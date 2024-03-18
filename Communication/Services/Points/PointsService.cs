using Communication.ServiceInterfaces;
using Communication.Services;
using Communication.Services.Validation;
using Microsoft.AspNetCore.Identity;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsService : Service<IPointsService>
{
    
    private IPointsService? _pointsService;
    private string login = "login"; // should be obtained in method call
    private string key = "key";
    
    public PointsService(PointsAdminService adminService,
        PointsBadValidationService badValidation,
        PointsBannedPlayerService bannedPlayerService,
        PointsIdentifiedPlayerService identifiedPlayerService,
        PointsUnidentifiedPlayerService unidentifiedPlayerService,
        IPlayerValidator validator)
        : base(adminService, badValidation, bannedPlayerService, identifiedPlayerService, unidentifiedPlayerService,
            validator)
    {
        
    }
    public async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
    {
        _pointsService = Validate(login, key);
        return await _pointsService.SetPointsForPlayer(playerId, points);
    } 
    
    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        _pointsService = Validate(login, key);
        return await _pointsService.GetHistoryForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> GetLeaderboards()
    {
        _pointsService = Validate(login, key);
        return await _pointsService.GetLeaderboards();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        _pointsService = Validate(login, key);
        return await _pointsService.GetPointsForPlayer(playerId);
    }
    
    
}