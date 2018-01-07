namespace SocialNetwork.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SocialNetwork.Models;

    public class AlbumTagConfig : IEntityTypeConfiguration<AlbumTag>
    {
        public void Configure(EntityTypeBuilder<AlbumTag> builder)
        {
            builder
                .HasKey(at => new
                {
                    at.AlbumId,
                    at.TagId
                });
        }
    }
}
