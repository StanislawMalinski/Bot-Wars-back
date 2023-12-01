namespace Shared.DataAccess.DataBaseEntities;

public class PointHistory
{
    public long Id { get; set; }
    
    public DateTime LogDate { get; set; }
    
    public long Loss  { get; set; }
    public long Gain { get; set; }
    public long PlayerId { get; set; }
    public Player? Player { get; set; }
}