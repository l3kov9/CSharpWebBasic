namespace Shop.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Shop.Models;

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
               .HasMany(o => o.Items)
               .WithOne(io => io.Order)
               .HasForeignKey(io => io.OrderId);
        }
    }
}
