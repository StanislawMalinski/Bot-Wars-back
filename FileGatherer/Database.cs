using Microsoft.EntityFrameworkCore;

namespace FileGatherer;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options) : base(options)
    {
    }

    public DbSet<FileData> Files { get; set; }
}