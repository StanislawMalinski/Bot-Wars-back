using System.ComponentModel.DataAnnotations;

namespace Shared.DataAccess.DTO.Requests;

public class PasswordRequest
{
    [Required]
    public string Password { get; set; }
}