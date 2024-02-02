using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IAchievementRecordMapper
{
    public AchievementRecordDto ToDto(AchievementRecord achievementRecord);
    public AchievementRecord ToAchievementRecord(AchievementRecordDto achievementRecordDto);
}