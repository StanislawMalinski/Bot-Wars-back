using System.ComponentModel.DataAnnotations;

namespace Shared.DataAccess.DTO.Requests;

public class ChangePasswordRequest
{
    [Required] public string Password { get; set; }

    [Required] public string ChangePassword { get; set; }
}