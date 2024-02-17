using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Achievement;

public class AchievementIdentifiedPlayerService : IAchievementService
{
    private readonly AchievementServiceProvider _achievementServiceProvider;

    public AchievementIdentifiedPlayerService(AchievementServiceProvider achievementServiceProvider)
    {
        _achievementServiceProvider = achievementServiceProvider;
    }

    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId)
    {
        return await _achievementServiceProvider.GetAchievementsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        return await _achievementServiceProvider.UnlockAchievement(playerId, achievementTypeId, currentPlayerThreshold);
    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        return await _achievementServiceProvider.GetAchievementTypes();
    }
}