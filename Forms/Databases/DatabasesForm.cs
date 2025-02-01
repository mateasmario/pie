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
using System.Windows.Forms;
using System.Collections.Generic;
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
    public partial class DatabasesForm : KryptonForm
    {
        private ConfigurationService configurationService = new ConfigurationService();
        private ThemeService themeService = new ThemeService();

        public bool OutputSaveStatus;
        private List<DatabaseConnection> tempDatabases;

        public DatabasesForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            databasesListView.ShowGroups = false;
            databasesListView.UseCustomSelectionColors = true;
            databasesListView.FullRowSelect = true;
            databasesListView.MultiSelect = false;
            databasesListView.HeaderStyle = ColumnHeaderStyle.None;

            databasesListView.BackColor = Globals.theme.Primary;
            databasesListView.ForeColor = Globals.theme.Fore;
            databasesListView.HighlightBackgroundColor = Globals.theme.Secondary;
            databasesListView.HighlightForegroundColor = Globals.theme.Fore;
            databasesListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            databasesListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;

            ConnectionNameColumn.FillsFreeSpace = true;
        }

        private void DatabasesForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            tempDatabases = new List<DatabaseConnection>();

            SynchronizeObjectListViewWithTheme();

            if (Globals.databases != null)
            {
                foreach (DatabaseConnection database in Globals.databases)
                {
                    tempDatabases.Add(database);
                }

                databasesListView.SetObjects(tempDatabases);
            }

            OutputSaveStatus = false;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AddDatabaseForm addDatabaseForm = new AddDatabaseForm();
            addDatabaseForm.ShowDialog();

            if (Globals.addDatabaseConnectionName != null)
            {
                DatabaseConnection database = new DatabaseConnection();
                database.Name = Globals.addDatabaseConnectionName;
                database.DatabaseType = Globals.addDatabaseType;
                database.Hostname = Globals.addDatabaseHostname;
                database.Port = Globals.addDatabasePort;
                database.DatabaseName = Globals.addDatabaseDbName;
                database.Username = Globals.addDatabaseUsername;
                database.Password = Globals.addDatabasePassword;

                bool exists = false;

                // Check if a database connection with the specified name already exists
                foreach (DatabaseConnection tempDatabase in tempDatabases)
                {
                    if (tempDatabase.Name.Equals(database.Name))
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    MainForm.ShowNotification("A database entry with the same name already exists.");
                }
                else
                {
                    tempDatabases.Add(database);
                    databasesListView.SetObjects(tempDatabases);
                }
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (databasesListView.SelectedItems.Count == 0)
            {
                MainForm.ShowNotification("Please choose an item to delete.");
            }
            else
            {
                foreach (var s in databasesListView.SelectedObjects)
                {
                    DatabaseConnection database = (DatabaseConnection)s;

                    if (tempDatabases.Contains(database))
                    {
                        tempDatabases.Remove(database);
                    }
                }

                databasesListView.SetObjects(tempDatabases);
            }
        }

        private void databasesListView_DoubleClick(object sender, EventArgs e)
        {
            if (databasesListView.SelectedObjects.Count == 1)
            {
                Globals.databaseEditIndex = databasesListView.SelectedIndex;

                Globals.databaseToEditConnectionName = tempDatabases[Globals.databaseEditIndex].Name;
                Globals.databaseToEditType = tempDatabases[Globals.databaseEditIndex].DatabaseType;
                Globals.databaseToEditHostname = tempDatabases[Globals.databaseEditIndex].Hostname;
                Globals.databaseToEditPort = tempDatabases[Globals.databaseEditIndex].Port;
                Globals.databaseToEditDbName = tempDatabases[Globals.databaseEditIndex].DatabaseName;
                Globals.databaseToEditUsername = tempDatabases[Globals.databaseEditIndex].Username;
                Globals.databaseToEditPassword = tempDatabases[Globals.databaseEditIndex].Password;

                AddDatabaseForm addDatabaseForm = new AddDatabaseForm();
                addDatabaseForm.ShowDialog();

                if (Globals.addDatabaseConnectionName != null)
                {
                    bool exists = false;

                    foreach (DatabaseConnection tempDatabase in tempDatabases)
                    {
                        if (tempDatabase.Name != Globals.databaseToEditConnectionName && tempDatabase.Name.Equals(Globals.addDatabaseConnectionName))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        MainForm.ShowNotification("A database entry with the same name already exists.");
                    }
                    else
                    {
                        tempDatabases[Globals.databaseEditIndex].Name = Globals.addDatabaseConnectionName;
                        tempDatabases[Globals.databaseEditIndex].DatabaseType = Globals.addDatabaseType;
                        tempDatabases[Globals.databaseEditIndex].Hostname = Globals.addDatabaseHostname;
                        tempDatabases[Globals.databaseEditIndex].Port = Globals.addDatabasePort;
                        tempDatabases[Globals.databaseEditIndex].DatabaseName = Globals.addDatabaseDbName;
                        tempDatabases[Globals.databaseEditIndex].Username = Globals.addDatabaseUsername;
                        tempDatabases[Globals.databaseEditIndex].Password = Globals.addDatabasePassword;
                    }
                }

                Globals.databaseEditIndex = -1;

                databasesListView.SetObjects(tempDatabases);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            configurationService.WriteToFile("config/databases.json", tempDatabases);

            OutputSaveStatus = true;

            this.Close();
        }
    }
}
