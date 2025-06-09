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
using System.Windows.Forms;

namespace pie.Forms.Git
{
    public partial class GitSettingsForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public GitSettingsFormInput Input { get; set; }
        public GitSettingsFormOutput Output { get; set; }

        public GitSettingsForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Output = new GitSettingsFormOutput();
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

        private void GitSettingsForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input.GitCredentials.Name != null)
            {
                authorNameTextBox.Text = Input.GitCredentials.Name;
            }

            if (Input.GitCredentials.Email != null)
            {
                authorEmailTextBox.Text = Input.GitCredentials.Email;
            }

            if (Input.GitCredentials.Username != null)
            {
                remoteServerUsernameTextBox.Text = Input.GitCredentials.Username;
            }

            if (Input.GitCredentials.Password != null)
            {
                remoteServerPasswordTextBox.Text = Input.GitCredentials.Password;
            }

            if (Input.GitCredentials.Proxy != null)
            {
                proxyTextBox.Text = Input.GitCredentials.Proxy;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Input.GitCredentials.Name = authorNameTextBox.Text;
            Input.GitCredentials.Email = authorEmailTextBox.Text;
            Input.GitCredentials.Username = remoteServerUsernameTextBox.Text;
            Input.GitCredentials.Password = remoteServerPasswordTextBox.Text;
            Input.GitCredentials.Proxy = proxyTextBox.Text;

            Output.Saved = true;

            this.Close();
        }
    }
}
