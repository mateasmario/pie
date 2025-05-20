/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace plugin.Classes;

public class PluginTask
{
    public string Name { get; set; }
    public PluginTask(string name)
    {
        Name = name;
    }
}