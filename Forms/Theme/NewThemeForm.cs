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
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Theme
{
    public partial class NewThemeForm : KryptonForm
    {
        public NewThemeForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text.Trim().Length == 0)
            {
                MainForm.ShowNotification("Theme name cannot contain whitespaces only.");
            }
            else if (themeExists(kryptonTextBox1.Text.Trim()))
            {
                MainForm.ShowNotification("Theme with specified name already exists. Please choose another name.");
            }
            else
            {
                Globals.newThemeName = kryptonTextBox1.Text.Trim();
                this.Close();
            }
        }

        private bool themeExists(string name)
        {
            foreach (ThemeInfo themeInfo in Globals.themeInfos)
            {
                if (themeInfo.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }

        private void NewThemeForm_Load(object sender, EventArgs e)
        {
            Globals.newThemeName = null;
        }
    }
}
