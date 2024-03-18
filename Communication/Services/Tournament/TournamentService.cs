using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	// created to test state pattern implementation in TournamentService
	public class TournamentService : ITournamentService
	{
		private readonly TournamentRepository _tournamentRepository;

		public TournamentService(TournamentRepository tournamentRepository)
		{
			_tournamentRepository = tournamentRepository;
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(long userId, TournamentRequest tournamentRequest)
		{
			if (tournamentRequest.Image.Length % 4 != 0) return new IncorrectOperation(){Message = "to nie jest string base 64 musi miec wielkosc podzielna przez 4"};
			return await _tournamentRepository.CreateTournamentAsync(userId, tournamentRequest);
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			return await _tournamentRepository.DeleteTournamentAsync(id);
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournaments()
		{
			return await _tournamentRepository.GetTournamentsAsync();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournamentsFiltered(
			TournamentFilterRequest tournamentFilterRequest)
		{
			return await _tournamentRepository.GetFilteredTournamentsAsync(tournamentFilterRequest);
		}

		public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id)
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

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentRequest tournamentRequest)
		{
			return await _tournamentRepository.UpdateTournamentAsync(id, tournamentRequest);
		}
	}
}