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

        public AddBuildCommandForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "")
            {
                MainForm.ShowNotification("Display Name and Command cannot be empty.");
            }
            else
            {
                Globals.addBuildCommandName = kryptonTextBox1.Text;
                Globals.addBuildCommandCmd = kryptonTextBox2.Text;

                this.Close();
            }
        }

        private void AddBuildCommandForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            Globals.addBuildCommandName = null;
            Globals.addBuildCommandCmd = null;

            if (Globals.buildCommandEditIndex >= 0)
            {
                kryptonTextBox1.Text = Globals.buildCommandToEditName;
                kryptonTextBox2.Text = Globals.buildCommandToEditCmd;
            }
        }
    }
}
