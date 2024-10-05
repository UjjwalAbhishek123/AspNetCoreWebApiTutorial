using Logging_InBuiltLoggingDemoAspNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Logging_InBuiltLoggingDemoAspNetCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
