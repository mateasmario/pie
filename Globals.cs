using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pie
{
    public class Globals
    {
        private static string commitMessage;
        private static int lastSelectedTabIndex;
        private static int maxLineNumberCharLength;

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
    }
}
