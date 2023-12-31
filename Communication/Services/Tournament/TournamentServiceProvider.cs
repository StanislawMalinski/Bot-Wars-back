﻿using Shared.DataAccess.DAO;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.Services.Results;

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

		public async Task<ServiceResponse<TournamentDto>> AddTournament(TournamentDto tournament)
		{
			return await _tournamentRepository.CreateTournamentAsync(tournament);
		}

		public async Task<ServiceResponse<TournamentDto>> DeleteTournament(long id)
		{
			return await _tournamentRepository.DeleteTournamentAsync(id);
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournaments()
		{
			return await _tournamentRepository.GetTournamentsAsync();
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournamentsFiltered()
		{
			var tourlist = await _tournamentRepository.GetTournamentsAsync();
			if (tourlist.Success)
			{
				//filter;
			}
			return tourlist;
		}

		public async Task<ServiceResponse<TournamentDto>> GetTournament(long id)
		{
			return await _tournamentRepository.GetTournamentAsync(id);
		}

		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentRepository.RegisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentRepository.UnregisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament)
		{
			tournament.Id = id;
			return await _tournamentRepository.UpdateTournamentAsync(tournament);
		}
	}
}