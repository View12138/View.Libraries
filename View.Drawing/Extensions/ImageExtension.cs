using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using View.Drawing.Extensions.Models;
using ImageEncoder = System.Drawing.Imaging.Encoder;
using ImageEncoderValue = System.Drawing.Imaging.EncoderValue;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// <see cref="Image"/> 扩展。
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// 按原图宽高比尽可能的将原图高质量的缩放到新的尺寸中。
        /// <para>新的图像尺寸不会超过给定的尺寸</para>
        /// <para>原图像将会被 <see cref="Image.Dispose()"/>；如果想保留原图像，请使用 <paramref name="reserve"/> 参数。</para>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <param name="reserve">是否保留原图像</param>
        /// <returns></returns>
        public static Image ChangeSize(this Image image, Size size, bool reserve = false) 
        {
            if (image is null)
            { throw new ArgumentNullException(nameof(image)); }

            Size newSize = ImageHandle.SizeToSize(image.Size, size);
            Bitmap destBitmap;
            if (ImageHandle.IsIndexedPixelFormat(image.PixelFormat))
            {
                destBitmap = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(destBitmap))
                {
                    g.DrawImage(image, 0, 0);
                }
            }
            else
            {
                destBitmap = new Bitmap(newSize.Width, newSize.Height, image.PixelFormat);
            }
            Graphics.FromImage(destBitmap)
                    .SetCompositingQuality(CompositingQuality.HighQuality)
                    .SetSmoothingMode(SmoothingMode.HighQuality)
                    .SetInterpolationMode(InterpolationMode.HighQualityBicubic)
                    .SetCompositingMode(CompositingMode.SourceCopy)
                    .SetPixelOffsetMode(PixelOffsetMode.HighQuality)
                    .ClearEx(Color.Transparent)
                    .DrawImageEx(image, new Rectangle(new Point(0, 0), newSize), new Rectangle(new Point(0, 0), image.Size), GraphicsUnit.Pixel)
                    .Dispose();
            if (!reserve) { image.Dispose(); }
            return destBitmap;
        }

        /// <summary>
        /// 将此 <see cref="Image"/> 中每个像素的颜色数据格式改为指定的颜色数据格式。
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pixelFormat">指定的颜色数据格式。</param>
        /// <returns></returns>
        [Obsolete]
        public static Image ChangePixelFormat(this Image image, PixelFormat pixelFormat)
        {
            if (image.PixelFormat != pixelFormat)
            {
                Bitmap destBitmap = new Bitmap(image.Width, image.Height, pixelFormat);
                Graphics.FromImage(destBitmap)
                        .DrawImageEx(image, new Rectangle(new Point(0, 0), image.Size))
                        .Dispose();
                image.Dispose();
                return destBitmap;
            }
            else
            { return image; }
        }

        /// <summary>
        /// 使用 DGI 内置的编码器将图片的质量尽可能的压缩到指定的大小。
        /// <para>原图像将会被 <see cref="Image.Dispose()"/>；如果想保留原图像，请使用 <paramref name="reserve"/> 参数。</para>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="length">指定的文件大小 ( 单位: <see langword="KB"/> )</param>
        /// <param name="codecs">DGI 内置的编码器</param>
        /// <param name="reserve">是否保留原图像</param>
        /// <returns></returns>
        public static async Task<Image> ChangeQualityAsync(this Image image, int length, ImageCodecs codecs = ImageCodecs.JPEG, bool reserve = false)
            => await ChangeQualityAsync(image, length, ImageHandle.GetCodecInfo(codecs), reserve);
        /// <summary>
        /// 使用指定的编码器将图片的质量尽可能的压缩到指定的大小。
        /// <para>原图像将会被 <see cref="Image.Dispose()"/>；如果想保留原图像，请使用 <paramref name="reserve"/> 参数。</para>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="length">指定的文件大小 ( 单位: <see langword="KB"/> )</param>
        /// <param name="imageCodecInfo">指定的编码器</param>
        /// <param name="reserve">是否保留原图像</param>
        /// <returns></returns>
        public static async Task<Image> ChangeQualityAsync(this Image image, int length, ImageCodecInfo imageCodecInfo, bool reserve = false)
        {
            return await Task.Run(() =>
            {
                int quality = 100;
                length *= 1024;
                do
                {
                    Stream stream = new MemoryStream();
                    image.Save(stream, imageCodecInfo, ImageHandle.GetQualityParam(quality));
                    //quality -= (int)Math.Pow(Math.Abs(stream.Length - length), 0.15);
                    quality -= 5;
                    if (quality < 5 || stream.Length < length) // 无法继续降低质量了
                    {
                        if (!reserve) { image.Dispose(); }
                        stream.Position = 0;
                        return Image.FromStream(stream);
                    }
                    else
                    { stream.Dispose(); }

                } while (true);
            });
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

    /// <summary>
    /// 内部使用
    /// </summary>
    static class ImageHandle
    {
        /// <summary>
        /// 获取指定名称的 <see cref="ImageCodecInfo"/>
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetCodecInfo(ImageCodecs ext)
            => ImageCodecInfo.GetImageEncoders().ToList().Find(info => info.FormatDescription.Equals(ext.ToString().ToUpper()));

        /// <summary>
        /// 获取指定的质量参数
        /// </summary>
        /// <param name="quality">质量，[1-100]之间</param>
        /// <returns></returns>
        public static EncoderParameters GetQualityParam(int quality)
            => new EncoderParameters() { Param = new EncoderParameter[] { new EncoderParameter(ImageEncoder.Quality, new long[] { quality }) } };
        /// <summary>
        /// 获取指定的质量参数
        /// </summary>
        /// <param name="encoder">参数</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static EncoderParameters GetEncoderParam(ImageEncoder encoder, ImageEncoderValue value)
            => new EncoderParameters() { Param = new EncoderParameter[] { new EncoderParameter(encoder, new long[] { (long)value }) } };

        /// <summary>
        /// 调整尺寸
        /// </summary>
        /// <param name="oldSize"></param>
        /// <param name="newSize"></param>
        /// <returns></returns>
        public static Size SizeToSize(Size oldSize, Size newSize)
        {
            // scale : 宽高比 ; 宽 = scale × 高 ; 高 = 宽 ÷ scale .
            var scale = oldSize.Width / (oldSize.Height * 1.0);
            if ((int)(scale * newSize.Height) < newSize.Width)
            { return new Size((int)(scale * newSize.Height), newSize.Height); }
            else
            { return new Size(newSize.Width, (int)(newSize.Width / scale)); }
        }

        /// <summary>
        /// 判断图片是否索引像素格式,是否是引发异常的像素格式
        /// </summary>
        /// <param name="imagePixelFormat">图片的像素格式</param>
        /// <returns></returns>
        public static bool IsIndexedPixelFormat(PixelFormat imagePixelFormat)
        {
            PixelFormat[] pixelFormatArray = {
                                            PixelFormat.Format1bppIndexed
                                            ,PixelFormat.Format4bppIndexed
                                            ,PixelFormat.Format8bppIndexed
                                            ,PixelFormat.Undefined
                                            ,PixelFormat.DontCare
                                            ,PixelFormat.Format16bppArgb1555
                                            ,PixelFormat.Format16bppGrayScale
                                        };
            foreach (PixelFormat pf in pixelFormatArray)
            {
                if (imagePixelFormat == pf)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
