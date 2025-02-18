﻿/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Diagnostics;
using System.Reflection;
using pie.Services;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class AboutForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public AboutFormInput Input { get; set; }

        public AboutForm()
        {
            InitializeComponent();
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
