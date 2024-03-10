using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface ITournamentRepository
{
    Task<HandlerResult<Success, IErrorResult>> CreateTournamentAsync(TournamentDto dto);
    Task<HandlerResult<Success, IErrorResult>> DeleteTournamentAsync(long id);
    Task<HandlerResult<SuccessData<TournamentDto>, IErrorResult>> GetTournamentAsync(long id);
    Task<HandlerResult<Success, IErrorResult>> UpdateTournamentAsync(TournamentDto dto);
    Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetTournamentsAsync();
    Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId);
    Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId);
    Task<HandlerResult<SuccessData<List<Bot>>, IErrorResult>> TournamentBotsToPlay(long tournamentId);
    Task<HandlerResult<SuccessData<Game>, IErrorResult>> TournamentGame(long tournamentId);
    Task<HandlerResult<Success, IErrorResult>> TournamentEnded(long tournamentId);
}