using System;

namespace View.Windows.Shortcut
{
    /// <summary>
    /// 快捷方式的类型
    /// </summary>
    [Flags]
    public enum ShortcutType
    {
        /// <summary>
        /// 桌面快捷方式。
        /// </summary>
        Desktop = 0x01 << 0,
        /// <summary>
        /// 自启快捷方式。
        /// <para>此方式需要管理员运行</para>
        /// </summary>
        Startup = 0x01 << 1,
        /// <summary>
        /// 开始菜单快捷方式
        /// </summary>
        StartMenu = 0x01 << 2,
    }
}
