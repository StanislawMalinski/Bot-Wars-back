﻿using BotWars.GameTypeData;
using BotWars.Models;
using BotWars.Services.Constants;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Services.GameTypeService
{
    public class GameTypeService : IGameTypeService, IPlayerValidator
    {

        private readonly IGameTypeMapper _mapper;
        private readonly DataContext _dataContext;

        public GameTypeService(IGameTypeMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<GameTypeDto>> CreateGameType(GameTypeDto gameTypeDto)
        {
            try
            {
                GameType gameType = _mapper.ToGameType(gameTypeDto);
                await _dataContext.GameTypes.AddAsync(gameType);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<GameTypeDto>() { Data = gameTypeDto, Success = true, Message = GameTypeConstants.GAMETYPE_CREATED_SUCCESS };
            }
            catch (Exception)
            {
                return new ServiceResponse<GameTypeDto>()
                {
                    Data = null,
                    Success = false,
                    Message = GameTypeConstants.GAMETYPE_CREATED_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<GameTypeDto>> DeleteGame(long id)
        {
            try
            {
                var gameType =  _dataContext.GameTypes.Find(id);
                if (gameType == null) return new ServiceResponse<GameTypeDto>() { Data = null, Success = false, Message = $"Game Type with id {id} does not exist" };
                _dataContext.GameTypes.Remove(gameType);
                var gameTypeDto = _mapper.ToDto(gameType);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<GameTypeDto>()
                {
                    Data = gameTypeDto,
                    Message = GameTypeConstants.GAMETYPE_DELETED,
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<GameTypeDto>()
                {
                    Data = null,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE,
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<List<GameTypeDto>>> GetGameTypes()
        {
            try
            {
                List<GameType> gameTypes = await _dataContext.GameTypes.ToListAsync();
                if (!gameTypes.Any()) return new ServiceResponse<List<GameTypeDto>>()
                {
                    Data = null,
                    Success = false,
                    Message = GameTypeConstants.NO_GAMETYPES_FOUND
                };
                List<GameTypeDto> gameTypeDtos = gameTypes.Select(x => _mapper.ToDto(x)).ToList();
                return new ServiceResponse<List<GameTypeDto>>()
                {
                    Data = gameTypeDtos,
                    Success = true,
                    Message = GameTypeConstants.GAMETYPE_LIST_SUCCESS
                };

            }
            catch (Exception)
            {
                return new ServiceResponse<List<GameTypeDto>>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<GameTypeDto>> ModifyGameType(long id, GameTypeDto gameTypeDto)
        {
            try 
            {
                var gameType = _mapper.ToGameType(gameTypeDto);
                gameType.Id = id;
                var oldValue = _dataContext.GameTypes.Update(gameType);
                if (oldValue == null) return new ServiceResponse<GameTypeDto>() { Data = null, Success = false, Message = $"Game Type with id {id} does not exist" };
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<GameTypeDto>()
                {
                    Data = gameTypeDto,
                    Message = GameTypeConstants.GAMETYPE_MODIFIED_SUCCESS,
                    Success = true
                };
                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<GameTypeDto>()
                {
                    Data = null,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE,
                    Success = false
                };
            }
        }

        public int ValidateUser(string login, string key)
        {
            //TODO Zaimplementować walidację kiedy będzie użytkownik
            throw new NotImplementedException();
        }
    }
}
