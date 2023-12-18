using Microsoft.EntityFrameworkCore;
using Utrans_API.Models;

namespace Utrans_API.DBContexts
{
    public partial class VendorContext : DbContext
    {
        public VendorContext()
        {

        }

        public VendorContext(DbContextOptions<VendorContext> options) : base(options)
        {

        }

        public virtual DbSet<Vendors> Vendors { get; set; }

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
            modelBuilder.Entity<Vendors>(entity =>
            {
                entity.ToTable("vendors").HasKey(k => k.id);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.District).IsRequired();

                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.Property(e => e.Email).IsRequired();
            });
        }
    }
}
