using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace View.Controls
{
    /// <summary>
    /// 带有 Icon 的按钮
    /// </summary>
    [DefaultEvent("Click")/*, ToolboxBitmap(typeof(AControlPanel), "Icons.AControlPanel.bmp")*/]
    public partial class IconButton : UserControl
    {
        private string _iconText;
        private Color _backColorLeave;
        private Color _iconForeColor;

        /// <summary>
        /// 构造带有 Icon 的按钮
        /// </summary>
        public IconButton()
        {
            InitializeComponent();
            //Segoe MDL2 Assets, 9pt
            lbText.Click += (sender, e) => { OnClick(e); };
            lbText.DoubleClick += (sender, e) => { OnDoubleClick(e); };
            lbText.MouseClick += (sender, e) => { OnMouseClick(e); };
            lbText.MouseDoubleClick += (sender, e) => { OnMouseDoubleClick(e); };
            lbText.MouseDown += (sender, e) =>
            {
                BackColor = BackColorDown;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconDown; }
                lbIcon.ForeColor = IconForeColorDown;
                //BorderColor = BorderDown;
                OnMouseDown(e);
            };
            lbText.MouseEnter += (sender, e) =>
            {
                BackColor = BackColorEnter;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconEnter; }
                lbIcon.ForeColor = IconForeColorEnter;
                //BorderColor = BorderEnter;
                OnMouseEnter(e);
            };
            lbText.MouseHover += (sender, e) => { OnMouseHover(e); };
            lbText.MouseLeave += (sender, e) =>
            {
                BackColor = BackColorN;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconText; }
                lbIcon.ForeColor = IconForeColor;
                //BorderColor = BorderLeave;
                OnMouseLeave(e);
            };
            lbText.MouseMove += (sender, e) => { OnMouseMove(e); };
            lbText.MouseUp += (sender, e) =>
            {
                BackColor = BackColorEnter;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconEnter; }
                lbIcon.ForeColor = IconForeColorEnter;
                //BorderColor = BorderEnter;
                OnMouseUp(e);
            };
            lbText.MouseWheel += (sender, e) => { OnMouseWheel(e); };

            lbIcon.Click += (sender, e) => { OnClick(e); };
            lbIcon.DoubleClick += (sender, e) => { OnDoubleClick(e); };
            lbIcon.MouseClick += (sender, e) => { OnMouseClick(e); };
            lbIcon.MouseDoubleClick += (sender, e) => { OnMouseDoubleClick(e); };
            lbIcon.MouseDown += (sender, e) =>
            {
                BackColor = BackColorDown;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconDown; }
                lbIcon.ForeColor = IconForeColorDown;
                //BorderColor = BorderDown;
                OnMouseDown(e);
            };
            lbIcon.MouseEnter += (sender, e) =>
            {
                BackColor = BackColorEnter;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconEnter; }
                lbIcon.ForeColor = IconForeColorEnter;
                //BorderColor = BorderEnter;
                OnMouseEnter(e);
            };
            lbIcon.MouseHover += (sender, e) => { OnMouseHover(e); };
            lbIcon.MouseLeave += (sender, e) =>
            {
                BackColor = BackColorN;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconText; }
                lbIcon.ForeColor = IconForeColor;
                //BorderColor = BorderLeave;
                OnMouseLeave(e);
            };
            lbIcon.MouseMove += (sender, e) => { OnMouseMove(e); };
            lbIcon.MouseUp += (sender, e) =>
            {
                BackColor = BackColorEnter;
                if (string.IsNullOrEmpty(IconDown))
                { lbIcon.Text = IconText; }
                else { lbIcon.Text = IconEnter; }
                lbIcon.ForeColor = IconForeColorEnter;
                //BorderColor = BorderEnter;
                OnMouseUp(e);
            };
            lbIcon.MouseWheel += (sender, e) => { OnMouseWheel(e); };


            //路径     
            //string path = @"\Fonts\segmdl2.ttf";

            ////读取字体文件             
            //PrivateFontCollection = new PrivateFontCollection();
            //try
            //{
            //    PrivateFontCollection.AddFontFile(path);
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            lbText.Text = "按钮文本";
            lbIcon.Font = new Font("Segoe MDL2 Assets", 9);
            lbIcon.Text = "\uE11D";
            BackColor = Color.FromArgb(225, 225, 225);
            BackColorDown = Color.FromArgb(204, 228, 247);
            BackColorEnter = Color.FromArgb(229, 241, 251);
            BackColorN = Color.FromArgb(225, 225, 225);

            //BorderSize = 1;
            //BorderDown = Color.FromArgb(0, 84, 153);
            //BorderEnter = Color.FromArgb(0, 120, 215);
            //BorderLeave = Color.FromArgb(173, 173, 173);
            //BorderColor = BorderLeave;

        }

        // Text属性
        /// <summary>
        /// 获取或设置与此控件关联的文本
        /// </summary>
        [Category("Text 属性"), Description("获取或设置与此控件关联的文本"), Browsable(true)]
        public string StringText
        {
            get
            {
                return lbText.Text;
            }

            set
            {
                lbText.Text = value;
                if (lbText.Text == "" || lbText.Text == null)
                {
                    lbText.Visible = false;
                    lbIcon.Dock = DockStyle.Fill;
                }
                else
                {
                    if (lbText.Visible == false)
                    {
                        lbText.Visible = true;
                        IconDock = IconDockStyle.Left;
                        lbIcon.Width = IconSize;
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置控件显示的文本字体
        /// </summary>
        [Category("Text 属性"), Description("获取或设置控件显示的文本字体"), Browsable(true)]
        public Font StringFont
        {
            get => lbText.Font;
            set => lbText.Font = value;
        }
        /// <summary>
        /// 获取或设置文本的对其方式
        /// </summary>
        [Category("Text 属性"), Description("获取或设置文本的对其方式"), Browsable(true)]
        public ContentAlignment StringAlign
        {
            get => lbText.TextAlign;
            set => lbText.TextAlign = value;
        }
        /// <summary>
        /// 获取或设置文本的内边距
        /// </summary>
        [Category("Text 属性"), Description("获取或设置文本的内边距"), Browsable(true)]
        public Padding StringPadding
        {
            get => lbText.Padding;
            set => lbText.Padding = value;
        }
        /// <summary>
        /// 获取或设置控件的前景色
        /// </summary>
        [Category("Text 属性"), Description("获取或设置控件的前景色"), Browsable(true)]
        public Color StringForeColor
        {
            get => lbText.ForeColor;
            set => lbText.ForeColor = value;

        }

        //Icon属性
        /// <summary>
        /// 获取或设置 Icon 相对文本的方位
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置 Icon 相对文本的方位"), Browsable(true)]
        public IconDockStyle IconDock
        {
            get
            {
                switch (lbIcon.Dock)
                {
                    case DockStyle.Left: return IconDockStyle.Left;
                    case DockStyle.Top: return IconDockStyle.Top;
                    case DockStyle.Right: return IconDockStyle.Right;
                    case DockStyle.Bottom: return IconDockStyle.Bottom;
                    default: return IconDockStyle.Left;
                }
            }
            set
            {
                if (lbText.Visible == false && DesignMode == true)
                {
                    throw new Exception("无文本时,此属性不适用");
                }
                else
                {
                    switch (value)
                    {
                        case IconDockStyle.Left: lbIcon.Dock = DockStyle.Left; break;
                        case IconDockStyle.Top: lbIcon.Dock = DockStyle.Top; break;
                        case IconDockStyle.Right: lbIcon.Dock = DockStyle.Right; break;
                        case IconDockStyle.Bottom: lbIcon.Dock = DockStyle.Bottom; break;
                    }
                }
            }
        }
        /// <summary>
        /// 获取或设置图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon"), Browsable(true)]
        public string IconText
        {
            get => _iconText;
            set
            {
                _iconText = value;
                lbIcon.Text = value;
            }

        }
        /// <summary>
        /// 获取或设置鼠标进入时的图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置鼠标进入时的图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon"), Browsable(true)]
        public string IconEnter { get; set; }
        /// <summary>
        /// 获取或设置鼠标按下时的图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置鼠标按下时的图标文本，使用 Segoe MDL2 Assets 字体来显示 Icon"), Browsable(true)]
        public string IconDown { get; set; }
        /// <summary>
        /// 获取或设置 Icon 的字体;字体会被重置为 Segoe MDL2 Assets，但是保留其它字体信息
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置 Icon 的字体;字体会被重置为 Segoe MDL2 Assets，但是保留其它字体信息"), Browsable(true)]
        public Font IconFont
        {
            get => lbIcon.Font;
            set => lbIcon.Font = new Font("Segoe MDL2 Assets", value.Size, value.Style, value.Unit);

        }
        /// <summary>
        /// 获取或设置Icon的对其方式
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置Icon的对其方式"), Browsable(true)]
        public ContentAlignment IconTextAlign
        {
            get => lbIcon.TextAlign;
            set => lbIcon.TextAlign = value;
        }
        /// <summary>
        /// 获取或设置Icon的内边距
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置Icon的内边距"), Browsable(true)]
        public Padding IconPadding
        {
            get => lbIcon.Padding;
            set => lbIcon.Padding = value;
        }
        /// <summary>
        /// 获取或设置Icon的宽度或者高度
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置Icon的宽度或者高度"), Browsable(true)]
        public int IconSize
        {
            get
            {
                switch (IconDock)
                {
                    case IconDockStyle.Left: return lbIcon.Width;
                    case IconDockStyle.Right: return lbIcon.Width;
                    default: return lbIcon.Height;
                }
            }
            set
            {
                switch (IconDock)
                {
                    case IconDockStyle.Left:
                    case IconDockStyle.Right: lbIcon.Width = value; break;
                    default: lbIcon.Height = value; break;
                }

            }
        }
        /// <summary>
        /// 获取或设置Icon的前景色
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置Icon的前景色"), Browsable(true)]
        public Color IconForeColor
        {
            get => _iconForeColor;
            set
            {
                _iconForeColor = value;
                lbIcon.ForeColor = value;
            }
        }
        /// <summary>
        /// 获取或设置鼠标进入时Icon的前景色
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置鼠标进入时Icon的前景色"), Browsable(true)]
        public Color IconForeColorEnter { get; set; }
        /// <summary>
        /// 获取或设置鼠标按下时Icon的前景色
        /// </summary>
        [Category("Icon 属性"), Description("获取或设置鼠标按下时Icon的前景色"), Browsable(true)]
        public Color IconForeColorDown { get; set; }

        //外观属性
        /// <summary>
        /// 获取或设置控件背景色
        /// </summary>
        [Category("Button 外观"), Description("获取或设置控件背景色"), Browsable(true)]
        public Color BackColorN
        {
            get
            {
                return _backColorLeave;
            }
            set
            {
                _backColorLeave = value;
                BackColor = value;
            }
        }
        /// <summary>
        /// 获取或设置鼠标进入时的背景色
        /// </summary>
        [Category("Button 外观"), Description("获取或设置鼠标进入时的背景色"), Browsable(true)]
        public Color BackColorEnter { get; set; }
        /// <summary>
        /// 获取或设置鼠标按下时的背景色
        /// </summary>
        [Category("Button 外观"), Description("获取或设置鼠标按下时的背景色"), Browsable(true)]
        public Color BackColorDown { get; set; }
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
            }
        }
        

        //隐藏属性
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new string Text { get; set; }
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new Color ForeColor { get; set; }
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new Font Font { get; set; }
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new Image BackgroundImage { get; set; }
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new ImageLayout BackgroundImageLayout { get; set; }
        /// <summary>
        /// 不使用
        /// </summary>
        [Browsable(false)]
        public new Padding Padding { get; set; }

        //public int BorderSize { get; set; }
        //public Color BorderDown { get; set; }
        //public Color BorderEnter { get; set; }
        //public Color BorderLeave { get; set; }
        //private Color BorderColor;

        //枚举
        /// <summary>
        /// 表示Icon的方位
        /// </summary>
        public enum IconDockStyle : int
        {
            /// <summary>
            /// Icon 在左侧
            /// </summary>
            Left,
            /// <summary>
            /// Icon 在上方
            /// </summary>
            Top,
            /// <summary>
            /// Icon 在右侧
            /// </summary>
            Right,
            /// <summary>
            /// Icon 在下方
            /// </summary>
            Bottom,
        }

        private void AIconButton_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
