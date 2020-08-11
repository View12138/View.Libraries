using System.Diagnostics;
using System.Drawing;
using View.Drawing.Extensions;
using View.Drawing.Extensions.Models;

namespace View.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile("C:/Users/yangw/OneDrive/图片/Camera Roll/IMG_20190131_141623.jpg");
            Image image = Image.FromFile("C:/Users/yangw/OneDrive/图片/Camera Roll/IMG_20190131_141623.jpg");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            image.InverseColor().Save("123.jpg");
            stopwatch.Stop();
            System.Console.WriteLine(stopwatch.Elapsed.Milliseconds + " ms");


            System.Console.ReadKey();
        }
    }
}
