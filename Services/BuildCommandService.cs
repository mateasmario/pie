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

using System;
using System.Collections.Generic;
using pie.Classes;
using System.IO;

namespace pie.Services
{
    public class BuildCommandService
    {
        public static List<BuildCommand> GetBuildCommandsFromFile(string file)
        {
            List<BuildCommand> buildCommands = new List<BuildCommand>();

            IEnumerable<String> lines = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + file);
            int rowNum = 0;

            BuildCommand buildCommand = null;

            foreach (string line in lines)
            {
                if (rowNum % 2 == 0)
                {
                    buildCommand = new BuildCommand();
                    buildCommand.setName(line);
                }
                else if (buildCommand != null)
                {
                    buildCommand.setCommand(line);
                    buildCommands.Add(buildCommand);
                }
                else
                {
                    throw new Exception("There was an error while procesing the build commands.");
                }
                rowNum++;
            }

            return buildCommands;
        }
    }
}
