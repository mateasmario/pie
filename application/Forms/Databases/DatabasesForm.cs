/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Constants;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace pie.Forms.Databases
{
    public partial class DatabasesForm : KryptonForm
    {
        private ConfigurationService configurationService = new ConfigurationService();
        private ThemingService themeService = new ThemingService();

        public DatabasesFormInput Input { get; set; }
        public DatabasesFormOutput Output { get; set; }
        private List<DatabaseConnection> tempDatabases;

        public DatabasesForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Output = new DatabasesFormOutput();
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

        private void DatabasesForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            tempDatabases = new List<DatabaseConnection>();

            SynchronizeObjectListViewWithTheme();

            if (Input.Databases != null)
            {
                foreach (DatabaseConnection database in Input.Databases)
                {
                    tempDatabases.Add(database);
                }

                databasesListView.SetObjects(tempDatabases);
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

        private void SynchronizeObjectListViewWithTheme()
        {
            databasesListView.ShowGroups = false;
            databasesListView.UseCustomSelectionColors = true;
            databasesListView.FullRowSelect = true;
            databasesListView.MultiSelect = false;
            databasesListView.HeaderStyle = ColumnHeaderStyle.None;

            databasesListView.BackColor = Input.ActiveTheme.Primary;
            databasesListView.ForeColor = Input.ActiveTheme.Fore;
            databasesListView.HighlightBackgroundColor = Input.ActiveTheme.Secondary;
            databasesListView.HighlightForegroundColor = Input.ActiveTheme.Fore;
            databasesListView.UnfocusedHighlightBackgroundColor = Input.ActiveTheme.Secondary;
            databasesListView.UnfocusedHighlightForegroundColor = Input.ActiveTheme.Fore;

            ConnectionNameColumn.FillsFreeSpace = true;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AddDatabaseForm addDatabaseForm = new AddDatabaseForm();

            AddDatabaseFormInput addDatabaseFormInput = new AddDatabaseFormInput();
            addDatabaseFormInput.EditorProperties = Input.EditorProperties;
            addDatabaseFormInput.Palette = Input.Palette;
            addDatabaseFormInput.ActiveTheme = Input.ActiveTheme;

            addDatabaseForm.Input = addDatabaseFormInput;

            addDatabaseForm.ShowDialog();

            if (addDatabaseForm.Output.Database != null)
            {
                DatabaseConnection database = addDatabaseForm.Output.Database;

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
                    ShowNotification("A database entry with the same name already exists.");
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
                ShowNotification("Please choose an item to delete.");
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
                AddDatabaseForm addDatabaseForm = new AddDatabaseForm();

                int databaseEditIndex = databasesListView.SelectedIndex;

                DatabaseConnection databaseConnection = new DatabaseConnection();
                databaseConnection.Name = tempDatabases[databaseEditIndex].Name;
                databaseConnection.Hostname = tempDatabases[databaseEditIndex].Hostname;
                databaseConnection.Port = tempDatabases[databaseEditIndex].Port;
                databaseConnection.DatabaseName = tempDatabases[databaseEditIndex].DatabaseName;
                databaseConnection.Username = tempDatabases[databaseEditIndex].Username;
                databaseConnection.Password = tempDatabases[databaseEditIndex].Password;
                databaseConnection.DatabaseType = tempDatabases[databaseEditIndex].DatabaseType;

                AddDatabaseFormInput addDatabaseFormInput = new AddDatabaseFormInput();
                addDatabaseFormInput.Database = databaseConnection;
                addDatabaseFormInput.EditorProperties = Input.EditorProperties;
                addDatabaseFormInput.Palette = Input.Palette;
                addDatabaseFormInput.ActiveTheme = Input.ActiveTheme;

                addDatabaseForm.Input = addDatabaseFormInput;

                addDatabaseForm.ShowDialog();

                if (addDatabaseForm.Output.Database != null)
                {
                    bool exists = false;

                    foreach (DatabaseConnection tempDatabase in tempDatabases)
                    {
                        if (tempDatabase.Name != ((DatabaseConnection)databasesListView.SelectedObject).Name && tempDatabase.Name.Equals(addDatabaseForm.Output.Database.Name))
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (exists)
                    {
                        ShowNotification("A database entry with the same name already exists.");
                    }
                    else
                    {
                        tempDatabases[databaseEditIndex] = addDatabaseForm.Output.Database;
                    }
                }

                databasesListView.SetObjects(tempDatabases);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            configurationService.WriteToFile(System.IO.Path.Combine(SpecialFolders.Config, "databases.json"), tempDatabases);

            Output.Saved = true;

            this.Close();
        }
    }
}
