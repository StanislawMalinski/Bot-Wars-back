using Communication.Services.Validation;

namespace Communication.Services;

public abstract class Service<T>(T adminInterface, T identifiedPlayerInterface,
    T bannedPlayerInterface, T unidentifiedUserInterface,
    T badValidationInterface, IPlayerValidator validator)
{
    private readonly T _adminInterface = adminInterface;
    private readonly T _identifiedPlayerInterface = identifiedPlayerInterface;
    private readonly T _bannedPlayerInterface = bannedPlayerInterface;
    private readonly T _unidentifiedUserInterface = unidentifiedUserInterface;
    private readonly T _badValidationInterface = badValidationInterface;
    private readonly IPlayerValidator _validator = validator;

    public T Validate(string login, string key)
    {
        var permissions = _validator.ValidateUser(login, key);

        return permissions switch
        {
            PlayerPermitEnum.ADMIN => _adminInterface,
            PlayerPermitEnum.BANNED_PLAYER => _bannedPlayerInterface,
            PlayerPermitEnum.IDENTIFIED_PLAYER => _identifiedPlayerInterface,
            PlayerPermitEnum.UNIDENTIFIED_PLAYER => _unidentifiedUserInterface,
            _ => _badValidationInterface
        };
    }
}