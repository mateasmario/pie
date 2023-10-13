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
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
            kryptonButton3.Palette = Globals.kryptonPalette;
        }

        private void BuildCommandsForm_Load(object sender, EventArgs e)
        {
            tempCommands = new List<BuildCommand>();

            SynchronizeObjectListViewWithTheme();

            if (Globals.buildCommands != null)
            {
                foreach (BuildCommand buildCommand in Globals.buildCommands)
                {
                    tempCommands.Add(buildCommand);
                }

                buildCommandsListView.SetObjects(tempCommands);
            }

            Globals.closeAfterApplyingChanges = false;
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            buildCommandsListView.ShowGroups = false;
            buildCommandsListView.UseCustomSelectionColors = true;
            buildCommandsListView.FullRowSelect = true;
            buildCommandsListView.MultiSelect = false;
            buildCommandsListView.HeaderStyle = ColumnHeaderStyle.None;

            buildCommandsListView.BackColor = ThemeService.GetColor("Primary");
            buildCommandsListView.ForeColor = ThemeService.GetColor("Fore");
            buildCommandsListView.HighlightBackgroundColor = ThemeService.GetColor("Secondary");
            buildCommandsListView.HighlightForegroundColor = ThemeService.GetColor("Fore");
            buildCommandsListView.UnfocusedHighlightBackgroundColor = ThemeService.GetColor("Secondary");
            buildCommandsListView.UnfocusedHighlightForegroundColor = ThemeService.GetColor("Fore");

            BuildCommandNameColumn.FillsFreeSpace = true;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (buildCommandsListView.SelectedItems.Count == 0)
            {
                MainForm.ShowNotification("Please choose an item to delete.");
            }
            else
            {
                foreach(var s in buildCommandsListView.SelectedObjects)
                {
                    BuildCommand buildCommand = (BuildCommand)s;

                    if (tempCommands.Contains(buildCommand))
                    {
                        tempCommands.Remove(buildCommand);
                    }
                }

                buildCommandsListView.SetObjects(tempCommands);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AddBuildCommandForm addBuildCommandForm = new AddBuildCommandForm();
            addBuildCommandForm.ShowDialog();

            if (Globals.addBuildCommandName != null)
            {
                BuildCommand buildCommand = new BuildCommand();
                buildCommand.BuildCommandName = Globals.addBuildCommandName;
                buildCommand.BuildCommandCmd = Globals.addBuildCommandCmd;

                bool exists = false;

                // Check if a build command with the specified name already exists
                foreach(BuildCommand tempCommand in tempCommands)
                {
                    if (tempCommand.BuildCommandName.Equals(buildCommand.BuildCommandName))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    MainForm.ShowNotification("A command with the same name already exists.");
                }
                else
                {
                    tempCommands.Add(buildCommand);
                    buildCommandsListView.SetObjects(tempCommands);
                }
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            BuildCommandService.WriteBuildCommandsToFile("config/build.json", tempCommands);

            Globals.closeAfterApplyingChanges = true;

            this.Close();
        }

        private void buildCommandsListView_DoubleClick_1(object sender, EventArgs e)
        {
            if (buildCommandsListView.SelectedObjects.Count == 1)
            {
                Globals.buildCommandEditIndex = buildCommandsListView.SelectedIndex;

                Globals.buildCommandToEditName = tempCommands[Globals.buildCommandEditIndex].BuildCommandName;
                Globals.buildCommandToEditCmd = tempCommands[Globals.buildCommandEditIndex].BuildCommandCmd;

                AddBuildCommandForm addBuildCommandForm = new AddBuildCommandForm();
                addBuildCommandForm.ShowDialog();

                if (Globals.addBuildCommandName != null)
                {
                    bool exists = false;

                    foreach (BuildCommand tempCommand in tempCommands)
                    {
                        if (tempCommand.BuildCommandName != Globals.buildCommandToEditName && tempCommand.BuildCommandName.Equals(Globals.addBuildCommandName))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        MainForm.ShowNotification("A command with the same name already exists.");
                    }
                    else
                    {
                        tempCommands[Globals.buildCommandEditIndex].BuildCommandName = Globals.addBuildCommandName;
                        tempCommands[Globals.buildCommandEditIndex].BuildCommandCmd = Globals.addBuildCommandCmd;
                    }
                }

                Globals.buildCommandEditIndex = -1;

                buildCommandsListView.SetObjects(tempCommands);
            }
        }
    }
}
