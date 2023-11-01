using BotWars.Gry;
using BotWars.Models;
using BotWars.RockPaperScissorsData;
using BotWars.Services.Constants;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Services
{
    public class RockPaperScissorsService : IRockPaperScissorsSerivce
    {

        private readonly DataContext _dataContext;
        private readonly IRockPaperScissorsMapper _mapper;

        public RockPaperScissorsService(DataContext dataContext, IRockPaperScissorsMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RockPaperScissorsDto>> CreateGame(RockPaperScissors rockPaperScissors)
        {

            try
            {
                await _dataContext.RockPaperScissors.AddAsync(rockPaperScissors);
                await _dataContext.SaveChangesAsync();
                var rpsDto = _mapper.toDto(rockPaperScissors);
                return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = true, Message = RockPaperScissorsConstants.GAME_CREATED_SUCCESS };
            }
            catch (Exception)
            {
                return new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.GAME_CREATED_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<RockPaperScissorsDto>> GetGameById(long id)
        {
            try
            {
                RockPaperScissors rps = await _dataContext.RockPaperScissors.FindAsync(id);
                if (rps == null) return new ServiceResponse<RockPaperScissorsDto>() { Data = null, Success = false, Message = $"Game of rock papaer scissors of id {id} does not exist" };
                var rpsDto = _mapper.toDto(rps);
                return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<List<RockPaperScissorsDto>>> GetAllGames()
        {
            try
            {
                List<RockPaperScissors> rpss = await _dataContext.RockPaperScissors.ToListAsync();
                if (!rpss.Any()) return new ServiceResponse<List<RockPaperScissorsDto>>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.NO_GAMES_FOUND
                };
                List<RockPaperScissorsDto> rpssDto = rpss.Select(x => _mapper.toDto(x)).ToList();
                return new ServiceResponse<List<RockPaperScissorsDto>>()
                {
                    Data = rpssDto,
                    Success = true
                };

            }
            catch (Exception)
            {
                return new ServiceResponse<List<RockPaperScissorsDto>>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<RockPaperScissorsDto>> PlayerOneMove(long id, Symbol symbol) 
        {
            RockPaperScissors rps = await _dataContext.RockPaperScissors.FindAsync(id);

            try
            {
                if (rps == null) return new ServiceResponse<RockPaperScissorsDto>() { Data = null, Success = false, Message = $"Game of rock papaer scissors of id {id} does not exist" };
                var rpsDto = _mapper.toDto(rps);
                if (rpsDto.HasPlayerOneMoved) return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = false, Message = RockPaperScissorsConstants.PLAYER_ONE_MOVED };
                rps.SymbolPlayerOne = symbol;
                rps.Winner = CheckWinner(rps);
                rpsDto = _mapper.toDto(rps);    
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<RockPaperScissorsDto>> PlayerTwoMove(long id, Symbol symbol)
        {
            RockPaperScissors rps = await _dataContext.RockPaperScissors.FindAsync(id);

            try
            {
                if (rps == null) return new ServiceResponse<RockPaperScissorsDto>() { Data = null, Success = false, Message = $"Game of rock papaer scissors of id {id} does not exist" };
                var rpsDto = _mapper.toDto(rps);
                if (rpsDto.HasPlayerTwoMoved) return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = false, Message = RockPaperScissorsConstants.PLAYER_TWO_MOVED };
                rps.SymbolPlayerTwo = symbol;
                rps.Winner = CheckWinner(rps);
                string message = ConstructMessage(rps.Winner);
                rpsDto = _mapper.toDto(rps);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<RockPaperScissorsDto>() { Data = rpsDto, Success = true, Message = message };
            }
            catch (Exception)
            {
                return new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = null,
                    Success = false,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE
                };
            }
        }

        public async Task<ServiceResponse<RockPaperScissorsDto>> DeleteGame(long id) 
        {
            try
            {
                RockPaperScissors rps = _dataContext.RockPaperScissors.Find(id);
                if (rps == null) return new ServiceResponse<RockPaperScissorsDto>() { Data = null, Success = false, Message = $"Game of rock papaer scissors of id {id} does not exist" };
                _dataContext.RockPaperScissors.Remove(rps);
                var rpsDto = _mapper.toDto(rps);
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = rpsDto,
                    Message = RockPaperScissorsConstants.DELETED_MESSAGE,
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<RockPaperScissorsDto>()
                {
                    Data = null,
                    Message = RockPaperScissorsConstants.DATABASE_FAILURE,
                    Success = false
                };
            }
        }

        private string CheckWinner(RockPaperScissors rps) 
        {
            if(rps.SymbolPlayerOne.Equals(Symbol.NONE) || rps.SymbolPlayerTwo.Equals(Symbol.NONE)) return null;

            if (rps.SymbolPlayerOne.Equals(rps.SymbolPlayerTwo)) return RockPaperScissorsConstants.TIE;

            if ((rps.SymbolPlayerOne.Equals(Symbol.PAPER) && rps.SymbolPlayerTwo.Equals(Symbol.ROCK)) ||
                (rps.SymbolPlayerOne.Equals(Symbol.SCISSORS) && rps.SymbolPlayerTwo.Equals(Symbol.PAPER)) ||
                (rps.SymbolPlayerOne.Equals(Symbol.ROCK) && rps.SymbolPlayerTwo.Equals(Symbol.SCISSORS))) return rps.PlayerOneName;
            else return rps.PlayerTwoName;
        }

        private string ConstructMessage(string winner) 
        {
            if (winner==null) return RockPaperScissorsConstants.GAME_ONGOING_MESSAGE;
            if (winner.Equals(RockPaperScissorsConstants.TIE)) return RockPaperScissorsConstants.TIE_MESSAGE;
            else return RockPaperScissorsConstants.VICTORY_MESSAGE + winner;
        }
    }
}
