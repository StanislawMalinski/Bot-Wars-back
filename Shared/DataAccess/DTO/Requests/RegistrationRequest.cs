using System.ComponentModel.DataAnnotations;

namespace Shared.DataAccess.DTO.Requests;

public class RegistrationRequest
{
    [EmailAddress] public string Email { get; init; }

    public string Login { get; init; }
    public string Password { get; init; }
}