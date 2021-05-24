using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using View.Drawing.Extensions.Models;
using View.Drawing.Extensions.Configurations;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// <see cref="Image"/> 扩展。
    /// </summary>
    public static class ImageExtension
    {
        private static readonly GraphicsConfiguration defaultGraphicsConfiguration = new GraphicsConfiguration();
        private static readonly ChangeSizeConfiguration defaultChangeSizeConfiguration = new ChangeSizeConfiguration();
        private static readonly ChangeQualityConfiguration defaultChangeQualityConfiguration = new ChangeQualityConfiguration();

        /// <summary>
        /// 将原图缩放到新的尺寸中。
        /// <para>按照指定的缩放模式(<see cref="ChangeSizeConfiguration.SizeMode"/>) 进行缩放</para>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size">限定的大小</param>
        /// <param name="changeSizeConfiguration">调整大小配置配置</param>
        /// <param name="graphicsConfiguration">绘图配置</param>
        /// <returns></returns>
        public static Image ChangeSize(this Image image, Size size, ChangeSizeConfiguration changeSizeConfiguration = null, GraphicsConfiguration graphicsConfiguration = null)
        {
            if (image is null)
            { throw new ArgumentNullException(nameof(image)); }

            if (changeSizeConfiguration == null)
            { changeSizeConfiguration = defaultChangeSizeConfiguration; }

            if (graphicsConfiguration == null)
            { graphicsConfiguration = defaultGraphicsConfiguration; }

            Size newSize = ImageHandle.SizeToSize(image.Size, size, changeSizeConfiguration.SizeMode);
            Bitmap destBitmap;
            if (ImageHandle.IsErrorPixelFormat(image.PixelFormat))
            {
                destBitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(destBitmap))
                { g.DrawImage(image, 0, 0); }
            }
            else
            { destBitmap = new Bitmap(newSize.Width, newSize.Height, image.PixelFormat); }

            using (Graphics g = Graphics.FromImage(destBitmap))
            {
                g.CompositingQuality = graphicsConfiguration.CompositingQuality;
                g.SmoothingMode = graphicsConfiguration.SmoothingMode;
                g.InterpolationMode = graphicsConfiguration.InterpolationMode;
                g.CompositingMode = graphicsConfiguration.CompositingMode;
                g.PixelOffsetMode = graphicsConfiguration.PixelOffsetMode;
                g.TextRenderingHint = graphicsConfiguration.TextRenderingHint;

                g.Clear(Color.Transparent);
                if (changeSizeConfiguration.SizeMode == SizeMode.ToMax || changeSizeConfiguration.SizeMode == SizeMode.ToMin || changeSizeConfiguration.SizeMode == SizeMode.Stretch)
                { g.DrawImage(image, new Rectangle(new Point(0, 0), newSize), new Rectangle(new Point(0, 0), image.Size), GraphicsUnit.Pixel); }
                if (changeSizeConfiguration.SizeMode == SizeMode.Cut)
                {
                    Size cutSize = ImageHandle.SizeToSize(newSize, image.Size, SizeMode.ToMax);
                    int x = changeSizeConfiguration.Location.X;
                    int y = changeSizeConfiguration.Location.Y;
                    if (changeSizeConfiguration.ClipInBox)
                    {
                        if (x + cutSize.Width > image.Width) { x = image.Width - cutSize.Width; }
                        if (y + cutSize.Height > image.Height) { y = image.Height - cutSize.Height; }
                    }
                    g.DrawImage(image, new Rectangle(new Point(0, 0), newSize), new Rectangle(new Point(x, y), cutSize), GraphicsUnit.Pixel);
                }
            }

            return destBitmap;
        }
        /// <summary>
        /// 使用指定的编码器将图片的质量尽可能的压缩到指定的大小。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="length">指定的文件大小 ( 单位: <see langword="KB"/> )</param>
        /// <param name="changeQualityConfiguration">配置</param>
        /// <returns></returns>
        public static async Task<Stream> ChangeQualityAsync(this Image image, int length, ChangeQualityConfiguration changeQualityConfiguration = null)
        {
            if (changeQualityConfiguration == null)
            { changeQualityConfiguration = defaultChangeQualityConfiguration; }

            return await Task.Run(() =>
            {
                int quality = 100;
                length *= 1024;
                do
                {
                    Stream stream = new MemoryStream();
                    image.Save(stream, changeQualityConfiguration.ImageEncodecInfo, ImageHandle.GetQualityParam(quality));
                    quality -= changeQualityConfiguration.Interval;
                    if (quality < changeQualityConfiguration.MinQuality || stream.Length < length) // 无法继续降低质量了
                    {
                        stream.Position = 0;
                        return stream;
                    }
                    else
                    { stream.Dispose(); }

                } while (true);
            });
        }

        /// <summary>
        /// 修改原图的像素格式。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pixelFormat"></param>
        /// <param name="graphicsConfiguration"></param>
        /// <returns></returns>
        public static Image ChangePixelFormat(this Image image, PixelFormat pixelFormat, GraphicsConfiguration graphicsConfiguration = null)
        {
            if (ImageHandle.IsErrorPixelFormat(pixelFormat))
            { throw new ArgumentException($"{nameof(pixelFormat)} 不是有效的值。"); }

            if (graphicsConfiguration == null)
            { graphicsConfiguration = defaultGraphicsConfiguration; }

            if (image.PixelFormat == pixelFormat)
            {
                return (Image)image.Clone();
            }
            else
            {
                Bitmap bitmap = new Bitmap(image.Width, image.Height, pixelFormat);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CompositingQuality = graphicsConfiguration.CompositingQuality;
                    g.SmoothingMode = graphicsConfiguration.SmoothingMode;
                    g.InterpolationMode = graphicsConfiguration.InterpolationMode;
                    g.CompositingMode = graphicsConfiguration.CompositingMode;
                    g.PixelOffsetMode = graphicsConfiguration.PixelOffsetMode;
                    g.TextRenderingHint = graphicsConfiguration.TextRenderingHint;
                    g.DrawImage(image, 0, 0, image.Width, image.Height);
                }
                return bitmap;
            }
        }

        /// <summary>
        /// 将图片的指定颜色替换为另一个颜色
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="oldColor">待替换的颜色</param>
        /// <param name="newColor">替换后的颜色</param>
        /// <param name="radius">待替换颜色的偏移范围</param>
        /// <returns></returns>
        public static Image ReplaceColor(this Image image, Color oldColor, Color newColor, int radius)
        {
            byte[] colorBuffer; // 图片的颜色信息  
            Bitmap bitmap = new Bitmap(image);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            colorBuffer = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, colorBuffer, 0, colorBuffer.Length);

            int channels = bitmapData.Stride / bitmapData.Width;
            if (channels != 3 && channels != 4)
            { throw new ArgumentException("不支持这种像素格式的图片"); }
            if (channels == 4)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {

                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int a = colorBuffer[y * bitmapData.Stride + x * channels + 3];
                        int r = colorBuffer[y * bitmapData.Stride + x * channels + 2];
                        int g = colorBuffer[y * bitmapData.Stride + x * channels + 1];
                        int b = colorBuffer[y * bitmapData.Stride + x * channels];
                        if (
                            a >= oldColor.A - radius && a <= oldColor.A + radius &&
                            r >= oldColor.R - radius && r <= oldColor.R + radius &&
                            g >= oldColor.G - radius && g <= oldColor.G + radius &&
                            b >= oldColor.B - radius && b <= oldColor.B + radius
                            )
                        {
                            colorBuffer[y * bitmapData.Stride + x * channels + 3] = newColor.A;
                            colorBuffer[y * bitmapData.Stride + x * channels + 2] = newColor.R;
                            colorBuffer[y * bitmapData.Stride + x * channels + 1] = newColor.G;
                            colorBuffer[y * bitmapData.Stride + x * channels] = newColor.B;
                        }
                    }
                }
            }
            else
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int r = colorBuffer[y * bitmapData.Stride + x * channels + 2];
                        int g = colorBuffer[y * bitmapData.Stride + x * channels + 1];
                        int b = colorBuffer[y * bitmapData.Stride + x * channels];
                        if (
                            r >= oldColor.R - radius && r <= oldColor.R + radius &&
                            g >= oldColor.G - radius && g <= oldColor.G + radius &&
                            b >= oldColor.B - radius && b <= oldColor.B + radius
                            )
                        {
                            colorBuffer[y * bitmapData.Stride + x * channels + 2] = newColor.R;
                            colorBuffer[y * bitmapData.Stride + x * channels + 1] = newColor.G;
                            colorBuffer[y * bitmapData.Stride + x * channels] = newColor.B;
                        }
                    }
                }
            }

            Marshal.Copy(colorBuffer, 0, bitmapData.Scan0, colorBuffer.Length);
            bitmap.UnlockBits(bitmapData);
            image.Dispose();
            return bitmap;
        }

        /// <summary>
        /// 将图片的指定颜色替换为另一个颜色
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="oldColor">待替换的颜色</param>
        /// <param name="newColor">替换后的颜色</param>
        /// <param name="radius">待替换颜色的偏移范围</param>
        /// <param name="cutSize"></param>
        /// <returns></returns>
        [Obsolete]
        public static Image ReplaceColor(this Image image, Color oldColor, Color newColor, int radius, out Rectangle cutSize)
        {
            byte[] colorBuffer; // 图片的颜色信息  
            Bitmap bitmap = new Bitmap(image);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            colorBuffer = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, colorBuffer, 0, colorBuffer.Length);

            int xStart = bitmap.Width, yStart = bitmap.Height, xEnd = 0, yEnd = 0;
            int byteCount = bitmapData.PixelFormat.ToString().ToLower().IndexOf("argb") > 0 ? 4 : 3;
            if (byteCount == 4)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {

                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int a = colorBuffer[y * bitmapData.Stride + x * byteCount + 3];
                        int r = colorBuffer[y * bitmapData.Stride + x * byteCount + 2];
                        int g = colorBuffer[y * bitmapData.Stride + x * byteCount + 1];
                        int b = colorBuffer[y * bitmapData.Stride + x * byteCount];
                        if (
                            a >= oldColor.A - radius && a <= oldColor.A + radius &&
                            r >= oldColor.R - radius && r <= oldColor.R + radius &&
                            g >= oldColor.G - radius && g <= oldColor.G + radius &&
                            b >= oldColor.B - radius && b <= oldColor.B + radius
                            )
                        {
                            colorBuffer[y * bitmapData.Stride + x * byteCount + 3] = newColor.A;
                            colorBuffer[y * bitmapData.Stride + x * byteCount + 2] = newColor.R;
                            colorBuffer[y * bitmapData.Stride + x * byteCount + 1] = newColor.G;
                            colorBuffer[y * bitmapData.Stride + x * byteCount] = newColor.B;
                        }
                        else
                        {// 不需要替换
                            if (x < xStart) xStart = x;
                            if (y < yStart) yStart = y;
                            if (x > xEnd) xEnd = x;
                            if (y > yEnd) yEnd = y;
                        }
                    }
                }
            }
            else
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int r = colorBuffer[y * bitmapData.Stride + x * byteCount + 2];
                        int g = colorBuffer[y * bitmapData.Stride + x * byteCount + 1];
                        int b = colorBuffer[y * bitmapData.Stride + x * byteCount];
                        if (
                            r >= oldColor.R - radius && r <= oldColor.R + radius &&
                            g >= oldColor.G - radius && g <= oldColor.G + radius &&
                            b >= oldColor.B - radius && b <= oldColor.B + radius
                            )
                        {
                            colorBuffer[y * bitmapData.Stride + x * byteCount + 2] = newColor.R;
                            colorBuffer[y * bitmapData.Stride + x * byteCount + 1] = newColor.G;
                            colorBuffer[y * bitmapData.Stride + x * byteCount] = newColor.B;
                        }
                        else
                        {// 不需要替换
                            if (x < xStart) xStart = x;
                            if (y < yStart) yStart = y;
                            if (x > xEnd) xEnd = x;
                            if (y > yEnd) yEnd = y;
                        }
                    }
                }
            }
            cutSize = new Rectangle(xStart, yStart, xEnd - xStart + 1, yEnd - yStart + 1);

            Marshal.Copy(colorBuffer, 0, bitmapData.Scan0, colorBuffer.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
        /// <summary>
        /// 将图片的颜色反转
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static Image InverseColor(this Image image)
        {
            byte[] colorBuffer; // 图片的颜色信息  
            Bitmap bitmap = new Bitmap(image);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            colorBuffer = new byte[bitmapData.Stride * bitmapData.Height];
            Marshal.Copy(bitmapData.Scan0, colorBuffer, 0, colorBuffer.Length);
            int channels = bitmapData.Stride / bitmapData.Width;
            if (channels != 3 && channels != 4)
            { throw new ArgumentException("不支持这种像素格式的图片"); }
            if (channels == 3)
            {
                for (int i = 0; i < colorBuffer.Length; i++)
                { colorBuffer[i] = (byte)(colorBuffer[i] ^ byte.MaxValue); }
            }
            if (channels == 4)
            {
                for (int i = 0; i < colorBuffer.Length; i++)
                {
                    if ((i + 1) % 4 == 0) { continue; } // 排除 alpha 通道
                    colorBuffer[i] = (byte)(colorBuffer[i] ^ byte.MaxValue);
                }
            }
            Marshal.Copy(colorBuffer, 0, bitmapData.Scan0, colorBuffer.Length);
            bitmap.UnlockBits(bitmapData);
            image.Dispose();
            return bitmap;
        }
    }
}
