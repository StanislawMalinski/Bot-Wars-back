using Shared.DataAccess.DTO;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	// created to test state pattern implementation in TournamentService
	public class TournamentServiceProvider
	{
		private readonly TournamentRepository _tournamentRepository;

		public TournamentServiceProvider(TournamentRepository tournamentRepository)
		{
			_tournamentRepository = tournamentRepository;
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentDto tournament)
		{
			return await _tournamentRepository.CreateTournamentAsync(tournament);
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			return await _tournamentRepository.DeleteTournamentAsync(id);
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournaments()
		{
			return await _tournamentRepository.GetTournamentsAsync();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournamentsFiltered()
		{
			var tourlist = await _tournamentRepository.GetTournamentsAsync();
			
			return tourlist;
		}

		public async Task<HandlerResult<SuccessData<TournamentDto>, IErrorResult>> GetTournament(long id)
		{
			return await _tournamentRepository.GetTournamentAsync(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentRepository.RegisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentRepository.UnregisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentDto tournament)
		{
			tournament.Id = id;
			return await _tournamentRepository.UpdateTournamentAsync(tournament);
		}
	}
}