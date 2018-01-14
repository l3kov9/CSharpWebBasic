namespace MyCoolWebServer.ByTheCakeApplication.Controllers
{
    using Helpers;
    using Models;
    using Server.Http.Contracts;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesController : Controller
    {
        private static readonly List<Cake> cakes = new List<Cake>();

        public IHttpResponse Add()
            => this.FileViewResponse(@"Cakes\add", new Dictionary<string, string>
            {
                ["showResult"] = "none"
            });

        public IHttpResponse Add(string name, string price)
        {
            var cake = new Cake
            {
                Name = name,
                Price = decimal.Parse(price)
            };

            cakes.Add(cake);

            using (var streamWriter = new StreamWriter(@"ByTheCakeApplication\Data\Database.csv", true))
            {
                streamWriter.WriteLine($"{name},{price}");
            }

            return this.FileViewResponse(@"Cakes\add", new Dictionary<string, string>
            {
                ["name"] = name,
                ["price"] = price,
                ["showResult"] = "block"
            });
        }

        public IHttpResponse Search(IDictionary<string, string> urlParameters)
        {
            const string searchTermKey = "searchTerm";

            var results = string.Empty;

            if (urlParameters.ContainsKey(searchTermKey))
            {
                var searchTerm = urlParameters[searchTermKey];

                var savedCakesDivs = File
                    .ReadAllLines(@"ByTheCakeApplication\Data\Database.csv")
                    .Where(l => l.Contains(','))
                    .Select(l => l.Split(','))
                    .Select(l => new Cake
                    {
                        Name = l[0],
                        Price = decimal.Parse(l[1])
                    })
                    .Where(c => c.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Select(c => $"<div>{c.Name} - ${c.Price}</div>");

                results = string.Join(Environment.NewLine, savedCakesDivs);
            }

            return this.FileViewResponse(@"Cakes\search", new Dictionary<string, string>
            {
                ["results"] = results
            });
        }
    }
}
