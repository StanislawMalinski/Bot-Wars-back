using Communication.APIs.Controllers.Helper;
using Communication.Services.GameType;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO.Requests;

namespace Communication.APIs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameTypeController : Controller
    {
        private readonly GameTypeService _gameTypeService;

        public GameTypeController(GameTypeService gameTypeService)
        {
            _gameTypeService = gameTypeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateGameType([FromForm] GameRequest gameRequest)
        {
            return (await _gameTypeService
                    .CreateGameType(gameRequest))
                .Match(Ok, this.ErrorResult);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetGames()
        {
            return (await _gameTypeService.GetGameTypes()).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getAvailable")]
        public async Task<IActionResult> GetAvailableGames()
        {
            return (await _gameTypeService.GetAvailableGames()).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getOne")]
        public async Task<IActionResult> GetGameById([FromQuery] long id)
        {
            return (await _gameTypeService.GetGame(id)).Match(Ok, this.ErrorResult);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGame([FromQuery] long id)
        {
            return (await _gameTypeService.DeleteGame(id)).Match(Ok, this.ErrorResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> ModifyGameType([FromQuery] long id, [FromForm] GameRequest gameRequest)
        {
            return (await _gameTypeService
                    .ModifyGameType(id, gameRequest))
                .Match(Ok, this.ErrorResult);
        }
    }
}