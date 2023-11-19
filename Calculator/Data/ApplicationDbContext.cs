using Calculator.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Calculation> Calculations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
           
        }

     
    }
}
