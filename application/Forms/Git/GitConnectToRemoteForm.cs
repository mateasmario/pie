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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie.Forms.Git
{
    public partial class GitConnectToRemoteForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();
        public GitConnectToRemoteFormInput Input { get; set; }
        public GitConnectToRemoteFormOutput Output { get; set; }

        public GitConnectToRemoteForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Output = new GitConnectToRemoteFormOutput();
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

        private void GitConnectToRemoteForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(repositoryURLTextBox.Text.Trim()))
            {
                ShowNotification("Repository URL cannot be empty.");
            }
            else
            {
                Output.RepositoryUrl = repositoryURLTextBox.Text.Trim();
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
