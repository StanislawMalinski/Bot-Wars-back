using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesService
{
    Task<HandlerResult<Success,IErrorResult>> CreateArchivedMatchesAsync(Matches matches);
    Task<HandlerResult<Success,IErrorResult>> DeleteArchivedMatchesAsync(long id);
    Task<HandlerResult<SuccessData<Matches>,IErrorResult>> GetArchivedMatchesAsync(long id);
    Task<HandlerResult<SuccessData<List<Matches>>,IErrorResult>> GetArchivedMatchessAsync();
    Task<HandlerResult<Success,IErrorResult>> UpdateArchivedMatchesAsync(Matches matches);
}