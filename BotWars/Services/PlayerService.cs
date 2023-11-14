using BotWars.Gry;
using BotWars.Models;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Services
{
    public class PlayerService
    {
        private readonly DataContext _dataContext;
        public PlayerService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Player>> CreatePlayerAsync(Player Player)
        {
            try
            {
                await _dataContext.Players.AddAsync(Player);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Player>() { Data = Player, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>()
                {
                    Data = Player,
                    Success = false,
                    Message = "Cannot create Player"
                };
            }
        }

        public async Task<ServiceResponse<Player>> DeletePlayerAsync(long id)
        {


            try
            {
                Player book = _dataContext.Players.Find(id);
                if (book == null) return new ServiceResponse<Player>() { Data = book, Success = false, Message = $"Player of id {id} dont exits" };
                _dataContext.Players.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Player>()
                {
                    Data = book,
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
                Player Player = _dataContext.Players.Find(id);
                if (Player == null) return new ServiceResponse<Player>() { Data = Player, Success = false, Message = $"Player of id {id} dont exits" };

                return new ServiceResponse<Player>() { Data = Player, Success = true };
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

            var Players = await _dataContext.Players.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Player>>()
                {
                    Data = Players,
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

        public async Task<ServiceResponse<Player>> UpdatePlayerAsync(Player Player)
        {
            try
            {
                var productToEdit = new Player() { Id = Player.Id };
                _dataContext.Players.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Player> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Player>
                {
                    Data = Player,
                    Success = false,
                    Message = "An error occured while updating Player"
                };
            }
        }
    }
}
