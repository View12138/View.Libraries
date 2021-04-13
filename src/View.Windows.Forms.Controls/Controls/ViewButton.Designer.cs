using System;
using System.Drawing;
using System.Windows.Forms;

namespace View.Windows.Forms.Controls
{
    partial class ViewButton
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
            this.label1 = new Label();
            base.SuspendLayout();
            this.label1.BackColor = Color.Transparent;
            this.label1.Dock = DockStyle.Fill;
            this.label1.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(90, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "button";
            this.label1.TextAlign = ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseEnter += new EventHandler(this.label1_MouseEnter);
            this.label1.MouseLeave += new EventHandler(this.label1_MouseLeave);
            this.label1.MouseUp += new MouseEventHandler(this.label1_MouseUp);
            base.AutoScaleMode = AutoScaleMode.None;
            base.Controls.Add(this.label1);
            base.Name = "ViewButton";
            base.Size = new Size(90, 31);
            base.SizeChanged += new EventHandler(this.ViewButton_SizeChanged);
            base.ResumeLayout(false);
        }

        #endregion

        private Label label1;
    }
}
