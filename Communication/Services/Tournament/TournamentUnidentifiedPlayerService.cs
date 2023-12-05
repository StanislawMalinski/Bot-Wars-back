using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentUnidentifiedPlayerService : ITournamentService
	{
		protected readonly TournamentServiceProvider _tournamentServiceProvider;

		public TournamentUnidentifiedPlayerService(TournamentServiceProvider tournamentServiceProvider)
		{
			_tournamentServiceProvider = tournamentServiceProvider;
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentDto tournament)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournaments()
		{
			return await _tournamentServiceProvider.GetListOfTournaments();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournamentsFiltered()
		{
			return await _tournamentServiceProvider.GetListOfTournamentsFiltered();
		}

		public async Task<HandlerResult<SuccessData<TournamentDto>, IErrorResult>> GetTournament(long id)
		{
			return await _tournamentServiceProvider.GetTournament(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentDto tournament)
		{
			
			return new AccessDeniedError();
		}
	}
}
