using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesPlayersService
{
    Task<HandlerResult<Success, IErrorResult>> CreateArchivedMatchPlayersAsync(MatchPlayers matchPlayers);
    Task<HandlerResult<Success, IErrorResult>> DeleteArchivedMatchPlayersAsync(long id);
    Task<HandlerResult<SuccessData<MatchPlayers>, IErrorResult>> GetArchivedMatchPlayersAsync(long id);
    Task<HandlerResult<SuccessData<List<MatchPlayers>>, IErrorResult>> GetArchivedMatchPlayerssAsync();
    Task<HandlerResult<Success, IErrorResult>> UpdateArchivedMatchPlayersAsync(MatchPlayers matchPlayers);
}