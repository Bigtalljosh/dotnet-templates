using Microsoft.EntityFrameworkCore;
using MyProject.Business.Models;

namespace MyProject.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<House> Houses { get; set; }

        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
