using Microsoft.EntityFrameworkCore;
using Utrans_API.Models;

namespace Utrans_API.DBContexts
{
    public partial class BrandContext : DbContext
    {

        public BrandContext()
        {

        }

        public BrandContext(DbContextOptions<BrandContext> options) : base(options)
        {
        }
        public virtual DbSet<Brands> Brands { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
                var connectionString = "server=localhost;user=root;password=;database=utrans";
                optionsBuilder.UseMySql(connectionString, serverVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brands>(entity =>
            {
                entity.ToTable("brands").HasKey(k => k.id);

                //entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Number).IsRequired();

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Website).IsRequired();
            });
        }
    }
}
