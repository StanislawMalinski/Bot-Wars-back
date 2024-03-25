﻿using System.Security.Claims;
using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers
{
	[Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]PlayerDto dto)
        {
            return (await _playerService.registerNewPlayer(dto)).Match(Ok,this.ErrorResult);
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            return (await _playerService.GenerateJwt(dto)).Match(Ok,this.ErrorResult);
        }
        
        [HttpGet("getPlayerInfo")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetPlayerInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.GetPlayerInfoAsync(long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
        
        
        [HttpPost("changePassword")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.ChangePassword(dto, long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
        
        [HttpGet("getMyGames")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetMyGames()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.GetMyGames(long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
         
        [HttpGet("getMyImage")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetMyImage()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.GetPlayerImage(long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
         
        [HttpPost("changeMyImage")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangeMyImage(PlayerImageRequest imageRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.ChangePlayerImage(imageRequest,long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
        /*
        [HttpPost("add")]
        public async Task<IActionResult> AddTournament([FromBody] PlayerDto dto)
        {
            return (await _playerService.CreatePlayerAsync(dto)).Match(Ok,this.ErrorResult);;
           
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTournament([FromQuery] long id)
        {
            return (await _playerService.DeletePlayerAsync(id)).Match(Ok,this.ErrorResult);;
        }

        

        [HttpDelete("get")]
        public async Task<IActionResult> GetTournament([FromQuery] long id)
        {
            return (await _playerService.GetPlayerAsync(id)).Match(Ok,this.ErrorResult);;
           
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTournament([FromBody] PlayerDto player)
        {
            return (await _playerService.UpdatePlayerAsync(player)).Match(Ok,this.ErrorResult);;
            
        }*/
    }
}
