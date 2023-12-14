namespace Shared.DataAccess.DAO;

public class PlayerDto
{
    public long Id { get; init; }
    public string Email { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public bool isBanned {get; init;}
    public long Points { get; init; } 
}