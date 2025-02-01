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
using pie.Services;

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
        private ConfigurationService configurationService = new ConfigurationService();
        private ThemeService themeService = new ThemeService();

        public BuildCommandsFormInput Input { get; set; }
        public BuildCommandsFormOutput Output { get; set; }

        private List<BuildCommand> tempCommands;

        public BuildCommandsForm()
        {
            InitializeComponent();

            Output = new BuildCommandsFormOutput();
        }

        private void BuildCommandsForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            tempCommands = new List<BuildCommand>();

            SynchronizeObjectListViewWithTheme();

            if (Input.BuildCommands != null)
            {
                foreach (BuildCommand buildCommand in Input.BuildCommands)
                {
                    tempCommands.Add(buildCommand);
                }

                buildCommandsListView.SetObjects(tempCommands);
            }
        }

        public void ShowNotification(string text)
        {
            NotificationOKForm notificationOkForm = new NotificationOKForm();

            NotificationFormInput notificationFormInput = new NotificationFormInput();
            notificationFormInput.EditorProperties = new EditorProperties();
            notificationFormInput.Palette = Input.Palette;
            notificationFormInput.NotificationText = text;

            notificationOkForm.Input = notificationFormInput;

            notificationOkForm.ShowDialog();
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            buildCommandsListView.ShowGroups = false;
            buildCommandsListView.UseCustomSelectionColors = true;
            buildCommandsListView.FullRowSelect = true;
            buildCommandsListView.MultiSelect = false;
            buildCommandsListView.HeaderStyle = ColumnHeaderStyle.None;

            buildCommandsListView.BackColor = Input.ActiveTheme.Primary;
            buildCommandsListView.ForeColor = Input.ActiveTheme.Fore;
            buildCommandsListView.HighlightBackgroundColor = Input.ActiveTheme.Secondary;
            buildCommandsListView.HighlightForegroundColor = Input.ActiveTheme.Fore;
            buildCommandsListView.UnfocusedHighlightBackgroundColor = Input.ActiveTheme.Secondary;
            buildCommandsListView.UnfocusedHighlightForegroundColor = Input.ActiveTheme.Fore;

            BuildCommandNameColumn.FillsFreeSpace = true;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (buildCommandsListView.SelectedItems.Count == 0)
            {
                ShowNotification("Please choose an item to delete.");
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

            BuildCommand outputBuildCommand = addBuildCommandForm.Output.BuildCommand;

            if (outputBuildCommand != null)
            {
                bool exists = false;

                // Check if a build command with the specified name already exists
                foreach(BuildCommand tempCommand in tempCommands)
                {
                    if (tempCommand.Name.Equals(outputBuildCommand.Name))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    ShowNotification("A command with the same name already exists.");
                }
                else
                {
                    tempCommands.Add(outputBuildCommand);
                    buildCommandsListView.SetObjects(tempCommands);
                }
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            configurationService.WriteToFile("config/build.json", tempCommands);

            Output.Saved = true;

            this.Close();
        }

        private void buildCommandsListView_DoubleClick_1(object sender, EventArgs e)
        {
            if (buildCommandsListView.SelectedObjects.Count == 1)
            {
                AddBuildCommandForm addBuildCommandForm = new AddBuildCommandForm();

                AddBuildCommandFormInput addBuildCommandFormInput = new AddBuildCommandFormInput();
                addBuildCommandFormInput.Palette = Input.Palette;
                addBuildCommandFormInput.EditorProperties = Input.EditorProperties;
                addBuildCommandFormInput.BuildCommand = tempCommands[buildCommandsListView.SelectedIndex];

                addBuildCommandForm.Input = addBuildCommandFormInput;

                addBuildCommandForm.ShowDialog();

                if (addBuildCommandForm.Output != null)
                {
                    bool exists = false;

                    foreach (BuildCommand tempCommand in tempCommands)
                    {
                        if (tempCommand.Name != tempCommands[buildCommandsListView.SelectedIndex].Name && tempCommand.Name.Equals(addBuildCommandForm.Output.BuildCommand.Name))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        ShowNotification("A command with the same name already exists.");
                    }
                    else
                    {
                        tempCommands[buildCommandsListView.SelectedIndex] = addBuildCommandForm.Output.BuildCommand;
                    }
                }

                buildCommandsListView.SetObjects(tempCommands);
            }
        }
    }
}
