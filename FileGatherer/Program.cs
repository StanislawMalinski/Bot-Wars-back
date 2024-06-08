using FileGatherer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<GathererService>();
builder.Services.AddScoped<Seeder>();

// Add DbContext with SQLite integration
builder.Services.AddDbContext<Database>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors(options =>
{
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

// init database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<Database>();
    if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
}

await app.Services.GetRequiredService<Seeder>().Seed();

app.Run();