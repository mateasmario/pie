/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

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
                kryptonTextBox1.Text = Input.GitCredentials.Username;
            }

            if (Input.GitCredentials.Password != null)
            {
                kryptonTextBox2.Text = Input.GitCredentials.Password;
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
            if (kryptonTextBox1.Text != "" && kryptonTextBox2.Text != "")
            {
                Input.GitCredentials.Username = kryptonTextBox1.Text;
                Input.GitCredentials.Password = kryptonTextBox2.Text;

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
