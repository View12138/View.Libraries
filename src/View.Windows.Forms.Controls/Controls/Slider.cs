using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using View.Windows.Forms.Animations;
using View.Windows.Forms.Controls.Enums;

namespace View.Windows.Forms.Controls
{
    /// <summary>
    /// 滑块控件
    /// </summary>
    [DefaultEvent("SliderChanged"), DefaultProperty("Index")]
    public partial class Slider : UserControl
    {
        private int _startNum;

        private int _endNum;

        private int _index;

        private Point mouse_offset;

        private Color buttonColor;

        private Image imgBack;

        private Image imgButton;

        private Label label;

        private Animation anim;

        /// <summary>
		/// 构造器
		/// </summary>
		public Slider()
        {
            InitializeComponent();
            anim = new Animation(60);
            _startNum = 0;
            _endNum = 100;
            _index = 0;
            buttonColor = ControlSet.ThemeColor;
            DrawImage();
            label = new Label
            {
                Font = new Font("微软雅黑", 10f),
                Padding = new Padding(5, 5, 5, 5),
                Text = Index.ToString(),
                AutoSize = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            ControlSet.ThemeStyleChanged += (sender, e) =>
            {
                label.BackColor = ControlSet.BackColor;
                label.ForeColor = ControlSet.ForeColor;
                DrawImage();
            };
            ControlSet.ThemeColorChanged += (sender, e) =>
            {
                buttonColor = ControlSet.ThemeColor;
                DrawImage();
            };
        }

        /// <summary>
		/// 滑块位置改变事件
		/// </summary>
		[Category("Slider 事件"), Description("滑块位置改变事件")]
        public event EventHandler SliderChanged;

        /// <summary>
        /// 开始的数字
        /// </summary>
        [Category("Slider 属性"), Description("开始的数字")]
        public int StartNum
        {
            get
            {
                return this._startNum;
            }
            set
            {
                bool flag = value > this.EndNum;
                if (flag)
                {
                    this._startNum = this.EndNum;
                }
                else
                {
                    this._startNum = value;
                }
                this.Index = this._index;
            }
        }

        /// <summary>
        /// 结束的数字大于开始的数字
        /// </summary>
        [Category("Slider 属性"), Description("结束的数字大于开始的数字")]
        public int EndNum
        {
            get
            {
                return this._endNum;
            }
            set
            {
                bool flag = value < this.StartNum;
                if (flag)
                {
                    this._endNum = this.StartNum;
                }
                else
                {
                    this._endNum = value;
                }
                this.Index = this._index;
            }
        }

        /// <summary>
        /// 滑块的数字
        /// </summary>
        [Category("Slider 属性"), Description("滑块的数字")]
        public int Index
        {
            get
            {
                return this._index;
            }
            set
            {
                bool flag = value < this.StartNum;
                if (flag)
                {
                    this._index = this.StartNum;
                }
                else
                {
                    bool flag2 = value > this.EndNum;
                    if (flag2)
                    {
                        this._index = this.EndNum;
                    }
                    else
                    {
                        this._index = value;
                    }
                }
                float num = (float)(this.Index - this.StartNum) / ((float)this.EndNum - (float)this.StartNum * 1f);
                int num2 = (int)((float)(this.pbBack.Width - this.pbButton.Width) * num);
                int num3 = num2;
                bool flag3 = num3 < 0;
                if (flag3)
                {
                    num3 = 0;
                }
                bool flag4 = num3 > this.pbBack.Width - this.pbButton.Width;
                if (flag4)
                {
                    num3 = this.pbBack.Width - this.pbButton.Width;
                }
                anim.Set(pbButton, Properties.Left, num3);
                this.DrawImage();
                this.SliderChanged?.Invoke(this, new EventArgs());
            }
        }

        private void DrawImage()
        {
            float num = (float)(Index - StartNum) / ((float)EndNum - ((float)StartNum * 1f));
            int num2 = (int)((float)(pbBack.Width - pbButton.Width) * num);
            this.imgBack = new Bitmap(pbBack.Width, pbBack.Height);
            using (Graphics graphics = Graphics.FromImage(imgBack))
            {
                graphics.FillRectangle(new SolidBrush(ControlSet.ThemeColor), new Rectangle(0, 0, num2, this.imgBack.Height));
                graphics.FillRectangle(new SolidBrush(Color.FromArgb(102, 102, 102)), new Rectangle(num2, 0, this.imgBack.Width - num2, this.imgBack.Height));
            }
            imgButton = new Bitmap(pbButton.Width, pbButton.Height);
            using (Graphics graphics2 = Graphics.FromImage(imgButton))
            {
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                int num3 = imgButton.Width / 2;
                int width = imgButton.Width;
                graphics2.FillRectangle(new SolidBrush(buttonColor), new Rectangle(-1, num3 - 1, width + 1, this.imgButton.Height - width + 1));
                graphics2.FillEllipse(new SolidBrush(buttonColor), new Rectangle(0, 0, width - 1, width - 1));
                graphics2.FillEllipse(new SolidBrush(buttonColor), new Rectangle(0, imgButton.Height - width, width - 1, width - 1));
            }
            this.pbBack.Image = this.imgBack;
            this.pbButton.Image = this.imgButton;
        }

        private void pbButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouse_offset = new Point(-e.X, -e.Y);
            this.buttonColor = ((ControlSet.ThemeStyle == ThemeType.Light) ? Color.FromArgb(204, 204, 204) : Color.FromArgb(118, 118, 118));
            this.DrawImage();
            this.label.Text = this.Index.ToString();
            base.ParentForm.Controls.Add(this.label);
            Point p = base.PointToScreen(this.pbButton.Location);
            Point point = base.ParentForm.PointToClient(p);
            this.label.Top = point.Y - this.label.Height - 10;
            this.label.Left = point.X - (this.label.Width - this.pbButton.Width) / 2;
            this.label.BackColor = this.BackColor;
            this.label.ForeColor = this.BackColor;
            bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
            if (flag)
            {
                this.anim.Set(this.label, Properties.Backcolor, Color.FromArgb(43, 43, 43));
                this.anim.Set(this.label, Properties.Forecolor, Color.FromArgb(239, 239, 239));
            }
            else
            {
                this.anim.Set(this.label, Properties.Backcolor, Color.FromArgb(242, 242, 242));
                this.anim.Set(this.label, Properties.Forecolor, Color.FromArgb(33, 33, 33));
            }
            this.label.BringToFront();
            this.label.Show();
        }

