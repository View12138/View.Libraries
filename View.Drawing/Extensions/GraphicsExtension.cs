using System.Drawing;
using System.Drawing.Drawing2D;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// <see cref="Graphics"/> 扩展方法。
    /// </summary>
    public static class GraphicsExtension
    {
        /// <summary>
        /// 在指定的位置使用原始物理大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, Point)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="point">Point 结构，它标识所绘制图像的左上角的位置。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, Point point)
        {
            graphics.DrawImage(image, point);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并按指定形状和大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, Point[])"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="destPoints">由三个 Point 结构组成的数据，这三个结构定义一个平行四边形。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, Point[] destPoints)
        {
            graphics.DrawImage(image, destPoints);
            return graphics;
        }
        /// <summary>
        /// 在指定的位置使用原始物理大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, PointF)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="point">PointF 结构，它标识所绘制图像的左上角的位置。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, PointF point)
        {
            graphics.DrawImage(image, point);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并按指定形状和大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, PointF[])"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="destPoints">由三个 Point 结构组成的数据，这三个结构定义一个平行四边形。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, PointF[] destPoints)
        {
            graphics.DrawImage(image, destPoints);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并且按指定大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, Rectangle)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="rect">Rectangle 结构，它指定所绘制图像的位置和大小。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, Rectangle rect)
        {
            graphics.DrawImage(image, rect);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并且按指定大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, RectangleF)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="rect">RectangleF 结构，它指定所绘制图像的位置和大小。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, RectangleF rect)
        {
            graphics.DrawImage(image, rect);
            return graphics;
        }
        /// <summary>
        /// 在指定的位置使用原始物理大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, float, float)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="x">所绘制图像的左上角的 x 坐标。</param>
        /// <param name="y">所绘制图像的左上角的 y 坐标。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, float x, float y)
        {
            graphics.DrawImage(image, x, y);
            return graphics;
        }
        /// <summary>
        /// 在指定的位置使用原始物理大小绘制指定的 <see cref="Image"/>。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, int, int)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="x">所绘制图像的左上角的 x 坐标。</param>
        /// <param name="y">所绘制图像的左上角的 y 坐标。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, int x, int y)
        {
            graphics.DrawImage(image, x, y);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并按指定大小绘制指定的 <see cref="Image"/> 的指定部分。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, Point[], Rectangle, GraphicsUnit)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="destPoints">由三个 Point 结构组成的数据，这三个结构定义一个平行四边形。</param>
        /// <param name="srcRect">Rectangle 结构，它指定 image 对象中要绘制的部分。</param>
        /// <param name="srcUnit">GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并按指定大小绘制指定的 <see cref="Image"/> 的指定部分。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, PointF[], RectangleF, GraphicsUnit)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="destPoints">由三个 Point 结构组成的数据，这三个结构定义一个平行四边形。</param>
        /// <param name="srcRect">Rectangle 结构，它指定 image 对象中要绘制的部分。</param>
        /// <param name="srcUnit">GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destPoints, srcRect, srcUnit);
            return graphics;
        }
        /// <summary>
        /// 在指定位置并按指定大小绘制指定的 <see cref="Image"/> 的指定部分。
        /// <para>可链式调用的 <see cref="Graphics.DrawImage(Image, Rectangle, Rectangle, GraphicsUnit)"/></para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="image">要绘制的 Image。</param>
        /// <param name="destRect">Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。</param>
        /// <param name="srcRect">Rectangle 结构，它指定 image 对象中要绘制的部分。</param>
        /// <param name="srcUnit">GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。</param>
        /// <returns></returns>
        public static Graphics DrawImageEx(this Graphics graphics, Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            graphics.DrawImage(image, destRect, srcRect, srcUnit);
            return graphics;
        }
        //TODO:第 12 个及以后的 DrawImage 重载

        /// <summary>
        /// 可链式调用的 <see cref="Graphics.Clear(Color)"/>。
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Graphics ClearEx(this Graphics graphics, Color color)
        {
            graphics.Clear(color);
            return graphics;
        }

        /// <summary>
        /// 设置如何将合成图像绘制到此 <see cref="Graphics"/>。
        /// <para>默认值为 <see cref="CompositingMode.SourceOver"/>。</para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="compositingMode"></param>
        /// <returns></returns>
        public static Graphics SetCompositingMode(this Graphics graphics, CompositingMode compositingMode)
        {
            graphics.CompositingMode = compositingMode;
            return graphics;
        }
        /// <summary>
        /// 设置绘制到此 <see cref="Graphics"/> 的合成图像的呈现质量。、
        /// <para>默认值为 <see cref="CompositingQuality.Default"/>。</para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="compositingQuality"></param>
        /// <returns></returns>
        public static Graphics SetCompositingQuality(this Graphics graphics, CompositingQuality compositingQuality)
        {
            graphics.CompositingQuality = compositingQuality;
            return graphics;
        }
        /// <summary>
        /// 设置与此  <see cref="Graphics"/> 关联的补插模式。
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="interpolationMode"></param>
        /// <returns></returns>
        public static Graphics SetInterpolationMode(this Graphics graphics, InterpolationMode interpolationMode)
        {
            graphics.InterpolationMode = interpolationMode;
            return graphics;
        }
        /// <summary>
        /// 设置在呈现此  <see cref="Graphics"/> 过程中的像素偏移方式。
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="pixelOffsetMode"></param>
        /// <returns></returns>
        public static Graphics SetPixelOffsetMode(this Graphics graphics, PixelOffsetMode pixelOffsetMode)
        {
            graphics.PixelOffsetMode = pixelOffsetMode;
            return graphics;
        }
        /// <summary>
        /// 设置此  <see cref="Graphics"/> 的呈现质量。
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="smoothingMode"></param>
        /// <returns></returns>
        public static Graphics SetSmoothingMode(this Graphics graphics, SmoothingMode smoothingMode)
        {
            graphics.SmoothingMode = smoothingMode;
            return graphics;
        }
    }
}
