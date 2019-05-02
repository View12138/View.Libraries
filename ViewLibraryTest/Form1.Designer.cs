namespace ViewLibraryTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.还原RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.大小SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最小化XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大化XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewButton21 = new View.Controls.ButtonWinForm();
            this.switchButton1 = new View.Controls.SwitchButton();
            this.viewComboBox4 = new View.Controls.ViewComboBox();
            this.viewComboBox3 = new View.Controls.ViewComboBox();
            this.viewComboBox2 = new View.Controls.ViewComboBox();
            this.viewComboBox1 = new View.Controls.ViewComboBox();
            this.viewButton1 = new View.Controls.ViewButton();
            this.slider1 = new View.Controls.Slider();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(98, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem2.Text = "111";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem3.Text = "222";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(97, 22);
            this.toolStripMenuItem4.Text = "333";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.还原RToolStripMenuItem,
            this.移动MToolStripMenuItem,
            this.大小SToolStripMenuItem,
            this.最小化XToolStripMenuItem,
            this.最大化XToolStripMenuItem,
            this.toolStripSeparator1,
            this.关闭CToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip2.Size = new System.Drawing.Size(168, 142);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // 还原RToolStripMenuItem
            // 
            this.还原RToolStripMenuItem.Name = "还原RToolStripMenuItem";
            this.还原RToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.还原RToolStripMenuItem.Text = "还原(R)";
            // 
            // 移动MToolStripMenuItem
            // 
            this.移动MToolStripMenuItem.Name = "移动MToolStripMenuItem";
            this.移动MToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.移动MToolStripMenuItem.Text = "移动(M)";
            // 
            // 大小SToolStripMenuItem
            // 
            this.大小SToolStripMenuItem.Name = "大小SToolStripMenuItem";
            this.大小SToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.大小SToolStripMenuItem.Text = "大小(S)";
            // 
            // 最小化XToolStripMenuItem
            // 
            this.最小化XToolStripMenuItem.Name = "最小化XToolStripMenuItem";
            this.最小化XToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.最小化XToolStripMenuItem.Text = "最小化(N)";
            // 
            // 最大化XToolStripMenuItem
            // 
            this.最大化XToolStripMenuItem.Name = "最大化XToolStripMenuItem";
            this.最大化XToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.最大化XToolStripMenuItem.Text = "最大化(X)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // 关闭CToolStripMenuItem
            // 
            this.关闭CToolStripMenuItem.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.关闭CToolStripMenuItem.Name = "关闭CToolStripMenuItem";
            this.关闭CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.关闭CToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.关闭CToolStripMenuItem.Text = "关闭(C)";
            // 
            // viewButton21
            // 
            this.viewButton21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.viewButton21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.viewButton21.FlatAppearance.BorderSize = 2;
            this.viewButton21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(98)))), ((int)(((byte)(255)))));
            this.viewButton21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.viewButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewButton21.Location = new System.Drawing.Point(224, 110);
            this.viewButton21.Name = "viewButton21";
            this.viewButton21.Size = new System.Drawing.Size(125, 38);
            this.viewButton21.TabIndex = 7;
            this.viewButton21.Text = "Button21";
            this.viewButton21.UseVisualStyleBackColor = false;
            // 
            // switchButton1
            // 
            this.switchButton1.BackColor = System.Drawing.Color.Transparent;
            this.switchButton1.ForeColor = System.Drawing.Color.White;
            this.switchButton1.Location = new System.Drawing.Point(93, 84);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.Size = new System.Drawing.Size(75, 20);
            this.switchButton1.TabIndex = 4;
            this.switchButton1.TextClose = "暗";
            this.switchButton1.TextDir = View.Enums.TextDirect.Right;
            this.switchButton1.TextOpen = "亮";
            this.switchButton1.SwitchChanged += new System.EventHandler(this.switchButton1_SwitchChanged);
            this.switchButton1.Click += new System.EventHandler(this.switchButton1_Click);
            // 
            // viewComboBox4
            // 
            this.viewComboBox4.AutoMove = true;
            this.viewComboBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewComboBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewComboBox4.Items = new string[] {
        "第一项",
        "第二项",
        "第三项",
        "第四项",
        "第五项",
        "第六项",
        "第七项",
        "第八项",
        "第九项",
        "第十项",
        "第十一项",
        "第十二项"};
            this.viewComboBox4.ItemsValues = null;
            this.viewComboBox4.Location = new System.Drawing.Point(224, 191);
            this.viewComboBox4.MaxHeight = 200;
            this.viewComboBox4.Name = "viewComboBox4";
            this.viewComboBox4.SelectedIndex = -1;
            this.viewComboBox4.ShowText = "请选择项目";
            this.viewComboBox4.Size = new System.Drawing.Size(125, 31);
            this.viewComboBox4.TabIndex = 3;
            this.viewComboBox4.UseAnimation = true;
            // 
            // viewComboBox3
            // 
            this.viewComboBox3.AutoMove = true;
            this.viewComboBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewComboBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewComboBox3.Items = new string[] {
        "第一项",
        "第二项",
        "第三项",
        "第四项",
        "第五项",
        "第六项",
        "第七项",
        "第八项",
        "第九项",
        "第十项",
        "第十一项",
        "第十二项"};
            this.viewComboBox3.ItemsValues = null;
            this.viewComboBox3.Location = new System.Drawing.Point(224, 154);
            this.viewComboBox3.MaxHeight = 200;
            this.viewComboBox3.Name = "viewComboBox3";
            this.viewComboBox3.SelectedIndex = -1;
            this.viewComboBox3.ShowText = "请选择项目";
            this.viewComboBox3.Size = new System.Drawing.Size(125, 31);
            this.viewComboBox3.TabIndex = 3;
            // 
            // viewComboBox2
            // 
            this.viewComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewComboBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewComboBox2.Items = new string[] {
        "第一项",
        "第二项",
        "第三项",
        "第四项",
        "第五项",
        "第六项",
        "第七项",
        "第八项",
        "第九项",
        "第十项",
        "第十一项",
        "第十二项"};
            this.viewComboBox2.ItemsValues = null;
            this.viewComboBox2.Location = new System.Drawing.Point(93, 191);
            this.viewComboBox2.MaxHeight = 200;
            this.viewComboBox2.Name = "viewComboBox2";
            this.viewComboBox2.SelectedIndex = -1;
            this.viewComboBox2.ShowText = "请选择项目";
            this.viewComboBox2.Size = new System.Drawing.Size(125, 31);
            this.viewComboBox2.TabIndex = 3;
            this.viewComboBox2.UseAnimation = true;
            // 
            // viewComboBox1
            // 
            this.viewComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.viewComboBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewComboBox1.Items = new string[] {
        "第一项",
        "第二项",
        "第三项",
        "第四项",
        "第五项",
        "第六项",
        "第七项",
        "第八项",
        "第九项",
        "第十项",
        "第十一项",
        "第十二项"};
            this.viewComboBox1.ItemsValues = null;
            this.viewComboBox1.Location = new System.Drawing.Point(93, 154);
            this.viewComboBox1.MaxHeight = 200;
            this.viewComboBox1.Name = "viewComboBox1";
            this.viewComboBox1.SelectedIndex = -1;
            this.viewComboBox1.ShowText = "请选择项目";
            this.viewComboBox1.Size = new System.Drawing.Size(125, 31);
            this.viewComboBox1.TabIndex = 3;
            // 
            // viewButton1
            // 
            this.viewButton1.ButtonFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.viewButton1.ButtonText = "button";
            this.viewButton1.Location = new System.Drawing.Point(93, 110);
            this.viewButton1.Name = "viewButton1";
            this.viewButton1.Size = new System.Drawing.Size(125, 38);
            this.viewButton1.TabIndex = 2;
            this.viewButton1.Click += new System.EventHandler(this.viewButton1_Click);
            // 
            // slider1
            // 
            this.slider1.BackColor = System.Drawing.Color.Transparent;
            this.slider1.EndNum = 100;
            this.slider1.Index = 0;
            this.slider1.Location = new System.Drawing.Point(93, 54);
            this.slider1.MaximumSize = new System.Drawing.Size(500, 24);
            this.slider1.MinimumSize = new System.Drawing.Size(50, 24);
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(189, 24);
            this.slider1.StartNum = 0;
            this.slider1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip2;
            this.Controls.Add(this.viewButton21);
            this.Controls.Add(this.switchButton1);
            this.Controls.Add(this.viewComboBox4);
            this.Controls.Add(this.viewComboBox3);
            this.Controls.Add(this.viewComboBox2);
            this.Controls.Add(this.viewComboBox1);
            this.Controls.Add(this.viewButton1);
            this.Controls.Add(this.slider1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private View.Controls.Slider slider1;
        private View.Controls.ViewButton viewButton1;
        private View.Controls.ViewComboBox viewComboBox1;
        private View.Controls.ViewComboBox viewComboBox2;
        private View.Controls.ViewComboBox viewComboBox3;
        private View.Controls.ViewComboBox viewComboBox4;
        private View.Controls.SwitchButton switchButton1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 还原RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 大小SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最小化XToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 关闭CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大化XToolStripMenuItem;
        private View.Controls.ButtonWinForm viewButton21;
    }
}

