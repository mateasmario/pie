/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using pie.Classes;
using pie.Enums;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;
using pie.Services;

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

            if (Globals.buildCommands != null)
            {
                foreach (BuildCommand buildCommand in Globals.buildCommands)
                {
                    tempCommands.Add(buildCommand);
                    kryptonListBox2.Items.Add(buildCommand.getName());
                }
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
            BuildCommandService.WriteBuildCommandsToFile("build.json", tempCommands);

            MainForm.ShowYesNoCancelNotification("Close pie and reopen it manually, in order for the changes to take effect?");
            if (Globals.notificationButtonPressed == NotificationButton.YES)
            {
                Globals.closeAfterApplyingChanges = true;
            }

            this.Close();
        }
    }
}
