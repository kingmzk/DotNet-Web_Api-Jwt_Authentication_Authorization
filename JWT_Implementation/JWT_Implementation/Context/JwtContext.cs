using JWT_Implementation.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT_Implementation.Context
{
    public class JwtContext : DbContext
    {


        public JwtContext(DbContextOptions<JwtContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}
