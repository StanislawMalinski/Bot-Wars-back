using BotWars.Services;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Services
{
    public class ArchivedMatchesPlayersService
    {
        private readonly DataContext _dataContext;
        public ArchivedMatchesPlayersService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<ArchivedMatchPlayers>> CreateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers)
        {
            try
            {
                await _dataContext.ArchivedMatchPlayers.AddAsync(ArchivedMatchPlayers);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<ArchivedMatchPlayers>() { Data = ArchivedMatchPlayers, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatchPlayers>()
                {
                    Data = ArchivedMatchPlayers,
                    Success = false,
                    Message = "Cannot create ArchivedMatchPlayers"
                };
            }
        }

        public async Task<ServiceResponse<ArchivedMatchPlayers>> DeleteArchivedMatchPlayersAsync(long id)
        {


            try
            {
                ArchivedMatchPlayers book = _dataContext.ArchivedMatchPlayers.Find(id);
                if (book == null) return new ServiceResponse<ArchivedMatchPlayers>() { Data = book, Success = false, Message = $"ArchivedMatchPlayers of id {id} dont exits" };
                _dataContext.ArchivedMatchPlayers.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<ArchivedMatchPlayers>()
                {
                    Data = book,
                    Message = "ArchivedMatchPlayers was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatchPlayers>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<ArchivedMatchPlayers>> GetArchivedMatchPlayersAsync(long id)
        {
            try
            {
                ArchivedMatchPlayers ArchivedMatchPlayers = _dataContext.ArchivedMatchPlayers.Find(id);
                if (ArchivedMatchPlayers == null) return new ServiceResponse<ArchivedMatchPlayers>() { Data = ArchivedMatchPlayers, Success = false, Message = $"ArchivedMatchPlayers of id {id} dont exits" };

                return new ServiceResponse<ArchivedMatchPlayers>() { Data = ArchivedMatchPlayers, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatchPlayers>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }
        }

        public async Task<ServiceResponse<List<ArchivedMatchPlayers>>> GetArchivedMatchPlayerssAsync()
        {

            var ArchivedMatchPlayerss = await _dataContext.ArchivedMatchPlayers.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<ArchivedMatchPlayers>>()
                {
                    Data = ArchivedMatchPlayerss,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<ArchivedMatchPlayers>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }

        }

        public async Task<ServiceResponse<ArchivedMatchPlayers>> UpdateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers)
        {
            try
            {
                var productToEdit = new ArchivedMatchPlayers() { Id = ArchivedMatchPlayers.Id };
                _dataContext.ArchivedMatchPlayers.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<ArchivedMatchPlayers> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatchPlayers>
                {
                    Data = ArchivedMatchPlayers,
                    Success = false,
                    Message = "An error occured while updating ArchivedMatchPlayers"
                };
            }
        }
    }

}
