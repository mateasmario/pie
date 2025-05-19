/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Drawing;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;

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
