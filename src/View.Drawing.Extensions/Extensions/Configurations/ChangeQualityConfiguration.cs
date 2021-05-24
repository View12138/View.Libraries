using System.Drawing.Imaging;
using View.Drawing.Extensions.Models;

namespace View.Drawing.Extensions.Configurations
{
    /// <summary>
    /// 调整质量配置。
    /// </summary>
    public class ChangeQualityConfiguration
    {
        private int _interval = 3;
        private int _minQuality = 5;

        /// <summary>
        /// 指定编码器。
        /// <list type="bullet">
        /// <item>默认值：使用名为 JPEG 的编码器</item>
        /// <item>可通过 <see cref="ImageHandle.GetGDIPlusImageEncoders(ImageEncoders)"/> 获取 DGI+ 内置的图像编码器</item>
        /// </list>
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
