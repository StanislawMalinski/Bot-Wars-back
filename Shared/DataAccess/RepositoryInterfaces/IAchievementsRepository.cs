using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IAchievementsRepository
{
    Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId);
    Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAllAchievementsForPlayer(long playerId);
    Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold);
    Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes();
    Task<HandlerResult<Success, IErrorResult>> UpDateProgress(AchievementsTypes type, long userId);
    Task<HandlerResult<Success, IErrorResult>> UpDateProgressNoSave(AchievementsTypes type, long botId);

}