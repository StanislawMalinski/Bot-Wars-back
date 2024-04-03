using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shared.DataAccess.Context;

namespace Communication.APIs.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class DataBaseController : Controller
{
    private readonly DataContext _dataContext;
    private readonly IHostEnvironment _env;

    public DataBaseController(DataContext dataContext, IHostEnvironment env)
    {
        _dataContext = dataContext;
        _env = env;
    }
    
    [HttpGet("reset")]
    public async Task<IActionResult> reset()
    {
        if (!_env.IsDevelopment())
        {
            return NotFound();
        }
        
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
        if (!_env.IsDevelopment())
        {
            return NotFound();
        }
        await _dataContext.Database.EnsureDeletedAsync();
        
        return Ok();
        

        
    }
}