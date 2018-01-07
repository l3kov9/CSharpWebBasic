namespace BankSystem.Context
{
    using BankSystem.Context.EntityConfig;
    using BankSystem.Context.ServeConfig;
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        public DbSet<SavingAccount> SavingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(Configuration.ConfigString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new UserConfig());
        }
    }
}
