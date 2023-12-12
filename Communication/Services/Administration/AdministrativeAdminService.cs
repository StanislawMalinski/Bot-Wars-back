using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeAdminService : IAdministrativeService
{
    private readonly AdministrativeServiceProvider _administrativeServiceProvider;

    public AdministrativeAdminService(AdministrativeServiceProvider administrativeServiceProvider)
    {
        _administrativeServiceProvider = administrativeServiceProvider;
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        return await _administrativeServiceProvider.UnbanPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        return await _administrativeServiceProvider.BanPlayer(playerId);
    }
}