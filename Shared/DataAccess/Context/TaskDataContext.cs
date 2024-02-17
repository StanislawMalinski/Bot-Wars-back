using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.EntitiesConfigurations;

namespace Shared.DataAccess.Context;

public class TaskDataContext : DbContext
{
    public TaskDataContext(DbContextOptions<TaskDataContext> options) : base(options)
    {
        
    }
    public DbSet<_Task> Tasks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
}