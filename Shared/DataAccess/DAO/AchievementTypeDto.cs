namespace Shared.DataAccess.DAO;

public class AchievementTypeDto
{
    public long Id{ get; set; }
    public string Description{ get; set; }
    public List<long> Thresholds { get; set; }
}