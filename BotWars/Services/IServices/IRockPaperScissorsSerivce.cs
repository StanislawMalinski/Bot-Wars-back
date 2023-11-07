using BotWars.RockPaperScissorsData;
using Microsoft.AspNetCore.Mvc;

namespace BotWars.Services.IServices
{
    public interface IRockPaperScissorsSerivce
    {
        public Task<ServiceResponse<RockPaperScissorsDto>> CreateGame(RockPaperScissors rockPaperScissors);
        public Task<ServiceResponse<RockPaperScissorsDto>> GetGameById(long id);
        public Task<ServiceResponse<List<RockPaperScissorsDto>>> GetAllGames();
        public Task<ServiceResponse<RockPaperScissorsDto>> PlayerOneMove(long id, Symbol symbol);
        public Task<ServiceResponse<RockPaperScissorsDto>> PlayerTwoMove(long id, Symbol symbol);
        public Task<ServiceResponse<RockPaperScissorsDto>> DeleteGame(long id);
    }
}
