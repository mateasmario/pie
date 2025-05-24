/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using pie.Classes;
using pie.Services;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;

namespace pie.Forms.Git
{
    public partial class GitCommitMessageForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public GitCommitMessageFormInput Input;
        public GitCommitMessageFormOutput Output;

        public GitCommitMessageForm()
        {
            InitializeComponent();

            Output = new GitCommitMessageFormOutput();
        }

        private void GitCommitMessageForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(commitMessageRichTextBox.Text.Trim())) {
                ShowNotification("Commit message cannot be empty.");
            }
            else
            {
                Output.Message = commitMessageRichTextBox.Text.Trim();
                this.Close();
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
    }
}
