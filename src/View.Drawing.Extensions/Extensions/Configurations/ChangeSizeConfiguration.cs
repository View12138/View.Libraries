using System.Drawing;
using View.Drawing.Extensions.Models;

namespace View.Drawing.Extensions.Configurations
{
    /// <summary>
    /// 调整大小配置。
    /// </summary>
    public class ChangeSizeConfiguration
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
}
