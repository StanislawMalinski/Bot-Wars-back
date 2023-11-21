using BotWars.Services;
using Microsoft.AspNetCore.Mvc;

namespace Communication.APIs.Controllers
{
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<Player>>> AddTournament([FromBody] Player dto)
        {
            var response = await _playerService.CreatePlayerAsync(dto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<Player>>> DeleteTournament([FromQuery] long id)
        {
            var response = await _playerService.DeletePlayerAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("getPlayers")]
        public async Task<ActionResult<ServiceResponse<List<Player>>>> GetPlayers()
        {
            var response = await _playerService.GetPlayersAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("get")]
        public async Task<ActionResult<ServiceResponse<Player>>> GetTournament([FromQuery] long id)
        {
            var response = await _playerService.GetPlayerAsync(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<Player>>> UpdateTournament([FromBody] Player player)
        {
            var response = await _playerService.UpdatePlayerAsync(player);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
