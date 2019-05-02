using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace View.Controls
{
    partial class Slider
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
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pbButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBack
            // 
            this.pbBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBack.Location = new System.Drawing.Point(0, 11);
            this.pbBack.Margin = new System.Windows.Forms.Padding(0);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(154, 2);
            this.pbBack.TabIndex = 0;
            this.pbBack.TabStop = false;
            this.pbBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseDown);
            this.pbBack.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
            this.pbBack.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
            this.pbBack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseMove);
            this.pbBack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseUp);
            // 
            // pbButton
            // 
            this.pbButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pbButton.Location = new System.Drawing.Point(0, 0);
            this.pbButton.Margin = new System.Windows.Forms.Padding(0);
            this.pbButton.MaximumSize = new System.Drawing.Size(8, 24);
            this.pbButton.MinimumSize = new System.Drawing.Size(8, 24);
            this.pbButton.Name = "pbButton";
            this.pbButton.Size = new System.Drawing.Size(8, 24);
            this.pbButton.TabIndex = 1;
            this.pbButton.TabStop = false;
            this.pbButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbButton_MouseDown);
            this.pbButton.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
            this.pbButton.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
            this.pbButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbButton_MouseMove);
            this.pbButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbButton_MouseUp);
            // 
            // Slider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pbButton);
            this.Controls.Add(this.pbBack);
            this.MaximumSize = new System.Drawing.Size(500, 24);
            this.MinimumSize = new System.Drawing.Size(50, 0);
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(154, 24);
            this.SizeChanged += new System.EventHandler(this.Slider_SizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseDown);
            this.MouseEnter += new System.EventHandler(this.pbButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.pbButton_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private PictureBox pbBack;

        private PictureBox pbButton;
    }
}
