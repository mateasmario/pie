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
 * Copyright (Tc) 2007 James Newton-King
 */
using Newtonsoft.Json;

/**
 * Used for communication between pie and MySQL Databases
 * 
 * Copyright (c) 2016 Alan Savage
 */
using MySql.Data.MySqlClient;

/**
 * Used for communication between pie and PostgreSQL Databases
 * 
 * Copyright (c) 2002-2023, Npgsql
 */
using Npgsql;
using System.Data.Common;

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
                            if (token.ToLower() == "connectionname")
                            {
                                database = new DatabaseConnection();
                                database.ConnectionName = jsonTextReader.Value.ToString();
                            }
                            else if (token.ToLower() == "databasetype")
                            {
                                if (jsonTextReader.Value.ToString() == "MySQL")
                                {
                                    database.DatabaseType = DatabaseType.MySQL;
                                }
                                else if (jsonTextReader.Value.ToString() == "MSSQL")
                                {
                                    database.DatabaseType = DatabaseType.MSSQL;
                                }
                                else if (jsonTextReader.Value.ToString() == "PostgreSQL")
                                {
                                    database.DatabaseType = DatabaseType.PostgreSQL;
                                }
                            }
                            else if (token.ToLower() == "hostname")
                            {
                                database.Hostname = jsonTextReader.Value.ToString();
                            }
                            else if (token.ToLower() == "databasename")
                            {
                                database.DatabaseName = jsonTextReader.Value.ToString();
                            }
                            else if (token.ToLower() == "username")
                            {
                                database.Username = jsonTextReader.Value.ToString();
                            }
                            else if (token.ToLower() == "password")
                            {
                                database.Password = jsonTextReader.Value.ToString();
                                databases.Add(database);
                            }
                        }
                        else if (jsonTextReader.TokenType == JsonToken.Integer)
                        {
                            if (token.ToLower() == "port")
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
                    else if (database.DatabaseType == DatabaseType.PostgreSQL)
                    {
                        writer.WriteValue("PostgreSQL");
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

        public static DatabaseResponse CheckDatabaseConnection(DatabaseType databaseType, string hostname, int port, string databaseName,  string username, string password)
        {
            if (databaseType == DatabaseType.MySQL)
            {
                string myConnectionString = "server=" + hostname + ";port=" + port + ";database=" + databaseName + ";uid=" + username + ";pwd=" + password + ";";
                return AttemptConnection(new MySqlConnection(myConnectionString));
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

                return AttemptConnection(new SqlConnection(connectionStringBuilder.ConnectionString));
            }
            else if (databaseType == DatabaseType.PostgreSQL)
            {
                string strConnString = "Server=" + hostname + ";Port=" + port + ";User Id=" + username + ";Password=" + password + ";Database=" + databaseName;
                return AttemptConnection(new NpgsqlConnection(strConnString));
            }

            return null;
        }

        private static DatabaseResponse AttemptConnection(IDbConnection connection)
        {
            try
            {
                connection.Open();
                connection.Close();
                return new DatabaseResponse(true, null);
            }
            catch (DbException ex)
            {
                return new DatabaseResponse(false, ex.InnerException.Message);
            }
            catch (InvalidOperationException ex)
            {
                return new DatabaseResponse(false, ex.Message);
            }
        }

        public static DatabaseResponse ExecuteSQLCommand(string query, DatabaseConnection databaseConnection)
        {
            DataTable dt = new DataTable();

            if (databaseConnection.DatabaseType == DatabaseType.MySQL)
            {
                string myConnectionString = "server=" + databaseConnection.Hostname + ";port=" + databaseConnection.Port + ";database=" + databaseConnection.DatabaseName + ";uid=" + databaseConnection.Username + ";pwd=" + databaseConnection.Password + ";";
                MySqlConnection cnn = new MySqlConnection(myConnectionString);
                try
                {
                    cnn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, cnn);

                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);

                    cnn.Close();

                    if (dt.Columns.Count == 0)
                    {
                        return new DatabaseResponse(false, "No columns returned.", null);
                    }

                    return new DatabaseResponse(true, null, dt);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        return new DatabaseResponse(false, ex.InnerException.Message, null);
                    }
                    else
                    {
                        return new DatabaseResponse(false, ex.Message, null);
                    }
                }
            }
            else if (databaseConnection.DatabaseType == DatabaseType.MSSQL)
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = databaseConnection.Hostname + "," + databaseConnection.Port,
                    InitialCatalog = databaseConnection.DatabaseName,
                    UserID = databaseConnection.Username,
                    Password = databaseConnection.Password
                };

                using (var l_oConnection = new SqlConnection(connectionStringBuilder.ConnectionString))
                {
                    try
                    {
                        l_oConnection.Open();

                        SqlCommand cmd = l_oConnection.CreateCommand();
                        cmd.CommandText = query;

                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        cmd.CommandType = CommandType.Text;

                        var rows_returned = sda.Fill(dt);

                        l_oConnection.Close();

                        if (dt.Columns.Count == 0)
                        {
                            return new DatabaseResponse(false, "No columns returned", null);
                        }

                        return new DatabaseResponse(true, null, dt);
                    }
                    catch (SqlException ex)
                    {
                        if (ex.InnerException != null)
                        {
                            return new DatabaseResponse(false, ex.InnerException.Message, null);
                        }
                        else
                        {
                            return new DatabaseResponse(false, ex.Message, null);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        return new DatabaseResponse(false, ex.Message, null);
                    }
                }
            }
            else if (databaseConnection.DatabaseType == DatabaseType.PostgreSQL)
            {
                string strConnString = "Server=" + databaseConnection.Hostname + ";Port=" + databaseConnection.Port + ";User Id=" + databaseConnection.Username + ";Password=" + databaseConnection.Password + ";Database=" + databaseConnection.DatabaseName;

                try
                {
                    NpgsqlConnection conn = new NpgsqlConnection(strConnString);
                    conn.Open();

                    NpgsqlCommand command = new NpgsqlCommand(query, conn);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);

                    adapter.Fill(dt);

                    conn.Close();

                    if (dt.Columns.Count == 0)
                    {
                        return new DatabaseResponse(false, "No columns returned", null);
                    }

                    return new DatabaseResponse(true, null, dt);
                }
                catch (NpgsqlException ex)
                {
                    return new DatabaseResponse(false, ex.InnerException.Message, null);
                }
            }
            else
            {
                return new DatabaseResponse(false, null, null);
            }
        }
    }
}
