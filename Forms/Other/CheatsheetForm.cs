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

namespace pie.Forms.Other
{
    public partial class CheatsheetForm : KryptonForm 
    {
        private ThemeService themeService = new ThemeService();

        public CheatsheetFormInput Input { get; set; }

        public CheatsheetForm()
        {
            InitializeComponent();
        }

        private void CheatsheetForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            ControlHelper.SuspendDrawing(this);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input.ActiveTheme.IconType == "dark")
            {
                keybind1Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind2Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind3Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind4Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind5Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind6Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind7Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind6Key2PictureBox.BackgroundImage = Properties.Resources.alt_dark;
                keybind7Key2PictureBox.BackgroundImage = Properties.Resources.alt_dark;
                keybind8Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind9Key1PictureBox.BackgroundImage = Properties.Resources.ctrl_dark;
                keybind1Key2PictureBox.BackgroundImage = Properties.Resources.f_dark;
                keybind2Key2PictureBox.BackgroundImage = Properties.Resources.b_dark;
                keybind3Key2PictureBox.BackgroundImage = Properties.Resources.g_dark;
                keybind4Key2PictureBox.BackgroundImage = Properties.Resources.x_dark;
                keybind5Key2PictureBox.BackgroundImage = Properties.Resources.v_dark;
                keybind6Key3PictureBox.BackgroundImage = Properties.Resources.cursor_left_dark;
                keybind7Key3PictureBox.BackgroundImage = Properties.Resources.cursor_right_dark;
                keybind8Key2PictureBox.BackgroundImage = Properties.Resources.cursor_up_dark;
                keybind9Key2PictureBox.BackgroundImage = Properties.Resources.cursor_down_dark;
            }
            else
            {
                keybind1Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind2Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind3Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind4Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind5Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind6Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind7Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind6Key2PictureBox.BackgroundImage = Properties.Resources.alt;
                keybind7Key2PictureBox.BackgroundImage = Properties.Resources.alt;
                keybind8Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind9Key1PictureBox.BackgroundImage = Properties.Resources.ctrl;
                keybind1Key2PictureBox.BackgroundImage = Properties.Resources.f;
                keybind2Key2PictureBox.BackgroundImage = Properties.Resources.b;
                keybind3Key2PictureBox.BackgroundImage = Properties.Resources.g;
                keybind4Key2PictureBox.BackgroundImage = Properties.Resources.x;
                keybind5Key2PictureBox.BackgroundImage = Properties.Resources.v;
                keybind6Key3PictureBox.BackgroundImage = Properties.Resources.cursor_left;
                keybind7Key3PictureBox.BackgroundImage = Properties.Resources.cursor_right;
                keybind8Key2PictureBox.BackgroundImage = Properties.Resources.cursor_up;
                keybind9Key2PictureBox.BackgroundImage = Properties.Resources.cursor_down;
            }

            ControlHelper.ResumeDrawing(this);
        }
    }
}
