﻿using Communication.APIs.Controllers.Helper;
using Communication.Services.GameType;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;

namespace Communication.APIs.Controllers
{
	[Route("api/v1/[controller]")]
    [ApiController]
    public class GameTypeController : Controller
    {

        private readonly GameTypeService _gameTypeService;

        public GameTypeController(GameTypeService gameTypeService) {
            _gameTypeService = gameTypeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateGameType([FromBody] GameDto game)
        {
            return (await _gameTypeService.CreateGameType(game)).Match(Ok,this.ErrorResult);
            
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetGameTypes()
        {
            return (await _gameTypeService.GetGameTypes()).Match(Ok,this.ErrorResult);;
            
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteGame([FromQuery] long id)
        {
            return (await _gameTypeService.DeleteGame(id)).Match(Ok,this.ErrorResult);;
            
        }

        [HttpPut("update")]
        public async Task<IActionResult> ModifyGameType([FromQuery] long id, [FromBody] GameDto gameDto)
        {
            return (await _gameTypeService.ModifyGameType(id, gameDto)).Match(Ok,this.ErrorResult);;
            
        }
    }
}
