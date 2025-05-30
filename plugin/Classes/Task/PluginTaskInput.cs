/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace plugin.Classes;

public class Metadata
{
    public string Version { get; set; }
}

public class Tab
{
    public bool IsFileOpened { get; set; }
    public string FilePath { get; set; }
    public string Text { get; set; }
}

public class PluginTask
{
    public string Name { get; set; }
    public PluginTask(string name)
    {
        Name = name;
    }
}

public class PluginTaskInput
{
    public PluginContext Context { get; set; }
    public Metadata Metadata { get; set; }
    public List<Tab> Tabs { get; set; }
    public string OpenedDirectory { get; set; }
}
