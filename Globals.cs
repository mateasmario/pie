using System;
using System.Collections.Generic;
using System.Windows.Forms;
using pie.Classes;

namespace pie
{
    public class Globals
    {
        private static string commitMessage;
        private static int lastSelectedTabIndex;
        private static int maxLineNumberCharLength;
        private static List<BuildCommand> buildCommands;
        private static List<ToolStripMenuItem> buildCommandToolStripMenuItems;
        private static string addBuildCommandName;
        private static string addBuildCommandCmd;

        public static string getCommitMessage()
        {
            return commitMessage;
        }

        public static void setCommitMessage(string commitMessageNew)
        {
            commitMessage = commitMessageNew;
        }

        public static int getLastSelectedTabIndex()
        {
            return lastSelectedTabIndex;
        }

        public static void setLastSelectedTabIndex(int lastSelectedTabIndexNew)
        {
            lastSelectedTabIndex = lastSelectedTabIndexNew;
        }

        public static int getMaxLineNumberCharLength()
        {
            return maxLineNumberCharLength;
        }

        public static void setMaxLineNumberCharLength(int maxLineNumberCharLengthNew)
        {
            maxLineNumberCharLength = maxLineNumberCharLengthNew;
        }

        public static List<BuildCommand> getBuildCommands()
        {
            return buildCommands;
        }

        public static void setBuildCommands(List<BuildCommand> buildCommandsNew)
        {
            buildCommands = buildCommandsNew;
        }

        public static List<ToolStripMenuItem> getBuildCommandsToolstripMenuItems()
        {
            return buildCommandToolStripMenuItems;
        }

        public static void setBuildCommandsToolstripMenuItems(List<ToolStripMenuItem> buildCommandToolStripMenuItemsNew)
        {
            buildCommandToolStripMenuItems = buildCommandToolStripMenuItemsNew;
        }

        public static string getAddBuildCommandName()
        {
            return addBuildCommandName;
        }

        public static void setAddBuildCommandName(string addBuildCommandNameNew)
        {
            addBuildCommandName = addBuildCommandNameNew;
        }

        public static string getAddBuildCommandCmd()
        {
            return addBuildCommandCmd;
        }

        public static void setAddBuildCommandCmd(string addBuildCommandCmdNew)
        {
            addBuildCommandCmd = addBuildCommandCmdNew;
        }
    }
}
