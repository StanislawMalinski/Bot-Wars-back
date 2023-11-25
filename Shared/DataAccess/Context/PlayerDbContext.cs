using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Context;

public class PlayerDbContext : DbContext
{
    public DbSet<Player> Players { get; set; } = null!;

    public PlayerDbContext()
    {
        
    }

    public PlayerDbContext(DbContextOptions<PlayerDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerDbContext).Assembly);
    }
}