using Communication.APIs.Controllers.Helper;
using Communication.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace Communication.APIs.Controllers;

[Route("api/[controller]")]
public class AdministrativeController : Controller
{
    private readonly AdministrativeService _administrativeService;

    public AdministrativeController(AdministrativeService administrativeService)
    {
        _administrativeService = administrativeService;
    }

    [HttpPut("ban")]
    public async Task<IActionResult> BanPlayer([FromQuery] long playerId)
    {
        return (await _administrativeService.BanPlayer(playerId)).Match(Ok,this.ErrorResult);
    }
    
    [HttpPut("unban")]
    public async Task<IActionResult> UnbanPlayer([FromQuery] long playerId)
    {
        return (await _administrativeService.UnbanPlayer(playerId)).Match(Ok,this.ErrorResult);
    }
}