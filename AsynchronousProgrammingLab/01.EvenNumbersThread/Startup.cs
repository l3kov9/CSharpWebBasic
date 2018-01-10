namespace _01.EvenNumbersThread
{
    using System;
    using System.Threading;

    public class Startup
    {
        public static void Main()
        {
            var min = int.Parse(Console.ReadLine());
            var max = int.Parse(Console.ReadLine());

            var thread = new Thread(() => PrintEvenNumbers(min, max));
            thread.Start();

            Console.WriteLine("Printing ...");

            Thread.Sleep(3000);

            Console.WriteLine("Still Printing ...");

            Thread.Sleep(3000);

            Console.WriteLine("Not finished yet ... still Printing ...");

            thread.Join();

            Console.WriteLine("Printing finished");
        }

        private static void PrintEvenNumbers(int min, int max)
        {
            for (int i = min; i <= max; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}
