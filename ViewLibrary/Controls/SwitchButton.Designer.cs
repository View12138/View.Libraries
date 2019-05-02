using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace View.Controls
{
    partial class SwitchButton
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
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.labelText = new Label();
            this.pbBack = new PictureBox();
            this.pbFore = new PictureBox();
            ((ISupportInitialize)this.pbBack).BeginInit();
            ((ISupportInitialize)this.pbFore).BeginInit();
            base.SuspendLayout();
            this.labelText.AutoSize = true;
            this.labelText.Dock = DockStyle.Right;
            this.labelText.Font = new Font("微软雅黑", 11.25f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.labelText.Location = new Point(51, 0);
            this.labelText.Margin = new Padding(0);
            this.labelText.Name = "labelText";
            this.labelText.Size = new Size(24, 20);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "开";
            this.labelText.TextAlign = ContentAlignment.MiddleCenter;
            this.labelText.MouseDown += new MouseEventHandler(this.SwitchButton_MouseDown);
            this.labelText.MouseEnter += new EventHandler(this.SwitchButton_MouseEnter);
            this.labelText.MouseLeave += new EventHandler(this.SwitchButton_MouseLeave);
            this.labelText.MouseUp += new MouseEventHandler(this.SwitchButton_MouseUp);
            this.pbBack.Dock = DockStyle.Left;
            this.pbBack.Location = new Point(0, 0);
            this.pbBack.Margin = new Padding(0);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new Size(45, 20);
            this.pbBack.TabIndex = 1;
            this.pbBack.TabStop = false;
            this.pbBack.MouseDown += new MouseEventHandler(this.SwitchButton_MouseDown);
            this.pbBack.MouseEnter += new EventHandler(this.SwitchButton_MouseEnter);
            this.pbBack.MouseLeave += new EventHandler(this.SwitchButton_MouseLeave);
            this.pbBack.MouseUp += new MouseEventHandler(this.SwitchButton_MouseUp);
            this.pbFore.Location = new Point(5, 5);
            this.pbFore.Name = "pbFore";
            this.pbFore.Size = new Size(10, 10);
            this.pbFore.TabIndex = 2;
            this.pbFore.TabStop = false;
            this.pbFore.MouseDown += new MouseEventHandler(this.SwitchButton_MouseDown);
            this.pbFore.MouseEnter += new EventHandler(this.SwitchButton_MouseEnter);
            this.pbFore.MouseLeave += new EventHandler(this.SwitchButton_MouseLeave);
            this.pbFore.MouseUp += new MouseEventHandler(this.SwitchButton_MouseUp);
            base.AutoScaleDimensions = new SizeF(96f, 96f);
            base.AutoScaleMode = AutoScaleMode.Dpi;
            this.BackColor = Color.Transparent;
            base.Controls.Add(this.pbFore);
            base.Controls.Add(this.pbBack);
            base.Controls.Add(this.labelText);
            this.ForeColor = Color.White;
            base.Name = "SwitchButton";
            base.Size = new Size(75, 20);
            base.Load += new EventHandler(this.SwitchButton_Load);
            base.MouseDown += new MouseEventHandler(this.SwitchButton_MouseDown);
            base.MouseEnter += new EventHandler(this.SwitchButton_MouseEnter);
            base.MouseLeave += new EventHandler(this.SwitchButton_MouseLeave);
            base.MouseUp += new MouseEventHandler(this.SwitchButton_MouseUp);
            ((ISupportInitialize)this.pbBack).EndInit();
            ((ISupportInitialize)this.pbFore).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Label labelText;

		private PictureBox pbBack;

        private PictureBox pbFore;
    }
}
