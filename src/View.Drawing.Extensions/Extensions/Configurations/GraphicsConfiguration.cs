using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using View.Drawing.Extensions.Models;

namespace View.Drawing.Extensions.Configurations
{
    /// <summary>
    /// GDI+ 绘图配置。
    /// </summary>
    public class GraphicsConfiguration
    {
        /// <summary>
        /// 指定在复合期间使用的质量等级。
        /// <para>默认值：<see cref="CompositingQuality.HighQuality"/></para>
        /// </summary>
        public CompositingQuality CompositingQuality { get; set; } = CompositingQuality.HighQuality;
        /// <summary>
        /// 指定是否将平滑处理（抗锯齿）应用于直线、曲线和已填充区域的边缘。
        /// <para>默认值：<see cref="SmoothingMode.HighQuality"/></para>
        /// </summary>
        public SmoothingMode SmoothingMode { get; set; } = SmoothingMode.HighQuality;
        /// <summary>
        /// 指定在缩放或旋转图像时使用的算法。
        /// <para>默认值：<see cref="InterpolationMode.HighQualityBicubic"/></para>
        /// </summary>
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.HighQualityBicubic;
        /// <summary>
        /// 指定源色与背景色组合的方式。
        /// <para>默认值：<see cref="CompositingMode.SourceCopy"/></para>
        /// </summary>
        public CompositingMode CompositingMode { get; set; } = CompositingMode.SourceCopy;
        /// <summary>
        /// 指定在呈现期间像素偏移的方式。
        /// <para>默认值：<see cref="PixelOffsetMode.HighQuality"/></para>
        /// </summary>
        public PixelOffsetMode PixelOffsetMode { get; set; } = PixelOffsetMode.HighQuality;
        /// <summary>
        /// 指定文本呈现的质量。
        /// <para>默认值：<see cref="TextRenderingHint.ClearTypeGridFit"/></para>
        /// </summary>
        public TextRenderingHint TextRenderingHint { get; set; } = TextRenderingHint.ClearTypeGridFit;
    }
}
