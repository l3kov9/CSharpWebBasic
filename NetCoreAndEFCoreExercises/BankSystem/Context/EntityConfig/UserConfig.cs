namespace BankSystem.Context.EntityConfig
{
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.SavingAccounts)
                .WithOne(sa => sa.User)
                .HasForeignKey(sa => sa.UserId);

            builder
                .HasMany(u => u.CheckingAccounts)
                .WithOne(ca => ca.User)
                .HasForeignKey(ca => ca.UserId);
        }
    }
}
