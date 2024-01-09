namespace Shared.DataAccess.DataBaseEntities;

public class PlayerPassword
{
    public long Id { get; set; }
    public string? HashedPassword { get; set; }
    
    public long PlayerId { get; set; }
    public Player Player { get; set; }
}