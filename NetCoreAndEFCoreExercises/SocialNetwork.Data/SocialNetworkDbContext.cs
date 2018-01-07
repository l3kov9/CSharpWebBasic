namespace SocialNetwork.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using SocialNetwork.Data.EntityConfig;
    using SocialNetwork.Data.ServeConfig;
    using SocialNetwork.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class SocialNetworkDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<AlbumPicture> AlbumPictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<AlbumTag> AlbumTags { get; set; }



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
                .ApplyConfiguration(new FriendshipConfig());

            builder
                .ApplyConfiguration(new AlbumConfig());

            builder
                .ApplyConfiguration(new PictureConfig());

            builder
                .ApplyConfiguration(new AlbumPictureConfig());

            builder
                .ApplyConfiguration(new TagConfig());

            builder
                .ApplyConfiguration(new AlbumTagConfig());
            
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var serviceProvider = this.GetService<IServiceProvider>();
            var items = new Dictionary<object, object>();

            foreach (var entry in this.ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    foreach (var result in results)
                    {
                        if (result != ValidationResult.Success)
                        {
                            throw new ValidationException(result.ErrorMessage);
                        }
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
