using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeServiceProvider
{
    private readonly IAdministrativeRepository _administrativeRepository;

    public AdministrativeServiceProvider(IAdministrativeRepository administrativeRepository)
    {
        _administrativeRepository = administrativeRepository;
    }

    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        return await _administrativeRepository.UnbanPlayer(playerId);
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        return await _administrativeRepository.BanPlayer(playerId);
    }
}