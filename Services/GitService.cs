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
using System.IO;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

namespace pie.Services
{
    internal class GitService
    {
        public static GitCredentials ReadCredentialsFromFile(String file)
        {
            GitCredentials gitCredentials = new GitCredentials();

            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                string token = null;

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
                                gitCredentials.Name = jsonTextReader.Value.ToString();
                            }
                            else if (token == "email")
                            {
                                gitCredentials.Email = jsonTextReader.Value.ToString();
                            }
                            else if (token == "username")
                            {
                                gitCredentials.Username = jsonTextReader.Value.ToString();
                            }
                            else if (token == "password")
                            {
                                gitCredentials.Password = jsonTextReader.Value.ToString();
                            }
                        }
                    }
                }
            }

            return gitCredentials;
        }

        public static void WriteCredentials(GitCredentials gitCredentials)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/git.json", "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "config/git.json");

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(gitCredentials.Name != null ? gitCredentials.Name : "");
                writer.WritePropertyName("email");
                writer.WriteValue(gitCredentials.Email != null ? gitCredentials.Email : "");
                writer.WritePropertyName("username");
                writer.WriteValue(gitCredentials.Username != null ? gitCredentials.Username : "");
                writer.WritePropertyName("password");
                writer.WriteValue(gitCredentials.Password != null ? gitCredentials.Password : "");
                writer.WriteEndObject();
            }
        }
    }
}
