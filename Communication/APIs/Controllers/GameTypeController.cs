using BotWars.Services;
using Communication.APIs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameTypeController : Controller
    {

        private readonly IGameTypeService _gameTypeService;

        public GameTypeController(IGameTypeService gameTypeService) {
            _gameTypeService = gameTypeService;
        }

        [HttpPost("addGameType")]
        public async Task<ActionResult<ServiceResponse<GameDto>>> CreateGameType([FromBody] GameDto game)
        {
            var response = await _gameTypeService.CreateGameType(game);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("getGameTypes")]
        public async Task<ActionResult<ServiceResponse<GameDto>>> GetGameTypes()
        {
            var response = await _gameTypeService.GetGameTypes();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
            
        }

        [HttpDelete("deleteGameType")]
        public async Task<IActionResult> DeleteGame([FromQuery] long id)
        {
            var response = await _gameTypeService.DeleteGame(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPut("modifyGameType")]
        public async Task<ActionResult<ServiceResponse<GameDto>>> ModifyGameType([FromQuery] long id, [FromBody] GameDto gameDto)
        {
            var response = await _gameTypeService.ModifyGameType(id, gameDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
