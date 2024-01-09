namespace Shared.DataAccess.DataBaseEntities;

public class AchievementThresholds
{
    public long Id{ get; set; }
    public long Threshold{ get; set; }
    public long AchievementTypeId{ get; set; }
    public AchievementType AchievementType{ get; set; }

}