/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

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

        public AddDatabaseFormInput Input { get; set; }
        public AddDatabaseFormOutput Output { get; set; }

        DatabaseType selectedDatabaseType;


        public AddDatabaseForm()
        {
            InitializeComponent();

            Output = new AddDatabaseFormOutput();
        }

        private void AddDatabaseForm_Load(object sender, EventArgs e)
        {
            mysqlCheckButton.StateCommon.Back.Color1 = Input.ActiveTheme.Primary;
            mysqlCheckButton.StateCommon.Back.Color2 = Input.ActiveTheme.Primary;
            mysqlCheckButton.StateCheckedNormal.Back.Color1 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCheckedNormal.Back.Color2 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCheckedTracking.Back.Color1 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCheckedTracking.Back.Color2 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCheckedPressed.Back.Color1 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCheckedPressed.Back.Color2 = Input.ActiveTheme.Secondary;
            mysqlCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            mysqlCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            mysqlCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            mysqlCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            mssqlCheckButton.StateCommon.Back.Color1 = Input.ActiveTheme.Primary;
            mssqlCheckButton.StateCommon.Back.Color2 = Input.ActiveTheme.Primary;
            mssqlCheckButton.StateCheckedNormal.Back.Color1 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCheckedNormal.Back.Color2 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCheckedTracking.Back.Color1 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCheckedTracking.Back.Color2 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCheckedPressed.Back.Color1 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCheckedPressed.Back.Color2 = Input.ActiveTheme.Secondary;
            mssqlCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            mssqlCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            mssqlCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            mssqlCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            postgresqlCheckButton.StateCommon.Back.Color1 = Input.ActiveTheme.Primary;
            postgresqlCheckButton.StateCommon.Back.Color2 = Input.ActiveTheme.Primary;
            postgresqlCheckButton.StateCheckedNormal.Back.Color1 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCheckedNormal.Back.Color2 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCheckedTracking.Back.Color1 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCheckedTracking.Back.Color2 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCheckedPressed.Back.Color1 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCheckedPressed.Back.Color2 = Input.ActiveTheme.Secondary;
            postgresqlCheckButton.StateCommon.Back.ColorStyle = PaletteColorStyle.Solid;
            postgresqlCheckButton.StateCheckedNormal.Back.ColorStyle = PaletteColorStyle.Solid;
            postgresqlCheckButton.StateCheckedTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            postgresqlCheckButton.StateCheckedPressed.Back.ColorStyle = PaletteColorStyle.Solid;

            if (Input.ActiveTheme.IconType == "dark")
            {
                mysqlCheckButton.Values.Image = Properties.Resources.mysql_black;
                mssqlCheckButton.Values.Image = Properties.Resources.microsoft_black;
                postgresqlCheckButton.Values.Image = Properties.Resources.postgresql_black;
            }
            else if (Input.ActiveTheme.IconType == "light")
            {
                mysqlCheckButton.Values.Image = Properties.Resources.mysql_white;
                mssqlCheckButton.Values.Image = Properties.Resources.microsoft_white;
                postgresqlCheckButton.Values.Image = Properties.Resources.postgresql_white;
            }

            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            if (Input != null && Input.Database != null)
            {
                databaseConnectionNameTextBox.Text = Input.Database.Name;
                databaseHostnameTextBox.Text = Input.Database.Hostname;
                databasePortTextBox.Text = Input.Database.Port.ToString();
                databaseNameTextBox.Text = Input.Database.DatabaseName;
                databaseUsernameTextBox.Text = Input.Database.Username;
                databasePasswordTextBox.Text = Input.Database.Password;
                selectedDatabaseType = Input.Database.DatabaseType;
                
                switch(Input.Database.DatabaseType)
                {
                    case DatabaseType.MySQL:
                        mysqlCheckButton.Checked = true;
                        break;
                    case DatabaseType.MSSQL:
                        mssqlCheckButton.Checked = true;
                        break;
                    case DatabaseType.PostgreSQL:
                        postgresqlCheckButton.Checked = true;
                        break;
                }
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
            if (databaseConnectionNameTextBox.Text == "" || databaseHostnameTextBox.Text == "" || databasePortTextBox.Text == "" || databaseNameTextBox.Text == "" || databaseUsernameTextBox.Text == "" || databasePasswordTextBox.Text == "" || selectedDatabaseType == DatabaseType.None)
            {
                ShowNotification("Database fields cannot be empty.");
            }
            else
            {
                int port;
                bool result = int.TryParse(databasePortTextBox.Text, out port);

                if (!result)
                {
                    ShowNotification("Port needs to be a number.");
                }
                else
                {
                    DatabaseConnection databaseConnection = new DatabaseConnection();

                    databaseConnection.Name = databaseConnectionNameTextBox.Text;
                    databaseConnection.Hostname = databaseHostnameTextBox.Text;
                    databaseConnection.Port = port;
                    databaseConnection.DatabaseName = databaseNameTextBox.Text;
                    databaseConnection.Username = databaseUsernameTextBox.Text;
                    databaseConnection.Password = databasePasswordTextBox.Text;
                    databaseConnection.DatabaseType = selectedDatabaseType;

                    Output.Database = databaseConnection;
                }

                this.Close();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (databaseConnectionNameTextBox.Text == "" || databaseHostnameTextBox.Text == "" || databasePortTextBox.Text == "" || databaseNameTextBox.Text == "" || databaseUsernameTextBox.Text == "" || databasePasswordTextBox.Text == "" || selectedDatabaseType == null)
            {
                ShowNotification("Database fields cannot be empty.");
            }
            else
            {
                int port;
                bool result = int.TryParse(databasePortTextBox.Text, out port);

                if (!result)
                {
                    ShowNotification("Port needs to be a number.");
                }
                else
                {
                    DatabaseResponse dbConnectionCheckOutput = DatabaseService.CheckDatabaseConnection(selectedDatabaseType, databaseHostnameTextBox.Text, port, databaseNameTextBox.Text, databaseUsernameTextBox.Text, databasePasswordTextBox.Text);

                    if (dbConnectionCheckOutput.Success)
                    {
                        ShowNotification("Database connection established successfully.");
                    }
                    else
                    {
                        ShowNotification(dbConnectionCheckOutput.Message);
                    }
                }
            }
        }

        private void kryptonCheckButton1_Click(object sender, EventArgs e)
        {
            mysqlCheckButton.Checked = false;
            mssqlCheckButton.Checked = false;
            postgresqlCheckButton.Checked = false;

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
