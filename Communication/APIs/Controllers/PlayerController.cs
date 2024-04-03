using System.Security.Claims;
using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;

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

        [HttpPost("registerPlayer")]
        public async Task<IActionResult> RegisterUser([FromBody] RegistrationRequest registrationRequest)
        {
            return (await _playerService.RegisterNewPlayer(registrationRequest)).Match(Ok, this.ErrorResult);
        }

        [HttpPost("registerAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegistrationRequest registrationRequest)
        {
            return (await _playerService.RegisterNewAdmin(registrationRequest)).Match(Ok, this.ErrorResult);
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
        
        [HttpGet("GetPlayerInfoByName")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetPlayerInfoByName(string playerName)
        {
            return (await _playerService.GetPlayerInfoAsync(playerName)).Match(Ok, this.ErrorResult);
        }
        
        [HttpPost("changePassword")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.ChangePassword(changePasswordRequest, long.Parse(userId))).Match(Ok, this.ErrorResult);
        }

        [HttpPost("changeLogin")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> ChangeLogin([FromBody] ChangeLoginRequest changeLoginRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.ChangeLogin(changeLoginRequest, long.Parse(userId))).Match(Ok, this.ErrorResult);
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

        [HttpDelete("deleteAccount")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.DeletePlayerAsync(long.Parse(userId))).Match(Ok, this.ErrorResult);
        }
        
        [HttpGet("getMyBots")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetMyBots()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _playerService.GetPlayerBots(long.Parse(userId))).Match(Ok,this.ErrorResult);
        }
    }
}