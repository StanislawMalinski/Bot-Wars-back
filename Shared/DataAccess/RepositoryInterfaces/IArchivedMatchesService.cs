using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesService
{
    Task<HandlerResult<Success,IErrorResult>> CreateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
    Task<HandlerResult<Success,IErrorResult>> DeleteArchivedMatchesAsync(long id);
    Task<HandlerResult<SuccessData<ArchivedMatches>,IErrorResult>> GetArchivedMatchesAsync(long id);
    Task<HandlerResult<SuccessData<List<ArchivedMatches>>,IErrorResult>> GetArchivedMatchessAsync();
    Task<HandlerResult<Success,IErrorResult>> UpdateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
}