using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Achievement;

public class AchievementUnidentifiedPlayerService : IAchievementService
{
    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId)
    {
        return new AccessDeniedError();;
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        return new AccessDeniedError();
    }
}