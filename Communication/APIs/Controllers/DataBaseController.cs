using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;

namespace Communication.APIs.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class DataBaseController : Controller
{
    private readonly DataContext _dataContext;
    public DataBaseController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    [HttpGet("reset")]
    public async Task<IActionResult> reset()
    {
        var pendingMigrations = await _dataContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            Console.WriteLine($"Applying {pendingMigrations.Count()} pending migrations.");
            await _dataContext.Database.MigrateAsync();
            return Ok();
        }
    
        Console.WriteLine("No pending migrations.");
        return NotFound();
        

        
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> delete()
    {

        await _dataContext.Database.EnsureDeletedAsync();
        
        return Ok();
        

        
    }
}