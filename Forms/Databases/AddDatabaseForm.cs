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

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;
using pie.Services;

namespace pie.Forms.Databases
{
    public partial class AddDatabaseForm : KryptonForm
    {
        public AddDatabaseForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonLabel3.Palette = Globals.kryptonPalette;
            kryptonLabel4.Palette = Globals.kryptonPalette;
            kryptonLabel5.Palette = Globals.kryptonPalette;
            kryptonLabel6.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonTextBox3.Palette = Globals.kryptonPalette;
            kryptonTextBox4.Palette = Globals.kryptonPalette;
            kryptonTextBox5.Palette = Globals.kryptonPalette;
            kryptonTextBox6.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "" || kryptonTextBox3.Text == "" || kryptonTextBox4.Text == "" || kryptonTextBox5.Text == "" || kryptonTextBox6.Text == "")
            {
                MainForm.ShowNotification("Database fields cannot be empty.");
            }
            else
            {
                int port;
                bool result = int.TryParse(kryptonTextBox3.Text, out port);

                if (!result)
                {
                    MainForm.ShowNotification("Port needs to be a number.");
                }
                else
                {
                    Globals.addDatabaseConnectionName = kryptonTextBox1.Text;
                    Globals.addDatabaseHostname = kryptonTextBox2.Text;
                    Globals.addDatabasePort = port;
                    Globals.addDatabaseDbName = kryptonTextBox4.Text;
                    Globals.addDatabaseUsername = kryptonTextBox5.Text;
                    Globals.addDatabasePassword = kryptonTextBox6.Text;
                }

                this.Close();
            }
        }

        private void AddDatabaseForm_Load(object sender, EventArgs e)
        {
            Globals.addDatabaseConnectionName = null;
            Globals.addDatabaseHostname = null;
            Globals.addDatabasePort = -1;
            Globals.addDatabaseDbName = null;
            Globals.addDatabaseUsername = null;
            Globals.addDatabasePassword = null;

            if (Globals.databaseEditIndex >= 0)
            {
                kryptonTextBox1.Text = Globals.databaseToEditConnectionName;
                kryptonTextBox2.Text = Globals.databaseToEditHostname;
                kryptonTextBox3.Text = Globals.databaseToEditPort.ToString();
                kryptonTextBox4.Text = Globals.databaseToEditDbName;
                kryptonTextBox5.Text = Globals.databaseToEditUsername;
                kryptonTextBox6.Text = Globals.databaseToEditPassword;
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "" || kryptonTextBox3.Text == "" || kryptonTextBox4.Text == "" || kryptonTextBox5.Text == "" || kryptonTextBox6.Text == "")
            {
                MainForm.ShowNotification("Database fields cannot be empty.");
            }
            else
            {
                int port;
                bool result = int.TryParse(kryptonTextBox3.Text, out port);

                if (!result)
                {
                    MainForm.ShowNotification("Port needs to be a number.");
                }
                else
                {
                    if (DatabaseService.CheckDatabaseConnection(kryptonTextBox2.Text, port, kryptonTextBox4.Text, kryptonTextBox5.Text, kryptonTextBox6.Text))
                    {
                        MainForm.ShowNotification("Database connection established successfully.");
                    }
                    else
                    {
                        MainForm.ShowNotification("Could not establish Database connection.");
                    }
                }
            }
        }
    }
}
