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

namespace pie
{
    public partial class GitCommitCredentialsForm : KryptonForm
    {
        public GitCommitCredentialsFormInput Input { get; set; }
        public GitCommitCredentialsFormOutput Output { get; set; }

        private ThemingService themeService = new ThemingService();
        public GitCommitCredentialsForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Output = new GitCommitCredentialsFormOutput();
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

        private void GitCommitCredentialsForm_Load(object sender, EventArgs e)
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
            if (authorNameTextBox.Text != "" && authorEmailTextBox.Text != "")
            {
                Input.GitCredentials.Name = authorNameTextBox.Text;
                Input.GitCredentials.Email = authorEmailTextBox.Text;

                Output.Saved = true;

                this.Close();
            }
            else
            {
                ShowNotification("Author Name and Email cannot be blank.");
            }
        }
    }
}
