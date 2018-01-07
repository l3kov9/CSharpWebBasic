namespace FootballBetting.Data
{
    using FootballBetting.Data.EntityConfig;
    using FootballBetting.Data.ServeConfig;
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class FootballBettingDbContext : DbContext
    {
        public DbSet<Bet> Bets { get; set; }

        public DbSet<BetGame> BetGames { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<Continent> Continent { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<ResultPrediction> ResultPredictions { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(Configuration.ConfigString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfig());

            builder
                .ApplyConfiguration(new PlayerStatisticConfig());

            builder
                .ApplyConfiguration(new BetGameConfig());

            builder
                .ApplyConfiguration(new TeamConfig());

            builder
                .ApplyConfiguration(new TownConfig());

            builder
                .ApplyConfiguration(new ContinentConfig());

            builder
                .ApplyConfiguration(new GameConfig());
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(
                    entity,
                    validationContext,
                    validateAllProperties: true);
            }

            return base.SaveChanges();
        }
    }
}
