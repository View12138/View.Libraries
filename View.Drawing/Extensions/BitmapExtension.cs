using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// <see cref="Bitmap"/> 扩展方法。
    /// </summary>
    public static class BitmapExtension
    {
        /// <summary>
        /// <see cref="Bitmap"/> 转为 4*8bit BGR <see cref="byte"/> 数组。
        /// </summary>
        /// <param name="bitmap">待转换图像</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="channels">图像通道</param>
        /// <returns>图像的 BGR <see cref="byte"/> 数组</returns>
        public static byte[] To32BGRByteArray(this Bitmap bitmap, out int width, out int height, out int channels)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            width = bitmap.Width;
            height = bitmap.Height;
            channels = bitmapData.Stride / bitmap.Width;
            int num = bitmap.Height * bitmapData.Stride;
            byte[] array = new byte[num];
            Marshal.Copy(bitmapData.Scan0, array, 0, num);
            bitmap.UnlockBits(bitmapData);
            return array;
        }

        /// <summary>
        /// <see cref="Bitmap"/> 转为 3*8bit BGR <see cref="byte"/> 数组。
        /// </summary>
        /// <param name="bitmap">待转换图像</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="channels">图像通道</param>
        /// <returns>图像的 BGR <see cref="byte"/> 数组</returns>
        public static byte[] To24BGRByteArray(this Bitmap bitmap, out int width, out int height, out int channels)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            width = bitmap.Width;
            height = bitmap.Height;
            int num = bitmap.Height * bitmapData.Stride;
            byte[] array = new byte[num];
            Marshal.Copy(bitmapData.Scan0, array, 0, num);
            bitmap.UnlockBits(bitmapData);

            channels = 3;
            byte[] bgr = new byte[array.Length / 4 * channels];
            // brga
            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if ((i + 1) % 4 == 0) continue;
                bgr[j] = array[i];
                j++;
            }
            return bgr;
        }

        /// <summary>
        /// 设置此 <see cref="Bitmap"/> 的分辨率。
        /// <para>可链式调用的 <see cref="Bitmap.SetResolution(float, float)"/> </para>
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="xDpi">Bitmap 的水平分辨率 (以"点/英寸"为单位)。</param>
        /// <param name="yDpi">Bitmap 的垂直分辨率 (以"点/英寸"为单位)。</param>
        /// <returns></returns>
        public static Bitmap SetResolution(this Bitmap bitmap, float xDpi, float yDpi)
        {
            bitmap.SetResolution(xDpi, yDpi);
            return bitmap;
        }
    }
}
