/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace plugin.Classes;

public class PluginContext
{
    public Dictionary<string, object> Map { get; set; }
    public PluginContext()
    {
        Map = new Dictionary<string, object>();
    }
}