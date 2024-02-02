namespace Shared.DataAccess.DAO;

public class AchievementRecordDto
{
    public long Id { get; set; }
    public long Value { get; set; }
    public long AchievementTypeId { get; set; }
    public string Description { get; set; }
    public long PlayerId { get; set; }
}