        private void pbButton_MouseMove(object sender, MouseEventArgs e)
        {
            bool flag = e.Button == MouseButtons.Left;
            if (flag)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(this.mouse_offset.X, this.mouse_offset.Y);
                int num = ((Control)sender).Parent.PointToClient(mousePosition).X;
                bool flag2 = num < 0;
                if (flag2)
                {
                    num = 0;
                }
                bool flag3 = num > this.pbBack.Width - this.pbButton.Width;
                if (flag3)
                {
                    num = this.pbBack.Width - this.pbButton.Width;
                }
                this.pbButton.Left = num;
                float num2 = (float)pbButton.Left / ((float)pbBack.Width - ((float)pbButton.Width * 1f));
                int index = this.Index;
                _index = (int)(((EndNum - StartNum) * num2) + 0.5) + StartNum;
                DrawImage();
                Point p = PointToScreen(pbButton.Location);
                Point point = ParentForm.PointToClient(p);
                anim.Set(label, Properties.Left, point.X - ((label.Width - pbButton.Width) / 2));
                label.Text = Index.ToString();
                bool flag4 = index != Index;
                if (flag4)
                {
                    this.SliderChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private void pbButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.Index = this._index;
            this.buttonColor = ((ControlSet.ThemeStyle == ThemeType.Dark) ? Color.White : Color.Black);
            this.DrawImage();
            base.ParentForm.Controls.Remove(this.label);
        }

        private void pbButton_MouseEnter(object sender, EventArgs e)
        {
            this.buttonColor = ((ControlSet.ThemeStyle == ThemeType.Dark) ? Color.White : Color.Black);
            this.DrawImage();
        }

        private void pbButton_MouseLeave(object sender, EventArgs e)
        {
            this.buttonColor = ControlSet.ThemeColor;
            this.DrawImage();
        }

        private void Slider_SizeChanged(object sender, EventArgs e)
        {
            this.pbBack.Width = base.Width;
            float num = (float)(this.Index - this.StartNum) / ((float)this.EndNum - (float)this.StartNum * 1f);
            int num2 = (int)((float)(this.pbBack.Width - this.pbButton.Width) * num);
            int num3 = num2;
            bool flag = num3 < 0;
            if (flag)
            {
                num3 = 0;
            }
            bool flag2 = num3 > this.pbBack.Width - this.pbButton.Width;
            if (flag2)
            {
                num3 = this.pbBack.Width - this.pbButton.Width;
            }
            this.pbButton.Left = num3;
            this.DrawImage();
        }

        private void Slider_MouseDown(object sender, MouseEventArgs e)
        {
            int num = e.X;
            bool flag = num > this.pbBack.Width - this.pbButton.Width / 2;
            if (flag)
            {
                num = this.pbBack.Width - this.pbButton.Width / 2;
            }
            bool flag2 = num < this.pbButton.Width / 2;
            if (flag2)
            {
                num = this.pbButton.Width / 2;
            }
            this.pbButton.Left = num;
            this.pbButton_MouseDown(this.pbButton, new MouseEventArgs(e.Button, e.Clicks, 4, e.Y, e.Delta));
        }

        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            this.pbButton_MouseMove(this.pbButton, new MouseEventArgs(e.Button, e.Clicks, 4, e.Y, e.Delta));
        }

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            this.pbButton_MouseUp(this.pbButton, new MouseEventArgs(e.Button, e.Clicks, 4, e.Y, e.Delta));
        }
    }
}
