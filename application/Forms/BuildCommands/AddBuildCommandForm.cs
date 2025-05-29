/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pie
{
    public partial class AddBuildCommandForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public AddBuildCommandFormInput Input { get; set; }
        public AddBuildCommandFormOutput Output { get; set; }

        public AddBuildCommandForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Output = new AddBuildCommandFormOutput();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void AddBuildCommandForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input.BuildCommand != null)
            {
                displayNameTextBox.Text = Input.BuildCommand.Name;
                commandTextBox.Text = Input.BuildCommand.Command;
                fileExtensionsRichTextBox.Text = Input.BuildCommand.Extensions.Replace(',', '\n').Replace(" ", "");
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (displayNameTextBox.Text == "" || commandTextBox.Text == "")
            {
                ShowNotification("Display Name and Command cannot be empty.");
            }
            else
            {
                Output.BuildCommand = new BuildCommand();

                Output.BuildCommand.Name = displayNameTextBox.Text;
                Output.BuildCommand.Command = commandTextBox.Text;

                List<string> fileExtensions = new List<string>();
                fileExtensions.AddRange(fileExtensionsRichTextBox.Text.Split('\n'));
                fileExtensions.ForEach(extension => extension.Trim());

                string extensions = "";
                for (int i = 0; i < fileExtensions.Count; i++)
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

                Output.BuildCommand.Extensions = extensions;

                this.Close();
            }
        }
    }
}
