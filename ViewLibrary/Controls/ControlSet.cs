using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Enums;

namespace View.Controls
{
    [DefaultEvent("ThemeStyleChanged"), DefaultProperty("ThemeStyle")]
    public partial class ControlSet : Component
    {
        // 字段
        private static ThemeType _themeStyle = ThemeType.Light;
        private static Color themeColor = Color.FromArgb(41, 98, 255);
        private static Color lightForeColor = Color.FromArgb(33, 33, 33);
        private static Color lightBackColor = Color.FromArgb(242, 242, 242);
        private static Color drakForeColor = Color.FromArgb(239, 239, 239);
        private static Color drakBackColor = Color.FromArgb(43, 43, 43);

        // 构造
        /// <summary>
        /// 初始化
        /// </summary>
        public ControlSet()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="container"></param>
        public ControlSet(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        // 事件
        /// <summary>
		/// 当背景色改变时发生
		/// </summary>
		[Browsable(true), Description("当主题样式改变时发生")]
        public static event EventHandler ThemeStyleChanged;
        /// <summary>
        /// 当主题色改变时发生
        /// </summary>
        [Browsable(true), Description("当主题色改变时发生")]
        public static event EventHandler ThemeColorChanged;

        // 属性
        /// <summary>
        /// 获取或设置所有控件的主题样式
        /// </summary>
        [Browsable(true), Category("数据"), Description("获取或设置所有控件的主题样式")]
        public static ThemeType ThemeStyle
        {
            get
            {
                return _themeStyle;
            }
            set
            {
                _themeStyle = value;
                ThemeStyleChanged?.Invoke(_themeStyle, new EventArgs());
            }
        }
        /// <summary>
        /// 获取或设置所有控件的主题色
        /// </summary>
        [Browsable(true), Category("数据"), Description("获取或设置所有控件的主题色")]
        public static Color ThemeColor
        {
            get
            {
                return themeColor;
            }
            set
            {
                themeColor = value;
                ThemeColorChanged?.Invoke(themeColor, new EventArgs());
            }
        }
        /// <summary>
        /// 获取或设置当前主题下的背景色
        /// </summary>
        [Browsable(true), Category("数据"), Description("获取或设置当前主题下的背景色")]
        public static Color BackColor
        {
            get
            {
                if (_themeStyle == ThemeType.Light)
                {
                    return lightBackColor;
                }
                else
                {
                    return drakBackColor;
                }
            }
            set
            {
                if (_themeStyle == ThemeType.Light)
                {
                    lightBackColor = value;
                }
                else
                {
                    drakBackColor = value;
                }
            }
        }
        /// <summary>
        /// 获取或设置当前主题下的前景色
        /// </summary>
        [Browsable(true), Category("数据"), Description("获取或设置当前主题下的前景色")]
        public static Color ForeColor
        {
            get
            {
                if (_themeStyle == ThemeType.Light)
                {
                    return lightForeColor;
                }
                else
                {
                    return drakForeColor;
                }
            }
            set
            {
                if (_themeStyle == ThemeType.Light)
                {
                    lightForeColor = value;
                }
                else
                {
                    drakForeColor = value;
                }
            }
        }
    }
}
