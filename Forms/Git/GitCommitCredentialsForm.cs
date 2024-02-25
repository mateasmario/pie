﻿/** Copyright (C) 2023  Mario-Mihai Mateas
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

namespace pie
{
    public partial class GitCommitCredentialsForm : KryptonForm
    {
        public GitCommitCredentialsForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text != "" && kryptonTextBox2.Text != "")
            {
                Globals.gitCredentials.Name = kryptonTextBox1.Text;
                Globals.gitCredentials.Email = kryptonTextBox2.Text;

                Globals.gitFormClosedWithOk = true;

                this.Close();
            }
            else
            {
                MainForm.ShowNotification("Author Name and Email cannot be blank.");
            }
        }

        private void GitCommitCredentialsForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            if (Globals.gitCredentials.Name != null)
            {
                kryptonTextBox1.Text = Globals.gitCredentials.Name;
            }

            if (Globals.gitCredentials.Email != null)
            {
                kryptonTextBox2.Text = Globals.gitCredentials.Email;
            }
        }
    }
}
