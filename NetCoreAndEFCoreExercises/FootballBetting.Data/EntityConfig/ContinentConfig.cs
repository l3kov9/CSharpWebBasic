namespace FootballBetting.Data.EntityConfig
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ContinentConfig : IEntityTypeConfiguration<Continent>
    {
        public void Configure(EntityTypeBuilder<Continent> builder)
        {
            builder
                .HasMany(c => c.Countries)
                .WithOne(c => c.Continent)
                .HasForeignKey(c => c.ContinentId);
        }
    }
}
