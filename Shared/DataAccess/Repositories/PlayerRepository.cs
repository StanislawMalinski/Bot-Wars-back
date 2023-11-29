using BotWars.Services;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Repositories
{
    public class PlayerRepository
    {
        private readonly DataContext _context;

        public PlayerRepository(DataContext context) { 
            _context = context;
        }

        public async Task<ServiceResponse<Player>> CreatePlayerAsync(Player player)
        {
            try
            {
                
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
                return new ServiceResponse<Player>() { Data = player, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>()
                {
                    Data = player,
                    Success = false,
                    Message = "Cannot create Player"
                };
            }
        }
        public async Task<ServiceResponse<Player>> DeletePlayerAsync(long id)
        {
            try
            {
                var player = _context.Players.Find(id);
                if (player == null) return new ServiceResponse<Player>() { Data = player, Success = false, Message = $"Player of id {id} does not exist" };
                _context.Players.Remove(player);
                await _context.SaveChangesAsync();
                var response = new ServiceResponse<Player>()
                {
                    Data = player,
                    Message = "Player was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }
        public async Task<ServiceResponse<Player>> GetPlayerAsync(long id)
        {
            try
            {
                var player = _context.Players.Find(id);
                if (player == null) return new ServiceResponse<Player>() { Data = player, Success = false, Message = $"Player of id {id} dont exits" };

                return new ServiceResponse<Player>() { Data = player, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }
        public async Task<ServiceResponse<List<Player>>> GetPlayersAsync()
        {

            var players = await _context.Players.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Player>>()
                {
                    Data = players,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Player>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }

        }
        public async Task<ServiceResponse<Player>> UpdatePlayerAsync(Player player)
        {
            try
            {
                var productToEdit = new Player() { Id = player.Id };
                _context.Players.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _context.SaveChangesAsync();
                return new ServiceResponse<Player> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>
                {
                    Data = player,
                    Success = false,
                    Message = "An error occured while updating Player"
                };
            }
        }
    }
}
