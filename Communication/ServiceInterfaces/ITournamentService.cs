using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface ITournamentService
{
    public Task<HandlerResult<SuccessData<PageResponse<TournamentResponse>>, IErrorResult>>
        GetListOfTournamentsFiltered(
            TournamentFilterRequest tournamentFilterRequest, PageParameters pageParameters);

    public Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id);

    public Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentRequest tournamentRequest,
        long playerId);

    public Task<HandlerResult<Success, IErrorResult>> DeleteUserScheduledTournaments(long userId);
    public Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id, long playerId);

    public Task<HandlerResult<Success, IErrorResult>> AddTournament(long userId,
        TournamentRequest tournamentRequest);

    public Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId,
        long playerId);

    public Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId,
        long playerId);
}