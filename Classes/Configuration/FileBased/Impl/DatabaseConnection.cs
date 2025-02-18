/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Enums;
using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class DatabaseConnection : ConfigurationEntity
    {
        public string Name { get; set; }
        public string Hostname { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DatabaseType DatabaseType { get; set; }
    }
}
