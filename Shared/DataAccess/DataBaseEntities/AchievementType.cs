namespace Shared.DataAccess.DataBaseEntities;

public class AchievementType
{
    public long Id{ get; set; }
    public string Description{ get; set; }

    public List<AchievementRecord> AchievementRecords{ get; set; }
    public List<AchievementThresholds> AchievementThresholds{ get; set; }
}