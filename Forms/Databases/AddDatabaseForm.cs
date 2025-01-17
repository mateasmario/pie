﻿/** Copyright (C) 2023  Mario-Mihai Mateas
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

namespace pie.Forms.Databases
{
    public partial class AddDatabaseForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        DatabaseType selectedDatabaseType = Globals.databaseToEditType;

        public AddDatabaseForm()
        {
            InitializeComponent();

            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);

            kryptonCheckButton1.StateCommon.Back.Color1 = Globals.theme.Primary;
            kryptonCheckButton1.StateCommon.Back.Color2 = Globals.theme.Primary;
            kryptonCheckButton1.StateCheckedNormal.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton1.StateCheckedNormal.Back.Color2 = Globals.theme.Secondary;            
            kryptonCheckButton1.StateCheckedTracking.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton1.StateCheckedTracking.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton1.StateCheckedPressed.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton1.StateCheckedPressed.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton1.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton1.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton1.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton1.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            kryptonCheckButton2.StateCommon.Back.Color1 = Globals.theme.Primary;
            kryptonCheckButton2.StateCommon.Back.Color2 = Globals.theme.Primary;
            kryptonCheckButton2.StateCheckedNormal.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCheckedNormal.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCheckedTracking.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCheckedTracking.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCheckedPressed.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCheckedPressed.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton2.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton2.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton2.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton2.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            kryptonCheckButton3.StateCommon.Back.Color1 = Globals.theme.Primary;
            kryptonCheckButton3.StateCommon.Back.Color2 = Globals.theme.Primary;
            kryptonCheckButton3.StateCheckedNormal.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCheckedNormal.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCheckedTracking.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCheckedTracking.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCheckedPressed.Back.Color1 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCheckedPressed.Back.Color2 = Globals.theme.Secondary;
            kryptonCheckButton3.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton3.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton3.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonCheckButton3.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            if (Globals.theme.IconType == "dark")
            {
                kryptonCheckButton1.Values.Image = Properties.Resources.mysql_black;
                kryptonCheckButton2.Values.Image = Properties.Resources.microsoft_black;
                kryptonCheckButton3.Values.Image = Properties.Resources.postgresql_black;
            }
            else if (Globals.theme.IconType == "light")
            {
                kryptonCheckButton1.Values.Image = Properties.Resources.mysql_white;
                kryptonCheckButton2.Values.Image = Properties.Resources.microsoft_white;
                kryptonCheckButton3.Values.Image = Properties.Resources.postgresql_white;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "" || kryptonTextBox3.Text == "" || kryptonTextBox4.Text == "" || kryptonTextBox5.Text == "" || kryptonTextBox6.Text == "" || selectedDatabaseType == DatabaseType.None)
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
                    Globals.addDatabaseType = selectedDatabaseType;
                }

                this.Close();
            }
        }

        private void AddDatabaseForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Globals.addDatabaseConnectionName = null;
            Globals.addDatabaseHostname = null;
            Globals.addDatabasePort = -1;
            Globals.addDatabaseDbName = null;
            Globals.addDatabaseUsername = null;
            Globals.addDatabasePassword = null;
            Globals.addDatabaseType = DatabaseType.None;

            if (Globals.databaseEditIndex >= 0)
            {
                kryptonTextBox1.Text = Globals.databaseToEditConnectionName;
                kryptonTextBox2.Text = Globals.databaseToEditHostname;
                kryptonTextBox3.Text = Globals.databaseToEditPort.ToString();
                kryptonTextBox4.Text = Globals.databaseToEditDbName;
                kryptonTextBox5.Text = Globals.databaseToEditUsername;
                kryptonTextBox6.Text = Globals.databaseToEditPassword;
                
                switch(Globals.databaseToEditType)
                {
                    case DatabaseType.MySQL:
                        kryptonCheckButton1.Checked = true;
                        break;
                    case DatabaseType.MSSQL:
                        kryptonCheckButton2.Checked = true;
                        break;
                    case DatabaseType.PostgreSQL:
                        kryptonCheckButton3.Checked = true;
                        break;
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "" || kryptonTextBox3.Text == "" || kryptonTextBox4.Text == "" || kryptonTextBox5.Text == "" || kryptonTextBox6.Text == "" || selectedDatabaseType == null)
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
                    DatabaseResponse dbConnectionCheckOutput = DatabaseService.CheckDatabaseConnection(selectedDatabaseType, kryptonTextBox2.Text, port, kryptonTextBox4.Text, kryptonTextBox5.Text, kryptonTextBox6.Text);

                    if (dbConnectionCheckOutput.Success)
                    {
                        MainForm.ShowNotification("Database connection established successfully.");
                    }
                    else
                    {
                        MainForm.ShowNotification(dbConnectionCheckOutput.Message);
                    }
                }
            }
        }

        private void kryptonCheckButton1_Click(object sender, EventArgs e)
        {
            kryptonCheckButton1.Checked = false;
            kryptonCheckButton2.Checked = false;
            kryptonCheckButton3.Checked = false;

            ((KryptonCheckButton)sender).Checked = true;

            string type = ((KryptonCheckButton)sender).Text.Trim();

            switch(type)
            {
                case "MySQL":
                    selectedDatabaseType = DatabaseType.MySQL;
                    break;
                case "Microsoft SQL":
                    selectedDatabaseType= DatabaseType.MSSQL;
                    break;
                case "PostgreSQL":
                    selectedDatabaseType = DatabaseType.PostgreSQL;
                    break;
            }

        }
    }
}
