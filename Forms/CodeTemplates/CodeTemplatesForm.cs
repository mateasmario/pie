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
using System.Windows.Forms;
using pie.Services;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.CodeTemplates
{
    public partial class CodeTemplatesForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public CodeTemplatesForm()
        {
            InitializeComponent();

            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);

            ColorizeButton(kryptonCheckButton1);
            ColorizeButton(kryptonCheckButton2);
            ColorizeButton(kryptonCheckButton3);
            ColorizeButton(kryptonCheckButton4);
            ColorizeButton(kryptonCheckButton5);
            ColorizeButton(kryptonCheckButton6);
            ColorizeButton(kryptonCheckButton7);
            ColorizeButton(kryptonCheckButton8);
            ColorizeButton(kryptonCheckButton9);
            ColorizeButton(kryptonCheckButton10);
        }

        private void kryptonCheckButton_Click(object sender, EventArgs e)
        {
            kryptonCheckButton1.Checked = false;
            kryptonCheckButton2.Checked = false;
            kryptonCheckButton3.Checked = false;
            kryptonCheckButton4.Checked = false;
            kryptonCheckButton5.Checked = false;
            kryptonCheckButton6.Checked = false;
            kryptonCheckButton7.Checked = false;
            kryptonCheckButton8.Checked = false;
            kryptonCheckButton9.Checked = false;
            kryptonCheckButton10.Checked = false;

            ((KryptonCheckButton)sender).Checked = true;
        }

        private void CodeTemplatesForm_Load(object sender, EventArgs e)
        {
            Scintilla TextArea = new Scintilla();

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = Globals.theme.Fore;
            TextArea.CaretForeColor = Globals.theme.Fore;
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = Globals.theme.Primary;
            TextArea.SetSelectionBackColor(true, Globals.theme.Selection);
            TextArea.CaretLineBackColor = Globals.theme.CaretLineBack;
            TextArea.StyleClearAll();
            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = Globals.theme.NumberMargin;
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = Globals.theme.Fore;
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = Globals.theme.Fore;
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = Globals.theme.NumberMargin;

            TextArea.Margins[0].Width = 24;
            TextArea.Parent = kryptonPanel2;
            TextArea.Dock = DockStyle.Fill;
        }

        private void ColorizeButton(KryptonCheckButton kryptonCheckButton)
        {
            kryptonCheckButton.StateCommon.Back.Color1 = Globals.theme.Primary;
            kryptonCheckButton.StateCommon.Back.Color2 = Globals.theme.Primary;
            kryptonCheckButton.StateCheckedNormal.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedNormal.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedTracking.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton.StateCheckedPressed.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
