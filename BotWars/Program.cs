using Communication.Services.Tournament;
using Communication.Services.Validation;
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
builder.Services.AddScoped<PlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IGameTypeMapper, GameTypeMapper>();
builder.Services.AddScoped<IPlayerValidator, MockValidator>();

// Tournament related services
builder.Services.AddScoped<TournamentAdminService, TournamentAdminService>();
builder.Services.AddScoped<TournamentIdentifiedPlayerService, TournamentIdentifiedPlayerService>();
builder.Services.AddScoped<TournamentUnidentifiedPlayerService, TournamentUnidentifiedPlayerService>();
builder.Services.AddScoped<TournamentBannedPlayerService, TournamentBannedPlayerService>();
builder.Services.AddScoped<TournamentBadValidation, TournamentBadValidation>();
builder.Services.AddScoped<TournamentService, TournamentService>();
builder.Services.AddScoped<TournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentMapper, TournamentMapper>();
builder.Services.AddScoped<TournamentServiceProvider, TournamentServiceProvider>();

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
