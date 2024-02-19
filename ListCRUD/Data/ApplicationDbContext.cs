using ListCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ListCRUD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<PersonalInfo> PersonalInfo { get; set; }
    }
}
