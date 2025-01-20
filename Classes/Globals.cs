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

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using pie.Classes;
using pie.Enums;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

namespace pie
{
    public class Globals
    {
        // General
        public static List<TabInfo> tabInfos = new List<TabInfo>();
        public static int lastSelectedTabIndex;
        public static int maxLineNumberCharLength;
        public static bool closeAfterApplyingChanges;
        public static int lastSelectedIndex;
        public static bool canUpdateUI;
        public static bool firstBrowserTab;
        public static bool deletesLastTab;
        public static bool deletesTab;

        // Theme
        public static ThemeInfo theme;
        public static KryptonPalette kryptonPalette;
        public static List<ThemeInfo> themeInfos;
        public static Dictionary<string, Color> colorDictionary;
        public static string newThemeName;

        // Build Commands
        public static List<BuildCommand> buildCommands;
        public static List<ToolStripMenuItem> buildCommandToolStripMenuItems = new List<ToolStripMenuItem>();
        public static string addBuildCommandName;
        public static string addBuildCommandCmd;
        public static int buildCommandEditIndex = -1;
        public static string buildCommandToEditName;
        public static string buildCommandToEditCmd;

        // Find & Replace Panel
        public static bool mouseDown;
        public static Point lastLocation;

        // Git
        public static Repository repo;
        public static Branch selectedBranch;
        public static GitCredentials gitCredentials = new GitCredentials();
        public static bool gitTabOpened;
        public static bool gitFormClosedWithOk;
        public static string clonePath;
        public static int selectedBranchIndex;
        public static bool doNotTriggerBranchChangeEvent;
        public static bool doNotShowBranchChangeNotification;
        public static bool showGitTabPressed;

        // Notification
        public static string notificationText;
        public static NotificationButton notificationButtonPressed;

        // Databases
        public static List<DatabaseConnection> databases;
        public static string addDatabaseConnectionName;
        public static string addDatabaseHostname;
        public static int addDatabasePort;
        public static string addDatabaseDbName;
        public static string addDatabaseUsername;
        public static string addDatabasePassword;
        public static DatabaseType addDatabaseType;
        public static int databaseEditIndex;
        public static string databaseToEditConnectionName;
        public static string databaseToEditHostname;
        public static int databaseToEditPort;
        public static string databaseToEditDbName;
        public static string databaseToEditUsername;
        public static string databaseToEditPassword;
        public static DatabaseType databaseToEditType;

        // Scintilla preferences
        public static EditorProperties editorProperties;
        public static List<LanguageDefinition> languageDefinitions;
        public static List<LanguageMapping> languageMappings;

        // Format
        public static List<Formatter> formatters;
    }
}
