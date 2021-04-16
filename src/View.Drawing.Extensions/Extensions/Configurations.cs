using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using View.Drawing.Extensions.Models;

namespace View.Drawing.Extensions
{
    /// <summary>
    /// 基础配置。
    /// </summary>
    public class ConfigurationBase
    {
        /// <summary>
        /// 是否保留原图。否：原图像将会被 <see cref="Image.Dispose()"/>；
        /// <para>默认值：<see langword="false"/></para>
        /// </summary>
        public bool Reserve { get; set; } = false;
    }

    /// <summary>
    /// GDI+ 绘图配置。
    /// </summary>
    public class GraphicsConfiguration : ConfigurationBase
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

    /// <summary>
    /// 调整大小配置。
    /// </summary>
    public class ChangeSizeConfiguration : GraphicsConfiguration
    {
        /// <summary>
        /// 指定图像尺寸的缩放模式。
        /// <para>默认值：<see cref="SizeMode.ToMax"/></para>
        /// </summary>
        public SizeMode SizeMode { get; set; } = SizeMode.ToMax;
        /// <summary>
        /// 指定要裁剪的相对于左上角的位置。
        /// <list type="bullet">
        /// <item>当 <see cref="SizeMode"/> <see langword="=="/> <see cref="SizeMode.Cut"/> 时生效。</item>
        /// <item>默认值： (0, 0)</item>
        /// </list>
        /// </summary>
        public Point Location { get; set; } = new Point(0, 0);
        /// <summary>
        /// 裁剪时是否保证裁剪内容始终在原图内。
        /// <list type="bullet">
        /// <item>当 <see cref="SizeMode"/> <see langword="=="/> <see cref="SizeMode.Cut"/> 时生效。</item>
        /// <item><see langword="true"/> [默认值]: 自动调整以保证裁剪内容不会超出原图像。<see langword="false"/>：按照指定的内容裁剪，当裁剪内容超出原图像时，使用 <see cref="ClipColor"/> 进行填充。</item>
        /// </list>
        /// </summary>
        public bool ClipInBox { get; set; } = true;
    }

    /// <summary>
    /// 调整质量配置。
    /// </summary>
    public class ChangeQualityConfiguration : ConfigurationBase
    {
        private int _interval = 3;
        private int _minQuality = 5;

        /// <summary>
        /// 指定编码器。
        /// <para>默认值：使用名为 JPEG 的编码器</para>
        /// </summary>
        public ImageCodecInfo ImageEncodecInfo { get; set; } = ImageHandle.GetGDIPlusImageEncoders(ImageEncoders.JPEG);

        /// <summary>
        /// 质量递减间隔。
        /// <list type="bullet">
        /// <item>图片质量从 100 开始以此值递减来测试是否符合要求。</item>
        /// <item>默认值：3。取值：[1,10]。此值越小，计算的次数就越多，得到的结果也就越接近目标。</item>
        /// </list>
        /// </summary>
        public int Interval
        {
            get => _interval; set
            {
                if (value < 1 || value > 10)
                { throw new System.ArgumentException($"值不在取值区间范围。"); }
                _interval = value;
            }
        }

        /// <summary>
        /// 最小质量。
        /// <list type="bullet">
        /// <item>当压缩质量小于此值时，即时仍然不满足要求也不再进行压缩了。</item>
        /// <item>默认值：5。取值：[1,10]。</item>
        /// </list>
        /// </summary>
        public int MinQuality
        {
            get => _minQuality; set
            {
                if (value < 1 || value > 10)
                { throw new System.ArgumentException($"值不在取值区间范围。"); }
                _minQuality = value;
            }
        }
    }
}
