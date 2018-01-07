namespace Shop.App
{
    using System;
    using Shop.Data;
    using Shop.Models;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var db = new ShopDbContext();

            using (db)
            {
                // CreateDatabase(db);

                // SeedSalesmen(db);
                 
                // SeedItems(db);
                
                // ReadCommands(db);

                // PrintSalesmenWithCustomerCount(db);

                // PrintCustomersWithTheirReviewsAndOrders(db);

                // PrintCustomerInfoById(db);

                // PrintCustomerDataById(db);

                // PrintCustomersOrderWithMoreThanOneItem(db);
            }
        }

        private static void PrintSalesmenWithCustomerCount(ShopDbContext db)
        {
            var salesmen = db
                .Salesmen
                .Select(s => new
                {
                    s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var salesman in salesmen)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.Customers} customers.");
            }
        }

        private static void PrintCustomersOrderWithMoreThanOneItem(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            db
                .Customers
                .Where(c => c.Id == customerId)
                .Where(c => c.Orders.Count > 1)
                .ToList()
                .ForEach(c => Console.WriteLine($"Orders Count: {c.Orders.Count}"));
        }

        private static void PrintCustomerDataById(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            db
                .Customers
                .Where(c => c.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Revies.Count,
                    Salesman = c.Salesman.Name
                })
                .ToList()
                .ForEach(c => Console.WriteLine($"{c.Name}, orders-{c.Orders}, reviews-{c.Reviews} - {c.Salesman}"));
        }

        private static void PrintCustomerInfoById(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());

            var orderData = db
                .Orders
                .Where(i => i.CustomerId == customerId)
                .Select(o => new
                {
                    o.Id,
                    Items = o.Items.Count
                })
                .OrderBy(o => o.Id)
                .ToList();

            foreach (var order in orderData)
            {
                Console.WriteLine($"order {order.Id}: {order.Items} items");
            }
        }

        private static void SeedItems(ShopDbContext db)
        {
            while (true)
            {
                var command = Console.ReadLine();

                if (command == "END")
                {
                    break;
                }

                var parts = command.Split(';');

                var itemName = parts[0];
                var itemPrice = decimal.Parse(parts[1]);

                db
                    .Items
                    .Add(new Item
                    {
                        Name = itemName,
                        Price = itemPrice
                    });
            }

            db.SaveChanges();
        }

        private static void PrintCustomersWithTheirReviewsAndOrders(ShopDbContext db)
        {
            var customers = db
                .Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Revies.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews)
                .ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.Name);
                Console.WriteLine($"Orders: {customer.Orders}");
                Console.WriteLine($"Reviews: {customer.Reviews}");
            }
        }
        
        private static void ReadCommands(ShopDbContext db)
        {
            while (true)
            {
                var parts = Console.ReadLine();

                if (parts == "END")
                {
                    break;
                }

                var commands = parts
                    .Split('-');

                var command = commands[0];

                var arguments = commands[1];

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, arguments);
                        break;
                    case "order":
                        AddOrder(db, arguments);
                        break;
                    case "review":
                        AddReview(db, arguments);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void AddReview(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');

            var customerId = int.Parse(parts[0]);
            var itemId = int.Parse(parts[1]);

            db
                .Reviews
                .Add(new Review
                {
                    CustomerId = customerId,
                    ItemId = itemId
                });

            db.SaveChanges();
        }

        private static void AddOrder(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(';');

            var customerId = int.Parse(parts[0]);

            var order = new Order
            {
                CustomerId = customerId
            };

            for (int i = 1; i < parts.Length; i++)
            {
                var itemId = int.Parse(parts[i]);

                order
                    .Items
                    .Add(new ItemOrder
                    {
                        ItemId = itemId
                    });
            }

            db
                .Orders
                .Add(order);

            db.SaveChanges();
        }

        private static void RegisterCustomer(ShopDbContext db, string arguments)
        {
            var parts = arguments
                .Split(';');

            var customerName = parts[0];
            var salesmanId = int.Parse(parts[1]);

            db
                .Customers
                .Add(new Customer
                {
                    Name = customerName,
                    SalesmanId = salesmanId
                });

            db.SaveChanges();
        }

        private static void SeedSalesmen(ShopDbContext db)
        {
            var names = Console.ReadLine()
                .Split(';');

            foreach (var name in names)
            {
                db
                    .Salesmen
                    .Add(new Salesman
                    {
                        Name = name,
                    });
            }

            db.SaveChanges();
        }

        private static void CreateDatabase(ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
