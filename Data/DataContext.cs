using Microsoft.EntityFrameworkCore;
using Shortener_Link.Models;

namespace Shortener_Link.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
