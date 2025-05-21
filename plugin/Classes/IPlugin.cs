/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace plugin.Classes;

public interface IPlugin
{
    Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> GetTaskDictionary();
}
