namespace Shared.DataAccess.DataBaseEntities;

public class AchievementRecord
{
    public long Id { get; set; }
    public long Value { get; set; }
    public long AchievementTypeId { get; set; }
    public AchievementType AchievementType { get; set; }
    public long PlayerId { get; set; }
    public Player Player { get; set; }
}