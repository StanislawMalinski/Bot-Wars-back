using Communication.APIs.Controllers.Helper;
using Communication.Services.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.APIs.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AdministrativeController : Controller
{
    private readonly IAdministrativeService _administrativeService;

    public AdministrativeController(IAdministrativeService administrativeService)
    {
        _administrativeService = administrativeService;
    }

    [HttpPut("banPlayer")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BanPlayer([FromQuery] long playerId)
    {
        return (await _administrativeService.BanPlayer(playerId)).Match(Ok,this.ErrorResult);
    }
    
    [HttpPut("unbanPlayer")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnbanPlayer([FromQuery] long playerId)
    {
        return (await _administrativeService.UnbanPlayer(playerId)).Match(Ok,this.ErrorResult);
    }
}