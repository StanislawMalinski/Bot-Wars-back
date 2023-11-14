﻿using BotWars.Services;
using BotWars.Services.IServices;
using BotWars.TournamentData;
using Microsoft.AspNetCore.Mvc;

namespace BotWars.Controllers
{
    [Route("api/[controller]")]
    public class TournamentController : Controller
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> AddTournament([FromBody] TournamentDTO dto) 
        {
            var response = await _tournamentService.AddTournament(dto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> DeleteTournament([FromQuery] long id)
        {
            var response = await _tournamentService.DeleteTournament(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ServiceResponse<List<TournamentDTO>>>> GetListOfTournaments()
        {
            var response = await _tournamentService.GetListOfTournaments();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("list/filtered")]
        public async Task<ActionResult<ServiceResponse<List<TournamentDTO>>>> GetListOfTournamentsFiltered()
        {
            var response = await _tournamentService.GetListOfTournamentsFiltered();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("get")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> GetTournament([FromQuery] long id)
        {
            var response = await _tournamentService.GetTournament(id);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("register")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> RegisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            var response = await _tournamentService.RegisterSelfForTournament(tournamentId, playerId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("unregister")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> UnregisterSelfForTournament([FromQuery] long tournamentId, [FromQuery] long playerId)
        {
            var response = await _tournamentService.UnregisterSelfForTournament(tournamentId, playerId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<TournamentDTO>>> UpdateTournament([FromQuery] long id, [FromBody] TournamentDTO tournament)
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
