using BotWars.DependencyInjection;
using Communication.ServiceInterfaces;
using Communication.Services.Administration;
using Communication.Services.GameType;
using Communication.Services.Player;
using Communication.Services.Tournament;
using Communication.Services.Validation;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Shared.DataAccess.Context;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

//Logger
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlayerValidator, MockValidator>();

builder.Services
    .AddGameType()
    .AddPlayer()
    .AddTournament()
    .AddAdministrative();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// apply migrations to initialize database
using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
        Console.WriteLine($"Applying {pendingMigrations.Count()} pending migrations.");
        await dbContext.Database.MigrateAsync();
    }
    else
    {
        Console.WriteLine("No pending migrations.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
