using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotService
{
    public Task<ServiceResponse<Bot>> CreateBotAsync(Bot bot);

    public Task<ServiceResponse<Bot>> DeleteBotAsync(long id);

    public Task<ServiceResponse<Bot>> GetBotAsync(long id);

    public Task<ServiceResponse<List<Bot>>> GetBotsAsync();

    public Task<ServiceResponse<Bot>> UpdateBotAsync(Bot bot);
}