using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View.Controls
{
    public partial class ButtonWinForm : Button
    {
        /// <summary>
        /// 
        /// </summary>
        public ButtonWinForm()
        {
            InitializeComponent();
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(41, 98, 255);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(122, 122, 122);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(204, 204, 204);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderColor = Color.FromArgb(122, 122, 122);
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderSize = 1;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            ((Button)sender).FlatAppearance.BorderSize = 2;
        }
    }
}
