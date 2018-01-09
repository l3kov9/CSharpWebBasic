namespace _02.ValidateUrl
{
    using System;
    using System.Net;

    public class Startup
    {
        public static void Main()
        {
            var url = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(url);

            var urlParts = new Uri(decodedUrl);
            var isValidUrl = true;

            var protocol = urlParts.Scheme;
            var host = urlParts.Host;
            var port = urlParts.Port;
            var path = urlParts.AbsolutePath;
            var queryString = urlParts.Query;
            var fragment = urlParts.Fragment;

            if (protocol == null || host == null || port == 0 || path == "/")
            {
                isValidUrl = false;
            }

            if (isValidUrl)
            {
                Console.WriteLine($"Protocol: {protocol}");
                Console.WriteLine($"Host: {host}");
                Console.WriteLine($"Port: {port}");
                Console.WriteLine($"Path: {path}");
                Console.WriteLine($"Query: {queryString}");
                Console.WriteLine($"Fragment: {fragment}");
            }
            else
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
