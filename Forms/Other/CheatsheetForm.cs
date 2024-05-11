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

namespace pie.Forms.Other
{
    public partial class CheatsheetForm : KryptonForm
    {
        public CheatsheetForm()
        {
            InitializeComponent();
            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel3.Palette = Globals.kryptonPalette;
            kryptonLabel5.Palette = Globals.kryptonPalette;
            kryptonLabel6.Palette = Globals.kryptonPalette;
            kryptonLabel7.Palette = Globals.kryptonPalette;
            kryptonLabel8.Palette = Globals.kryptonPalette;
        }

        private void CheatsheetForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            if (ThemeService.GetIconType(Globals.theme) == "light")
            {
                pictureBox1.BackgroundImage = Properties.Resources.ctrl_dark_theme;
                pictureBox4.BackgroundImage = Properties.Resources.ctrl_dark_theme;
                pictureBox6.BackgroundImage = Properties.Resources.ctrl_dark_theme;
                pictureBox8.BackgroundImage = Properties.Resources.ctrl_dark_theme;
                pictureBox10.BackgroundImage = Properties.Resources.ctrl_dark_theme;
                pictureBox2.BackgroundImage = Properties.Resources.f_dark_theme;
                pictureBox3.BackgroundImage = Properties.Resources.b_dark_theme;
                pictureBox5.BackgroundImage = Properties.Resources.g_dark_theme;
                pictureBox7.BackgroundImage = Properties.Resources.x_dark_theme;
                pictureBox9.BackgroundImage = Properties.Resources.v_dark_theme;
                
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.ctrl_light_theme;
                pictureBox4.BackgroundImage = Properties.Resources.ctrl_light_theme;
                pictureBox6.BackgroundImage = Properties.Resources.ctrl_light_theme;
                pictureBox8.BackgroundImage = Properties.Resources.ctrl_light_theme;
                pictureBox10.BackgroundImage = Properties.Resources.ctrl_light_theme;
                pictureBox2.BackgroundImage = Properties.Resources.f_light_theme;
                pictureBox3.BackgroundImage = Properties.Resources.f_light_theme;
                pictureBox5.BackgroundImage = Properties.Resources.f_light_theme;
                pictureBox7.BackgroundImage = Properties.Resources.f_light_theme;
                pictureBox9.BackgroundImage = Properties.Resources.f_light_theme;
            }
        }
    }
}
