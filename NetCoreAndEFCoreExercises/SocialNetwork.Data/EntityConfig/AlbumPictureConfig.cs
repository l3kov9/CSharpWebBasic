namespace SocialNetwork.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetwork.Models;

    public class AlbumPictureConfig : IEntityTypeConfiguration<AlbumPicture>
    {
        public void Configure(EntityTypeBuilder<AlbumPicture> builder)
        {
            builder
                .HasKey(ap => new
                {
                    ap.AlbumId,
                    ap.PictureId
                });
        }
    }
}
