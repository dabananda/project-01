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
    }
}
