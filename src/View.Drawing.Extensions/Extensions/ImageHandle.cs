using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using View.Drawing.Extensions.Models;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// 一些辅助方法
    /// </summary>
    public static class ImageHandle
    {
        /// <summary>
        /// 获取 GDI+ 中内置的指定的图像编码器。
        /// </summary>
        /// <param name="encoders">指定的图像编码器</param>
        /// <returns></returns>
        public static ImageCodecInfo GetGDIPlusImageEncoders(ImageEncoders encoders)
            => ImageCodecInfo.GetImageEncoders().Where(x => x.FormatDescription == encoders.ToString()).FirstOrDefault();
        /// <summary>
        /// 获取 GDI+ 中内置的指定的图像解码器。
        /// </summary>
        /// <param name="decoders">指定的图像解码器</param>
        /// <returns></returns>
        public static ImageCodecInfo GetGDIPlusImageDecoders(ImageDecoders decoders)
            => ImageCodecInfo.GetImageDecoders().Where(x => x.FormatDescription == decoders.ToString()).FirstOrDefault();

        /// <summary>
        /// 获取指定的质量参数
        /// </summary>
        /// <param name="quality">质量，[1-100]之间</param>
        /// <returns></returns>
        public static EncoderParameters GetQualityParam(int quality)
            => new EncoderParameters() { Param = new EncoderParameter[] { new EncoderParameter(Encoder.Quality, quality) } };

        /// <summary>
        /// 调整尺寸
        /// </summary>
        /// <param name="oldSize"></param>
        /// <param name="newSize"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Size SizeToSize(Size oldSize, Size newSize, SizeMode mode)
        {
            // scale : 宽高比 ;
            // 宽 = scale × 高 ; 高 = 宽 ÷ scale .
            var scale = oldSize.Width / (oldSize.Height * 1.0);
            if (mode == SizeMode.ToMax)
            {
                if ((int)(scale * newSize.Height) < newSize.Width)
                { return new Size((int)(scale * newSize.Height), newSize.Height); }
                else
                { return new Size(newSize.Width, (int)(newSize.Width / scale)); }
            }
            else if (mode == SizeMode.ToMin)
            {
                if ((int)(scale * newSize.Height) < newSize.Width)
                { return new Size(newSize.Width, (int)(newSize.Width / scale)); }
                else
                { return new Size((int)(scale * newSize.Height), newSize.Height); }
            }
            else if (mode == SizeMode.Stretch)
            { return newSize; }
            else if (mode == SizeMode.Cut)
            { return newSize; }
            else
            { throw new ArgumentException($"{nameof(mode)} 不是指定的值"); }
        }

        /// <summary>
        /// 是否是引发异常的像素的颜色数据的格式。
        /// </summary>
        /// <param name="imagePixelFormat">像素的颜色数据的格式</param>
        /// <returns></returns>
        public static bool IsErrorPixelFormat(PixelFormat imagePixelFormat)
        {
            PixelFormat[] pixelFormatArray = new PixelFormat[]
            {
                PixelFormat.Undefined,
                PixelFormat.DontCare,
                PixelFormat.Max,
                PixelFormat.Indexed,
                PixelFormat.Gdi,
                PixelFormat.Alpha,
                PixelFormat.PAlpha,
                PixelFormat.Extended,
                PixelFormat.Canonical,
                PixelFormat.Format1bppIndexed,
                PixelFormat.Format4bppIndexed,
                PixelFormat.Format8bppIndexed,
                PixelFormat.Format16bppArgb1555,
                PixelFormat.Format16bppGrayScale,
            };
            return pixelFormatArray.Contains(imagePixelFormat);
        }
    }
}
