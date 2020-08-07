using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View.Windows.Forms.Controls.Enums;

namespace View.Windows.Forms.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Text")]
    public partial class ViewButton : UserControl
    {
        private string _buttonText;

        private Font _buttonFont;

        private Image image;

        private Color backColor;

        private Color borderColor;

        /// <summary>
        /// 构造器
        /// </summary>
        public ViewButton()
        {
            this.InitializeComponent();
            this._buttonFont = this.label1.Font;
            this.ButtonText = "button";
            this.backColor = Color.FromArgb(51, 51, 51);
            this.borderColor = Color.FromArgb(51, 51, 51);
            this.DrawImage();
            ControlSet.ThemeStyleChanged += (object sender, EventArgs e)=>
            {
                bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
                if (flag)
                {
                    this.label1.ForeColor = Color.White;
                    this.backColor = Color.FromArgb(51, 51, 51);
                    this.borderColor = Color.FromArgb(51, 51, 51);
                }
                else
                {
                    this.label1.ForeColor = Color.Black;
                    this.backColor = Color.FromArgb(204, 204, 204);
                    this.borderColor = Color.FromArgb(204, 204, 204);
                }
                this.DrawImage();
            };
            ControlSet.ThemeColorChanged += delegate (object sender, EventArgs e)
            {
                this.DrawImage();
            };
        }

        /// <summary>
		/// 按钮文本
		/// </summary>
		[Category("ViewButton 属性"), Description("按钮文本")]
        public string ButtonText
        {
            get
            {
                return this._buttonText;
            }
            set
            {
                this._buttonText = value;
                this.label1.Text = this._buttonText;
            }
        }

        /// <summary>
        /// 按钮的字体
        /// </summary>
        [Category("ViewButton 属性"), Description("按钮的字体")]
        public Font ButtonFont
        {
            get
            {
                return this._buttonFont;
            }
            set
            {
                this._buttonFont = value;
                this.label1.Font = this._buttonFont;
            }
        }

        private void DrawImage()
        {
            this.image = new Bitmap(this.label1.Width, this.label1.Height);
            using (Graphics graphics = Graphics.FromImage(this.image))
            {
                graphics.FillRectangle(new SolidBrush(this.backColor), 0, 0, this.label1.Width - 1, this.label1.Height - 1);
                graphics.DrawRectangle(new Pen(this.borderColor, 2f), 1, 1, this.label1.Width - 2, this.label1.Height - 2);
            }
            this.label1.Image = this.image;
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
            if (flag)
            {
                this.backColor = Color.FromArgb(51, 51, 51);
                this.borderColor = Color.FromArgb(133, 133, 133);
            }
            else
            {
                this.backColor = Color.FromArgb(204, 204, 204);
                this.borderColor = Color.FromArgb(122, 122, 122);
            }
            this.DrawImage();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
            if (flag)
            {
                this.backColor = Color.FromArgb(51, 51, 51);
                this.borderColor = Color.FromArgb(51, 51, 51);
            }
            else
            {
                this.backColor = Color.FromArgb(204, 204, 204);
                this.borderColor = Color.FromArgb(204, 204, 204);
            }
            this.DrawImage();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
            if (flag)
            {
                this.backColor = ControlSet.ThemeColor;
                this.borderColor = ControlSet.ThemeColor;
            }
            else
            {
                this.backColor = ControlSet.ThemeColor;
                this.borderColor = ControlSet.ThemeColor;
            }
            this.DrawImage();
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            bool flag = ControlSet.ThemeStyle == ThemeType.Dark;
            if (flag)
            {
                this.backColor = Color.FromArgb(51, 51, 51);
                this.borderColor = Color.FromArgb(133, 133, 133);
            }
            else
            {
                this.backColor = Color.FromArgb(204, 204, 204);
                this.borderColor = Color.FromArgb(122, 122, 122);
            }
            this.DrawImage();
            this.OnClick(new EventArgs());
        }

        private void ViewButton_SizeChanged(object sender, EventArgs e)
        {
            this.DrawImage();
        }

    }
}
