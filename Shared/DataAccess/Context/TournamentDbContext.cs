using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Context;

public class TournamentDbContext : DbContext
{
    public DbSet<Tournament> Tournaments { get; set; } = null!;

    public TournamentDbContext()
    {
        
    }

    public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentDbContext).Assembly);
    }
}