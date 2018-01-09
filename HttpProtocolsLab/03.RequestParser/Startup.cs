namespace _03.RequestParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var urlTokens = Console.ReadLine()
                    .Split('/')
                    .ToArray();

                if (urlTokens[0] == "END")
                {
                    break;
                }

                var path = urlTokens[1];
                var method = urlTokens[2];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add($"/method");
            }

            var requests = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var requestMethod = requests[0];
            var requestUrl = requests[1];
            var requestProtocol = requests[2];

            if (validUrls.ContainsKey(requestUrl) && validUrls[requestUrl].Contains(requestMethod.ToLower()))
            {
                Console.WriteLine($"{requestProtocol} 200 OK");
            }
            else
            {
                Console.WriteLine($"{requestProtocol} NotFound");
            }
        }
    }
}
