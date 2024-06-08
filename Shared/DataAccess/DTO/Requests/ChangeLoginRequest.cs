using System.ComponentModel.DataAnnotations;

namespace Shared.DataAccess.DTO.Requests;

public class ChangeLoginRequest
{
    [Required] public string Login { get; set; }

    [Required] public string NewLogin { get; set; }
}