using Microsoft.EntityFrameworkCore;
using SafeVault.API.Models;

namespace SafeVault.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users => Set<User>();
    }
}
