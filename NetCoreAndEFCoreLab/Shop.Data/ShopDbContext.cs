namespace Shop.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shop.Data.EntityConfig;
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
                .ApplyConfiguration(new CustomerConfig());

            builder
                .ApplyConfiguration(new ItemConfig());

            builder
                 .ApplyConfiguration(new ItemOrderConfig());

            builder
                .ApplyConfiguration(new OrderConfig());
        }
    }
}
