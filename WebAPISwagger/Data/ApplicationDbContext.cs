using Microsoft.EntityFrameworkCore;
using WebAPISwagger.Data.Models;

namespace WebAPISwagger.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Book>()
                .Property(p => p.Price)
                    .HasPrecision(5, 2);

            base.OnModelCreating(builder);
        }
    }
}
