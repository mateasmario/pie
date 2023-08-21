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
    public partial class AddBuildCommandForm : Form
    {
        public AddBuildCommandForm()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "")
            {
                MessageBox.Show("Display Name and Command cannot be empty.");
            }
            else
            {
                Globals.setAddBuildCommandName(kryptonTextBox1.Text);
                Globals.setAddBuildCommandCmd(kryptonTextBox2.Text);

                this.Close();
            }
        }

        private void AddBuildCommandForm_Load(object sender, EventArgs e)
        {
            Globals.setAddBuildCommandName(null);
            Globals.setAddBuildCommandCmd(null);
        }
    }
}
