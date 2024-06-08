namespace Shared.DataAccess.DataBaseEntities;

public class PointHistory
{
    public long Id { get; set; }

    public DateTime LogDate { get; set; }

    public long Change { get; set; }
    public long Before { get; set; }
    public long PlayerId { get; set; }
    public Player? Player { get; set; }
    public Tournament Tournament { get; set; }
    public long TournamentId { get; set; }
}