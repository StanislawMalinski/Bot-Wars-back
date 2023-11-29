using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Services
{
	public class GameService : IGameService
    {

        private readonly DataContext _dataContext;
        public GameService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Game>> CreateGameAsync(Game game)
        {
            throw new NotImplementedException();
            /*
            try
            {
                await _dataContext.Games.AddAsync(game);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Game>() { Data = game, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>()
                {
                    Data = game,
                    Success = false,
                    Message = "Cannot create game"
                };
            }*/
        }

        public async Task<ServiceResponse<Game>> DeleteGameAsync(long id)
        {

            throw new NotImplementedException();
            /*
            try
            {
                Game book = _dataContext.Games.Find(id);
                if (book == null) return new ServiceResponse<Game>() { Data = book, Success = false, Message = $"Game of id {id} dont exits" };
                _dataContext.Games.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Game>()
                {
                    Data = book,
                    Message = "Game was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/
        }

        public async Task<ServiceResponse<Game>> GetGameAsync(long id)
        {
            throw new NotImplementedException();
            /*
            try
            {
                Game game = _dataContext.Games.Find(id);
                if (game == null) return new ServiceResponse<Game>() { Data = game, Success = false, Message = $"Game of id {id} dont exits" };

                return new ServiceResponse<Game>() { Data = game, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }*/
        }

        public async Task<ServiceResponse<List<Game>>> GetGamesAsync()
        {
            throw new NotImplementedException();
            /*
            var games = await _dataContext.Games.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Game>>()
                {
                    Data = games,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Game>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/

        }

        public async Task<ServiceResponse<Game>> UpdateGameAsync(Game game)
        {
            throw new NotImplementedException();
            /*
            try
            {
                var productToEdit = new Game() { Id = game.Id };
                _dataContext.Games.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Game> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>
                {
                    Data = game,
                    Success = false,
                    Message = "An error occured while updating game"
                };
            }*/
        }
    }
}
