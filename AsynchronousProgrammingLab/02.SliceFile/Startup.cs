namespace _02.SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            var videoPath = Console.ReadLine();
            var destination = Console.ReadLine();
            var pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destination, pieces);

            Console.WriteLine("Anything else?");

            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                var fileInfo = new FileInfo(sourceFile);

                long partLength = (source.Length / parts) + 1;
                long currentByte = 0;

                for (int currentPart = 1; currentPart < parts; currentPart++)
                {
                    string filePath = $"{destinationPath}/Part-{currentPart}{fileInfo.Extension}";

                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        var buffer = new byte[partLength];

                        while (currentByte <= partLength * currentPart)
                        {
                            var readByteCount = source.Read(buffer, 0, buffer.Length);

                            if (readByteCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readByteCount);
                            currentByte += readByteCount;
                        }
                    }

                    Console.WriteLine("Slice completed.");
                }
            }
        }

        private static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }
    }
}
