using Denoy_INFASS2.Models;
using Microsoft.EntityFrameworkCore;

namespace Denoy_INFASS2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
    }
}