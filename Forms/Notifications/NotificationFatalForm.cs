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
using System.Drawing;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class NotificationFatalForm : KryptonForm
    {
        public NotificationFatalFormInput Input { get; set; }

        public NotificationFatalForm()
        {
            InitializeComponent();

            okButton.StateCommon.Back.Color1 = Color.FromArgb(240, 0, 0);
            okButton.StateCommon.Back.Color2 = Color.FromArgb(240, 0, 0);

            okButton.OverrideDefault.Back.Color1 = Color.FromArgb(240, 0, 0);
            okButton.OverrideDefault.Back.Color2 = Color.FromArgb(240, 0, 0);
            okButton.OverrideDefault.Border.Color1 = Color.FromArgb(230, 0, 0);
            okButton.OverrideDefault.Border.Color2 = Color.FromArgb(230, 0, 0);

            okButton.OverrideFocus.Back.ColorStyle = PaletteColorStyle.Solid;
            okButton.OverrideFocus.Border.ColorStyle = PaletteColorStyle.Solid;
            okButton.OverrideFocus.Back.Color1 = Color.FromArgb(255, 0, 0);
            okButton.OverrideFocus.Back.Color2 = Color.FromArgb(255, 0, 0);
            okButton.OverrideFocus.Border.Color1 = Color.FromArgb(230, 0, 0);
            okButton.OverrideFocus.Border.Color2 = Color.FromArgb(230, 0, 0);

            okButton.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
            okButton.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;

            okButton.StateCommon.Content.ShortText.Color1 = Color.White;
            okButton.StateCommon.Content.ShortText.Color2 = Color.White;

            okButton.StateTracking.Back.Color1 = Color.FromArgb(255, 0, 0);
            okButton.StateTracking.Back.Color2 = Color.FromArgb(255, 0, 0);

            okButton.StateTracking.Border.Color1 = Color.FromArgb(245, 0, 0);
            okButton.StateTracking.Border.Color2 = Color.FromArgb(245, 0, 0);

            okButton.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            okButton.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            Environment.Exit(1);
        }

        private void NotificationFatalForm_Load(object sender, System.EventArgs e)
        {
            contentRichTextBox.Text = Input.NotificationText;
        }

        private void NotificationFatalForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = true;
            Environment.Exit(1);
        }
    }
}
