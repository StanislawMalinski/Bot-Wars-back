using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class AchievementRecordMapper : IAchievementRecordMapper
{
    public AchievementRecordDto ToDto(AchievementRecord achievementRecord)
    {
        return new AchievementRecordDto
        {
            Id = achievementRecord.Id,
            AchievementTypeId = achievementRecord.AchievementTypeId,
            Description = achievementRecord.AchievementType.Description,
            PlayerId = achievementRecord.PlayerId,
            Value = achievementRecord.Value
        };
        
    }

    public AchievementRecordDto ToDto(AchievementRecord achievementRecord, AchievementThresholds achievementThresholds)
    {
        return new AchievementRecordDto
        {
            Id = achievementRecord.Id,
            AchievementTypeId = achievementRecord.AchievementTypeId,
            Description = achievementRecord.AchievementType.Description,
            PlayerId = achievementRecord.PlayerId,
            Value = achievementThresholds.Threshold,
        };
    }

    public AchievementRecord ToAchievementRecord(AchievementRecordDto achievementRecordDto)
    {
        return new AchievementRecord
        {
            Id = achievementRecordDto.Id,
            AchievementTypeId = achievementRecordDto.AchievementTypeId,
            PlayerId = achievementRecordDto.PlayerId,
            Value = achievementRecordDto.Value
        };
    }
}