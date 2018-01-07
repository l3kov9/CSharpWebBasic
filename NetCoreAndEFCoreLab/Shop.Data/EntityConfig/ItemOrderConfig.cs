namespace Shop.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Shop.Models;

    public class ItemOrderConfig : IEntityTypeConfiguration<ItemOrder>
    {
        public void Configure(EntityTypeBuilder<ItemOrder> builder)
        {
            builder
               .HasKey(io => new
               {
                   io.ItemId,
                   io.OrderId
               });
        }
    }
}
