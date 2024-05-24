using System.Net;
using Communication.APIs.Controllers.Helper;
using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.Pagination;
using ContentDispositionHeaderValue = System.Net.Http.Headers.ContentDispositionHeaderValue;
using MediaTypeHeaderValue = System.Net.Http.Headers.MediaTypeHeaderValue;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MatchController : Controller
{
    private readonly IMatchService _matchService;

    public MatchController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpPost("getFiltered")]
    public async Task<IActionResult> GetListOfMatchesFiltered([FromBody] MatchFilterRequest matchFilterRequest, [FromQuery] PageParameters pageParameters)
    {
        return (await _matchService.GetListOfMatchesFiltered(matchFilterRequest, pageParameters)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("getById")]
    public async Task<IActionResult> GetMatchById([FromQuery] long id)
    {
        return (await _matchService.GetMatchById(id)).Match(Ok, this.ErrorResult);
    }

    [HttpGet("GetLog")]
    public async Task<IActionResult> GetLog([FromQuery] long matchId)
    {
        var result = await _matchService.GetLogFile(matchId);
        return result.Match(
            x => File(x.Data.OpenReadStream(), x.Data.ContentType, x.Data.FileName),
            this.ErrorResult
        ); 
    }
    
    

}