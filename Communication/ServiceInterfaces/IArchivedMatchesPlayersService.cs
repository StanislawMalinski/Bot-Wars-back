using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesPlayersService
{
	Task<HandlerResult<Success,IErrorResult>> CreateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers);
	Task<HandlerResult<Success,IErrorResult>> DeleteArchivedMatchPlayersAsync(long id);
	Task<HandlerResult<SuccessData<ArchivedMatchPlayers>,IErrorResult>> GetArchivedMatchPlayersAsync(long id);
	Task<HandlerResult<SuccessData<List<ArchivedMatchPlayers>>,IErrorResult>> GetArchivedMatchPlayerssAsync();
	Task<HandlerResult<Success,IErrorResult>> UpdateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers);
}