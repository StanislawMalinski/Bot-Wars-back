using BotWars.Services;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;

namespace Shared.DataAccess.Services
{
    public class ArchivedMatchesService : IArchivedMatchesService
    {
        private readonly DataContext _dataContext;
        public ArchivedMatchesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<ArchivedMatches>> CreateArchivedMatchesAsync(ArchivedMatches ArchivedMatches)
        {
            throw new NotImplementedException();
            /*
            try
            {
                await _dataContext.ArchivedMatches.AddAsync(ArchivedMatches);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<ArchivedMatches>() { Data = ArchivedMatches, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatches>()
                {
                    Data = ArchivedMatches,
                    Success = false,
                    Message = "Cannot create ArchivedMatches"
                };
            }*/
        }

        public async Task<ServiceResponse<ArchivedMatches>> DeleteArchivedMatchesAsync(long id)
        {

            throw new NotImplementedException();
            /*
            try
            {
                ArchivedMatches book = _dataContext.ArchivedMatches.Find(id);
                if (book == null) return new ServiceResponse<ArchivedMatches>() { Data = book, Success = false, Message = $"ArchivedMatches of id {id} dont exits" };
                _dataContext.ArchivedMatches.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<ArchivedMatches>()
                {
                    Data = book,
                    Message = "ArchivedMatches was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatches>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/
        }

        public async Task<ServiceResponse<ArchivedMatches>> GetArchivedMatchesAsync(long id)
        {
            throw new NotImplementedException();
            /*
            try
            {
                ArchivedMatches ArchivedMatches = _dataContext.ArchivedMatches.Find(id);
                if (ArchivedMatches == null) return new ServiceResponse<ArchivedMatches>() { Data = ArchivedMatches, Success = false, Message = $"ArchivedMatches of id {id} dont exits" };

                return new ServiceResponse<ArchivedMatches>() { Data = ArchivedMatches, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatches>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }*/
        }

        public async Task<ServiceResponse<List<ArchivedMatches>>> GetArchivedMatchessAsync()
        {
            throw new NotImplementedException();
            /*
            var ArchivedMatchess = await _dataContext.ArchivedMatches.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<ArchivedMatches>>()
                {
                    Data = ArchivedMatchess,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<ArchivedMatches>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/

        }

        public async Task<ServiceResponse<ArchivedMatches>> UpdateArchivedMatchesAsync(ArchivedMatches ArchivedMatches)
        {
            throw new NotImplementedException();
            /*
            try
            {
                var productToEdit = new ArchivedMatches() { Id = ArchivedMatches.Id };
                _dataContext.ArchivedMatches.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<ArchivedMatches> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<ArchivedMatches>
                {
                    Data = ArchivedMatches,
                    Success = false,
                    Message = "An error occured while updating ArchivedMatches"
                };
            }*/
        }
    }
}
