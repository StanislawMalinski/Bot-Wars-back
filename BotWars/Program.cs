using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Shared.DataAccess.Context;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services;

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
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<TournamentRepository, TournamentRepository>();
builder.Services.AddScoped<PlayerRepository, PlayerRepository>();
builder.Services.AddScoped<ITournamentMapper, TournamentMapper>();
builder.Services.AddScoped<IGameTypeMapper, GameTypeMapper>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
