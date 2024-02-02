using Communication.ServiceInterfaces;
using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Achievement;

public class AchievementService : Service<IAchievementService>
{
    private IAchievementService _achievementService;
    private string login = "login";
    private string key = "key";

    public AchievementService(
        AchievementAdminService adminInterface,
        AchievementIdentifiedPlayerService identifiedPlayerInterface,
        AchievementUnidentifiedPlayerService unidentifiedPlayerInterface,
        AchievementBannedPlayerService bannedPlayerInterface,
        AchievementBadValidationService badValidationInterface,
        IPlayerValidator validator
    ) : base(
        adminInterface,
        identifiedPlayerInterface,
        bannedPlayerInterface,
        unidentifiedPlayerInterface,
        badValidationInterface,
        validator
    )
    {
    }
    
    public async Task<HandlerResult<SuccessData<List<AchievementRecordDto>>, IErrorResult>> GetAchievementsForPlayer(long playerId)
    {
        _achievementService = Validate(login, key);
        return await _achievementService.GetAchievementsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnlockAchievement(long playerId, long achievementTypeId, long currentPlayerThreshold)
    {
        _achievementService = Validate(login, key);
        return await _achievementService.UnlockAchievement(playerId, achievementTypeId, currentPlayerThreshold);
    }

    public async Task<HandlerResult<SuccessData<List<AchievementTypeDto>>, IErrorResult>> GetAchievementTypes()
    {
        _achievementService = Validate(login, key);
        return await _achievementService.GetAchievementTypes();
    }
    
}