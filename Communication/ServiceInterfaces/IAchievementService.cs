using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IAchievementService
{
    Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId);
    Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold);
    Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes();
    Task GetAchievementIcon();
}