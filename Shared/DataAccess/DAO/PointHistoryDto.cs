using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.DAO;

public class PointHistoryDto
{
    public long Id { get; set; }
    public DateTime LogDate { get; set; }
    public long Loss  { get; set; }
    public long Gain { get; set; }
    public long PlayerId { get; set; }
    public Player? Player { get; set; }
}

