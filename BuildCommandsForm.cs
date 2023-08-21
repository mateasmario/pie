using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pie.Classes;
using System.IO;

namespace pie
{
    public partial class BuildCommandsForm : Form
    {
        List<BuildCommand> tempCommands;

        public BuildCommandsForm()
        {
            InitializeComponent();
        }

        private void BuildCommandsForm_Load(object sender, EventArgs e)
        {
            tempCommands = new List<BuildCommand>();
            
            foreach(BuildCommand buildCommand in Globals.getBuildCommands())
            {
                tempCommands.Add(buildCommand);
                kryptonListBox2.Items.Add(buildCommand.getName());
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonListBox2.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please choose an item to delete.", "Build Commands");
            }
            else
            {
                List<string> selectedItems = new List<string>();
                ListBox.SelectedObjectCollection selectedObjectCollection = kryptonListBox2.SelectedItems;

                foreach(object s in selectedObjectCollection)
                {
                    selectedItems.Add(s.ToString());
                }

                foreach(string s in selectedItems)
                {
                    tempCommands.RemoveAt(kryptonListBox2.Items.IndexOf(s.ToString()));
                    kryptonListBox2.Items.Remove(s.ToString());
                }

            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AddBuildCommandForm addBuildCommandForm = new AddBuildCommandForm();
            addBuildCommandForm.ShowDialog();

            if (Globals.getAddBuildCommandName() != null)
            {
                BuildCommand buildCommand = new BuildCommand();
                buildCommand.setName(Globals.getAddBuildCommandName());
                buildCommand.setCommand(Globals.getAddBuildCommandCmd());

                if (kryptonListBox2.Items.Contains(buildCommand.getName()))
                {
                    MessageBox.Show("A command with the same name already exists.", "Build Commands");
                }
                else
                {
                    tempCommands.Add(buildCommand);
                    kryptonListBox2.Items.Add(buildCommand.getName());
                }
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            string buildCommands = "";
            
            foreach(BuildCommand buildCommand in tempCommands) {
                buildCommands += buildCommand.getName() + Environment.NewLine;
                buildCommands += buildCommand.getCommand() + Environment.NewLine;
            }

            File.WriteAllText("pie.config", buildCommands);

            DialogResult dialogResult = MessageBox.Show("Close pie and reopen it manually, in order for the changes to take effect?", "Build Commands", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialogResult == DialogResult.No)
            {
                this.Close();
            }
        }
    }
}
