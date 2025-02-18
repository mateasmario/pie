/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using pie.Services;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Git
{
    public partial class GitSettingsForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public GitSettingsFormInput Input { get; set; }
        public GitSettingsFormOutput Output { get; set; }

        public GitSettingsForm()
        {
            InitializeComponent();

            Output = new GitSettingsFormOutput();
        }

        private void GitSettingsForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input.GitCredentials.Name != null)
            {
                authorNameTextBox.Text = Input.GitCredentials.Name;
            }

            if (Input.GitCredentials.Email != null)
            {
                authorEmailTextBox.Text = Input.GitCredentials.Email;
            }

            if (Input.GitCredentials.Username != null)
            {
                remoteServerUsernameTextBox.Text = Input.GitCredentials.Username;
            }

            if (Input.GitCredentials.Password != null)
            {
                remoteServerPasswordTextBox.Text = Input.GitCredentials.Password;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Input.GitCredentials.Name = authorNameTextBox.Text;
            Input.GitCredentials.Email = authorEmailTextBox.Text;
            Input.GitCredentials.Username = remoteServerUsernameTextBox.Text;
            Input.GitCredentials.Password = remoteServerPasswordTextBox.Text;

            Output.Saved = true;

            this.Close();
        }
    }
}
