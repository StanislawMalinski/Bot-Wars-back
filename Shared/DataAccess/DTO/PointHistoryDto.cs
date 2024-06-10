namespace Shared.DataAccess.DTO;

public class PointHistoryDto
{
    public long Id { get; set; }
    public DateTime LogDate { get; set; }
    public long Before { get; set; }
    public long Change { get; set; }
    public long PlayerId { get; set; }
}