using Coravel;
using Engine.BusinessLogic.BackgroundWorkers;
using Engine.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories()
    .AddMappers()
    .AddBackGroundTask();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

app.Services.UseScheduler(async x => x.Schedule<InicjalizeWorkers>()
  .EverySecond().Once().PreventOverlapping("Initializer"));
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();