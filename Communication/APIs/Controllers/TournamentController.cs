using Communication.Services.Tournament;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

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
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> AddTournament([FromBody] TournamentDto dto) 
        {
            var response = await _tournamentService.AddTournament(dto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> DeleteTournament([FromQuery] long id)
        {
            var response = await _tournamentService.DeleteTournament(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ServiceResponse<List<TournamentDto>>>> GetListOfTournaments()
        {
            var response = await _tournamentService.GetListOfTournaments();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("list/filtered")]
        public async Task<ActionResult<ServiceResponse<List<TournamentDto>>>> GetListOfTournamentsFiltered()
        {
            var response = await _tournamentService.GetListOfTournamentsFiltered();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("get")]
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> GetTournament([FromQuery] long id)
        {
            var response = await _tournamentService.GetTournament(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("register")]
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> RegisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            var response = await _tournamentService.RegisterSelfForTournament(tournamentId, playerId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("unregister")]
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> UnregisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            var response = await _tournamentService.UnregisterSelfForTournament(tournamentId, playerId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<TournamentDto>>> UpdateTournament([FromQuery] long id, [FromBody] TournamentDto tournament)
        {
            var response = await _tournamentService.UpdateTournament(id, tournament);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
