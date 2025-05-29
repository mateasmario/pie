/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Enums;
using pie.Services;
using System;
using System.Windows.Forms;

namespace pie
{
    public partial class NotificationYesNoCancelForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public NotificationFormInput Input { get; set; }
        public NotificationYesNoCancelFormOutput Output { get; set; }

        public NotificationYesNoCancelForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Output = new NotificationYesNoCancelFormOutput();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void NotificationYesNoCancel_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Output.NotificationButton = NotificationButton.CANCEL;
            contentRichTextBox.Text = Input.NotificationText;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.YES;
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.NO;
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.CANCEL;
            this.Close();
        }
    }
}
