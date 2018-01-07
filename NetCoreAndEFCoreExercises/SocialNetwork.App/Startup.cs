namespace SocialNetwork.App
{
    using System;
    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using System.Collections.Generic;
    using System.Linq;
    using SocialNetwork.Models.Logic;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new SocialNetworkDbContext())
            {
                SeedData(db);

                PrintData(db);
            }
        }

        private static void PrintData(SocialNetworkDbContext db)
        {
            // PrintUsersWithTheirFriends(db);

            // PrintUsersWithMoreThanTwelveFriends(db);

            // PrintAlbumData(db);

            // PrintPicturesInMoreThanTwoAlbums(db);

            // PrintAlbumDataByUserId(db);

            // PrintAllAlbumsByGivenTag(db);

            // PrintUserWithAlbumWithMoreThanThreeTags(db);
        }

        private static void PrintUserWithAlbumWithMoreThanThreeTags(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Where(u => u.Albums.Any(a => a.Tags.Count > 20))
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums.Where(a => a.Tags.Count > 3).Select(a => new
                    {
                        a.Name,
                        Tags = a.Tags.Select(t => t.Tag.Name).ToList()
                    })
                        .OrderByDescending(a => a.Tags.Count)
                        .ThenBy(a => a.Name)
                        .ToList()
                })
                .OrderByDescending(us => us.Albums.Count)
                .ToList();

            foreach (var user in users)
            {
                var username = user.Username;
                var albums = user.Albums;

                Console.WriteLine($"{username}");

                foreach (var album in albums)
                {
                    var albumName = album.Name;
                    var tags = album.Tags;

                    Console.WriteLine($"    {albumName}:");

                    foreach (var tag in tags)
                    {
                        Console.WriteLine($"    ---- {tag}");
                    }
                }
            }
        }

        private static void PrintAllAlbumsByGivenTag(SocialNetworkDbContext db)
        {
            var testTag = "#Tag844699";

            var albums = db
                .Albums
                .Where(a => a.Tags.Any(t => t.Tag.Name == testTag))
                .OrderByDescending(a => a.Tags.Count)
                .ThenBy(a => a.Name)
                .Select(a => new
                {
                    a.Name,
                    a.User.Username
                })
                .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Name} - Owner: {album.Username}");
            }
        }

        private static void PrintAlbumDataByUserId(SocialNetworkDbContext db)
        {
            var testingUserId = 5;

            var user = db
                .Users
                .Where(u => u.Id == testingUserId)
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums.Select(a => new
                    {
                        a.Name,
                        a.IsPublic,
                        Pictures = a.Pictures.Select(p => new
                        {
                            p.Picture.Title,
                            p.Picture.Caption
                        })
                    })
                    .OrderBy(a => a.Name)
                })
                .FirstOrDefault();

            Console.WriteLine($"{user.Username}:");

            foreach (var album in user.Albums)
            {
                Console.WriteLine($"    {album.Name}");

                var pictures = album.Pictures;

                if (album.IsPublic == true)
                {
                    foreach (var picture in pictures)
                    {
                        Console.WriteLine($"    ---- {picture.Title} - {picture.Caption}");
                    }
                }
                else
                {
                    Console.WriteLine("Private content");
                }
            }
        }

        private static void PrintPicturesInMoreThanTwoAlbums(SocialNetworkDbContext db)
        {
            var pictures = db
                .Pictures
                .Where(p => p.Albums.Count > 2)
                .Select(p => new
                {
                    p.Title,
                    Albums = p.Albums.Select(a => new
                    {
                        a.Album.Name,
                        a.Album.User.Username
                    })
                    .ToList()
                })
                .OrderByDescending(p => p.Albums.Count);

            foreach (var picture in pictures)
            {
                var title = picture.Title;
                var albums = picture.Albums;

                Console.WriteLine(title);

                foreach (var album in albums)
                {
                    Console.WriteLine($"    ---- {album.Name} - {album.Username}");
                }
            }
        }

        private static void PrintAlbumData(SocialNetworkDbContext db)
        {
            var albums = db
                .Albums
                .Select(a => new
                {
                    a.Name,
                    Owner = a.User.Username,
                    Pictures = a.Pictures.Count
                })
                .OrderByDescending(a => a.Pictures)
                .ThenBy(a => a.Owner);

            foreach (var album in albums)
            {
                Console.WriteLine($"{album.Owner} album - {album.Name} has {album.Pictures} pictures.");
            }
        }

        private static void PrintUsersWithMoreThanTwelveFriends(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Where(u => (u.FromFriends.Count + u.ToFriends.Count) > 12)
                .OrderBy(u => u.RegisteredOn)
                .ThenBy(u => (u.FromFriends.Count + u.ToFriends.Count))
                .Select(u => new
                {
                    u.Username,
                    TotalFriends = u.FromFriends.Count + u.ToFriends.Count,
                    PeriodBeingUser = 100 //(DateTime.Now - ((DateTime)u.RegisteredOn)).TotalDays
                })
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} has {user.TotalFriends} friends ... {user.PeriodBeingUser} days from registration");
            }
        }

        private static void PrintUsersWithTheirFriends(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    TotalFriends = u.ToFriends.Count + u.FromFriends.Count,
                    Status = u.IsDeleted == true ? "Inactive" : "Active"
                })
                .OrderByDescending(u => u.TotalFriends)
                .ThenBy(u => u.Username)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Username} is {user.Status} and has {user.TotalFriends} friends.");
            }
        }

        private static void SeedData(SocialNetworkDbContext db)
        {
            // SeedUsers(db);

            // MakeFriendships(db);

            // SeedPictures(db);

            // SeedAlbums(db);

            // AddPicturesInAlbums(db);

            // SeedTags(db);
        }

        private static void SeedTags(SocialNetworkDbContext db)
        {
            const int totalTags = 500;

            var rnd = new Random();

            var albumIds = db
                .Albums
                .Select(a => a.Id)
                .ToList();

            for (int i = 3; i < totalTags; i++)
            {
                var tag = new Tag
                {
                    Name = TagTransformer.Transform($"Tag {rnd.Next(100000, 999999)}")
                };

                db
                    .Tags
                    .Add(tag);

                db.SaveChanges();

                var tagInAlbum = rnd.Next(5, 20);

                for (int j = 0; j < tagInAlbum; j++)
                {
                    var albumToAddId = albumIds[rnd.Next(0, albumIds.Count)];

                    if (albumToAddId == i)
                    {
                        j--;
                        continue;
                    }

                    if (db.AlbumTags.Any(at => at.TagId == i && at.AlbumId == albumToAddId))
                    {
                        j--;
                        continue;
                    }

                    db
                        .Tags
                        .Where(t => t.Id == i)
                        .FirstOrDefault()
                        .Albums
                        .Add(new AlbumTag
                        {
                            AlbumId = albumToAddId
                        });

                    db.SaveChanges();
                }
            }
        }

        private static void AddPicturesInAlbums(SocialNetworkDbContext db)
        {
            var albumIds = db
                .Albums
                .Select(a => a.Id)
                .ToList();

            var pictureIds = db
                .Pictures
                .Select(p => p.Id)
                .ToList();

            var rnd = new Random();

            var albumPictures = new List<AlbumPicture>();

            for (int i = 0; i < albumIds.Count; i++)
            {
                var albumId = albumIds[i];
                var pictureToAdd = rnd.Next(20, 100);

                for (int j = 0; j < pictureToAdd; j++)
                {
                    var pictureId = rnd.Next(0, pictureIds.Count);

                    if (albumId == pictureId)
                    {
                        j--;
                        continue;
                    }

                    if (db.AlbumPictures.Any(ap => ap.AlbumId == albumId && ap.PictureId == pictureId))
                    {
                        j--;
                        continue;
                    }

                    db
                        .Albums
                        .Where(a => a.Id == albumId)
                        .FirstOrDefault()
                        .Pictures
                        .Add(new AlbumPicture
                        {
                            PictureId = pictureId
                        });

                    db.SaveChanges();
                }
            }
        }

        private static void SeedAlbums(SocialNetworkDbContext db)
        {
            const int totalAlbums = 100;

            var userIds = db
                .Users
                .Select(u => u.Id)
                .ToList();

            var rnd = new Random();

            for (int i = 0; i < totalAlbums; i++)
            {
                db
                    .Albums
                    .Add(new Album
                    {
                        Name = "Album" + i,
                        IsPublic = true,
                        BackgroundColor = "Color",
                        UserId = userIds[rnd.Next(0, userIds.Count)]
                    });
            }

            db.SaveChanges();
        }

        private static void SeedPictures(SocialNetworkDbContext db)
        {
            const int totalPictures = 1000;

            var pics = new List<Picture>();

            for (int i = 0; i < totalPictures; i++)
            {
                var picture = new Picture
                {
                    Title = $"Title {i}",
                    Caption = $"Caption {i}",
                    Path = $"Path {i}"
                };

                pics.Add(picture);
            }

            db
                .Pictures
                .AddRange(pics);

            db.SaveChanges();
        }

        private static void MakeFriendships(SocialNetworkDbContext db)
        {
            var rnd = new Random();

            var userIds = db
                .Users
                .Select(u => u.Id)
                .ToList();


            for (int i = 0; i < userIds.Count; i++)
            {
                var userId = userIds[i];
                var friends = rnd.Next(5, 10);

                for (int j = 0; j < friends; j++)
                {
                    var isValidFriendship = true;

                    var friendIdToAdd = userIds[rnd.Next(0, userIds.Count)];

                    if (friendIdToAdd == userId)
                    {
                        isValidFriendship = false;
                        j--;
                        continue;
                    }

                    if (db
                        .Friendships
                        .Any(f => (f.FromUserId == userId && f.ToUserId == friendIdToAdd) ||
                            (f.ToUserId == userId && f.FromUserId == friendIdToAdd)))
                    {
                        isValidFriendship = false;
                        j--;
                        continue;
                    }

                    if (isValidFriendship)
                    {
                        db
                            .Users
                            .Where(u => u.Id == userId)
                            .FirstOrDefault()
                            .ToFriends
                            .Add(new Friendship
                            {
                                FromUserId = friendIdToAdd
                            });
                    }

                    db.SaveChanges();
                }
            }
        }

        private static void SeedUsers(SocialNetworkDbContext db)
        {
            const int totalUsers = 100;

            var rnd = new Random();
            var currentDate = DateTime.Now;

            var users = new List<User>();

            for (int i = 0; i < totalUsers; i++)
            {
                var user = new User
                {
                    Username = $"User {i + 1}",
                    Age = rnd.Next(1, 121),
                    Email = $"test{i}{i + 1}{i + 2}bg@gmail.com",
                    RegisteredOn = currentDate.AddDays(rnd.Next(-1000, -100)),
                    IsDeleted = i % 7 == 0 ? true : false,
                    LastTimeLoggedIn = currentDate.AddDays(rnd.Next(-100, 0)),
                    Password = "Aa1%$.IsValid"
                };

                users.Add(user);
            }

            db
                .Users
                .AddRange(users);

            db.SaveChanges();
        }
    }
}
