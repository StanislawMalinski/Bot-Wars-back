using Communication.APIs.Controllers.Helper;
using Communication.Services.GameType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;
using System.ComponentModel.DataAnnotations;

namespace Communication.APIs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameTypeController : Controller
    {
        private readonly IGameService _gameTypeService;

        public GameTypeController(IGameService gameTypeService)
        {
            _gameTypeService = gameTypeService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> CreateGameType([FromForm] GameRequest gameRequest)
        {
            return (await _gameTypeService
                    .CreateGameType(1L,gameRequest))
                .Match(Ok, this.ErrorResult);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetGames([FromQuery] int page = 0, [FromQuery] int pagesize = 10)
        {
            return (await _gameTypeService.GetGames(page, pagesize)).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getAvailable")]
        public async Task<IActionResult> GetAvailableGames([FromQuery] int page = 0, [FromQuery] int pagesize = 10)
        {
            return (await _gameTypeService.GetListOfTypesOfAvailableGames(page, pagesize)).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getOne")]
        public async Task<IActionResult> GetGameById([FromQuery] long id)
        {
            return (await _gameTypeService.GetGame(id)).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name, [FromQuery] int page = 0, [FromQuery] int pagesize = 10)
        {
            return (await _gameTypeService.Search(name, page, pagesize)).Match(Ok, this.ErrorResult);
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteGame([FromQuery] long id)
        {
            return (await _gameTypeService.DeleteGame(id)).Match(Ok, this.ErrorResult);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> ModifyGameType([FromQuery] long id, [FromForm] GameRequest gameRequest)
        {
            return (await _gameTypeService
                    .ModifyGameType(id, gameRequest))
                .Match(Ok, this.ErrorResult);
        }
    }
}