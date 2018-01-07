namespace FootballBetting.App.SeedingData
{
    using FootballBetting.Data;
    using FootballBetting.Models;
    using System;
    using System.Collections.Generic;

    public class SeedData
    {
        private FootballBettingDbContext db;

        private const int totalUsers = 100;
        private const int totalContinents = 10;

        private Random rnd = new Random();

        private List<User> users = new List<User>();
        private List<Continent> continents = new List<Continent>();
        private List<Country> countries = new List<Country>();

        public SeedData(FootballBettingDbContext db)
        {
            this.db = db;
        }

        public void SeedUsers()
        {
            for (int i = 0; i < totalUsers; i++)
            {
                var user = new User
                {
                    Username = $"Username {i}",
                    Email = $"email{i}{i + 1}{i + 2}@gmail.com",
                    FullName = $"Full Name {rnd.Next(1, 999999)}",
                    Password = $"Pasw0rd{i}"
                };

                users.Add(user);
            }

            db
                .Users
                .AddRange(users);

            db.SaveChanges();
        }

        public void SeedContinents()
        {
            for (int i = 0; i < totalContinents; i++)
            {
                var continent = new Continent
                {
                    Name = $"Continent {i}"
                };

                continents.Add(continent);
            }

            db
                .Continent
                .AddRange(continents);

            db.SaveChanges();
        }

        public void SeedCountries()
        {
            for (int i = 0; i < totalContinents; i++)
            {
                var countriesInContinent = rnd.Next(1, 20);

                for (int j = 0; j < countriesInContinent; j++)
                {
                    var country = new Country
                    {
                        Id = $"{rnd.Next(100,999)}",
                        Name = $"Country {rnd.Next(0,200000)}",
                        ContinentId = i
                    };

                    countries.Add(country);
                }
            }

            db
                .Countries
                .AddRange(countries);

            db.SaveChanges();
        }
    }
}
