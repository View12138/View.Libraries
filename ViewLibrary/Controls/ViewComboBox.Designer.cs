using System;
using System.Drawing;
using System.Windows.Forms;

namespace View.Controls
{
    partial class ViewComboBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.border1 = new System.Windows.Forms.Label();
            this.border3 = new System.Windows.Forms.Label();
            this.border2 = new System.Windows.Forms.Label();
            this.border4 = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.lbIcon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // border1
            // 
            this.border1.BackColor = System.Drawing.Color.White;
            this.border1.Dock = System.Windows.Forms.DockStyle.Top;
            this.border1.Location = new System.Drawing.Point(0, 0);
            this.border1.Margin = new System.Windows.Forms.Padding(0);
            this.border1.Name = "border1";
            this.border1.Size = new System.Drawing.Size(95, 2);
            this.border1.TabIndex = 0;
            this.border1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.border1.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.border1.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.border1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // border3
            // 
            this.border3.BackColor = System.Drawing.Color.White;
            this.border3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.border3.Location = new System.Drawing.Point(0, 22);
            this.border3.Margin = new System.Windows.Forms.Padding(0);
            this.border3.Name = "border3";
            this.border3.Size = new System.Drawing.Size(95, 2);
            this.border3.TabIndex = 0;
            this.border3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.border3.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.border3.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.border3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // border2
            // 
            this.border2.BackColor = System.Drawing.Color.White;
            this.border2.Dock = System.Windows.Forms.DockStyle.Right;
            this.border2.Location = new System.Drawing.Point(93, 2);
            this.border2.Margin = new System.Windows.Forms.Padding(0);
            this.border2.Name = "border2";
            this.border2.Size = new System.Drawing.Size(2, 20);
            this.border2.TabIndex = 0;
            this.border2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.border2.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.border2.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.border2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // border4
            // 
            this.border4.BackColor = System.Drawing.Color.White;
            this.border4.Dock = System.Windows.Forms.DockStyle.Left;
            this.border4.Location = new System.Drawing.Point(0, 2);
            this.border4.Margin = new System.Windows.Forms.Padding(0);
            this.border4.Name = "border4";
            this.border4.Size = new System.Drawing.Size(2, 20);
            this.border4.TabIndex = 0;
            this.border4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.border4.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.border4.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.border4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // lbText
            // 
            this.lbText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbText.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbText.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbText.Location = new System.Drawing.Point(2, 2);
            this.lbText.Margin = new System.Windows.Forms.Padding(0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(71, 20);
            this.lbText.TabIndex = 1;
            this.lbText.Text = "text";
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.lbText.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.lbText.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.lbText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // lbIcon
            // 
            this.lbIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIcon.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbIcon.Location = new System.Drawing.Point(73, 2);
            this.lbIcon.Margin = new System.Windows.Forms.Padding(0);
            this.lbIcon.Name = "lbIcon";
            this.lbIcon.Size = new System.Drawing.Size(20, 20);
            this.lbIcon.TabIndex = 1;
            this.lbIcon.Text = "图";
            this.lbIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.lbIcon.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.lbIcon.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.lbIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            // 
            // ViewComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbIcon);
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.border4);
            this.Controls.Add(this.border2);
            this.Controls.Add(this.border3);
            this.Controls.Add(this.border1);
            this.Name = "ViewComboBox";
            this.Size = new System.Drawing.Size(95, 24);
            this.SizeChanged += new System.EventHandler(this.ViewComboBox_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ViewComboBox_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ViewComboBox_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewComboBox_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Label border1;

        private Label border3;

        private Label border2;

        private Label border4;

        private Label lbText;

        private Label lbIcon;
    }
}
