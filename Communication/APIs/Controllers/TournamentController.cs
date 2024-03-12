using Communication.APIs.Controllers.Helper;
using Communication.Services.Tournament;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;

namespace Communication.APIs.Controllers
{
	[Route("api/v1/[controller]")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTournament([FromBody] TournamentDto dto) 
        {
            return  (await _tournamentService.AddTournament(dto)).Match(Ok,this.ErrorResult);
            
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTournament([FromQuery] long id)
        {
            return (await _tournamentService.DeleteTournament(id)).Match(Ok,this.ErrorResult);
            
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetListOfTournaments()
        {
            return (await _tournamentService.GetListOfTournaments()).Match(Ok,this.ErrorResult);
        }

        [HttpPost("getFiltered")]
        public async Task<IActionResult> GetListOfTournamentsFiltered(TournamentFilterDto tournamentFilterDto)
        {
            return (await _tournamentService.GetListOfTournamentsFiltered(tournamentFilterDto)).Match(Ok,this.ErrorResult);
            
        }

        [HttpGet("getOne")]
        public async Task<IActionResult> GetTournament([FromQuery] long id)
        {
            return (await _tournamentService.GetTournament(id)).Match(Ok,this.ErrorResult);
            
        }

        [HttpPut("registerBot")]
        public async Task<IActionResult> RegisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long botId)
        {
            return (await _tournamentService.RegisterSelfForTournament(tournamentId, botId)).Match(Ok,this.ErrorResult);
        }

        [HttpPut("unregisterBot")]
        public async Task<IActionResult> UnregisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long botId)
        {
            return (await _tournamentService.UnregisterSelfForTournament(tournamentId, botId)).Match(Ok,this.ErrorResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTournament([FromQuery] long id, [FromBody] TournamentDto tournament)
        {
            return (await _tournamentService.UpdateTournament(id, tournament)).Match(Ok,this.ErrorResult);
            
        }
    }
}
