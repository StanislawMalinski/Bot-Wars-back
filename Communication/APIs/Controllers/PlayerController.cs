using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;

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
        
        [HttpGet("getOne")]
        public async Task<IActionResult> GetPlayers(long id)
        {
            return (await _playerService.GetPlayerInfoAsync(id)).Match(Ok,this.ErrorResult);;
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

        [HttpGet("getPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            return (await _playerService.GetPlayersAsync()).Match(Ok,this.ErrorResult);;
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
