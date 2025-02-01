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
using pie.Services;
using pie.Classes;
using pie.Enums;
using System.Collections.Generic;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class AddBuildCommandForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public BuildCommand InputBuildCommand;
        public BuildCommand Output { get; private set; }

        public AddBuildCommandForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (displayNameTextBox.Text == "" || commandTextBox.Text == "")
            {
                MainForm.ShowNotification("Display Name and Command cannot be empty.");
            }
            else
            {
                Output = new BuildCommand();

                Output.Name = displayNameTextBox.Text;
                Output.Command = commandTextBox.Text;

                List<string> fileExtensions = new List<string>();
                fileExtensions.AddRange(fileExtensionsRichTextBox.Text.Split('\n'));
                fileExtensions.ForEach(extension => extension.Trim());

                string extensions = "";
                for (int i = 0; i<fileExtensions.Count; i++)
                {
                    if (fileExtensions[i].StartsWith("."))
                    {
                        if (fileExtensions[i].Length > 1)
                        {
                            fileExtensions[i] = fileExtensions[i].Substring(1);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (fileExtensions[i].Trim() == "")
                    {
                        continue;
                    }

                    extensions += fileExtensions[i];
                    if (i != fileExtensions.Count - 1)
                    {
                        extensions += ", ";
                    }
                }

                Output.Extensions = extensions;

                this.Close();
            }
        }

        private void AddBuildCommandForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (InputBuildCommand != null)
            {
                displayNameTextBox.Text = InputBuildCommand.Name;
                commandTextBox.Text = InputBuildCommand.Command;
                fileExtensionsRichTextBox.Text = InputBuildCommand.Extensions.Replace(',', '\n').Replace(" ", "");
            }
        }
    }
}
