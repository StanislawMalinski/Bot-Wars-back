using BotWars.Services.GameTypeService;

namespace BotWars.Services.IServices;

public abstract class Service<T>
{
    private readonly T _adminInterface;
    private readonly T _identifiedPlayerInterface;
    private readonly T _bannedPlayerInterface;
    private readonly T _unidentifiedUserInterface;
    private readonly T _badValidationInterface;
    private readonly IPlayerValidator _validator;

    protected Service(T adminInterface, T identifiedPlayerInterface,
        T bannedPlayerInterface, T unidentifiedUserInterface,
        T badValidationInterface, IPlayerValidator validator)
    {
        _adminInterface = adminInterface;
        _identifiedPlayerInterface = identifiedPlayerInterface;
        _bannedPlayerInterface = bannedPlayerInterface;
        _unidentifiedUserInterface = unidentifiedUserInterface;
        _badValidationInterface = badValidationInterface;
        _validator = validator;
    }

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