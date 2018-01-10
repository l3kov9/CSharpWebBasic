namespace SpliceImages
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            var imgDirectory = Directory.GetCurrentDirectory() + @"\images";

            var images = new DirectoryInfo(imgDirectory).GetFiles();

            const string resultDir = @"images\Rotated images";

            if (Directory.Exists(resultDir))
            {
                Directory.Delete(resultDir, true);
            }

            Directory.CreateDirectory(resultDir);

            var tasks = new List<Task>();

            foreach (var img in images)
            {
                var task = Task.Run(() =>
                {
                    var image = Image.FromFile(img.FullName);
                    image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    image.Save($@"{resultDir}\{img.Name}");

                    Console.WriteLine($"{img.Name} processed..");
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Rotating finished");
        }
    }
}
