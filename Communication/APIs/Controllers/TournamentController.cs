using Communication.APIs.Controllers.Helper;
using Communication.Services.Tournament;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers
{
	[Route("api/[controller]")]
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

        [HttpGet("list")]
        public async Task<IActionResult> GetListOfTournaments()
        {
            return (await _tournamentService.GetListOfTournaments()).Match(Ok,this.ErrorResult);
        }

        [HttpGet("list/filtered")]
        public async Task<IActionResult> GetListOfTournamentsFiltered()
        {
            return (await _tournamentService.GetListOfTournamentsFiltered()).Match(Ok,this.ErrorResult);
            
        }

        [HttpDelete("get")]
        public async Task<IActionResult> GetTournament([FromQuery] long id)
        {
            return (await _tournamentService.GetTournament(id)).Match(Ok,this.ErrorResult);
            
        }

        [HttpPut("register")]
        public async Task<IActionResult> RegisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            return (await _tournamentService.RegisterSelfForTournament(tournamentId, playerId)).Match(Ok,this.ErrorResult);
        }

        [HttpPut("unregister")]
        public async Task<IActionResult> UnregisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            return (await _tournamentService.UnregisterSelfForTournament(tournamentId, playerId)).Match(Ok,this.ErrorResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTournament([FromQuery] long id, [FromBody] TournamentDto tournament)
        {
            return (await _tournamentService.UpdateTournament(id, tournament)).Match(Ok,this.ErrorResult);
            
        }
    }
}
