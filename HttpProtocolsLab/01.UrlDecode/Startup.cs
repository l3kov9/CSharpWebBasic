namespace _01.UrlDecode
{
    using System;
    using System.Net;

    public class Startup
    {
        public static void Main()
        {
            var url = Console.ReadLine();

            var decodedUrl = WebUtility.UrlDecode(url);

            Console.WriteLine(decodedUrl);
        }
    }
}
