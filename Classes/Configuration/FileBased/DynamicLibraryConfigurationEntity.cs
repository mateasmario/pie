/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Reflection;

namespace pie.Classes.ConfigurationEntities
{
    public class DynamicLibraryConfigurationEntity : MultiFileConfigurationEntity
    {
        public object Instance { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string InvokeMethod(string argument)
        {
            return (string)this.MethodInfo.Invoke(this.Instance, new object[] { argument });
        }
    }
}
