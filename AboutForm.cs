using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
        {
            Process.Start("https://github.com/mateasmario/pie");
        }

        private void kryptonLinkLabel2_LinkClicked(object sender, EventArgs e)
        {
            Process.Start("https://mateasmario.com");
        }
    }
}
