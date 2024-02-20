using Engine.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRepositories()
    .AddMappers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<TaskDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultTaskConnection"));
});
var app = builder.Build();

//app.Services.UseScheduler(async x => x.Schedule<InicjalizeWorkers>()
//  .EverySecond().Once().PreventOverlapping("Initializer"));

app.Run();