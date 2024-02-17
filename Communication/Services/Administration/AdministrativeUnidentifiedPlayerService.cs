using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeUnidentifiedPlayerService : IAdministrativeService
{

    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        return new AccessDeniedError();
    }
}