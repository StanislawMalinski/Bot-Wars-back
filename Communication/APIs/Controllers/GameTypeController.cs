using BotWars.Services;
using Communication.APIs.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<ServiceResponse<GameTypeDto>>> CreateGameType([FromBody] GameTypeDto gameType)
        {
            var response = await _gameTypeService.CreateGameType(gameType);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("getGameTypes")]
        public async Task<ActionResult<ServiceResponse<GameTypeDto>>> GetGameTypes()
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
        public async Task<ActionResult<ServiceResponse<GameTypeDto>>> ModifyGameType([FromQuery] long id, [FromBody] GameTypeDto gameTypeDto)
        {
            var response = await _gameTypeService.ModifyGameType(id, gameTypeDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
