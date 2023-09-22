using System;
using System.Collections.Generic;
using System.Windows.Forms;
using pie.Classes;
using System.IO;
using pie.Enums;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class BuildCommandsForm : KryptonForm
    {
        List<BuildCommand> tempCommands;

        public BuildCommandsForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonListBox2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
            kryptonButton3.Palette = Globals.kryptonPalette;
        }

        private void BuildCommandsForm_Load(object sender, EventArgs e)
        {
            tempCommands = new List<BuildCommand>();
            
            foreach(BuildCommand buildCommand in Globals.buildCommands)
            {
                tempCommands.Add(buildCommand);
                kryptonListBox2.Items.Add(buildCommand.getName());
            }

            Globals.closeAfterApplyingChanges = false;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonListBox2.SelectedItems.Count == 0)
            {
                MainForm.ShowNotification("Please choose an item to delete.");
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

            if (Globals.addBuildCommandName != null)
            {
                BuildCommand buildCommand = new BuildCommand();
                buildCommand.setName(Globals.addBuildCommandName);
                buildCommand.setCommand(Globals.addBuildCommandCmd);

                if (kryptonListBox2.Items.Contains(buildCommand.getName()))
                {
                    MainForm.ShowNotification("A command with the same name already exists.");
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

            File.WriteAllText("build.config", buildCommands);

            MainForm.ShowYesNoCancelNotification("Close pie and reopen it manually, in order for the changes to take effect?");
            if (Globals.notificationButtonPressed == NotificationButton.YES)
            {
                Globals.closeAfterApplyingChanges = true;
            }

            this.Close();
        }
    }
}
