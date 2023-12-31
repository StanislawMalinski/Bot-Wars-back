﻿using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Services
{
	public class BotService : IBotService
    {
        private readonly DataContext _dataContext;
        public BotService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<Bot>> CreateBotAsync(Bot Bot)
        {
            throw new NotImplementedException();
            /*
            try
            {
                await _dataContext.Bots.AddAsync(Bot);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Bot>() { Data = Bot, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Bot>()
                {
                    Data = Bot,
                    Success = false,
                    Message = "Cannot create Bot"
                };
            }*/
        }

        public async Task<ServiceResponse<Bot>> DeleteBotAsync(long id)
        {

            throw new NotImplementedException();
            /*
            try
            {
                Bot book = _dataContext.Bots.Find(id);
                if (book == null) return new ServiceResponse<Bot>() { Data = book, Success = false, Message = $"Bot of id {id} dont exits" };
                _dataContext.Bots.Remove(book);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Bot>()
                {
                    Data = book,
                    Message = "Bot was delated",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Bot>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/
        }

        public async Task<ServiceResponse<Bot>> GetBotAsync(long id)
        {
            throw new NotImplementedException();
            /*
            try
            {
                Bot Bot = _dataContext.Bots.Find(id);
                if (Bot == null) return new ServiceResponse<Bot>() { Data = Bot, Success = false, Message = $"Bot of id {id} dont exits" };

                return new ServiceResponse<Bot>() { Data = Bot, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Bot>()
                {
                    Data = null,
                    Success = false,
                    Message = "Problem with database"
                };
            }*/
        }

        public async Task<ServiceResponse<List<Bot>>> GetBotsAsync()
        {
            
            throw new NotImplementedException();
            /*
            var Bots = await _dataContext.Bots.ToListAsync();
            try
            {
                var response = new ServiceResponse<List<Bot>>()
                {
                    Data = Bots,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Bot>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }*/

        }

        public async Task<ServiceResponse<Bot>> UpdateBotAsync(Bot Bot)
        {
            throw new NotImplementedException();
            /*
            try
            {
                var productToEdit = new Bot() { Id = Bot.Id };
                _dataContext.Bots.Attach(productToEdit);

                //productToEdit.Description = product.Description;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Bot> { Data = productToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Bot>
                {
                    Data = Bot,
                    Success = false,
                    Message = "An error occured while updating Bot"
                };
            }*/
        }
    }
}
