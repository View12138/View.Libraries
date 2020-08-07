using System.Drawing;

namespace View.Drawing.Papers
{
    /// <summary>
    /// 表示纸张的尺寸
    /// </summary>
    public class PaperSize
    {
        /// <summary>
        /// <see langword="mm"/> 到 <see langword="inch"/> 的转换比例
        /// </summary>
        private readonly double mm2inch = 0.03937;

        /// <summary>
        /// 
        /// </summary>
        public PaperSize() : this(0, 0) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PaperSize(int width, int height) { Width = width; Height = height; }

        /// <summary>
        /// 获取纸张的实际宽度 (单位：毫米)
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// 获取纸张的实际高度 (单位：毫米)
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// 获取指定 DPI 对应的像素尺寸。
        /// </summary>
        /// <param name="dpi">指定的 DPI</param>
        /// <returns></returns>
        public Size GetPixelSize(int dpi) => new Size((int)(Width * mm2inch * dpi), (int)(Height * mm2inch * dpi));
        /// <summary>
        /// 获取 DPI=300 时的像素尺寸。
        /// </summary>
        /// <returns></returns>
        public Size GetPixelSize() => GetPixelSize(300);
    }
}
