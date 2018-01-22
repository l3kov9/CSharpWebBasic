namespace MyCoolWebServer.GameStoreApplication.Data
{
    using Microsoft.EntityFrameworkCore;

    public class GameStoreDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer("Server=.;Database=GameStoreDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
