using Communication.APIs.Controllers.Helper;
using Communication.Services.Player;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;

namespace Communication.APIs.Controllers
{
	[Route("api/[controller]")]
    public class PlayerController : Controller
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpGet("get-info ")]
        public async Task<IActionResult> GetPlayerInfo([FromQuery] long id)
        {
            return (await _playerService.GetPlayerInfo(id)).Match(Ok,this.ErrorResult);;
           
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewPlayer([FromBody] PlayerDto dto)
        {
            return (await _playerService.RegisterNewPlayer(dto)).Match(Ok,this.ErrorResult);;
        }

        [HttpPut("reset-password-login")]
        public async Task<IActionResult> ResetPasswordByLogin([FromQuery]string login)
        {
            return (await _playerService.ResetPassWordByLogin(login)).Match(Ok,this.ErrorResult);;
        }

        [HttpPut("reset-password-email")]
        public async Task<IActionResult> ResetPasswordByEmail([FromQuery] string email)
        {
            return (await _playerService.ResetPassWordByEmail(email)).Match(Ok,this.ErrorResult);;
           
        }
    }
}
