namespace BankSystem
{
    using BankSystem.Context;
    using BankSystem.Models;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new BankSystemDbContext())
            {
                while (true)
                {
                    var command = Console.ReadLine();

                    if (command == "exit")
                    {
                        break;
                    }

                    var parts = command
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    var action = parts[0];

                    if (action == "Register")
                    {
                        RegisterUser(db, parts);
                    }

                    if (action == "Login")
                    {
                        LoginUser(db, parts);
                    }
                }
            }
        }

        private static void LoginUser(BankSystemDbContext db, string[] parts)
        {
            var username = parts[1];
            var password = parts[2];

            if(db.Users.Any(u=>u.Username == username && u.Password == password))
            {
                Console.WriteLine($"{username} successfully logged in");
            }
            else
            {
                Console.WriteLine($"Incorrect username/password");
            }
        }

        private static void RegisterUser(BankSystemDbContext db, string[] parts)
        {
            var username = parts[1];
            var password = parts[2];
            var email = parts[3];

            try
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email
                };

                db
                    .Users
                    .Add(user);

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
