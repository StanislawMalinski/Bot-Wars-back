using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Shared.DataAccess.Mappers;

public interface IAchievementTypeMapper
{
    public AchievementTypeDto ToDto(AchievementType achievementType);
    public AchievementType ToAchievementRecord(AchievementTypeDto achievementTypeDto);
}