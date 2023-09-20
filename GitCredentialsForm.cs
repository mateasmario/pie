using ComponentFactory.Krypton.Toolkit;
using pie.Classes;
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
    public partial class GitCredentialsForm : KryptonForm
    {
        public GitCredentialsForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text != "" && kryptonTextBox2.Text != "")
            {
                GitCredentials gitCredentials = new GitCredentials();
                gitCredentials.Name = kryptonTextBox1.Text;
                gitCredentials.Email = kryptonTextBox2.Text;

                Globals.gitCredentials = gitCredentials;

                Globals.gitFormClosedWithOk = true;

                this.Close();
            }
            else
            {
                MessageBox.Show("Author Name and Email cannot be blank.");
            }
        }
    }
}
