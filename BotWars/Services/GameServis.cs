using BotWars.Gry;
using BotWars.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BotWars.Services
{
    public class GameServis : IGameServis
    {

        private readonly DataContext _dataContext;
        public GameServis(DataContext dataContext) { 
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Game>> CreateGameAsync(Game product)
        {
            try
            {
                await _dataContext.Games.AddAsync(product);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Game>() { Data = product, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>()
                {
                    Data = product,
                    Success = false,
                    Message = "Cannot create book"
                };
            }
        }

        public async Task<ServiceResponse<Game>> DeleteGameAsync(long id)
        {
            
            
            try
            {
                Game book = _dataContext.Games.Find(id);
                if (book == null) return new ServiceResponse<Game>() { Data = book, Success = false, Message = $"Book of id {id} dont exits" };
                _dataContext.Games.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Game>()
                {
                    Data = book,
                    Message = "Book was delated",
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
            }
        }

        public async Task<ServiceResponse<Game>> GetGameAsync(long id)
        {
            try
            {
                Game book = _dataContext.Games.Find(id);
                if(book == null) return new ServiceResponse<Game>() { Data = book, Success = false,Message=$"Book of id {id} dont exits" };

                return new ServiceResponse<Game>() { Data = book, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }

        public async Task<ServiceResponse<List<Game>>> GetGamesAsync()
        {

            var books = await _dataContext.Games.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Game>>()
                {
                    Data = books,
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
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }

        }

        public async Task<ServiceResponse<Game>> UpdateGameAsync(Game product)
        {
            try
            {
                var productToEdit = new Game() { Id = product.Id };
                _dataContext.Games.Attach(productToEdit);
/*
                productToEdit.Title = product.Title;
                productToEdit.Description = product.Description;
                productToEdit.Author = product.Author;
                productToEdit.NumberOfBooks = product.NumberOfBooks;*/

                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Game> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Game>
                {
                    Data = product,
                    Success = false,
                    Message = "An error occured while updating product"
                };
            }
        }
    }
}
