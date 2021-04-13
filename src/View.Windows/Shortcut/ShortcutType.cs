namespace View.Windows.Shortcut
{
    /// <summary>
    /// 快捷方式的类型
    /// </summary>
    public enum ShortcutType
    {
        /// <summary>
        /// 桌面快捷方式。
        /// </summary>
        Desktop,
        /// <summary>
        /// 自启快捷方式。
        /// <para>此方式需要管理员运行</para>
        /// </summary>
        Startup,
    }
}
