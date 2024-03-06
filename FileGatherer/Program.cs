using FileGatherer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<GathererService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options => {
    options.AllowAnyMethod();
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
