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
