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
