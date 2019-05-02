namespace View.Controls
{
    partial class ButtonWinForm
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
            this.SuspendLayout();
            // 
            // ViewButton2
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.FlatAppearance.BorderSize = 2;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(98)))), ((int)(((byte)(255)))));
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Text = "button2";
            this.UseVisualStyleBackColor = false;
            this.Enter += new System.EventHandler(this.button1_Enter);
            this.Leave += new System.EventHandler(this.button1_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
