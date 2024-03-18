using Communication.APIs.Controllers.Helper;
using Communication.Services.Tournament;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers
{
	[Route("api/v1/[controller]")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTournament([FromBody] TournamentRequest tournamentRequest) 
        {
            return  (await _tournamentService.AddTournament(tournamentRequest)).Match(Ok,this.ErrorResult);
            
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
        public async Task<IActionResult> GetListOfTournamentsFiltered(TournamentFilterRequest tournamentFilterRequest)
        {
            return (await _tournamentService.GetListOfTournamentsFiltered(tournamentFilterRequest)).Match(Ok,this.ErrorResult);
            
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
        public async Task<IActionResult> UpdateTournament([FromQuery] long id, [FromBody] TournamentRequest tournamentRequest)
        {
            return (await _tournamentService.UpdateTournament(id, tournamentRequest)).Match(Ok,this.ErrorResult);
            
        }
    }
}
