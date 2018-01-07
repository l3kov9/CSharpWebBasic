namespace FootballBetting.Data.EntityConfig
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BetGameConfig : IEntityTypeConfiguration<BetGame>
    {
        public void Configure(EntityTypeBuilder<BetGame> builder)
        {
            builder
                .HasKey(bg => new
                {
                    bg.BetId,
                    bg.GameId
                });

            builder
                .HasOne(bg => bg.ResultPrediction)
                .WithMany(rp => rp.BetGames)
                .HasForeignKey(bg => bg.ResultPredictionId);
        }
    }
}
