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
            IEnumerable<string> lines = File.ReadLines(file);

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
