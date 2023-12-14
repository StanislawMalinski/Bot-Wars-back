using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IBotService
{
    public Task<HandlerResult<Success,IErrorResult>> CreateBotAsync(Bot bot);

    public Task<HandlerResult<Success,IErrorResult>> DeleteBotAsync(long id);

    public Task<HandlerResult<SuccessData<Bot>,IErrorResult>> GetBotAsync(long id);

    public Task<HandlerResult<SuccessData<List<Bot>>,IErrorResult>> GetBotsAsync();

    public Task<HandlerResult<Success,IErrorResult>> UpdateBotAsync(Bot bot);
}