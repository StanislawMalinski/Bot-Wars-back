using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Responses;
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

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournaments()
		{
			return await _tournamentServiceProvider.GetListOfTournaments();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournamentsFiltered(
			TournamentFilterDto tournamentFilterDto)
		{
			return await _tournamentServiceProvider.GetListOfTournamentsFiltered(tournamentFilterDto);
		}

		public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id)
		{
			return await _tournamentServiceProvider.GetTournament(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentDto tournament)
		{
			
			return new AccessDeniedError();
		}
	}
}
