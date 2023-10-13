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
using System.IO;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

namespace pie.Services
{
    public class DatabaseService
    {
        public static List<DatabaseConnection> GetDatabasesFromFile(string file)
        {
            List<DatabaseConnection> databases = new List<DatabaseConnection>();

            DatabaseConnection database = null;

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
                            if (token == "connectionName")
                            {
                                database = new DatabaseConnection();
                                database.ConnectionName = jsonTextReader.Value.ToString();
                            }
                            else if (token == "hostname")
                            {
                                database.Hostname = jsonTextReader.Value.ToString();
                            }
                            else if (token == "username")
                            {
                                database.Username = jsonTextReader.Value.ToString();
                            }
                            else if (token == "password")
                            {
                                database.Password = jsonTextReader.Value.ToString();
                                databases.Add(database);
                            }
                        }
                    }
                }
            }

            return databases;
        }

        internal static void WriteDatabasesToFile(string file, List<DatabaseConnection> tempDatabases)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartArray();

                foreach (DatabaseConnection database in tempDatabases)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("connectionName");
                    writer.WriteValue(database.ConnectionName);
                    writer.WritePropertyName("hostname");
                    writer.WriteValue(database.Hostname);                    
                    writer.WritePropertyName("username");
                    writer.WriteValue(database.Username);                    
                    writer.WritePropertyName("password");
                    writer.WriteValue(database.Password);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }
    }
}
