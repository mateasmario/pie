using pie.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace pie.Services
{
    internal class GitService
    {
        public static GitCredentials ReadCredentialsFromFile()
        {
            GitCredentials gitCredentials = new GitCredentials();

            IEnumerable<string> lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "git.config");
            List<string> lineList = lines.ToList();

            if (lineList.Count < 2)
            {
                return gitCredentials;
            }

            gitCredentials.Name = lineList[0];
            gitCredentials.Email = lineList[1];

            if (lineList.Count == 4)
            {
                gitCredentials.Username = lineList[2];
                gitCredentials.Password = lineList[3];
            }

            return gitCredentials;
        }
    }
}
