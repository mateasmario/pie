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

using pie.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
