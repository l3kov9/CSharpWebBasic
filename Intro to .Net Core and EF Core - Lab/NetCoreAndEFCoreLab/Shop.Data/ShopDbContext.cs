namespace Shop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shop.Data.ServeConfig;
    using Shop.Models;

    public class ShopDbContext : DbContext
    {
        public DbSet<Salesman> Salesmen { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(Configuration.ConfigString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SalesmanId);

            builder
                .Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            builder
                .Entity<Customer>()
                .HasMany(c => c.Revies)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId);

            builder
                .Entity<ItemOrder>()
                .HasKey(io => new
                {
                    io.ItemId,
                    io.OrderId
                });

            builder
                .Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(io => io.Item)
                .HasForeignKey(io => io.ItemId);

            builder
                .Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(io => io.Order)
                .HasForeignKey(io => io.OrderId);

            builder
                .Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Item)
                .HasForeignKey(r => r.ItemId);
        }
    }
}
