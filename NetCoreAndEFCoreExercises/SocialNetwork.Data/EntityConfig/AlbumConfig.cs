namespace SocialNetwork.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetwork.Models;

    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder
                .HasMany(a => a.Pictures)
                .WithOne(ap => ap.Album)
                .HasForeignKey(ap => ap.AlbumId);

            builder
                .HasMany(a => a.Tags)
                .WithOne(at => at.Album)
                .HasForeignKey(at => at.AlbumId);
        }
    }
}
