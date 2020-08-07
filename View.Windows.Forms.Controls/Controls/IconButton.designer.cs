namespace View.Windows.Forms.Controls
{
    partial class IconButton
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
            this.lbIcon = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbIcon
            // 
            this.lbIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbIcon.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIcon.Location = new System.Drawing.Point(0, 0);
            this.lbIcon.Margin = new System.Windows.Forms.Padding(0);
            this.lbIcon.Name = "lbIcon";
            this.lbIcon.Size = new System.Drawing.Size(25, 25);
            this.lbIcon.TabIndex = 0;
            this.lbIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbText
            // 
            this.lbText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbText.Location = new System.Drawing.Point(25, 0);
            this.lbText.Margin = new System.Windows.Forms.Padding(0);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(75, 25);
            this.lbText.TabIndex = 1;
            this.lbText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbText.Paint += new System.Windows.Forms.PaintEventHandler(this.AIconButton_Paint);
            // 
            // IconButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lbText);
            this.Controls.Add(this.lbIcon);
            this.Name = "IconButton";
            this.Size = new System.Drawing.Size(100, 25);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AIconButton_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbIcon;
        private System.Windows.Forms.Label lbText;
    }
}
