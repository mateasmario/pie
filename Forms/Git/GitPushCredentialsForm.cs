﻿using System;

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
        public GitPushCredentialsForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
        }

        private void GitPushCredentialsForm_Load(object sender, EventArgs e)
        {
            if (Globals.gitCredentials.Username != null)
            {
                kryptonTextBox1.Text = Globals.gitCredentials.Username;
            }

            if (Globals.gitCredentials.Password != null)
            {
                kryptonTextBox2.Text = Globals.gitCredentials.Password;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text != "" && kryptonTextBox2.Text != "")
            {
                Globals.gitCredentials.Username = kryptonTextBox1.Text;
                Globals.gitCredentials.Password = kryptonTextBox2.Text;

                Globals.gitFormClosedWithOk = true;

                this.Close();
            }
            else
            {
                MainForm.ShowNotification("Username and Password cannot be blank.");
            }
        }
    }
}
