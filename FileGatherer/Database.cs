using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace FileGatherer
{

    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {
        }

        public DbSet<FileData> Files { get; set; }
    }
}
