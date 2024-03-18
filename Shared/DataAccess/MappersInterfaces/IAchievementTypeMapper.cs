using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IAchievementTypeMapper
{
    public AchievementTypeDto ToDto(AchievementType achievementType);
    public AchievementType ToAchievementRecord(AchievementTypeDto achievementTypeDto);
}