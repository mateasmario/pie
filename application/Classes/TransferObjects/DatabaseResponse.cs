/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Data;

namespace pie.Classes
{
    public class DatabaseResponse
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public DataTable DataTable { get; private set; }

        public DatabaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public DatabaseResponse(bool success, string message, DataTable dataTable)
        {
            Success = success;
            Message = message;
            DataTable = dataTable;
        }
    }
}
