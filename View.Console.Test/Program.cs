using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Drawing.Extensions;

namespace View.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile("C:/Users/yangw/OneDrive/图片/Camera Roll/IMG_20190131_141623.jpg");
            Dictionary<PixelFormat, string> pixelFormats = new Dictionary<PixelFormat, string>()
            {
                { PixelFormat.Alpha, "像素数据没有进行过自左乘的 alpha 值。" },
                { PixelFormat.Canonical, "默认像素格式，每像素 32 位。 此格式指定 24 位颜色深度和一个 8 位 alpha 通道。" },
                { PixelFormat.DontCare, "没有指定像素格式。" },
                { PixelFormat.Extended, "保留。" },
                { PixelFormat.Format16bppArgb1555, "像素格式为每像素 16 位。 该颜色信息指定 32,768 种色调，其中 5 位为红色，5 位为绿色，5 位为蓝色，1 位为 alpha。" },
                { PixelFormat.Format16bppGrayScale, "像素格式为每像素 16 位。 该颜色信息指定 65536 种灰色调。" },
                { PixelFormat.Format16bppRgb555, "指定格式为每像素 16 位；红色、绿色和蓝色分量各使用 5 位。 剩余的 1 位未使用。" },
                { PixelFormat.Format16bppRgb565, "指定格式为每像素 16 位；红色分量使用 5 位，绿色分量使用 6 位，蓝色分量使用 5 位。" },
                { PixelFormat.Format1bppIndexed, "指定像素格式为每像素 1 位，并指定它使用索引颜色。 因此颜色表中有两种颜色。" },
                { PixelFormat.Format24bppRgb, "指定格式为每像素 24 位；红色、绿色和蓝色分量各使用 8 位。" },
                { PixelFormat.Format32bppArgb, "指定格式为每像素 32 位；alpha、红色、绿色和蓝色分量各使用 8 位。" },
                { PixelFormat.Format32bppPArgb, "指定格式为每像素 32 位；alpha、红色、绿色和蓝色分量各使用 8 位。 根据 alpha 分量，对红色、绿色和蓝色分量进行自左乘。" },
                { PixelFormat.Format32bppRgb, "指定格式为每像素 32 位；红色、绿色和蓝色分量各使用 8 位。 剩余的 8 位未使用。" },
                { PixelFormat.Format48bppRgb, "指定格式为每像素 48 位；红色、绿色和蓝色分量各使用 16 位。" },
                { PixelFormat.Format4bppIndexed, "指定格式为每像素 4 位而且已创建索引。" },
                { PixelFormat.Format64bppArgb, "指定格式为每像素 64 位；alpha、红色、绿色和蓝色分量各使用 16 位。" },
                { PixelFormat.Format64bppPArgb, "指定格式为每像素 64 位；alpha、红色、绿色和蓝色分量各使用 16 位。 根据 alpha 分量，对红色、绿色和蓝色分量进行自左乘。" },
                { PixelFormat.Format8bppIndexed, "指定格式为每像素 8 位而且已创建索引。 因此颜色表中有 256 种颜色。" },
                { PixelFormat.Gdi, "像素数据包含 GDI 颜色。" },
                { PixelFormat.Indexed, "该像素数据包含颜色索引值，这意味着这些值是系统颜色表中颜色的索引，而不是单个颜色值。" },
                { PixelFormat.Max, "此枚举的最大值。" },
                { PixelFormat.PAlpha, "像素格式包含自左乘的 alpha 值。" },
            };

            foreach (var pixelFormat in pixelFormats)
            {
                try
                {
                    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, pixelFormat.Key);
                    System.Console.WriteLine($"PixelFormat: {pixelFormat.Key}   Notes: {pixelFormat.Value}");
                    System.Console.WriteLine("Width: {0}\tHeight: {1}\tStride: {2}\tChannels: {3}", bitmapData.Width, bitmapData.Height, bitmapData.Stride, bitmapData.Stride / bitmapData.Width);
                    bitmap.UnlockBits(bitmapData);
                    System.Console.WriteLine();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"PixelFormat: {pixelFormat.Key}   Notes: {pixelFormat.Value}");
                    System.Console.WriteLine("ErrorMessage: {0}", ex.Message);
                    System.Console.WriteLine();
                }
            }

            System.Console.WriteLine();

            ImageCodecInfo.GetImageEncoders().ToList().ForEach((codecInfo) =>
            {
                System.Console.WriteLine($"CodecName:{codecInfo.CodecName}");
                System.Console.WriteLine($"DllName:{codecInfo.DllName}");
                System.Console.WriteLine($"Extension:{codecInfo.FilenameExtension}");
                System.Console.WriteLine($"FormatDescription:{codecInfo.FormatDescription}");
                System.Console.WriteLine($"Version:{codecInfo.Version}");
                System.Console.WriteLine();
                System.Console.WriteLine();
            });

            System.Console.ReadKey();
        }
    }
}
