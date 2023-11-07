using BotWars.RockPaperScissorsData;
using BotWars.Services;
using BotWars.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace BotWars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RockPaperScissorsController : Controller
    {

        private readonly IRockPaperScissorsSerivce _rockPaperScissorsSerivce;

        public RockPaperScissorsController(IRockPaperScissorsSerivce rockPaperScissorsSerivce)
        {
            _rockPaperScissorsSerivce = rockPaperScissorsSerivce;
        }

        [HttpPost("addGame")]
        public async Task<ActionResult<ServiceResponse<RockPaperScissorsDto>>> CreateGame(string playerOneName, string playerTwoName)
        {
            RockPaperScissors rockPaperScissors = new() { PlayerOneName = playerOneName, PlayerTwoName = playerTwoName, SymbolPlayerOne = Symbol.NONE, SymbolPlayerTwo = Symbol.NONE };
            var response = await _rockPaperScissorsSerivce.CreateGame(rockPaperScissors);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        
        }

        [HttpGet("getGame")]
        public async Task<IActionResult> GetGameById(long id) 
        {
            var response = await _rockPaperScissorsSerivce.GetGameById(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("getGames")]
        public async Task<IActionResult> GetGames()
        {
            var response = await _rockPaperScissorsSerivce.GetAllGames();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPatch("playerOneMove")]
        public async Task<IActionResult> PlayerOneMove(long id, Symbol symbol) 
        {
            var response = await _rockPaperScissorsSerivce.PlayerOneMove(id, symbol);
            if (response.Success) 
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPatch("playerTwoMove")]
        public async Task<IActionResult> PlayerTwoMove(long id, Symbol symbol)
        {
            var response = await _rockPaperScissorsSerivce.PlayerTwoMove(id, symbol);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("deleteGame")]
        public async Task<IActionResult> DeleteGame(long id) 
        {
            var response = await _rockPaperScissorsSerivce.DeleteGame(id);
            if (response.Success) 
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
