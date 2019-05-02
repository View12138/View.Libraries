using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Runtime.InteropServices;
using View.Controls;

namespace ViewLibraryTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void viewButton1_Click(object sender, EventArgs e)
        {
            //notifyIcon1.Visible = true;
            //notifyIcon1.ShowBalloonTip(1000, "当前时间：", DateTime.Now.ToLocalTime().ToString(), ToolTipIcon.Warning);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            contextMenuStrip1.Left += contextMenuStrip1.Width;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void switchButton1_Click(object sender, EventArgs e)
        {
        }

        private void switchButton1_SwitchChanged(object sender, EventArgs e)
        {
            if (!switchButton1.IsOpen)
            {
                ControlSet.ThemeStyle = View.Enums.ThemeType.Dark;
                BackColor = ControlSet.BackColor;
                ForeColor = ControlSet.ForeColor;
            }
            else
            {
                ControlSet.ThemeStyle = View.Enums.ThemeType.Light;
                BackColor = ControlSet.BackColor;
                ForeColor = ControlSet.ForeColor;
            }
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
