﻿/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using Krypton.Toolkit;
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
using pie.Classes;
using pie.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace pie.Services
{
    public class DatabaseService
    {
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
            catch (Exception ex)
            {
                return new DatabaseResponse(false, "There was an error while trying to connect to the database.");
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
            else
            {
                return new DatabaseResponse(false, null, null);
            }
        }
    }
}
