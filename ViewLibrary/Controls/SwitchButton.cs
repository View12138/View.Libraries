using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View.Animations;
using View.Enums;
using System.Drawing.Drawing2D;

namespace View.Controls
{
    /// <summary>
    /// 开关按钮
    /// </summary>
    [DefaultEvent("SwitchChanged"), DefaultProperty("IsOpen")]
    public partial class SwitchButton : UserControl
    {
        private bool _isOpen;

        private bool _isClose;

        private string _textOpen;

        private string _textClose;

        private TextDirect _textDir;

        private Image imageBackOpen;

        private Image imageBackClose;

        private Image imageForeOpen;

        private Image imageForeClose;

        private Animation anim;

        private MouseType mouseType;

        /// <summary>
		/// 构造器
		/// </summary>
		public SwitchButton()
        {
            this.InitializeComponent();
            this.anim = new Animation(60);
            this._textDir = TextDirect.Right;
            this.TextOpen = "开";
            this.TextClose = "关";
            this._isOpen = true;
            this._isClose = true;
            this.mouseType = MouseType.Leave;
            this.pbFore.Parent = this.pbBack;
            this.pbFore.BringToFront();
            ControlSet.ThemeStyleChanged += delegate (object sender, EventArgs e)
            {
                this.DrawImage();
            };
            ControlSet.ThemeColorChanged += delegate (object sender, EventArgs e)
            {
                this.DrawImage();
            };
        }

        /// <summary>
		/// 开关状态改变事件
		/// </summary>
		[Category("SwitchButton 事件"), Description("开关状态改变事件")]
        public event EventHandler SwitchChanged;

        /// <summary>
        /// 开关的状态
        /// </summary>
        [Category("SwitchButton 属性"), DefaultValue(true), Description("开关的状态")]
        public bool IsOpen
        {
            get
            {
                return this._isOpen;
            }
            set
            {
                this._isOpen = value;
                this.DrawImage();
                this.SwitchChanged?.Invoke(this, new EventArgs());
                bool isOpen = this._isOpen;
                if (isOpen)
                {
                    this.labelText.Text = this.TextOpen;
                    this.pbFore.Top = 5;
                    this.anim.Set(this.pbFore, Animation.Prop.Left, this.pbBack.Width - 5 - this.pbFore.Width);
                }
                else
                {
                    this.labelText.Text = this.TextClose;
                    this.pbFore.Top = 5;
                    this.anim.Set(this.pbFore, Animation.Prop.Left, 5);
                }
                bool flag = this.labelText.Text == "" || this.labelText.Text == null;
                if (flag)
                {
                    base.Width = this.pbBack.Width;
                }
                else
                {
                    base.Width = this.labelText.Width + this.pbBack.Width + 6;
                }
            }
        }

        /// <summary>
        /// 按钮打开时的文本
        /// </summary>
        [Category("SwitchButton 属性"), DefaultValue("开"), Description("按钮打开时的文本")]
        public string TextOpen
        {
            get
            {
                return this._textOpen;
            }
            set
            {
                this._textOpen = value;
                bool isOpen = this.IsOpen;
                if (isOpen)
                {
                    this.labelText.Text = this._textOpen;
                }
            }
        }

        /// <summary>
        /// 按钮关闭时的文本
        /// </summary>
        [Category("SwitchButton 属性"), DefaultValue("关"), Description("按钮关闭时的文本")]
        public string TextClose
        {
            get
            {
                return this._textClose;
            }
            set
            {
                this._textClose = value;
                bool flag = !this.IsOpen;
                if (flag)
                {
                    this.labelText.Text = this._textClose;
                }
            }
        }

        /// <summary>
        /// 控件的文本位置
        /// </summary>
        [Category("SwitchButton 属性"), Description("控件的文本位置")]
        public TextDirect TextDir
        {
            get
            {
                return this._textDir;
            }
            set
            {
                this._textDir = value;
                bool flag = this._textDir == TextDirect.Left;
                if (flag)
                {
                    this.labelText.Dock = DockStyle.Left;
                    this.pbBack.Dock = DockStyle.Right;
                }
                else
                {
                    bool flag2 = this._textDir == TextDirect.Right;
                    if (flag2)
                    {
                        this.labelText.Dock = DockStyle.Right;
                        this.pbBack.Dock = DockStyle.Left;
                    }
                }
            }
        }

        /// <summary>
        /// 按钮是否绘制关闭状态
        /// </summary>
        [Category("SwitchButton 属性"), DefaultValue(true), Description("按钮是否绘制关闭状态")]
        public bool IsClose
        {
            get
            {
                return this._isClose;
            }
            set
            {
                this._isClose = value;
            }
        }

