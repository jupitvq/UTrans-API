using Microsoft.EntityFrameworkCore;
using Utrans_API.Models;

namespace Utrans_API.DBContexts
{
    public partial class ProductContext : DbContext
    {
        public ProductContext()
        {

        }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }

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
            modelBuilder.Entity<Products>(entity =>
            {
                entity.ToTable("products").HasKey(k => k.id);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.stock).IsRequired();

                entity.Property(e => e.sales_price).IsRequired();

                entity.Property(e => e.standard_price).IsRequired();
            });
        }
    }
}
