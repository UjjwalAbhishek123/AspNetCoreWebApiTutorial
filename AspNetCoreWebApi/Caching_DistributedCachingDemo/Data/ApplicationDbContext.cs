using Caching_DistributedCachingDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace Caching_DistributedCachingDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
