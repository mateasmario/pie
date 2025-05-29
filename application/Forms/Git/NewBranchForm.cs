/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pie.Services;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;

namespace pie.Forms.Git
{
    public partial class NewBranchForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public NewBranchFormInput Input { get; set; }
        public NewBranchFormOutput Output { get; set; }

        public NewBranchForm()
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

        private void NewBranchForm_Load(object sender, EventArgs e)
        {
            Output = null;

            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(branchNameTextBox.Text.Trim()))
            {
                ShowNotification("Please enter a valid branch name.");
                return;
            }

            Output = new NewBranchFormOutput();
            Output.BranchName = branchNameTextBox.Text.Trim();
            this.Close();
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
