namespace SocialNetwork.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetwork.Models;

    public class PictureConfig : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder
                .HasMany(p => p.Albums)
                .WithOne(ap => ap.Picture)
                .HasForeignKey(ap => ap.PictureId);
        }
    }
}
