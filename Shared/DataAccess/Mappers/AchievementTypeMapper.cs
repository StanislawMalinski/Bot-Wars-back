using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class AchievementTypeMapper : IAchievementTypeMapper
{
    public AchievementTypeDto ToDto(AchievementType achievementType)
    {
        var thresholds = achievementType
            .AchievementThresholds
            .ConvertAll(threshold => threshold.Threshold)
            .ToList();
        return new AchievementTypeDto
        {
            Id = achievementType.Id,
            Description = achievementType.Description,
            Thresholds = thresholds
        };
    }

    public AchievementType ToAchievementRecord(AchievementTypeDto achievementTypeDto)
    {
        return new AchievementType
        {
            Id = achievementTypeDto.Id,
            Description = achievementTypeDto.Description,
        };
    }
}