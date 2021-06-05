using System.Drawing;

namespace View.Core.Models.Papers
{
    /// <summary>
    /// 表示纸张的尺寸
    /// </summary>
    public class PaperSize
    {
        // 1 inch == 25.4mm
        /// <summary>
        /// <see langword="mm"/> 到 <see langword="inch"/> 的转换比例
        /// </summary>
        private readonly decimal mm2inch = 1M / 25.4M;
        /// <summary>
        /// inch 到 mm 的换算比例
        /// </summary>
        private readonly double inch2mm = 25.4;

        /// <summary>
        /// 
        /// </summary>
        public PaperSize() : this(0, 0) { }
        /// <summary>
        /// 初始化一个指定大小的纸张
        /// </summary>
        /// <param name="width">纸张宽度 (单位：毫米)</param>
        /// <param name="height">纸张高度 (单位：毫米)</param>
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
        /// <param name="dpi">指定的 DPI (Dots Per Inch)</param>
        /// <returns></returns>
        public Size GetPixelSize(int dpi)
        {
            return new Size((int)(Width * mm2inch * dpi), (int)(Height * mm2inch * dpi));
        }

        /// <summary>
        /// 获取 DPI=300 时的像素尺寸。
        /// </summary>
        /// <returns></returns>
        public Size GetPixelSize()
        {
            return GetPixelSize(300);
        }
    }
}
