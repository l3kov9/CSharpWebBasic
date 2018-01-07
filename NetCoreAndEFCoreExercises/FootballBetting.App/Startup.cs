namespace FootballBetting.App
{
    using FootballBetting.App.SeedingData;
    using FootballBetting.Data;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new FootballBettingDbContext())
            {
                var seedingData = new SeedData(db);

                // seedingData.SeedUsers();
                // seedingData.SeedContinents();
                seedingData.SeedCountries();
            }
        }
    }
}
