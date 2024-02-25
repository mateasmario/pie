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
using System.Data;
using System.Data.SqlClient;
using pie.Classes;
using pie.Enums;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

namespace pie.Services
{
    public class EditorPropertiesService
    {
        public static void GetEditorPropertiesFromFile(string file)
        {
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
                        else if (jsonTextReader.TokenType == JsonToken.Boolean)
                        {
                            bool value = (bool)jsonTextReader.Value;
                            if (token == "wordwrap")
                            {
                                Globals.wordwrap = value;
                            }
                            else if (token == "autosave")
                            {
                                Globals.autosave = value;
                            }
                            else if (token == "glass")
                            {
                                Globals.glass = value;
                            }
                        }
                    }
                }
            }
        }

        public static void WriteEditorPropertiesToFile(string file, bool wordwrap, bool autosave, bool glass)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("wordwrap");
                writer.WriteValue(wordwrap);
                writer.WritePropertyName("autosave");
                writer.WriteValue(autosave);
                writer.WritePropertyName("glass");
                writer.WriteValue(glass);
                writer.WriteEndObject();
            }
        }
    }
}