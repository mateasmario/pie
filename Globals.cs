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

        public static string getCommitMessage()
        {
            return commitMessage;
        }

        public static void setCommitMessage(string commitMessageNew)
        {
            commitMessage = commitMessageNew;
        }
    }
}
