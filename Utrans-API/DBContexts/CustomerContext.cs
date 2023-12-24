using Microsoft.EntityFrameworkCore;
using Utrans_API.Models;

namespace Utrans_API.DBContexts
{
    public partial class CustomerContext : DbContext
    {
        public CustomerContext()
        {

        }

        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }
        public virtual DbSet<Customers> Customers { get; set; }
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
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("customers").HasKey(k => k.id);

                entity.Property(e => e.Code).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Address);

                entity.Property(e => e.District);

                entity.Property(e => e.City);

                entity.Property(e => e.Phone);

                entity.Property(e => e.Email);
            });
        }
    }
}
