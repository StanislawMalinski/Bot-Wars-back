using Communication.APIs.Controllers.Helper;
using Communication.Services.Tournament;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;
using System.Security.Claims;
using Shared.DataAccess.Pagination;

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
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> AddTournament(TournamentRequest tournamentRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _tournamentService.AddTournament(long.Parse(userId), tournamentRequest)).Match(Ok,
                this.ErrorResult);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> DeleteTournament([FromQuery] long id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _tournamentService.DeleteTournament(id, long.Parse(userId))).Match(Ok, this.ErrorResult);
        }

        [HttpPost("getFiltered")]
        public async Task<IActionResult> GetListOfTournamentsFiltered([FromBody] TournamentFilterRequest tournamentFilterRequest, [FromQuery] PageParameters pageParameters)
        {
            return (await _tournamentService.GetListOfTournamentsFiltered(tournamentFilterRequest, pageParameters)).Match(Ok,this.ErrorResult);
        }

        [HttpGet("getOne")]
        public async Task<IActionResult> GetTournament([FromQuery] long id)
        {
            return (await _tournamentService.GetTournament(id)).Match(Ok, this.ErrorResult);
        }

        [HttpPut("registerBot")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> RegisterSelfForTournament([FromQuery] long tournamentId,
            [FromQuery] long botId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _tournamentService.RegisterSelfForTournament(tournamentId, botId, long.Parse(userId))).Match(
                Ok, this.ErrorResult);
        }

        [HttpPut("unregisterBot")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UnregisterSelfForTournament([FromQuery] long tournamentId,
            [FromQuery] long botId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _tournamentService.UnregisterSelfForTournament(tournamentId, botId, long.Parse(userId)))
                .Match(Ok, this.ErrorResult);
        }

        [HttpPut("update")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateTournament([FromQuery] long id,
            [FromForm] TournamentRequest tournamentRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return (await _tournamentService.UpdateTournament(id, tournamentRequest, long.Parse(userId))).Match(Ok,
                this.ErrorResult);
        }
    }
}