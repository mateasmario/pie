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
using pie.Classes;
using pie.Services;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Theme
{
    public partial class NewThemeForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public NewThemeFormInput Input { get; set; }
        public NewThemeFormOutput Output { get; set; }

        public NewThemeForm()
        {
            InitializeComponent();

            Output = new NewThemeFormOutput();
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
            if (kryptonTextBox1.Text.Trim().Length == 0)
            {
                ShowNotification("Theme name cannot contain whitespaces only.");
            }
            else if (themeExists(kryptonTextBox1.Text.Trim()))
            {
                ShowNotification("Theme with specified name already exists. Please choose another name.");
            }
            else
            {
                Output.NewThemeName = kryptonTextBox1.Text.Trim();
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
