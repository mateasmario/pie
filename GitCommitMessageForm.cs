using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class GitCommitMessageForm : Form
    {
        public GitCommitMessageForm()
        {
            InitializeComponent();
        }

        private void GitCommitMessageForm_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Globals.setCommitMessage(kryptonRichTextBox1.Text);
            this.Close();
        }
    }
}
