using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using View.Enums;
using View.Win32API;
using View.Animations;

namespace View.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultEvent("SelectedChangedWithMouse"), DefaultProperty("Text")]
    public partial class ViewComboBox : UserControl
    {
        private Animation animation;
        private MouseHook mh;
        private Panel panelSelect;
        private string _showText;
        private Color borderColor;
        private Color backColor;
        private Color foreColor;
        private Color itemBackColor;
        private int _selectedIndex;
        private Label[] labels;

        /// <summary>
        /// 构造器
        /// </summary>
        public ViewComboBox()
        {
            InitializeComponent();
            animation = new Animation(40);
            lbIcon.Text = "";
            backColor = Color.FromArgb(0, 0, 0);
            borderColor = Color.FromArgb(102, 102, 102);
            foreColor = Color.FromArgb(236, 236, 236);
            itemBackColor = Color.FromArgb(43, 43, 43);
            lbIcon.ForeColor = Color.FromArgb(159, 159, 159);
            Items = new string[]
            {
                "item1"
            };
            MaxHeight = 50;
            SelectedIndex = -1;
            DrawImage();
            panelSelect = new Panel
            {
                BackColor = Color.FromArgb(43, 43, 43),
                Width = Width,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle
            };
            mh = new MouseHook();
            ControlSet.ThemeStyleChanged += (object sender, EventArgs e) =>
            {
                if (ControlSet.ThemeStyle == ThemeType.Dark)
                {
                    foreColor = Color.FromArgb(255, 255, 255);
                    backColor = Color.FromArgb(0, 0, 0);
                    borderColor = Color.FromArgb(102, 102, 102);
                    lbIcon.ForeColor = Color.FromArgb(159, 159, 159);
                    panelSelect.BackColor = Color.FromArgb(43, 43, 43);
                }
                else
                {
                    foreColor = Color.FromArgb(0, 0, 0);
                    backColor = Color.FromArgb(255, 255, 255);
                    borderColor = Color.FromArgb(153, 153, 153);
                    lbIcon.ForeColor = Color.FromArgb(135, 135, 135);
                    panelSelect.BackColor = Color.FromArgb(242, 242, 242);
                }
                DrawImage();
            };
            ControlSet.ThemeColorChanged += (object sender, EventArgs e) =>
            {
                DrawImage();
            };
        }

        /// <summary>
		/// SelectedIndex 改变时发生
		/// </summary>
        [Browsable(true), Category("事件"), Description("SelectedIndex 改变时发生")]
        public event EventHandler SelectedChanged;
        /// <summary>
        /// 鼠标选择某项时发生
        /// </summary>
        [Browsable(true), Category("事件"), Description("鼠标选择某项时发生")]
        public event EventHandler SelectedChangedWithMouse;
        /// <summary>
        /// 当没有选中时显示的文本
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("当没有选中时显示的文本")]
        public string ShowText
        {
            get
            {
                return _showText;
            }
            set
            {
                _showText = value;
                lbText.Text = _showText;
            }
        }
        /// <summary>
        /// 列表中包含的项目
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("列表中包含的项目")]
        public string[] Items
        {
            get
            {
                string[] array = new string[labels.Length];
                for (int i = 0; i < labels.Length; i++)
                {
                    array[i] = labels[i].Text;
                }
                return array;
            }
            set
            {
                labels = new Label[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    labels[i] = GetLabel(value[i]);
                    labels[i].TabIndex = i;
                    labels[i].AutoSize = false;
                    labels[i].Width = Width - 4;
                }
            }
        }
        /// <summary>
        /// 与 Items 对应的值
        /// <para>请在代码中写入与 Items 对应的值</para>
        /// <para>例如 ItemsValues[1] = Color.Red</para>
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("请在代码中写入与 Items 对应的值\r\n例如:ItemsValues[1] = Color.Red;")]
        public object[] ItemsValues { get; set; }
        /// <summary>
        /// 当前选中项
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("当前选中项")]
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                if (SelectedIndex == -1)
                {
                    lbText.Text = ShowText;
                }
                else
                {
                    lbText.Text = labels[SelectedIndex].Text;
                }
                SelectedChanged?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// 下拉框的最大高度
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("下拉框的最大高度")]
        public int MaxHeight { get; set; }
        /// <summary>
        /// 是否自动调整最大高度
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("当下拉框超过父容器时,是否自动调整最大高度"), DefaultValue(false)]
        public bool AutoMaxHeight { get; set; }
        /// <summary>
        /// 指示是否使用动画效果
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("指示是否使用动画效果"), DefaultValue(false)]
        public bool UseAnimation { get; set; }
        /// <summary>
        /// 指示是否自动上拉
        /// </summary>
        [Browsable(true), Category("ViewComboBox 属性"), Description("指示是否自动上拉"), DefaultValue(false)]
        public bool AutoMove { get; set; }
        /// <summary>
        /// 获取或设置字体
        /// </summary>
        [Browsable(true), Category("外观"), Description("获取或设置字体")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }

            set
            {
                base.Font = value;
                lbIcon.Font = new Font("Segoe MDL2 Assets", value.Size, value.Style, value.Unit);
                lbText.Font = value;
            }
        }

        private void Mh_MouseDownEvent(object sender, MouseEventArgs e)
        {//在其控件以外的地方按下鼠标
            if (panelSelect.PointToClient(MousePosition).X < 0 ||
                panelSelect.PointToClient(MousePosition).X > panelSelect.Width ||
                panelSelect.PointToClient(MousePosition).Y < 0 ||
                panelSelect.PointToClient(MousePosition).Y > panelSelect.Height)
            {
                Parent.Controls.Remove(panelSelect);
                panelSelect.MouseUp -= PanelSelect_MouseUp;
                mh.MouseDownEvent -= Mh_MouseDownEvent;
                mh.UnHook();
            }
        }

        private void DrawImage()
        {
            if (UseAnimation)
            {
                animation.Set(this, Animation.Prop.Backcolor, backColor);
                animation.Set(border1, Animation.Prop.Backcolor, borderColor);
                animation.Set(border2, Animation.Prop.Backcolor, borderColor);
                animation.Set(border3, Animation.Prop.Backcolor, borderColor);
                animation.Set(border4, Animation.Prop.Backcolor, borderColor);
                animation.Set(lbText, Animation.Prop.Forecolor, foreColor);
            }
            else
            {
                BackColor = backColor;
                border1.BackColor = borderColor;
                border2.BackColor = borderColor;
                border3.BackColor = borderColor;
                border4.BackColor = borderColor;
                lbText.ForeColor = foreColor;
            }
        }

        private Label GetLabel(string text)
        {
            return new Label
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = lbText.Font,
                Height = lbText.Height,
                Width = Width - 4,
                Padding = new Padding(3, 0, 0, 0),
                ForeColor = lbText.ForeColor,
                BackColor = itemBackColor
            };
        }

        private void ViewComboBox_MouseEnter(object sender, EventArgs e)
        {
            if (ControlSet.ThemeStyle == ThemeType.Dark)
            {
                backColor = Color.FromArgb(0, 0, 0);
                borderColor = Color.FromArgb(153, 153, 153);
            }
            else
            {
                backColor = Color.FromArgb(255, 255, 255);
                borderColor = Color.FromArgb(102, 102, 102);
            }
            DrawImage();
        }

        private void ViewComboBox_MouseLeave(object sender, EventArgs e)
        {
            if (ControlSet.ThemeStyle == ThemeType.Dark)
            {
                backColor = Color.FromArgb(0, 0, 0);
                borderColor = Color.FromArgb(102, 102, 102);
            }
            else
            {
                backColor = Color.FromArgb(255, 255, 255);
                borderColor = Color.FromArgb(153, 153, 153);
            }
            DrawImage();
        }

        private void ViewComboBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (ControlSet.ThemeStyle == ThemeType.Dark)
            {
                backColor = Color.FromArgb(51, 51, 51);
                borderColor = Color.FromArgb(102, 102, 102);
            }
            else
            {
                backColor = Color.FromArgb(204, 204, 204);
                borderColor = Color.FromArgb(153, 153, 153);
            }
            DrawImage();
        }

        private void ViewComboBox_MouseUp(object sender, MouseEventArgs e)
        {
            mh.SetHook();
            mh.MouseDownEvent += Mh_MouseDownEvent;
            if (MaxHeight > Parent.ClientSize.Height - Location.Y && AutoMaxHeight == true)
            {//修正下拉框的最大高度,保证不超过其父容器的裁剪区高度
                MaxHeight = Parent.ClientSize.Height - Location.Y;
            }
            if (ControlSet.ThemeStyle == ThemeType.Dark)
            {
                backColor = Color.FromArgb(0, 0, 0);
                borderColor = Color.FromArgb(153, 153, 153);
            }
            else
            {
                backColor = Color.FromArgb(255, 255, 255);
                borderColor = Color.FromArgb(102, 102, 102);
            }
            DrawImage();
            panelSelect.MouseUp += PanelSelect_MouseUp;
            int x = 1;
            int num = 4;
            panelSelect.Height = lbText.Height;
            panelSelect.Width = Width;
            panelSelect.MaximumSize = new Size(Parent.Width, Parent.Height);
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].MouseUp -= new MouseEventHandler(Label_MouseUp);
                labels[i].MouseEnter -= new EventHandler(Label_MouseEnter);
                labels[i].MouseLeave -= new EventHandler(Label_MouseLeave);
            }
            panelSelect.Controls.Clear();
            int selectHeight = 0;
            for (int j = 0; j < labels.Length; j++)
            {
                labels[j].AutoSize = false;
                labels[j].Location = new Point(x, num);
                labels[j].Width = panelSelect.Width - 4;
                labels[j].Height = lbText.Height;
                if (j == SelectedIndex)
                {
                    labels[j].BackColor = ControlSet.ThemeColor;
                    selectHeight = num;
                }
                else
                {
                    labels[j].BackColor = panelSelect.BackColor;
                }
                num += labels[j].Height + 2;
                labels[j].ForeColor = lbText.ForeColor;
                labels[j].MouseUp += new MouseEventHandler(Label_MouseUp);
                labels[j].MouseEnter += new EventHandler(Label_MouseEnter);
                labels[j].MouseLeave += new EventHandler(Label_MouseLeave);
            }
            num += 4;
            if (num > MaxHeight && MaxHeight != 0)
            {//当列表项目总高度超过最高高度时,为控件添加滚动事件
                //if (UseAnimation)
                //{ animation.Set(panelSelect, Animation.Prop.Height, MaxHeight); }
                //else
                { panelSelect.Height = MaxHeight; }
                panelSelect.MouseWheel -= PanelSelect_MouseWheel;
                panelSelect.MouseWheel += PanelSelect_MouseWheel;
            }
            else
            {
                //if (UseAnimation)
                //{ animation.Set(panelSelect, Animation.Prop.Height, num); }
                //else
                { panelSelect.Height = num; }
            }
            int Y = Location.Y - selectHeight;
            int psH = num < MaxHeight ? num : MaxHeight;
            if (Y <= Location.Y - psH + Height)
            {
                Y = Location.Y - psH + Height;
            }
            if (Y <= 0) { Y = 0; }
            panelSelect.Location = new Point(Location.X, Location.Y);
            if (AutoMove)
            {
                if (UseAnimation)
                { animation.Set(panelSelect, Animation.Prop.Top, Y); }
                else { panelSelect.Location = new Point(Location.X, Y); }
            }
            //if (UseAnimation)
            //{ animation.Set(panelSelect, Animation.Prop.Top, Y); }
            //else { panelSelect.Top = Y; }
            panelSelect.Controls.AddRange(labels);

            {//定位到当前选中项
                if (selectHeight > MaxHeight)
                {
                    int hs = selectHeight - MaxHeight;
                    for (int j = 0; j < labels.Length; j++)
                    {
                        if (UseAnimation)
                        { animation.Set(labels[j], Animation.Prop.Top, labels[j].Top - hs - labels[j].Height - 4); }
                        else { labels[j].Top = labels[j].Top - hs - labels[j].Height - 4; }
                    }
                }
            }
            Parent.Controls.Add(panelSelect);
            panelSelect.BringToFront();
        }

        private void PanelSelect_MouseUp(object sender, MouseEventArgs e)
        {
            Parent.Controls.Remove(panelSelect);
            panelSelect.MouseUp -= PanelSelect_MouseUp;
            mh.MouseDownEvent -= Mh_MouseDownEvent;
            mh.UnHook();
        }

        private void PanelSelect_MouseWheel(object sender, MouseEventArgs e)
        {
            //鼠标滚动
            int h = 0;
            if (e.Delta > 0)
            {
                h = -15;
            }
            else if (e.Delta < 0)
            {
                h = 15;
            }
            else { h = 0; }
            for (int j = 0; j < labels.Length; j++)
            {
                //if (UseAnimation)
                { animation.Set(labels[j], Animation.Prop.Top, labels[j].Top - h); }
                //else { labels[j].Top = labels[j].Top - h; }
            }
            if (labels[0].Top > 4)
            {//上拉不越界
                int num = 4;
                for (int j = 0; j < labels.Length; j++)
                {
                    //if (UseAnimation)
                    { animation.Set(labels[j], Animation.Prop.Top, num); }
                    //else { labels[j].Top = num; }
                    num += labels[j].Height + 2;
                }
            }
            if (labels[labels.Length - 1].Top < MaxHeight - 8 - labels[labels.Length - 1].Height)
            {//下拉不越界
                int num = MaxHeight - 8 - labels[labels.Length - 1].Height;
                for (int j = labels.Length - 1; j >= 0; j--)
                {
                    //if (UseAnimation)
                    { animation.Set(labels[j], Animation.Prop.Top, num); }
                    //else { labels[j].Top = num; }
                    num -= labels[j].Height + 2;
                }
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            if (((Label)sender).TabIndex != SelectedIndex)
            {
                if (ControlSet.ThemeStyle == ThemeType.Dark)
                {
                    if (UseAnimation)
                    { animation.Set((Label)sender, Animation.Prop.Backcolor, Color.FromArgb(43, 43, 43)); }
                    else { ((Label)sender).BackColor = Color.FromArgb(43, 43, 43); }
                }
                else
                {
                    if (UseAnimation)
                    { animation.Set((Label)sender, Animation.Prop.Backcolor, Color.FromArgb(242, 242, 242)); }
                    else
                    { ((Label)sender).BackColor = Color.FromArgb(242, 242, 242); }
                }
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            if (UseAnimation)
            { animation.Set((Label)sender, Animation.Prop.Backcolor, ControlSet.ThemeColor); }
            else
            { ((Label)sender).BackColor = ControlSet.ThemeColor; }
        }

        private void Label_MouseUp(object sender, MouseEventArgs e)
        {
            SelectedIndex = ((Label)sender).TabIndex;
            Parent.Controls.Remove(panelSelect);
            SelectedChangedWithMouse?.Invoke(this, e);
            panelSelect.MouseUp -= PanelSelect_MouseUp;
            mh.MouseDownEvent -= Mh_MouseDownEvent;
            mh.UnHook();
        }

        private void ViewComboBox_SizeChanged(object sender, EventArgs e)
        {
            lbIcon.Width = lbIcon.Height;
            lbText.Width = Width - lbIcon.Width - 2 - 2;
        }
    }
}