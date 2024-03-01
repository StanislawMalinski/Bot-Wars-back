using System.ComponentModel.DataAnnotations;

namespace Shared.DataAccess.DTO;

public class PlayerDto
{   
    [EmailAddress]
    public string Email { get; init; }
    public string Login { get; init; }
    public string? Password { get; init; }
    [Range(1, 2, ErrorMessage = "Value must be between 1 and 2.")]
    public int RoleId { get; set; }
    public bool isBanned {get; init;}
    public long Points { get; init; } 
}