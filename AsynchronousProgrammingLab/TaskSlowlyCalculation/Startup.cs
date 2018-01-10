using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSlowlyCalculation
{
    public class Startup
    {
        private static string result;

        public static void Main()
        {
            Console.WriteLine("Calculating ...");

            Task.Run(() =>
            {
                CalculateSlowly();
            });

            Console.WriteLine("Enter command: ");

            while (true)
            {
                var command = Console.ReadLine();

                if(command == "show")
                {
                    if(result == null)
                    {
                        Console.WriteLine("Still calculating ... Please wait!");
                    }
                    else
                    {
                        Console.WriteLine($"Result is {result}");
                    }
                }

                if(command == "exit")
                {
                    break;
                }
            }
        }

        private static void CalculateSlowly()
        {
            Thread.Sleep(10000);

            result = "9";
        }
    }
}
