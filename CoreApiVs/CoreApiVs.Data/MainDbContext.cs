using Microsoft.EntityFrameworkCore;
using CoreApiVs.Data.Entities;
namespace CoreApiVs.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
    }
}