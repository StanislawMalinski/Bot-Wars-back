using Communication.APIs.Controllers.Helper;
using Communication.Services.GameType;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;

namespace Communication.APIs.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class GameTypeController : Controller
    {

        private readonly GameTypeService _gameTypeService;

        public GameTypeController(GameTypeService gameTypeService) {
            _gameTypeService = gameTypeService;
        }

        [HttpPost("addGameType")]
        public async Task<IActionResult> CreateGameType([FromBody] GameDto game)
        {
            return (await _gameTypeService.CreateGameType(game)).Match(Ok,this.ErrorResult);
            
        }

        [HttpGet("getGameTypes")]
        public async Task<IActionResult> GetGameTypes()
        {
            return (await _gameTypeService.GetGameTypes()).Match(Ok,this.ErrorResult);;
            
        }

        [HttpDelete("deleteGameType")]
        public async Task<IActionResult> DeleteGame([FromQuery] long id)
        {
            return (await _gameTypeService.DeleteGame(id)).Match(Ok,this.ErrorResult);;
            
        }

        [HttpPut("modifyGameType")]
        public async Task<IActionResult> ModifyGameType([FromQuery] long id, [FromBody] GameDto gameDto)
        {
            return (await _gameTypeService.ModifyGameType(id, gameDto)).Match(Ok,this.ErrorResult);;
            
        }
    }
}
