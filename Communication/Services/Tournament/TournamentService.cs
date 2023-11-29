using BotWars.Services;
using BotWars.Services.GameTypeService;
using BotWars.TournamentData;
using Communication.APIs.DTOs;
using Communication.Services;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.Tournament
{
	public class TournamentService : Service<ITournamentService>, ITournamentService
	{
		private ITournamentService? _playerTypeService;
		private string login = "login"; // should be obtained in method call
		private string key = "key";

		public TournamentService(ITournamentService adminInterface,
			ITournamentService identifiedPlayerInterface,
			ITournamentService bannedPlayerInterface,
			ITournamentService unidentifiedUserInterface,
			ITournamentService badValidationInterface,
			IPlayerValidator validator)
		: base(adminInterface, identifiedPlayerInterface, bannedPlayerInterface, unidentifiedUserInterface, badValidationInterface, validator)
		{
		}

		public async Task<ServiceResponse<TournamentDto>> AddTournament(TournamentDto tournament)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.AddTournament(tournament);
		}

		public async Task<ServiceResponse<TournamentDto>> DeleteTournament(long id)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.DeleteTournament(id);
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournaments()
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetListOfTournaments();
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournamentsFiltered()
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetListOfTournamentsFiltered();
		}

		public async Task<ServiceResponse<TournamentDto>> GetTournament(long id)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetTournament(id);
		}

		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.RegisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.UnregisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.UpdateTournament(id, tournament);
		}
	}
}