using Communication.Services.Validation;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeService : Service<IAdministrativeService>
{
    private IAdministrativeService _administrativeService;
    private string login = "login";
    private string key = "key";

    public AdministrativeService(
        AdministrativeAdminService adminInterface,
        AdministrativeIdentifiedPlayerService identifiedPlayerInterface,
        AdministrativeUnidentifiedPlayerService unidentifiedPlayerInterface,
        AdministrativeBannedPlayerService bannedPlayerInterface,
        AdministrativeBadValidationService badValidationInterface,
        IPlayerValidator validator
    ) : base(
        adminInterface,
        identifiedPlayerInterface,
        bannedPlayerInterface,
        unidentifiedPlayerInterface,
        badValidationInterface,
        validator
    )
    {
    }

    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        _administrativeService = Validate(login, key);
        return await _administrativeService.BanPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        _administrativeService = Validate(login, key);
        return await _administrativeService.UnbanPlayer(playerId);
    }
}