using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class AdministrativeRepository : IAdministrativeRepository
{
    public Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        throw new NotImplementedException();
    }

    public Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        throw new NotImplementedException();
    }
}