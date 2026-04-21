using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<PersonData> PersonDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonData>()
                .Property(x => x.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<PersonData>()
                .Property(x => x.MaritalStatus)
                .HasConversion<string>();
        }
    }
}
