namespace View.Drawing.Extensions.Models
{
    /// <summary>
    /// 调整大小的模式。
    /// </summary>
    public enum SizeMode
    {
        /// <summary>
        /// 按原尺寸比例尽可能的将尺寸缩放到指定的尺寸，新尺寸不会超过指定的尺寸。
        /// <para>适用于指定了最大尺寸的情况。</para>
        /// </summary>
        ToMax,
        /// <summary>
        /// 按原尺寸比例尽可能的将尺寸缩放到指定的尺寸，新尺寸不会小于指定的尺寸。
        /// <para>适用于指定了最小尺寸的情况。</para>
        /// </summary>
        ToMin,
        /// <summary>
        /// 将原尺寸拉伸到指定的尺寸，此种模式下，原尺寸的比例将改变。
        /// </summary>
        Stretch,
        /// <summary>
        /// 按照指定尺寸的比例裁剪原尺寸。
        /// </summary>
        Cut,
    }
}