        /// <summary>
		/// 绘制图案
		/// </summary>
		private void DrawImage()
        {
            int num = base.Height / 2;
            bool flag = ControlSet.ThemeStyle == ThemeType.Light;
            if (flag)
            {
                Brush brush = new SolidBrush(Color.Black);
                this.labelText.ForeColor = Color.Black;
            }
            else
            {
                Brush brush = new SolidBrush(Color.White);
                this.labelText.ForeColor = Color.White;
            }
            Color color = ControlSet.ThemeColor;
            bool isOpen = this._isOpen;
            if (isOpen)
            {
                this.imageBackOpen = new Bitmap(this.pbBack.Width, this.pbBack.Height);
                using (Graphics graphics = Graphics.FromImage(this.imageBackOpen))
                {
                    bool flag2 = ControlSet.ThemeStyle == ThemeType.Dark;
                    if (flag2)
                    {
                        switch (this.mouseType)
                        {
                            case MouseType.Enter:
                            case MouseType.Up:
                            {
                                int red = (int)((ControlSet.ThemeColor.R - 10 < 0) ? 0 : (ControlSet.ThemeColor.R - 10));
                                int green = (int)((ControlSet.ThemeColor.G - 4 < 0) ? 0 : (ControlSet.ThemeColor.G - 4));
                                int blue = (int)((ControlSet.ThemeColor.B - 26 < 0) ? 0 : (ControlSet.ThemeColor.B - 26));
                                color = Color.FromArgb(red, green, blue);
                                break;
                            }
                            case MouseType.Leave:
                                color = ControlSet.ThemeColor;
                                break;
                            case MouseType.Down:
                                color = Color.FromArgb(153, 153, 153);
                                break;
                        }
                    }
                    else
                    {
                        switch (this.mouseType)
                        {
                            case MouseType.Enter:
                            case MouseType.Up:
                            {
                                int red2 = (int)((ControlSet.ThemeColor.R + 47 > 255) ? 255 : (ControlSet.ThemeColor.R + 47));
                                int green2 = (int)((ControlSet.ThemeColor.G + 65 > 255) ? 255 : (ControlSet.ThemeColor.G + 65));
                                int blue2 = (int)((ControlSet.ThemeColor.B + 30 > 255) ? 255 : (ControlSet.ThemeColor.B + 30));
                                color = Color.FromArgb(red2, green2, blue2);
                                break;
                            }
                            case MouseType.Leave:
                                color = ControlSet.ThemeColor;
                                break;
                            case MouseType.Down:
                                color = Color.FromArgb(102, 102, 102);
                                break;
                        }
                    }
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.Clear(Color.Transparent);
                    graphics.FillEllipse(new SolidBrush(color), new Rectangle(0, 0, 2 * num - 1, 2 * num - 1));
                    graphics.FillEllipse(new SolidBrush(color), new Rectangle(this.imageBackOpen.Width - num * 2, 0, 2 * num - 1, 2 * num - 1));
                    graphics.FillRectangle(new SolidBrush(color), new Rectangle(num, 0, this.imageBackOpen.Width - this.imageBackOpen.Height - 1, 2 * num - 1));
                }
                this.imageForeOpen = new Bitmap(this.pbFore.Width, this.pbFore.Height);
                using (Graphics graphics2 = Graphics.FromImage(this.imageForeOpen))
                {
                    graphics2.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics2.Clear(color);
                    graphics2.FillEllipse(new SolidBrush(Color.White), new Rectangle(0, 0, this.imageForeOpen.Width - 1, this.imageForeOpen.Height - 1));
                }
                this.pbBack.Image = this.imageBackOpen;
                this.pbFore.Image = this.imageForeOpen;
            }
            else
            {
                this.imageBackClose = new Bitmap(this.pbBack.Width, this.pbBack.Height);
                using (Graphics graphics3 = Graphics.FromImage(this.imageBackClose))
                {
                    graphics3.SmoothingMode = SmoothingMode.AntiAlias;
                    bool isClose = this.IsClose;
                    if (isClose)
                    {
                        bool flag3 = ControlSet.ThemeStyle == ThemeType.Dark;
                        if (flag3)
                        {
                            switch (this.mouseType)
                            {
                                case MouseType.Enter:
                                case MouseType.Up:
                                    color = Color.White;
                                    break;
                                case MouseType.Leave:
                                    color = Color.FromArgb(204, 204, 204);
                                    break;
                                case MouseType.Down:
                                    color = Color.FromArgb(153, 153, 153);
                                    break;
                            }
                        }
                        else
                        {
                            switch (this.mouseType)
                            {
                                case MouseType.Enter:
                                case MouseType.Up:
                                    color = Color.Black;
                                    break;
                                case MouseType.Leave:
                                    color = Color.FromArgb(51, 51, 51);
                                    break;
                                case MouseType.Down:
                                    color = Color.FromArgb(102, 102, 102);
                                    break;
                            }
                        }
                        Pen pen = new Pen(color, 2f);
                        graphics3.Clear(Color.Transparent);
                        graphics3.DrawArc(pen, new Rectangle(1, 0, 2 * num - 1, 2 * num - 1), 90f, 180f);
                        graphics3.DrawArc(pen, new Rectangle(this.imageBackClose.Width - num * 2 - 1, 0, 2 * num - 1, 2 * num - 1), 270f, 180f);
                        graphics3.DrawLine(pen, num, 0, this.imageBackClose.Width - num, 0);
                        graphics3.DrawLine(pen, num, 2 * num - 1, this.imageBackClose.Width - num, 2 * num - 1);
                    }
                    else
                    {
                        bool flag4 = ControlSet.ThemeStyle == ThemeType.Dark;
                        if (flag4)
                        {
                            switch (this.mouseType)
                            {
                                case MouseType.Enter:
                                case MouseType.Up:
                                {
                                    int red3 = (int)((ControlSet.ThemeColor.R - 10 < 0) ? 0 : (ControlSet.ThemeColor.R - 10));
                                    int green3 = (int)((ControlSet.ThemeColor.G - 4 < 0) ? 0 : (ControlSet.ThemeColor.G - 4));
                                    int blue3 = (int)((ControlSet.ThemeColor.B - 26 < 0) ? 0 : (ControlSet.ThemeColor.B - 26));
                                    color = Color.FromArgb(red3, green3, blue3);
                                    break;
                                }
                                case MouseType.Leave:
                                    color = ControlSet.ThemeColor;
                                    break;
                                case MouseType.Down:
                                    color = Color.FromArgb(153, 153, 153);
                                    break;
                            }
                        }
                        else
                        {
                            switch (this.mouseType)
                            {
                                case MouseType.Enter:
                                case MouseType.Up:
                                {
                                    int red4 = (int)((ControlSet.ThemeColor.R + 47 > 255) ? 255 : (ControlSet.ThemeColor.R + 47));
                                    int green4 = (int)((ControlSet.ThemeColor.G + 65 > 255) ? 255 : (ControlSet.ThemeColor.G + 65));
                                    int blue4 = (int)((ControlSet.ThemeColor.B + 30 > 255) ? 255 : (ControlSet.ThemeColor.B + 30));
                                    color = Color.FromArgb(red4, green4, blue4);
                                    break;
                                }
                                case MouseType.Leave:
                                    color = ControlSet.ThemeColor;
                                    break;
                                case MouseType.Down:
                                    color = Color.FromArgb(102, 102, 102);
                                    break;
                            }
                        }
                        graphics3.Clear(Color.Transparent);
                        graphics3.FillEllipse(new SolidBrush(color), new Rectangle(0, 0, 2 * num - 1, 2 * num - 1));
                        graphics3.FillEllipse(new SolidBrush(color), new Rectangle(this.imageBackOpen.Width - num * 2, 0, 2 * num - 1, 2 * num - 1));
                        graphics3.FillRectangle(new SolidBrush(color), new Rectangle(num, 0, this.imageBackOpen.Width - this.imageBackOpen.Height - 1, 2 * num - 1));
                    }
                }
                this.imageForeClose = new Bitmap(this.pbFore.Width, this.pbFore.Height);
                using (Graphics graphics4 = Graphics.FromImage(this.imageForeClose))
                {
                    graphics4.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics4.Clear(Color.Transparent);
                    bool flag5 = !this.IsClose;
                    if (flag5)
                    {
                        graphics4.FillEllipse(new SolidBrush(Color.White), new Rectangle(0, 0, this.imageForeClose.Width - 1, this.imageForeClose.Height - 1));
                    }
                    else
                    {
                        bool flag6 = ControlSet.ThemeStyle == ThemeType.Dark;
                        if (flag6)
                        {
                            graphics4.FillEllipse(new SolidBrush(Color.White), new Rectangle(0, 0, this.imageForeClose.Width - 1, this.imageForeClose.Height - 1));
                        }
                        else
                        {
                            graphics4.FillEllipse(new SolidBrush(Color.Black), new Rectangle(0, 0, this.imageForeClose.Width - 1, this.imageForeClose.Height - 1));
                        }
                    }
                }
                this.pbBack.Image = this.imageBackClose;
                this.pbFore.Image = this.imageForeClose;
            }
        }

        private void pbBack_Click(object sender, EventArgs e)
        {
        }

        private void SwitchButton_Load(object sender, EventArgs e)
        {
            this.IsOpen = this._isOpen;
        }

        private void SwitchButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseType = MouseType.Down;
            this.DrawImage();
        }

        private void SwitchButton_MouseEnter(object sender, EventArgs e)
        {
            this.mouseType = MouseType.Enter;
            this.DrawImage();
        }

        private void SwitchButton_MouseLeave(object sender, EventArgs e)
        {
            this.mouseType = MouseType.Leave;
            this.DrawImage();
        }

        private void SwitchButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseType = MouseType.Up;
            this.DrawImage();
            this.IsOpen = !this.IsOpen;
            EventHandler expr_25 = this.SwitchChanged;
            if (expr_25 != null)
            {
                expr_25(this, e);
            }
        }
    }
}
