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

namespace pie
{
    public partial class GitPushCredentialsForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();
        
        public GitPushCredentialsFormInput Input { get; set; }
        public GitPushCredentialsFormOutput Output { get; set; }

        public GitPushCredentialsForm()
        {
            InitializeComponent();
            Output = new GitPushCredentialsFormOutput();
        }

        private void GitPushCredentialsForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input.GitCredentials.Username != null)
            {
                remoteServerUsernameTextBox.Text = Input.GitCredentials.Username;
            }

            if (Input.GitCredentials.Password != null)
            {
                remoteServerPasswordTextBox.Text = Input.GitCredentials.Password;
            }
        }

        public void ShowNotification(string text)
        {
            NotificationOKForm notificationOkForm = new NotificationOKForm();

            NotificationFormInput notificationFormInput = new NotificationFormInput();
            notificationFormInput.EditorProperties = new EditorProperties();
            notificationFormInput.Palette = Input.Palette;
            notificationFormInput.NotificationText = text;

            notificationOkForm.Input = notificationFormInput;

            notificationOkForm.ShowDialog();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (remoteServerUsernameTextBox.Text != "" && remoteServerPasswordTextBox.Text != "")
            {
                Input.GitCredentials.Username = remoteServerUsernameTextBox.Text;
                Input.GitCredentials.Password = remoteServerPasswordTextBox.Text;

                Output.Saved = true;

                this.Close();
            }
            else
            {
                ShowNotification("Username and Password cannot be blank.");
            }
        }
    }
}
