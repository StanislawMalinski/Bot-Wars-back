using BotWars.GameTypeData;
using BotWars.Models;
using BotWars.RockPaperScissorsData;
using BotWars.Services;
using BotWars.Services.GameTypeService;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGameServis, GameServis>();
builder.Services.AddScoped<IRockPaperScissorsMapper, RockPaperScissorsMapper>();
builder.Services.AddScoped<IRockPaperScissorsSerivce, RockPaperScissorsService>();
builder.Services.AddScoped<IGameTypeService, GameTypeService>();
builder.Services.AddScoped<IGameTypeMapper, GameTypeMapper>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
