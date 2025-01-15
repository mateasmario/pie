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
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonLabel4.Palette = Globals.kryptonPalette;
            kryptonLabel9.Palette = Globals.kryptonPalette;
            kryptonLabel10.Palette = Globals.kryptonPalette;
        }

        private void CheatsheetForm_Load(object sender, EventArgs e)
        {
            ControlHelper.SuspendDrawing(this);

            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            if (Globals.theme.IconType == "dark")
            {
                pictureBox1.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox4.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox6.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox8.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox10.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox12.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox16.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox13.BackgroundImage = Properties.Resources.alt_dark;
                pictureBox14.BackgroundImage = Properties.Resources.alt_dark;
                pictureBox18.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox20.BackgroundImage = Properties.Resources.ctrl_dark;
                pictureBox2.BackgroundImage = Properties.Resources.f_dark;
                pictureBox3.BackgroundImage = Properties.Resources.b_dark;
                pictureBox5.BackgroundImage = Properties.Resources.g_dark;
                pictureBox7.BackgroundImage = Properties.Resources.x_dark;
                pictureBox9.BackgroundImage = Properties.Resources.v_dark;
                pictureBox11.BackgroundImage = Properties.Resources.cursor_left_dark;
                pictureBox15.BackgroundImage = Properties.Resources.cursor_right_dark;
                pictureBox17.BackgroundImage = Properties.Resources.cursor_up_dark;
                pictureBox19.BackgroundImage = Properties.Resources.cursor_down_dark;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.ctrl;
                pictureBox4.BackgroundImage = Properties.Resources.ctrl;
                pictureBox6.BackgroundImage = Properties.Resources.ctrl;
                pictureBox8.BackgroundImage = Properties.Resources.ctrl;
                pictureBox10.BackgroundImage = Properties.Resources.ctrl;
                pictureBox12.BackgroundImage = Properties.Resources.ctrl;
                pictureBox16.BackgroundImage = Properties.Resources.ctrl;
                pictureBox13.BackgroundImage = Properties.Resources.alt;
                pictureBox14.BackgroundImage = Properties.Resources.alt;
                pictureBox18.BackgroundImage = Properties.Resources.ctrl;
                pictureBox20.BackgroundImage = Properties.Resources.ctrl;
                pictureBox2.BackgroundImage = Properties.Resources.f;
                pictureBox3.BackgroundImage = Properties.Resources.b;
                pictureBox5.BackgroundImage = Properties.Resources.g;
                pictureBox7.BackgroundImage = Properties.Resources.x;
                pictureBox9.BackgroundImage = Properties.Resources.v;
                pictureBox11.BackgroundImage = Properties.Resources.cursor_left;
                pictureBox15.BackgroundImage = Properties.Resources.cursor_right;
                pictureBox17.BackgroundImage = Properties.Resources.cursor_up;
                pictureBox19.BackgroundImage = Properties.Resources.cursor_down;
            }

            ControlHelper.ResumeDrawing(this);
        }
    }
}
