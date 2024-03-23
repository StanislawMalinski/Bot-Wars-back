using System.Security.Claims;
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
        public async Task<IActionResult> RegisterUser([FromBody] PlayerDto dto)
        {
            return (await _playerService.registerNewPlayer(dto)).Match(Ok, this.ErrorResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            return (await _playerService.GenerateJwt(dto)).Match(Ok, this.ErrorResult);
        }

        [HttpGet("getPlayerInfo")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetPlayerInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.GetPlayerInfoAsync(long.Parse(userId))).Match(Ok, this.ErrorResult);
        }

        [HttpPost("changePassword")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.ChangePassword(dto, long.Parse(userId))).Match(Ok, this.ErrorResult);
        }

        [HttpDelete("deleteAccount")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.DeletePlayerAsync(long.Parse(userId))).Match(Ok, this.ErrorResult);
        }
    }
}
