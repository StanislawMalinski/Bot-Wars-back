﻿using BotWars.Services;

namespace Communication.Services
{
    public class PlayerService
    {
        private readonly PlayerRepository _playerRepository;
        public PlayerService(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;   
        }

        public async Task<ServiceResponse<Player>> CreatePlayerAsync(Player player)
        {
            return await _playerRepository.CreatePlayerAsync(player);
        }

        public async Task<ServiceResponse<Player>> DeletePlayerAsync(long id)
        {
            return await _playerRepository.DeletePlayerAsync(id);
        }

        public async Task<ServiceResponse<Player>> GetPlayerAsync(long id)
        {
            return await _playerRepository.GetPlayerAsync(id);
        }

        public async Task<ServiceResponse<List<Player>>> GetPlayersAsync()
        {

            return await _playerRepository.GetPlayersAsync();

        }

        public async Task<ServiceResponse<Player>> UpdatePlayerAsync(Player player)
        {
            return await _playerRepository.UpdatePlayerAsync(player);
        }
    }
}
