/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace pie.Classes;

public class PluginAction
{
}

public class CreateFileAction : PluginAction
{
    public string FileName { get; set; }
    public string Content { get; set; }
}

public class CreateDirectoryAction : PluginAction
{
    public string DirectoryName { get; set; }
}

public class ModifyEditorContentAction : PluginAction
{
    public int TabIndex { get; set; }
    public string Content { get; set; }
}