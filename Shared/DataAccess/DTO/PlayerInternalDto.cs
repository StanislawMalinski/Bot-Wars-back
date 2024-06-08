using System.ComponentModel.DataAnnotations;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.DTO;

public class PlayerInternalDto
{
    public long Id { get; set; }

    [EmailAddress] public string Email { get; init; }

    public string Login { get; init; }
    public string? Password { get; init; }

    [Range(1, 2, ErrorMessage = "Value must be between 1 and 2.")]
    public int RoleId { get; set; }

    public bool isBanned { get; init; }
    public long Points { get; init; }
    public Role Role { get; init; }
    public bool Deleted { get; init; }
}