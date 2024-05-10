using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IAchievementsRepository
{
    Task<AchievementType?> GetAchievementTypeById(long achievementTypeId);

    Task<List<AchievementRecord>> GetAchievementRecordsByPlayerIdAndAchievementTypeId(long playerId,
        long achievementTypeId);

    void AddAchievementRecord(AchievementRecord obtainedAchievement);
    Task<List<AchievementThresholds>> GetAchievementThresholdsByAchievementTypeId(long achievementTypeId);
    Task<List<AchievementType>> GetAchievementTypes();
    Task<HandlerResult<Success, IErrorResult>> UpDateProgress(AchievementsTypes type, long userId);
    Task<HandlerResult<Success, IErrorResult>> UpDateProgressNoSave(AchievementsTypes type, long botId);
    Task<List<AchievementRecord>> GetAchievementRecordsByPlayerId(long playerId);
    Task<List<AchievementThresholds>> GetAchievementRecordByAchievementRecord(AchievementRecord achievementRecord);

}