using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentBadValidation : ITournamentService
	{
		public TournamentBadValidation(TournamentServiceProvider tournamentServiceProvider)
		{
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentRequest tournamentRequest)
		{
		
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournaments()
		{
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournamentsFiltered(
			TournamentFilterRequest tournamentFilterRequest)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			
			return new AccessDeniedError();
		}

		public async  Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentRequest tournamentRequest)
		{
			return new AccessDeniedError();
		}
	}
}
