namespace View.AutoTask.Options
{
    /// <summary>
    /// 缓存内容写入格式
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 文本格式
        /// <para>使用 <see cref="System.Text.Encoding.UTF8"/> 编码</para>
        /// </summary>
        Text,
        /// <summary>
        /// 二进制格式
        /// </summary>
        Binary,
    }
}
