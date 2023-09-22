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
        public static List<TabInfo> tabInfos;
        public static int lastSelectedTabIndex;
        public static int maxLineNumberCharLength;
        public static bool closeAfterApplyingChanges;
        public static int lastSelectedIndex;
        public static bool canUpdateUI;
        public static bool firstBrowserTab;

        // Theme
        public static int theme;
        public static KryptonPalette kryptonPalette;

        // Build Commands
        public static List<BuildCommand> buildCommands;
        public static List<ToolStripMenuItem> buildCommandToolStripMenuItems;
        public static string addBuildCommandName;
        public static string addBuildCommandCmd;

        // Find & Replace Panel
        public static bool mouseDown;
        public static Point lastLocation;
        public static bool findReplacePanelToggled;

        // Git
        public static Repository repo;
        public static Branch selectedBranch;
        public static GitCredentials gitCredentials;
        public static bool gitTabOpened;
        public static bool gitFormClosedWithOk;
        public static string clonePath;
        public static int selectedBranchIndex;

        // Notification
        public static string notificationText;
        public static NotificationButton notificationButtonPressed;
    }
}
