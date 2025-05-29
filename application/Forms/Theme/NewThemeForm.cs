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

namespace pie.Forms.Theme
{
    public partial class NewThemeForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public NewThemeFormInput Input { get; set; }
        public NewThemeFormOutput Output { get; set; }

        public NewThemeForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Output = new NewThemeFormOutput();
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

        private void NewThemeForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
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
            if (themeNameTextBox.Text.Trim().Length == 0)
            {
                ShowNotification("Theme name cannot contain whitespaces only.");
            }
            else if (themeExists(themeNameTextBox.Text.Trim()))
            {
                ShowNotification("Theme with specified name already exists. Please choose another name.");
            }
            else
            {
                Output.NewThemeName = themeNameTextBox.Text.Trim();
                this.Close();
            }
        }

        private bool themeExists(string name)
        {
            foreach (ThemeInfo themeInfo in Input.ThemeInfos)
            {
                if (themeInfo.Name.Equals(name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
