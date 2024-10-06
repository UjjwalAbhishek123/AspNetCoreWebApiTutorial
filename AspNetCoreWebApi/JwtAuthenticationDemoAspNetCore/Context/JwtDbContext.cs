using JwtAuthenticationDemoAspNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationDemoAspNetCore.Context
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options)
        {
            
        }

        //registering tables for database
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
