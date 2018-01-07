namespace FootballBetting.Data.EntityConfig
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
                .HasMany(g => g.Bets)
                .WithOne(b => b.Game)
                .HasForeignKey(b => b.GameId);

            builder
                .HasMany(g => g.PlayerStatistics)
                .WithOne(ps => ps.Game)
                .HasForeignKey(ps => ps.GameId);

            builder
                .HasOne(g => g.Round)
                .WithMany(r => r.Games)
                .HasForeignKey(g => g.RoundId);

            builder
                .HasOne(g => g.Competition)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CompetitionId);
        }
    }
}
