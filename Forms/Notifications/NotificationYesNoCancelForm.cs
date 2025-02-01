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
using pie.Enums;
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
    public partial class NotificationYesNoCancelForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public NotificationFormInput Input { get; set; }
        public NotificationYesNoCancelFormOutput Output { get; set; }

        public NotificationYesNoCancelForm()
        {
            InitializeComponent();
            Output = new NotificationYesNoCancelFormOutput();
        }

        private void NotificationYesNoCancel_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Output.NotificationButton = NotificationButton.CANCEL;
            contentRichTextBox.Text = Input.NotificationText;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.YES;
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.NO;
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Output.NotificationButton = NotificationButton.CANCEL;
            this.Close();
        }
    }
}
