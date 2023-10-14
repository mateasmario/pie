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
using pie.Enums;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

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
                            else if (token == "databaseType")
                            {
                                if (jsonTextReader.Value.ToString() == "MySQL")
                                {
                                    database.DatabaseType = DatabaseType.MySQL;
                                }
                                else if (jsonTextReader.Value.ToString() == "MSSQL")
                                {
                                    database.DatabaseType = DatabaseType.MSSQL;
                                }
                            }
                            else if (token == "hostname")
                            {
                                database.Hostname = jsonTextReader.Value.ToString();
                            }
                            else if (token == "databaseName")
                            {
                                database.DatabaseName = jsonTextReader.Value.ToString();
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
                        else if (jsonTextReader.TokenType == JsonToken.Integer)
                        {
                            if (token == "port")
                            {
                                database.Port = int.Parse(jsonTextReader.Value.ToString());
                            }
                        }
                    }
                }
            }

            return databases;
        }

        public static void WriteDatabasesToFile(string file, List<DatabaseConnection> tempDatabases)
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
                    writer.WritePropertyName("databaseType");
                    if (database.DatabaseType == DatabaseType.MySQL)
                    {
                        writer.WriteValue("MySQL");
                    }
                    else if (database.DatabaseType == DatabaseType.MSSQL)
                    {
                        writer.WriteValue("MSSQL");
                    }
                    writer.WritePropertyName("hostname");
                    writer.WriteValue(database.Hostname);                        
                    writer.WritePropertyName("port");
                    writer.WriteValue(database.Port);
                    writer.WritePropertyName("databaseName");
                    writer.WriteValue(database.DatabaseName);
                    writer.WritePropertyName("username");
                    writer.WriteValue(database.Username);                    
                    writer.WritePropertyName("password");
                    writer.WriteValue(database.Password);
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }

        public static Tuple<bool, string> CheckDatabaseConnection(DatabaseType databaseType, string hostname, int port, string databaseName,  string username, string password)
        {
            if (databaseType == DatabaseType.MySQL)
            {
                string myConnectionString = "server=" + hostname + ";port=" + port + ";database=" + databaseName + ";uid=" + username + ";pwd=" + password + ";";
                MySqlConnection cnn = new MySqlConnection(myConnectionString);
                try
                {
                    cnn.Open();
                    cnn.Close();
                    return new Tuple<bool, string>(true, null);
                }
                catch (Exception ex)
                {
                    return new Tuple<bool, string>(false, ex.InnerException.Message);
                }
            }
            else if (databaseType == DatabaseType.MSSQL)
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = hostname + "," + port,
                    InitialCatalog = databaseName,
                    UserID = username,
                    Password = password
                };

                using (var l_oConnection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    try
                    {
                        l_oConnection.Open();
                        return new Tuple<bool, string>(true, null);
                    }
                    catch (SqlException ex)
                    {
                        return new Tuple<bool, string>(false, ex.InnerException.Message);
                    }
                    catch(InvalidOperationException ex)
                    {
                        return new Tuple<bool, string>(false, ex.Message);
                    }
                }
            }
            else
            {
                return new Tuple<bool, string>(false, null);
            }
        }
    }
}
