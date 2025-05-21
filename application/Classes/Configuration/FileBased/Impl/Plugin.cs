/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;
using plugin.Classes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace pie.Classes.Configuration.FileBased.Impl
{
    public class Plugin : MultiFileConfigurationEntity
    {
        public object Instance { get; set; }
        public string GetName()
        {
            return (string)Instance.GetType().GetMethod("GetName").Invoke(Instance, null);
        }
        public Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> GetTasks()
        {
            return (Dictionary <PluginTask, Func <PluginTaskInput, PluginTaskOutput>>) Instance.GetType().GetMethod("GetTaskDictionary").Invoke(Instance, null);
        }
        public PluginTaskOutput InvokeTask(Func<PluginTaskInput, PluginTaskOutput> taskFunction, PluginTaskInput pluginTaskInput)
        {
            MethodInfo method = Instance.GetType().GetMethod(taskFunction.GetMethodInfo().Name);
            var output = method.Invoke(Instance, [pluginTaskInput]);
            return (PluginTaskOutput)output;
        }
    }
}
