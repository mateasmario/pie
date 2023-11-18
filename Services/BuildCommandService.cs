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
using System.IO;
using pie.Classes;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

namespace pie.Services
{
    public class BuildCommandService
    {
        public static List<BuildCommand> GetBuildCommandsFromFile(string file)
        {
            List<BuildCommand> buildCommands = new List<BuildCommand>();

            BuildCommand buildCommand = null;

            string token = null;

            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.Value != null)
                    {
                        if (jsonTextReader.TokenType == JsonToken.PropertyName)
                        {
                            token = jsonTextReader.Value.ToString();
                        }
                        else if (jsonTextReader.TokenType == JsonToken.String)
                        {
                            if (token == "name")
                            {
                                buildCommand = new BuildCommand();
                                buildCommand.BuildCommandName = jsonTextReader.Value.ToString();
                            }
                            else if (token == "command")
                            {
                                buildCommand.BuildCommandCmd = jsonTextReader.Value.ToString();
                                buildCommands.Add(buildCommand);
                            }
                        }
                    }
                }
            }

            return buildCommands;
        }

        internal static void WriteBuildCommandsToFile(string file, List<BuildCommand> tempCommands)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();

                foreach (BuildCommand buildCommand in tempCommands)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("name");
                    writer.WriteValue(buildCommand.BuildCommandName);                    
                    writer.WritePropertyName("command");
                    writer.WriteValue(buildCommand.BuildCommandCmd);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }
    }
}
