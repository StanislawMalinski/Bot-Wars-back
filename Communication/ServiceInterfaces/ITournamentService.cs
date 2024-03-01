using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces
{
	public interface ITournamentService
    {
        public Task<HandlerResult<SuccessData<List<TournamentDto>>,IErrorResult>> GetListOfTournaments();

        public Task<HandlerResult<SuccessData<List<TournamentDto>>,IErrorResult>> GetListOfTournamentsFiltered();

        public Task<HandlerResult<SuccessData<TournamentDto>,IErrorResult>> GetTournament(long id);

        public Task<HandlerResult<Success,IErrorResult>> UpdateTournament(long id, TournamentDto tournament);

        public Task<HandlerResult<Success,IErrorResult>> DeleteTournament(long id);

        public Task<HandlerResult<Success,IErrorResult>> AddTournament(TournamentDto tournament);

        public Task<HandlerResult<Success,IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId);

        public Task<HandlerResult<Success,IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId);
    }
}
