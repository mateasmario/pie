using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using pie.Classes;

namespace pie
{
    public class Globals
    {
        public static string commitMessage;
        public static int lastSelectedTabIndex;
        public static int maxLineNumberCharLength;
        public static List<BuildCommand> buildCommands;
        public static List<ToolStripMenuItem> buildCommandToolStripMenuItems;
        public static string addBuildCommandName;
        public static string addBuildCommandCmd;
        public static bool closeAfterApplyingChanges;
        public static bool findReplacePanelToggled;
        public static int lastSelectedIndex;
        public static bool mouseDown;
        public static Point lastLocation;
        public static bool canUpdateUI;
        public static bool firstBrowserTab;
        public static List<TabInfo> tabInfos;
        public static int theme;
    }
}
