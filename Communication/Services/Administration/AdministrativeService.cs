using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Administration;

public class AdministrativeService : IAdministrativeService
{
    private readonly IAdministrativeRepository _administrativeRepository;
    private string login = "login";
    private string key = "key";

    public AdministrativeService(IAdministrativeRepository administrativeRepository)
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