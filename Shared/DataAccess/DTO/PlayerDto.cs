namespace Shared.DataAccess.DTO;

public class PlayerDto
{

    public string Email { get; init; }
    public string Login { get; init; }
    public string? Password { get; init; }
    
    public int RoleId { get; set; }
    public bool isBanned {get; init;}
    public long Points { get; init; } 
}