using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Achievement;

public class AchievementServiceProvider : IAchievementService
{

    private readonly IAchievementsRepository _achievementsRepository;

    public AchievementServiceProvider(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId)
    {
        return await _achievementsRepository.GetAchievementsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        return await _achievementsRepository.UnlockAchievement(playerId, achievementTypeId, currentPlayerThreshold);
    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        return await _achievementsRepository.GetAchievementTypes();
    }
}