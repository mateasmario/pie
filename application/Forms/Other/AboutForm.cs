/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Services;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace pie
{
    public partial class AboutForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public AboutFormInput Input { get; set; }

        public AboutForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
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

        private void AboutForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Version assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            versionLabel.Text = "Version " + assemblyVersion.ToString();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/mateasmario/pie");
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Process.Start("http://docsbymario.com");
        }
    }
}
