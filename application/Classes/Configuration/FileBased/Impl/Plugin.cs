/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;
using plugin.Classes;
using System;
using System.Collections.Generic;

namespace pie.Classes.Configuration.FileBased.Impl
{
    public class Plugin : MultiFileConfigurationEntity
    {
        public object Instance { get; set; }
        public Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> GetTasks()
        {
            return (Dictionary <PluginTask, Func <PluginTaskInput, PluginTaskOutput>>) Instance.GetType().GetMethod("GetTaskDictionary").Invoke(Instance, null);
        }
        public PluginTaskOutput InvokeTask(Func<PluginTaskInput, PluginTaskOutput> task, PluginTaskInput pluginTaskInput)
        {
            return (PluginTaskOutput)Instance.GetType().GetMethod(task.ToString()).Invoke(Instance, [pluginTaskInput]);
        }
    }
}
