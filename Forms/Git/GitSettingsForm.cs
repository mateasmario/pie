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
        public GitSettingsForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            authorNameTextBox.Palette = Globals.kryptonPalette;
            authorEmailTextBox.Palette = Globals.kryptonPalette;
            remoteServerUsernameTextBox.Palette = Globals.kryptonPalette;
            remoteServerPasswordTextBox.Palette = Globals.kryptonPalette;
            authorNameLabel.Palette = Globals.kryptonPalette;
            authorEmailLabel.Palette = Globals.kryptonPalette;
            remoteServerUsernameLabel.Palette = Globals.kryptonPalette;
            remoteServerPasswordLabel.Palette = Globals.kryptonPalette;
            saveButton.Palette = Globals.kryptonPalette;
        }

        private void GitSettingsForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            if (Globals.gitCredentials.Name != null)
            {
                authorNameTextBox.Text = Globals.gitCredentials.Name;
            }

            if (Globals.gitCredentials.Email != null)
            {
                authorEmailTextBox.Text = Globals.gitCredentials.Email;
            }

            if (Globals.gitCredentials.Username != null)
            {
                remoteServerUsernameTextBox.Text = Globals.gitCredentials.Username;
            }

            if (Globals.gitCredentials.Password != null)
            {
                remoteServerPasswordTextBox.Text = Globals.gitCredentials.Password;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Globals.gitCredentials.Name = authorNameTextBox.Text;
            Globals.gitCredentials.Email = authorEmailTextBox.Text;
            Globals.gitCredentials.Username = remoteServerUsernameTextBox.Text;
            Globals.gitCredentials.Password = remoteServerPasswordTextBox.Text;

            Globals.gitFormClosedWithOk = true;

            this.Close();
        }
    }
}
