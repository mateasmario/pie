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
        public CodeTemplatesForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonPanel2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonLabel8.Palette = Globals.kryptonPalette;
            kryptonCheckButton1.Palette = Globals.kryptonPalette;
            kryptonCheckButton2.Palette = Globals.kryptonPalette;
            kryptonCheckButton3.Palette = Globals.kryptonPalette;
            kryptonCheckButton4.Palette = Globals.kryptonPalette;
            kryptonCheckButton5.Palette = Globals.kryptonPalette;
            kryptonCheckButton6.Palette = Globals.kryptonPalette;
            kryptonCheckButton7.Palette = Globals.kryptonPalette;
            kryptonCheckButton8.Palette = Globals.kryptonPalette;
            kryptonCheckButton9.Palette = Globals.kryptonPalette;
            kryptonCheckButton10.Palette = Globals.kryptonPalette;

            kryptonCheckButton1.Click += kryptonCheckButton_Click;
            kryptonCheckButton2.Click += kryptonCheckButton_Click;
            kryptonCheckButton3.Click += kryptonCheckButton_Click;
            kryptonCheckButton4.Click += kryptonCheckButton_Click;
            kryptonCheckButton5.Click += kryptonCheckButton_Click;
            kryptonCheckButton6.Click += kryptonCheckButton_Click;
            kryptonCheckButton7.Click += kryptonCheckButton_Click;
            kryptonCheckButton8.Click += kryptonCheckButton_Click;
            kryptonCheckButton9.Click += kryptonCheckButton_Click;
            kryptonCheckButton10.Click += kryptonCheckButton_Click;

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
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ThemeService.GetColor("Fore");
            TextArea.CaretForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ThemeService.GetColor("Primary");
            TextArea.SetSelectionBackColor(true, ThemeService.GetColor("Selection"));
            TextArea.CaretLineBackColor = ThemeService.GetColor("CaretLineBack");
            TextArea.StyleClearAll();
            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = ThemeService.GetColor("NumberMargin");
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = ThemeService.GetColor("NumberMargin");

            TextArea.Margins[0].Width = 24;
            TextArea.Parent = kryptonPanel2;
            TextArea.Dock = DockStyle.Fill;
        }

        private void ColorizeButton(KryptonCheckButton kryptonCheckButton)
        {
            kryptonCheckButton.StateCommon.Back.Color1 = ThemeService.GetColor("Primary");
            kryptonCheckButton.StateCommon.Back.Color2 = ThemeService.GetColor("Primary");
            kryptonCheckButton.StateCheckedNormal.Back.Color1 = ThemeService.GetColor("Secondary");
            kryptonCheckButton.StateCheckedNormal.Back.Color2 = ThemeService.GetColor("Secondary");
            kryptonCheckButton.StateCheckedTracking.Back.Color1 = ThemeService.GetColor("Secondary");
            kryptonCheckButton.StateCheckedTracking.Back.Color2 = ThemeService.GetColor("Secondary");
            kryptonCheckButton.StateCheckedPressed.Back.Color1 = ThemeService.GetColor("Secondary");
            kryptonCheckButton.StateCheckedPressed.Back.Color2 = ThemeService.GetColor("Secondary");
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
