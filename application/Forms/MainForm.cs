/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/**
 * AutocompleteMenuNS is a namespace that comes from AutoCompleteMenu-ScintillaNet. It is used for various Autocomplete suggestions while writing code.
 * 
 * AutoCompleteMenu-ScintillaNet is licensed under the GNU Lesser General Public License (LGPLv3).
 * For more information related to the license, see https://www.gnu.org/licenses/lgpl-3.0.html.
 * 
 */
using AutocompleteMenuNS;
/**
 * This namespace provides access to the ObjectListView control.
 * Licensed under GNU General Public License (GPL 3.0). For more info, see https://www.gnu.org/licenses/gpl-3.0.html 
 * 
 * Author of the library: Phillip Piper
 * Copyright 2006-2016 Bright Ideas Software
 */
using BrightIdeasSoftware;
/**
 * CefSharp is used in order to integrate Chromium-Based Web Browsers inside pie.
 * 
 * Copyright © The CefSharp Authors. All rights reserved.
 */
using CefSharp;
using CefSharp.WinForms;
/**
 * ConEmu.Winforms is used for integrating terminal features inside the application.
 * 
 * Copyright (c) 2021, Maksim Moisiuk <ConEmu.Maximus5@gmail.com>
 */
using ConEmu.WinForms;
/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Navigator;
using Krypton.Toolkit;
/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;
/** Markdig is used in order to allow users to render their Markdown (.md) code into HTML.
 * 
 * Copyright (c) 2018-2019, Alexandre Mutel
 */
using Markdig;
using Markdig.Extensions.Footnotes;
using pie.Classes;
using pie.Classes.Configuration.FileBased.Impl;
using pie.Constants;
using pie.Enums;
using pie.Forms.CodeTemplates;
using pie.Forms.Databases;
using pie.Forms.Format;
using pie.Forms.Git;
using pie.Forms.Other;
using pie.Forms.Theme;
using pie.Services;
using plugin.Classes;
/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class MainForm : KryptonForm
    {
        private ConfigurationService configurationService = new ConfigurationService();
        private ThemingService themeService = new ThemingService();
        private ParsingService parsingService = new ParsingService();
        private UpdateService updateService = new UpdateService();
        private ScintillaLexerService scintillaLexerService = new ScintillaLexerService();
        private SecureFileService secureFileService = new SecureFileService();
        private Win32APIService win32APIService = new Win32APIService();
        private PluginContext pluginContext = new PluginContext();

        private List<TabInfo> tabInfos = new List<TabInfo>();
        private List<ThemeInfo> themeInfos;
        private List<BuildCommand> buildCommands;
        private List<DatabaseConnection> databases;
        private List<CodeTemplate> codeTemplates;
        private List<Formatter> formatters;
        private List<Plugin> plugins;
        private List<LanguageDefinition> languageDefinitions;
        private List<LanguageMapping> languageMappings;
        private EditorProperties editorProperties;
        private GitCredentials gitCredentials;

        private Repository repository;
        private ThemeInfo activeTheme;

        private int lastSelectedIndex;
        private bool canUpdateUI;
        private bool firstBrowserTab;
        private bool deletesLastTab;
        private bool deletesTab;
        private Branch selectedBranch;
        private int selectedBranchIndex;
        private bool doNotTriggerBranchChangeEvent;
        private bool doNotShowBranchChangeNotification;
        private bool showGitTabPressed;
        private string openedFolder;
        private bool openedGitRepository;
        private bool firstDirectoryNavigatorToggle = true;
        private FindReplaceForm findReplaceForm = new FindReplaceForm();
        private bool isFindReplaceDialogShown;
        private DirNavModificationType dirNavModificationType;
        private string newFolderName;
        private bool doNotExpand;
        private int selectedRepositoryIndex;
        private bool doNotReselect;
        private bool doNotUpdateDirNav;

        public string[] Args;

        #region Overrides

        const int WM_CONTEXTMENU = 0x007B;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CONTEXTMENU)
            {
                m.Result = IntPtr.Zero;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        #endregion
        #region ParameterProcessing
        public delegate void ProcessParametersDelegate(string[] args);

        public void ProcessParameters(string[] args)
        {
            NewTab(TabType.CODE, null);

            if (args.Length == 2)
            {
                Open(args[1].ToString());
            }
        }
        #endregion
        #region Initialization
        public MainForm()
        {
            /*
             * Temporal coupling between ProcessCommandLineArguments and the initialization methods.
             * ProcessCommandLineArguments is called after the configuration data is loaded from files and the dynamic design is set.
             */
            EnableDoubleBuffering();
            InitializeComponent();
            GetConfigurationDataFromFiles();
            InitializeControls();
            ProcessCommandLineArguments();
            InitializeContextMenus();
        }

        private void EnableDoubleBuffering()
        {
            DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        private void GetConfigurationDataFromFiles()
        {
            MoveConfigurationFoldersToAppData();
            ProcessGitCredentials();
            ProcessThemes();
            ProcessEditorProperties();
            ProcessFormatterDLLs();
            ProcessPlugins();
            ProcessBuildCommands();
            ProcessDatabaseConnections();
            ProcessLanguageMappings();
            ProcessLanguageDefinitions();
            ProcessCodeTemplates();
        }

        private void MoveConfigurationFoldersToAppData()
        {
            if (!Directory.Exists(SpecialPaths.AppData))
            {
                Directory.CreateDirectory(SpecialPaths.AppData);
            }

            if (!Directory.Exists(SpecialPaths.Config))
            {
                Directory.CreateDirectory(SpecialPaths.Config);
            }

            CopyConfigToAppData("git.json");
            CopyConfigToAppData("theme.json");
            CopyConfigToAppData("scintilla.json");
            CopyConfigToAppData("build.json");
            CopyConfigToAppData("databases.json");
            CopyConfigToAppData("mappings.json");
            CopyConfigToAppData("templates.json");

            if (!Directory.Exists(SpecialPaths.Themes))
            {
                CopyDirectoryToAppData("themes");
            }

            if (!Directory.Exists(SpecialPaths.Languages))
            {
                CopyDirectoryToAppData("languages");
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }
        private void CopyConfigToAppData(string fileName)
        {
            if (!File.Exists(System.IO.Path.Combine(SpecialPaths.Config, fileName)))
            {
                File.Copy(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", fileName), System.IO.Path.Combine(SpecialPaths.Config, fileName));
            }
        }

        private void CopyDirectoryToAppData(string folderName)
        {
            string oldDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", folderName);
            string newDir = System.IO.Path.Combine(SpecialPaths.Config, folderName);
            Directory.CreateDirectory(newDir);

            //Copy all the files & Replaces any files with the same name
            foreach (string file in Directory.GetFiles(oldDir, "*.*", SearchOption.AllDirectories))
            {
                string fileName = parsingService.GetFileName(file);
                File.Copy(System.IO.Path.Combine(oldDir, fileName), System.IO.Path.Combine(newDir, fileName), true);
            }
        }

        private void ProcessGitCredentials()
        {
            gitCredentials = configurationService.GetArrayFromFile<GitCredentials>(System.IO.Path.Combine(SpecialPaths.Config, "git.json"))[0];
        }

        private void ProcessThemes()
        {
            ProcessCustomThemes();
            SelectedTheme selectedTheme = configurationService.GetObjectFromFile<SelectedTheme>(System.IO.Path.Combine(SpecialPaths.Config, "theme.json"));

            foreach (ThemeInfo t in themeInfos)
            {
                if (t.Name == selectedTheme.Name)
                {
                    activeTheme = t;
                }
            }

            ScintillaLexerService.ResetDictionary(activeTheme);
        }

        private void ProcessCustomThemes()
        {
            if (themesToolStripMenuItem.DropDownItems.Count > 0)
            {
                int removeCount = themesToolStripMenuItem.DropDownItems.Count;

                while (removeCount > 0)
                {
                    themesToolStripMenuItem.DropDownItems.RemoveAt(0);
                    removeCount--;
                }
            }


            themeInfos = configurationService.GetArrayFromMultipleFiles<ThemeInfo>(SpecialPaths.Themes, "json");

            foreach (ThemeInfo themeInfo in themeInfos)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = themeInfo.Name;

                toolStripMenuItem.Click += ToolStripMenuItem_Click1;

                themesToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }
        }

        private void ProcessEditorProperties()
        {
            editorProperties = configurationService.GetObjectFromFile<EditorProperties>(System.IO.Path.Combine(SpecialPaths.Config, "scintilla.json"));

            if (editorProperties.Wordwrap)
            {
                enableWordWrapToolStripMenuItem.Text = "Disable Word Wrap";
            }

            if (editorProperties.Autosave)
            {
                enableAutosaveToolStripMenuItem.Text = "Disable Autosave";
            }

            if (editorProperties.Glass)
            {
                enableGlassEffectToolStripMenuItem.Text = "Disable Glass Effect";
            }
        }

        private void ProcessFormatterDLLs()
        {
            formatters = configurationService.LoadLinkLibrariesFromMultipleFiles<Formatter>(
                                AppDomain.CurrentDomain.BaseDirectory + "formatters",
                                new MethodValidator.Builder()
                                .WithMethodName("Format")
                                .WithMethodParameterCount(1)
                                .WithMethodReturnType(typeof(string))
                                .Build());
        }

        private void ProcessPlugins()
        {
            plugins = configurationService.LoadPluginsFromFolder<Plugin>(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins"));

            foreach (Plugin plugin in plugins)
            {
                ToolStripMenuItem pluginItem = new ToolStripMenuItem();
                pluginItem.Text = plugin.GetName();
                pluginsToolStripMenuItem.DropDownItems.Add(pluginItem);

                Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> pluginTasks = plugin.GetTasks();

                foreach (var pluginTask in pluginTasks)
                {
                    ToolStripMenuItem taskItem = new ToolStripMenuItem();
                    taskItem.Text = pluginTask.Key.Name;
                    taskItem.Tag = plugin.Name;
                    taskItem.Click += TaskItem_Click;
                    pluginItem.DropDownItems.Add(taskItem);
                }
            }
        }

        private void ProcessBuildCommands()
        {
            if (buildAndRunToolstripMenuItem.DropDownItems.Count > 0)
            {
                int removeCount = buildAndRunToolstripMenuItem.DropDownItems.Count;

                while (removeCount > 0)
                {
                    buildAndRunToolstripMenuItem.DropDownItems.RemoveAt(0);
                    removeCount--;
                }
            }

            buildCommands = configurationService.GetArrayFromFile<BuildCommand> (System.IO.Path.Combine(SpecialPaths.Config, "build.json"));

            for (int i = 0; i < buildCommands.Count; i++)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = buildCommands[i].Name;
                toolStripMenuItem.Tag = i;
                toolStripMenuItem.Click += BuildCommandTrigger_Click;
                buildAndRunToolstripMenuItem.DropDownItems.Add(toolStripMenuItem);

                if (tabControl.Pages.Count >= 1 && (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() == null ||
                    !buildCommands[i].Extensions.Contains(parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()))))
                {
                    toolStripMenuItem.Enabled = false;
                }
            }
        }

        private void ProcessDatabaseConnections()
        {
            databases = configurationService.GetArrayFromFile<DatabaseConnection>(System.IO.Path.Combine(SpecialPaths.Config, "databases.json"));
        }

        private void ProcessLanguageMappings()
        {
            languageMappings = configurationService.GetArrayFromFile<LanguageMapping>(System.IO.Path.Combine(SpecialPaths.Config, "mappings.json"));
        }

        private void ProcessLanguageDefinitions()
        {
            languageDefinitions = configurationService.GetArrayFromMultipleFiles<LanguageDefinition>(SpecialPaths.Languages, "json");
        }

        private void ProcessCodeTemplates()
        {
            codeTemplates = configurationService.GetArrayFromFile<CodeTemplate>(System.IO.Path.Combine(SpecialPaths.Config, "templates.json"));
        }

        private void InitializeControls()
        {
            this.MinimumSize = new Size(1036, 634);

            ToggleDirectoryNavigator(false);

            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.FormatRow += GitStagingAreaListView_FormatRow;

            themeService.SetPaletteToTheme(KryptonCustomPaletteBase, activeTheme);
            SynchronizeMainFormComponentsWithTheme();

            themeService.SetPaletteToObjects(this, KryptonCustomPaletteBase);
            codeContextMenu.LocalCustomPalette = KryptonCustomPaletteBase;
            terminalContextMenu.LocalCustomPalette = KryptonCustomPaletteBase;
            renderContextMenu.LocalCustomPalette = KryptonCustomPaletteBase;
            gitContextMenu.LocalCustomPalette = KryptonCustomPaletteBase;
            terminalTabControl.Hide();

            tabControl.AllowPageDrag = false;
            tabControl.AllowPageReorder = false;
        }

        private void ProcessCommandLineArguments()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 2)
            {
                string path = args[1];

                if (File.Exists(path))
                {
                    Open(path);
                }
                else
                {
                    ShowNotification("pie could not open " + path + ". Are you sure the file exists?");
                    Application.Exit();
                    return;
                }
            }
            else
            {
                NewTab(TabType.CODE, null);
            }
        }

        private void InitializeContextMenus()
        {
            KryptonContextMenuHeading heading = new KryptonContextMenuHeading();

            heading.Text = "Explorer Actions";
            directoryContextMenu.Items.Add(heading);

            KryptonContextMenuItems items = new KryptonContextMenuItems();

            KryptonContextMenuItem newFileItem = new KryptonContextMenuItem();
            newFileItem.Click += NewFileItem_Click;
            newFileItem.Text = "New File";
            items.Items.Add(newFileItem);

            KryptonContextMenuItem newFolderItem = new KryptonContextMenuItem();
            newFolderItem.Click += NewFolderItem_Click; ;
            newFolderItem.Text = "New Folder";
            items.Items.Add(newFolderItem);

            KryptonContextMenuItem deleteFileItem = new KryptonContextMenuItem();
            deleteFileItem.Click += DeleteFileItem_Click;
            deleteFileItem.Text = "Delete File";
            items.Items.Add(deleteFileItem);

            KryptonContextMenuItem renameFileItem = new KryptonContextMenuItem();
            renameFileItem.Click += RenameFileItem_Click;
            renameFileItem.Text = "Rename File";
            items.Items.Add(renameFileItem);

            KryptonContextMenuItem refreshItem = new KryptonContextMenuItem();
            refreshItem.Click += RefreshItem_Click;
            refreshItem.Text = "Refresh";
            items.Items.Add(refreshItem);

            directoryContextMenu.Items.Add(items);
        }

        private void RefreshItem_Click(object sender, EventArgs e)
        {
            if (openedFolder != null)
            {
                NavigateToPathAndExpand();
            }
        }

        #endregion

        private void NewFolderItem_Click(object sender, EventArgs e)
        {
            doNotExpand = true;

            TreeNode selectedNode = directoryNavigationTreeView.SelectedNode != null ? directoryNavigationTreeView.SelectedNode : directoryNavigationTreeView.Nodes[0]; 

            KryptonTreeNode newFileNode = new KryptonTreeNode();
            newFileNode.ImageKey = "folder.png";
            newFileNode.SelectedImageKey = "folder.png";
            selectedNode.Nodes.Add(newFileNode);
            selectedNode.Expand();

            newFolderName = selectedNode.ImageKey.Equals("folder.png") ? selectedNode.Tag.ToString() : parsingService.GetFolderName(selectedNode.Tag.ToString());

            dirNavModificationType = DirNavModificationType.CREATE_FOLDER;

            newFileNode.BeginEdit();
        }

        private void RenameFileItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = directoryNavigationTreeView.SelectedNode;

            if (selectedNode == null)
            {
                return;
            }


            foreach (TabInfo tabInfo in tabInfos)
            {
                if (selectedNode.Tag.ToString().Equals(tabInfo.getOpenedFilePath()))
                {
                    ShowNotification("The file is already opened in Pie. Please close the file before renaming it.");
                    return;
                }
            }

            // if node is root, you cannot rename it
            if (selectedNode.Equals(directoryNavigationTreeView.Nodes[0]))
            {
                ShowNotification("You cannot rename the root directory.");
                return;
            }

            if (selectedNode.ImageKey.Equals("folder.png"))
            {
                newFolderName = selectedNode.Tag.ToString();
                dirNavModificationType = DirNavModificationType.RENAME_DIRECTORY;
            }
            else
            {
                newFolderName = parsingService.GetFolderName(selectedNode.Tag.ToString());
                dirNavModificationType = DirNavModificationType.RENAME_FILE;
            }


            selectedNode.BeginEdit();
        }

        private void DeleteFileItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = directoryNavigationTreeView.SelectedNode;

            if (selectedNode == null)
            {
                return;
            }

            // if node is root, you cannot delete it
            if (selectedNode.Equals(directoryNavigationTreeView.Nodes[0]))
            {
                ShowNotification("You cannot delete the root directory.");
                return;
            }

            NotificationYesNoCancelFormOutput output = ShowYesNoCancelNotification("Are you sure you want to delete the selected file?");

            if (output.NotificationButton.Equals(NotificationButton.YES))
            {
                string path = selectedNode.Tag.ToString();

                if (Directory.Exists(path) && IsFileFromDirectoryOpened(path))
                {
                    ShowNotification("Please close all opened files from the directory before deleting it.");
                    return;
                }
                else if (IsFileOpened(path))
                {
                    ShowNotification("A file to be deleted is still opened in Pie. Please close it before proceeding with the deletion.");
                    return;
                }

                DeleteFile(path);
                selectedNode.Remove();
                directoryNavigationTreeView.Refresh();
            }
        }

        private void NewFileItem_Click(object sender, EventArgs e)
        {
            doNotExpand = true;

            TreeNode selectedNode = directoryNavigationTreeView.SelectedNode != null ? directoryNavigationTreeView.SelectedNode : directoryNavigationTreeView.Nodes[0]; // Create file as direct child of the root node if no node is selected

            KryptonTreeNode newFileNode = new KryptonTreeNode();
            newFileNode.ImageKey = "file.png";
            newFileNode.SelectedImageKey = "file.png";

            if (selectedNode.ImageKey.Equals("file.png"))
            {
                selectedNode = (KryptonTreeNode)selectedNode.Parent;
            }

            newFolderName = selectedNode.Tag.ToString();

            selectedNode.Nodes.Add(newFileNode);
            selectedNode.Expand();

            dirNavModificationType = DirNavModificationType.CREATE_FILE;

            newFileNode.BeginEdit();
        }

        private bool IsFileFromDirectoryOpened(string folderPath)
        {
            foreach (TabInfo tabInfo in tabInfos)
            {
                if (tabInfo.getOpenedFilePath() != null && (folderPath + "\\").Equals(parsingService.GetFolderName(tabInfo.getOpenedFilePath())))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFileOpened(string filePath)
        {
            foreach (TabInfo tabInfo in tabInfos)
            {
                if (filePath.Equals(tabInfo.getOpenedFilePath()))
                {
                    return true;
                }
            }

            return false;
        }

        private void TaskItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem taskItem = (ToolStripMenuItem)sender;

            foreach (Plugin plugin in plugins)
            {
                if (plugin.Name.Equals(taskItem.Tag))
                {
                    foreach (var task in plugin.GetTasks())
                    {
                        if (task.Key.Name.Equals(taskItem.Text))
                        {
                            PluginTaskInput pluginTaskInput = new PluginTaskInput();

                            pluginTaskInput.Context = pluginContext;
                            pluginTaskInput.Metadata = ProcessPluginInputMetadata();
                            pluginTaskInput.Tabs = ProcessPluginInputTabs();
                            pluginTaskInput.OpenedDirectory = openedFolder;

                            PluginTaskOutput pluginTaskOutput = plugin.InvokeTask(task.Value, pluginTaskInput);

                            if (pluginTaskOutput != null && pluginTaskOutput.Actions != null)
                            {
                                ExecuteActions(pluginTaskOutput);
                            }
                        }
                    }
                }
            }
        }

        private Metadata ProcessPluginInputMetadata()
        {
            Metadata output = new Metadata();

            output.Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            return output;
        }

        private List<Tab> ProcessPluginInputTabs()
        {
            List<Tab> output = new List<Tab>();

            for (int i = 0; i < tabInfos.Count; i++)
            {
                TabInfo tabInfo = tabInfos[i];

                if (tabInfo.getTabType().Equals(TabType.CODE))
                {
                    Tab tab = new Tab();
                    tab.IsFileOpened = tabInfo.getOpenedFilePath() != null;
                    tab.FilePath = tabInfo.getOpenedFilePath();
                    tab.Text = ((Scintilla)tabControl.Pages[i].Controls[0]).Text;
                    output.Add(tab);
                }
            }

            return output;
        }

        private void ExecuteActions(PluginTaskOutput pluginTaskOutput)
        {
            foreach (PluginAction action in pluginTaskOutput.Actions)
            {
                if (action is CreateFileAction)
                {
                    CreateFileAction createFileAction = (CreateFileAction)action;
                    secureFileService.CreateFile(createFileAction.Path, createFileAction.Content);
                }
                else if (action is CreateDirectoryAction)
                {
                    CreateDirectoryAction createDirectoryAction = (CreateDirectoryAction)action;
                    secureFileService.CreateDirectory(createDirectoryAction.DirectoryName);
                }
                else if (action is ModifyEditorContentAction)
                {
                    ModifyEditorContentAction modifyEditorContentAction = (ModifyEditorContentAction)action;

                    int codeIndex = 0;

                    for (int i = 0; i < tabInfos.Count; i++)
                    {
                        if (tabInfos.GetType().Equals(TabType.CODE))
                        {
                            codeIndex++;
                            if (codeIndex == modifyEditorContentAction.TabIndex)
                            {
                                break;
                            }
                        }
                    }

                    Scintilla scintilla = (Scintilla)tabControl.Pages[codeIndex].Controls[0];
                    scintilla.Text = modifyEditorContentAction.Content;
                }
                else if (action is SelectDirectoryAction)
                {
                    SelectDirectoryAction selectDirectoryAction = (SelectDirectoryAction)action;
                    NavigateToPath(selectDirectoryAction.Path);
                }
                else if (action is OpenTabAction)
                {
                    OpenTabAction openTabAction = (OpenTabAction)action;
                    NewTab(TabType.CODE, null);
                    Open(openTabAction.Path);
                }
                else if (action is RunTerminalCommandAction)
                {
                    RunTerminalCommandAction runTerminalCommandAction = (RunTerminalCommandAction)action;
                    ToggleTerminalTabControl(true);

                    string command = runTerminalCommandAction.Command;

                    int insertPosition = terminalTabControl.Pages.Count;

                    for (int i = 0; i < terminalTabControl.Pages.Count; i++)
                    {
                        if (terminalTabControl.Pages[i].Text.Equals(command))
                        {
                            insertPosition = i;
                            terminalTabControl.Pages.Remove(terminalTabControl.Pages[i]);
                            break;
                        }
                    }

                    NewTerminalTab(command, terminalTabControl.Pages.Count);
                }
            }
        }

        public void ChangeTheme(ThemeInfo theme)
        {
            ControlHelper.SuspendDrawing(this);

            activeTheme = theme;

            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "theme.json"), new SelectedTheme(theme.Name));

            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (tabInfos[i].getTabType() == TabType.CODE)
                {
                    KryptonPage kryptonPage = tabControl.Pages[i];
                    Scintilla scintilla = (Scintilla)kryptonPage.Controls[0];

                    ScintillaLexerService.ResetDictionary(activeTheme);

                    if (tabInfos[i].getOpenedFilePath() != null)
                    {
                        string extension = parsingService.GetFileExtension(tabInfos[i].getOpenedFilePath());
                        themeService.ColorizeTextArea(scintilla, activeTheme);
                        ColorizeAutocompleteMenu(tabInfos[i].getAutocompleteMenu());
                        scintillaLexerService.SetLexer(tabInfos[i].getAutocompleteMenu(), extension, scintilla, activeTheme, languageMappings, languageDefinitions);
                        UpdateNumberMarginWidth(scintilla, true);
                    }
                    else
                    {
                        themeService.ColorizeTextArea(scintilla, activeTheme);
                    }
                }
            }

            themeService.SetPaletteToTheme(KryptonCustomPaletteBase, activeTheme);
            SynchronizeMainFormComponentsWithTheme();

            doNotShowBranchChangeNotification = true;
            doNotTriggerBranchChangeEvent = true;
            UpdateGitRepositoryInfo();
            doNotShowBranchChangeNotification = false;
            doNotTriggerBranchChangeEvent = false;

            ControlHelper.ResumeDrawing(this);
            this.RedrawNonClient();
        }

        // [Event] Used for automatic indentation in a Scintilla instance
        private void TextArea_InsertCheck(object sender, InsertCheckEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            Regex indentLevel = new Regex("^[\\s]*");

            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                int startPos = TextArea.Lines[TextArea.LineFromPosition(TextArea.CurrentPosition)].Position;
                int endPos = e.Position;
                string curLineText = TextArea.GetTextRange(startPos, endPos - startPos);
                // Text until the caret.
                Match indent = indentLevel.Match(curLineText);
                e.Text = e.Text + indent.Value;
            }
        }

        // [Event] Triggered when zooming in the code editor. Also zooms in the code number (and folding) margins.
        private void TextArea_ZoomChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;

            const int padding = 2;

            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
        }

        // [Method] Creates a new instance of the AutocompleteMenu
        private AutocompleteMenu InitializeAutocompleteMenu(Scintilla scintilla)
        {
            AutocompleteMenu autocompleteMenu = new AutocompleteMenu();

            ColorizeAutocompleteMenu(autocompleteMenu);

            autocompleteMenu.TargetControlWrapper = new ScintillaWrapper(scintilla);

            return autocompleteMenu;
        }

        // [Method] Sets the theme-corresponding colors to the AutocompleteMenu's background and text.
        private void ColorizeAutocompleteMenu(AutocompleteMenu autocompleteMenu)
        {
            Colors colors = new Colors();
            colors.BackColor = activeTheme.Primary;
            colors.ForeColor = activeTheme.Fore;
            colors.HighlightingColor = activeTheme.Primary;
            colors.SelectedBackColor = activeTheme.CaretLineBack;
            colors.SelectedBackColor2 = activeTheme.CaretLineBack;
            colors.SelectedForeColor = activeTheme.Fore;
            autocompleteMenu.Colors = colors;
            autocompleteMenu.LeftPadding = 0;
        }

        // [Method] Used in order to remove file extension specific Build&Run actions
        // Additional: This is usually triggered when switching between tabs
        private void RemoveSuggestedActions()
        {
            for (int i = 0; i < codeContextMenu.Items.Count; i++)
            {
                if (codeContextMenu.Items[i] is KryptonContextMenuHeading && ((KryptonContextMenuHeading)codeContextMenu.Items[i]).Text == "Suggested Actions")
                {
                    codeContextMenu.Items.RemoveAt(i + 1);
                    codeContextMenu.Items.RemoveAt(i);
                    break;
                }
            }
        }

        // [Method] Fills the code editor's context menu with file extension specific Build&Run actions
        private void FillContextMenu(string extension)
        {
            // Remove old "Suggested Actions" item list
            RemoveSuggestedActions();

            KryptonContextMenuHeading fileActions = new KryptonContextMenuHeading();
            fileActions.Text = "File Actions";
            codeContextMenu.Items.Insert(2, fileActions);

            KryptonContextMenuItems fileActionsItems = new KryptonContextMenuItems();
            KryptonContextMenuItem fileActionsItem1 = new KryptonContextMenuItem();
            fileActionsItem1.Text = "Copy File Path";
            fileActionsItem1.Image = Properties.Resources.copy;
            fileActionsItem1.Click += FileActionsItem1_Click;
            fileActionsItems.Items.Add(fileActionsItem1);

            KryptonContextMenuItem fileActionsItem2 = new KryptonContextMenuItem();
            fileActionsItem2.Text = "Open Containing Folder";
            fileActionsItem2.Image = Properties.Resources.explorer;
            fileActionsItem2.Click += FileActionsItem2_Click;
            fileActionsItems.Items.Add(fileActionsItem2);

            codeContextMenu.Items.Insert(3, fileActionsItems);

            int buildCommandCount = 0;

            KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
            kryptonContextMenuHeading.Text = "Suggested Actions";
            codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);
            KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
            codeContextMenu.Items.Insert(3, kryptonContextMenuItems);

            if (extension == "html")
            {
                buildCommandCount++;

                KryptonContextMenuItem item = new KryptonContextMenuItem();
                item.Text = "Render HTML";
                item.Click += RenderHtmlCommandTrigger_Click;
                kryptonContextMenuItems.Items.Add(item);
            }
            else if (extension == "md")
            {
                buildCommandCount++;

                KryptonContextMenuItem item = new KryptonContextMenuItem();
                item.Text = "Render Markdown";
                item.Click += RenderMarkdownCommandTrigger_Click;
                kryptonContextMenuItems.Items.Add(item);
            }
            else if (extension == "sql" && databases.Count > 0)
            {
                foreach (DatabaseConnection database in databases)
                {
                    buildCommandCount++;

                    KryptonContextMenuItem item = new KryptonContextMenuItem();
                    item.Text = "Run query against " + database.Name;
                    item.Click += SqlCommandTrigger_Click;
                    kryptonContextMenuItems.Items.Add(item);
                }
            }
            else
            {
                for (int i = 0; i < buildCommands.Count; i++)
                {
                    if (buildCommands[i].Extensions.Contains(extension) || buildCommands[i].Extensions.Trim() == "")
                    {
                        buildCommandCount++;

                        KryptonContextMenuItem item = new KryptonContextMenuItem();
                        item.Text = buildCommands[i].Name;
                        item.Click += BuildCommandTrigger_Click;
                        item.Tag = i;
                        kryptonContextMenuItems.Items.Add(item);
                    }
                }
            }

            if (buildCommandCount == 0)
            {
                codeContextMenu.Items.RemoveAt(2);
                codeContextMenu.Items.RemoveAt(2);
            }
        }

        private void RenderHtmlCommandTrigger_Click(object sender, EventArgs e)
        {
            NewTab(TabType.RENDER_HTML, tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
        }

        private void RenderMarkdownCommandTrigger_Click(object sender, EventArgs e)
        {
            NewTab(TabType.RENDER_MD, tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
        }

        private void SqlCommandTrigger_Click(object sender, EventArgs e)
        {
            if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null &&
                parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "sql")
            {
                string name = ((KryptonContextMenuItem)sender).Text.Substring(18);

                DatabaseConnection database = null;

                foreach (DatabaseConnection tempDatabase in databases)
                {
                    if (name == tempDatabase.Name)
                    {
                        database = tempDatabase;
                        break;
                    }
                }

                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

                DatabaseResponse databaseResult = DatabaseService.ExecuteSQLCommand(TextArea.Text, database);

                if (databaseResult.Success == false)
                {
                    ShowNotification(databaseResult.Message);
                }
                else
                {
                    DatabaseOutputForm databaseOutputForm = new DatabaseOutputForm();

                    DatabaseOutputFormInput databaseOutputFormInput = new DatabaseOutputFormInput();
                    databaseOutputFormInput.Palette = KryptonCustomPaletteBase;
                    databaseOutputFormInput.EditorProperties = editorProperties;
                    databaseOutputFormInput.DataTable = databaseResult.DataTable;

                    databaseOutputForm.Input = databaseOutputFormInput;

                    databaseOutputForm.ShowDialog();
                }
            }
        }

        private void FileActionsItem2_Click(object sender, EventArgs e)
        {
            string path = parsingService.GoBackInFilePath(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = path,
                    UseShellExecute = true,
                    Verb = "open"
                }
                );
        }

        private void FileActionsItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
        }

        private void TextArea_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            if (canUpdateUI)
            {
                Scintilla TextArea = (Scintilla)sender;

                lastSelectedIndex = TextArea.CurrentPosition;
            }
            else
            {
                canUpdateUI = true;
            }
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            UpdateNumberMarginWidth(scintilla, false);


            tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);

            if (editorProperties.Autosave)
            {
                if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    Save(tabControl.SelectedIndex);
                }
            }
        }

        public void UpdateNumberMarginWidth(Scintilla scintilla, bool updateTheme)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == maxLineNumberCharLength && !updateTheme)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void TextArea_MouseDown(object sender, MouseEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            if (e.Button == MouseButtons.Right)
            {
                codeContextMenu.Show(sender);
            }

            lastSelectedIndex = TextArea.SelectionEnd;
        }

        private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            if (e.KeyChar < 32)
            {
                // Prevent control characters from getting inserted into the text buffer
                e.Handled = true;
                return;
            }

            tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);
        }

        // [Method] Creates a new tab and selects the new tab
        public void NewTab(TabType tabType, String path)
        {
            KryptonPage kryptonPage = new KryptonPage();
            kryptonPage.Text = "Untitled";
            kryptonPage.ToolTipTitle = kryptonPage.Text;

            string openedFilePath = null;
            AutocompleteMenu autocompleteMenu = null;

            if (tabType == TabType.CODE)
            {
                Scintilla TextArea = CreateNewTextArea();
                kryptonPage.Controls.Add(TextArea);
                autocompleteMenu = InitializeAutocompleteMenu(TextArea);
            }
            else
            {
                if (firstBrowserTab)
                {
                    Cef.Initialize(new CefSettings());
                    firstBrowserTab = false;
                }

                tabControl.KryptonContextMenu = renderContextMenu;
                ToggleTerminalTabControl(false);
                ToggleDirectoryNavigator(false);

                kryptonPage.Text = parsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                kryptonPage.ToolTipTitle = tabInfos[tabControl.SelectedIndex].getOpenedFilePath();

                ChromiumWebBrowser chromiumWebBrowser = new ChromiumWebBrowser();
                chromiumWebBrowser.Parent = this;
                this.Controls.Add(chromiumWebBrowser);
                chromiumWebBrowser.Dock = DockStyle.Fill;

                if (tabType == TabType.RENDER_HTML)
                {
                    openedFilePath = path;

                    chromiumWebBrowser.Load(path);
                }
                else if (tabType == TabType.RENDER_MD)
                {
                    openedFilePath = path;

                    string result = ConvertMarkdownToHtml(path);
                    chromiumWebBrowser.LoadHtml(result);
                }

                IKeyboardHandler keyboardHandler = new PieKeyboardHandler(this);
                IContextMenuHandler contextMenuHandler = new PieContextMenuHandler(this, renderContextMenu);
                chromiumWebBrowser.KeyboardHandler = keyboardHandler;
                chromiumWebBrowser.MenuHandler = contextMenuHandler;
                chromiumWebBrowser.FrameLoadEnd += ChromiumWebBrowser_FrameLoadEnd;
                chromiumWebBrowser.JavascriptMessageReceived += ChromiumWebBrowser_JavascriptMessageReceived;

                kryptonPage.Controls.Add(chromiumWebBrowser);
            }

            TabInfo tabInfo = new TabInfo(openedFilePath, false, tabType, autocompleteMenu);

            int index = tabControl.Pages.Count <= 0 ? 0 : tabControl.SelectedIndex + 1;
            tabInfos.Insert(index, tabInfo);

            tabControl.Pages.Insert(tabControl.SelectedIndex + 1, kryptonPage);
            tabControl.SelectedPage = kryptonPage;

            if (openedFilePath != null)
            {
                UpdateFormTitle(tabControl.SelectedIndex);
            }
        }

        private Scintilla CreateNewTextArea()
        {
            Scintilla TextArea = new Scintilla();
            TextArea.KeyDown += keyDownEvents;
            TextArea.KeyPress += TextArea_KeyPress;
            TextArea.MouseDown += TextArea_MouseDown;
            TextArea.TextChanged += TextArea_TextChanged;
            TextArea.UpdateUI += TextArea_UpdateUI;
            TextArea.IndentationGuides = IndentView.LookBoth;
            TextArea.ZoomChanged += TextArea_ZoomChanged;

            if (editorProperties.Wordwrap)
            {
                TextArea.WrapMode = WrapMode.Word;
            }
            else
            {
                TextArea.WrapMode = WrapMode.None;
            }

            TextArea.UsePopup(false);

            themeService.ColorizeTextArea(TextArea, activeTheme);

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;
            TextArea.InsertCheck += TextArea_InsertCheck;

            TextArea.Dock = DockStyle.Fill;

            return TextArea;
        }

        private void ChromiumWebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ChromiumWebBrowser browser = (ChromiumWebBrowser)sender;

            browser.ExecuteScriptAsync(@"
                    document.addEventListener('click', function(e) {
                        var parent = e.target.parentElement;

                        CefSharp.PostMessage(parent.outerHTML);
                    }, false);
                ");

        }

        private void ChromiumWebBrowser_JavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            if (e.Message != null)
            {
                this.Invoke(new Action(() =>
                {
                    renderContextMenu.Close();
                }));
            }
        }

        public bool CloseApp()
        {
            int tabCount = tabControl.Pages.Count;

            bool closeStatus = false;

            while (tabCount > 0)
            {
                closeStatus = CloseTab();

                if (!closeStatus)
                {
                    return false;
                }

                tabCount--;
            }

            return true;
        }

        // [Method] Closes the currently selected tab
        public bool CloseTab()
        {
            deletesTab = true;

            if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_HTML || tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_MD)
            {
                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.SelectedPage.Controls[0];
                CloseTabAfterWarning();
            }
            else if (tabInfos[tabControl.SelectedIndex].getOpenedFileChanges())
            {
                NotificationYesNoCancelFormOutput notificationYesNoCancelFormOutput = ShowYesNoCancelNotification("Save file before closing it?");
                if (notificationYesNoCancelFormOutput.NotificationButton == NotificationButton.YES)
                {
                    Save(tabControl.SelectedIndex);
                    CloseTabAfterWarning();
                }
                else if (notificationYesNoCancelFormOutput.NotificationButton == NotificationButton.NO)
                {
                    CloseTabAfterWarning();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                CloseTabAfterWarning();
            }

            if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                UpdateFormTitle(tabControl.SelectedIndex);
            }

            deletesTab = false;

            return true;
        }

        public void CloseTabAfterWarning()
        {
            KryptonPage selectedKryptonPage = tabControl.SelectedPage;

            if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
            {
                deletesLastTab = true;
            }
            else
            {
                deletesLastTab = false;
            }

            if (tabControl.SelectedIndex >= 0)
            {
                TabInfo tabInfo = tabInfos[tabControl.SelectedIndex];
                tabInfos.RemoveAt(tabControl.SelectedIndex);

                if (tabInfo.getTabType() != TabType.CODE)
                {
                    selectedKryptonPage.Dispose();
                }
                else
                {
                    tabControl.Pages.Remove(selectedKryptonPage);
                }
            }

            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < tabInfos.Count && tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                DeactivateBuildAndRunOptions();
                ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            }

            if (tabControl.Pages.Count <= 0)
            {
                NewTab(TabType.CODE, null);
            }

        }

        // [Method] Closes the currently selected *terminal* tab
        public void CloseTerminalTab()
        {
            terminalTabControl.Pages.Remove(terminalTabControl.SelectedPage);

            if (terminalTabControl.Pages.Count == 0)
            {
                ToggleTerminalTabControl(false);
            }
        }

        // [Method] Saves text stored in selected tab
        // Additional: If no file is open, lets the user decide the output path and replaces the position marked at selected tab index in the openFilePaths list (SaveAs)
        public void Save(int openedTabIndex)
        {
            if (tabInfos[openedTabIndex].getOpenedFilePath() == null)
            {
                SaveAs(openedTabIndex);
            }
            else
            {
                string chosenPath = tabInfos[openedTabIndex].getOpenedFilePath();

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.Pages[openedTabIndex].Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabControl.Pages[openedTabIndex].Text = parsingService.GetFileName(chosenPath);

                string extension = parsingService.GetFileExtension(parsingService.GetFileName(tabInfos[openedTabIndex].getOpenedFilePath()));
                scintillaLexerService.SetLexer(tabInfos[openedTabIndex].getAutocompleteMenu(), extension, TextArea, activeTheme, languageMappings, languageDefinitions);
                UpdateFormTitle(tabControl.SelectedIndex);
                tabInfos[openedTabIndex].setOpenedFileChanges(false);
            }

            DeactivateBuildAndRunOptions();

            if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            }

            if (openedGitRepository)
            {
                doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo();
            }
        }

        // [Method] Saves text stored in selected tab at a user-specified location
        // Additional: If no tab is selected, outputs a MessageBox
        public void SaveAs(int selectedIndex)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                string chosenPath = saveFileDialog.FileName;

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.Pages[selectedIndex].Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabInfos[selectedIndex].setOpenedFilePath(chosenPath);
                tabControl.Pages[selectedIndex].Text = parsingService.GetFileName(chosenPath);
                tabControl.Pages[selectedIndex].ToolTipTitle = chosenPath;

                string extension = parsingService.GetFileExtension(parsingService.GetFileName(tabInfos[selectedIndex].getOpenedFilePath()));
                scintillaLexerService.SetLexer(tabInfos[selectedIndex].getAutocompleteMenu(), extension, TextArea, activeTheme, languageMappings, languageDefinitions);
                UpdateFormTitle(selectedIndex);
                tabInfos[selectedIndex].setOpenedFileChanges(false);
            }
        }

        // [Method] Opens a file (without openFileDialog), given just a path
        public bool Open(string fileName)
        {
            if (tabControl.Pages.Count == 0)
            {
                NewTab(TabType.CODE, null);
            }

            int openedTabIndex = tabControl.SelectedIndex;

            string fileContent;
            try
            {
                fileContent = File.ReadAllText(fileName);
            }
            catch (Exception)
            {
                ShowNotification("An error has been encountered while trying to open the file.");
                return false;
            }

            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            TextArea.Text = fileContent;

            tabInfos[openedTabIndex].setOpenedFilePath(fileName);
            tabControl.SelectedPage.Text = parsingService.GetFileName(fileName);
            tabControl.SelectedPage.ToolTipTitle = fileName;

            string extension = parsingService.GetFileExtension(fileName);

            scintillaLexerService.SetLexer(tabInfos[openedTabIndex].getAutocompleteMenu(), extension, TextArea, activeTheme, languageMappings, languageDefinitions);

            DeactivateBuildAndRunOptions();
            ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            UpdateFormTitle(tabControl.SelectedIndex);

            if (tabControl.Pages.Count >= 1)
            {
                tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(false);
            }

            return true;
        }

        // [Method] Opens a new file and replaces text and path (in openedFilePaths) for selected tab (with openFileDialog)
        public void OpenPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                NewTab(TabType.CODE, null);
                Open(openFileDialog.FileName);
            }
        }

        public void NewTerminalTab(string process, int position)
        {
            KryptonPage kryptonPage = new KryptonPage();

            terminalTabControl.Pages.Insert(position, kryptonPage);

            ConEmuControl conEmuControl = new ConEmuControl();

            ConEmuStartInfo conEmuStartInfo = new ConEmuStartInfo();
            conEmuStartInfo.ConsoleProcessCommandLine = process;

            if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                conEmuStartInfo.StartupDirectory = parsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            }

            ConEmuSession conEmuSession = conEmuControl.Start(conEmuStartInfo);

            var refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 100; // ms
            refreshTimer.Tick += (sender, args) => {
                conEmuControl.Update();
            };
            refreshTimer.Start();

            conEmuControl.BackColor = Color.White;
            conEmuControl.ForeColor = Color.Red;
            conEmuControl.Parent = kryptonPage;

            if (process == "cmd.exe")
            {
                kryptonPage.Text = "Command Prompt";
                kryptonPage.ImageSmall = Properties.Resources.terminal;
                conEmuSession.ConsoleEmulatorClosed += ConEmuSession_ConsoleEmulatorClosed1;
            }
            else if (process == "powershell.exe")
            {
                kryptonPage.Text = "PowerShell";
                kryptonPage.ImageSmall = Properties.Resources.powershell;
                conEmuSession.ConsoleEmulatorClosed += ConEmuSession_ConsoleEmulatorClosed1;
            }
            else
            {
                kryptonPage.Text = process;
                conEmuSession.ConsoleEmulatorClosed += ConEmuSession_ConsoleEmulatorClosed;
            }

            conEmuControl.Dock = DockStyle.Fill;

            terminalTabControl.SelectedPage = kryptonPage;
        }

        private void ConEmuSession_ConsoleEmulatorClosed1(object sender, EventArgs e)
        {
            CreateDefaultTerminalIfNotExists();
        }

        private void CreateDefaultTerminalIfNotExists()
        {
            int termCount = -1;

            foreach (KryptonPage page in terminalTabControl.Pages)
            {
                if (page.ImageSmall == Properties.Resources.terminal || page.ImageSmall == Properties.Resources.powershell)
                {
                    termCount++;
                    break;
                }
            }

            if (termCount < 1)
            {
                NewTerminalTab("cmd.exe", terminalTabControl.Pages.Count);
            }
        }

        private void ConEmuSession_ConsoleEmulatorClosed(object sender, EventArgs e)
        {
            CloseTerminalTab();
        }

        private void DeleteFileActions()
        {
            for (int i = 0; i < codeContextMenu.Items.Count; i++)
            {
                if (codeContextMenu.Items[i] is KryptonContextMenuHeading && ((KryptonContextMenuHeading)codeContextMenu.Items[i]).Text == "File Actions")
                {
                    codeContextMenu.Items.RemoveAt(i + 1);
                    codeContextMenu.Items.RemoveAt(i);
                    break;
                }
            }
        }

        public void ActivateSpecificBuildAndRunOptions(string extension)
        {
            FillContextMenu(extension);

            foreach (ToolStripMenuItem toolStripMenuItem in buildAndRunToolstripMenuItem.DropDownItems)
            {
                int buildCommandIndex = (int)toolStripMenuItem.Tag;
                BuildCommand buildCommand = buildCommands[buildCommandIndex];

                if (buildCommand.Extensions.Contains(extension) || buildCommand.Extensions.Trim() == "")
                {
                    toolStripMenuItem.Enabled = true;
                }
            }
        }

        public void DeactivateBuildAndRunOptions()
        {
            DeleteFileActions();
            RemoveSuggestedActions();

            foreach (ToolStripMenuItem toolStripMenuItem in buildAndRunToolstripMenuItem.DropDownItems)
            {
                toolStripMenuItem.Enabled = false;
            }
        }

        public void ToggleTerminalTabControlWithInitialization(bool visible)
        {
            if (terminalTabControl.Pages.Count == 0)
            {
                NewTerminalTab("cmd.exe", terminalTabControl.Pages.Count);
            }

            ToggleTerminalTabControl(visible);
        }

        public void ToggleTerminalTabControl(bool status)
        {
            terminalTabControl.Visible = status;

            if (status)
            {
                showTerminalTabToolStripMenuItem.Text = "Hide Terminal Tab";
                kryptonContextMenuItem15.Text = "Hide Terminal Tab";
            }
            else
            {
                showTerminalTabToolStripMenuItem.Text = "Show Terminal Tab";
                kryptonContextMenuItem15.Text = "Show Terminal Tab";
            }
        }

        public void ShowNotification(string text)
        {
            NotificationOKForm notificationOkForm = new NotificationOKForm();

            NotificationFormInput notificationFormInput = new NotificationFormInput();
            notificationFormInput.EditorProperties = new EditorProperties();
            notificationFormInput.Palette = KryptonCustomPaletteBase;
            notificationFormInput.NotificationText = text;

            notificationOkForm.Input = notificationFormInput;

            notificationOkForm.ShowDialog();
        }

        public NotificationYesNoCancelFormOutput ShowYesNoCancelNotification(string text)
        {
            NotificationYesNoCancelForm notificationYesNoCancelForm = new NotificationYesNoCancelForm();

            NotificationFormInput notificationFormInput = new NotificationFormInput();
            notificationFormInput.EditorProperties = new EditorProperties();
            notificationFormInput.Palette = KryptonCustomPaletteBase;
            notificationFormInput.NotificationText = text;

            notificationYesNoCancelForm.Input = notificationFormInput;

            notificationYesNoCancelForm.ShowDialog();

            return notificationYesNoCancelForm.Output;
        }

        private string ConvertMarkdownToHtml(string path)
        {
            string content = parsingService.GetContentFromFile(path);

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string result = Markdown.ToHtml(content, pipeline);

            return result;
        }

        private void ToggleDirectoryNavigator(bool status)
        {
            if (status)
            {
                kryptonContextMenuItem12.Text = "Hide Directory Navigator";
            }
            else
            {
                if (tabControl.Pages.Count > 0 && tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    tabControl.Pages[tabControl.SelectedIndex].Controls[0].Focus();
                }

                kryptonContextMenuItem12.Text = "Show Directory Navigator";
            }

            kryptonSplitContainer.Panel1Collapsed = !status;

        }

        private void ShowFindReplacePanel()
        {
            ToggleFindReplacePanel();
        }

        private void ToggleFindReplacePanel()
        {
            if (!isFindReplaceDialogShown)
            {
                isFindReplaceDialogShown = true;

                FindReplaceFormInput input = new FindReplaceFormInput();
                input.Palette = KryptonCustomPaletteBase;
                input.ActiveTheme = activeTheme;
                input.EditorProperties = editorProperties;
                input.MainForm = this;
                findReplaceForm.Input = input;

                findReplaceForm.ShowDialog();
                isFindReplaceDialogShown = false;
            }
        }

        // [Event] Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            if (editorProperties.Glass)
            {
                this.Opacity = 0.90;
            }

            try
            {
                UpdateStatus updateStatus = updateService.GetUpdateStatus();

                if (updateStatus.NeedsUpdate)
                {
                    ShowNotification("There's a new version available for pie. You can initiate the automatic update process from the Interface menu.");
                    updateToolStripMenuItem.Visible = true;
                    updateToolStripMenuItem.Text = "Update to " + updateStatus.Version;
                }
                else
                {
                    updateToolStripMenuItem.Visible = false;
                }
            }
            catch (Exception)
            {
                // Update checker may fail due to missing network connection.
                updateToolStripMenuItem.Visible = false;
            }
        }

        private void GitStagingAreaListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            GitFile gitFile = (GitFile)e.Model;
            if (gitFile.Status == "Ignored")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(100, 100, 100) : Color.FromArgb(179, 179, 179);
            else if (gitFile.Status == "Deleted")
                e.Item.ForeColor = Color.FromArgb(251, 77, 77);
            else if (gitFile.Status == "New")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(20, 208, 158) : Color.FromArgb(40, 254, 158);
            else if (gitFile.Status == "Modified")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(254, 173, 0) : Color.FromArgb(255, 255, 5);
            else
                e.Item.ForeColor = activeTheme.Fore;
        }

        // [Event] Triggered when a new tab is opened/closed
        private void tabControl_TabCountChanged(object sender, EventArgs e)
        {
            if (tabControl.Pages.Count == 0)
            {
                kryptonContextMenuItem3.Enabled = false;

                saveToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                kryptonContextMenuItem3.Enabled = true;

                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
            }
        }

        // [Event] "New Tab" button pressed in kryptonContextMenu
        private void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {
            NewTab(TabType.CODE, null);
        }

        // [Event] "New" button pressed in upper menu => create new tab
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab(TabType.CODE, null);
        }

        // [Event] "Close Current Tab" button pressed in kryptonContextMenu
        private void kryptonContextMenuItem3_Click(object sender, EventArgs e)
        {
            CloseTab();
        }

        // [Event] "Save" button pressed in upper menu
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(tabControl.SelectedIndex);
        }

        // [Event] "Save As" button pressed in upper menu
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs(tabControl.SelectedIndex);
        }

        // [Event] "Open" button pressed in upper menu
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPath();
        }

        // [Generic Event] Used mostly for tab control and actions on opened files. Called by other event listeners
        private void keyDownEvents(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                NewTab(TabType.CODE, null);
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control)
            {
                CloseTab();
            }
            if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
            {
                if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                {
                    Save(tabControl.SelectedIndex);
                }
                else if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
                {
                    ToggleTerminalTabControlWithInitialization(!terminalTabControl.Visible);
                }
                else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                {
                    ShowFindReplacePanel();
                }
                else if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
                {
                    ShowDirectoryNavigator();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode.ToString().Length >= 2 && e.KeyCode.ToString()[1] >= '0' && e.KeyCode.ToString()[1] <= '9' && e.Modifiers == Keys.Control)
                {
                    CodeTemplate codeTemplate = codeTemplates.Find(template => template.Index == Convert.ToInt32(e.KeyCode.ToString().Substring(1, 1)));

                    if (codeTemplate != null)
                    {
                        Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];

                        int selectionEnd = scintilla.SelectionEnd;
                        scintilla.Text = scintilla.Text.Substring(0, scintilla.SelectionEnd) + codeTemplate.Content + scintilla.Text.Substring(scintilla.SelectionEnd);
                        scintilla.SelectionStart = selectionEnd + codeTemplate.Content.Length;
                        scintilla.SelectionEnd = scintilla.SelectionStart;
                    }
                }
                else if (sender is Scintilla)
                {
                    if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
                    {
                        Scintilla TextArea = (Scintilla)sender;

                        // If nothing is selected, cut the whole line
                        if (TextArea.SelectedText.Length == 0)
                        {
                            CutLine(TextArea);
                            e.SuppressKeyPress = true;
                        }
                    }
                    else if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Control)
                    {
                        Scintilla TextArea = (Scintilla)sender;

                        if (TextArea.Text.Length > 0)
                        {
                            int selectionEnd = TextArea.SelectionEnd;

                            if (selectionEnd < TextArea.Text.Length - 1 && TextArea.Text[selectionEnd] == '\n')
                            {
                                selectionEnd++;
                            }

                            while (selectionEnd < TextArea.Text.Length - 1 && TextArea.Text[selectionEnd] != '\n')
                            {
                                selectionEnd++;
                            }

                            TextArea.SelectionEnd = selectionEnd + 1;
                        }
                    }
                    else if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
                    {
                        Scintilla TextArea = (Scintilla)sender;

                        if (TextArea.Text.Length > 0)
                        {
                            int selectionStart = TextArea.SelectionStart - 1;

                            if (selectionStart > 0 && TextArea.Text[selectionStart] == '\n')
                            {
                                selectionStart--;
                            }

                            while (selectionStart > 0 && TextArea.Text[selectionStart] != '\n')
                            {
                                selectionStart--;
                            }

                            TextArea.SelectionStart = selectionStart == 0 ? selectionStart : selectionStart + 1;
                        }
                    }
                }
            }
            if (e.Modifiers == (Keys.Control | Keys.Alt))
            {
                if (e.KeyCode == Keys.Left)
                {
                    tabControl.SelectedIndex = tabControl.SelectedIndex > 0 ? tabControl.SelectedIndex - 1 : tabControl.Pages.Count - 1;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    tabControl.SelectedIndex = tabControl.SelectedIndex < tabControl.Pages.Count - 1 ? tabControl.SelectedIndex + 1 : 0;
                }
            }
        }

        private void CutLine(Scintilla TextArea)
        {
            if (TextArea.Text.Length > 0)
            {
                int startLine = TextArea.SelectionStart == 0 ? TextArea.SelectionStart : TextArea.SelectionStart;
                int endLine = TextArea.SelectionStart == 0 ? TextArea.SelectionStart : TextArea.SelectionStart;

                while (startLine > 0 && TextArea.Text[startLine - 1] != '\n')
                {
                    startLine--;
                }

                while (endLine < TextArea.TextLength - 1 && TextArea.Text[endLine] != '\n')
                {
                    endLine++;
                }

                if (endLine == TextArea.TextLength)
                {
                    endLine--;
                }

                string stringToCopy = TextArea.Text.Substring(startLine, endLine - startLine + 1);


                if (stringToCopy == "" || stringToCopy == "\r\n")
                {
                    endLine = startLine + 1;
                }
                else
                {
                    if (endLine == TextArea.Text.Length - 1 && TextArea.Text[endLine] != '\n')
                    {
                        stringToCopy += "\r\n";
                    }

                    Clipboard.SetText(stringToCopy);

                    if (endLine < startLine)
                    {
                        endLine = startLine + 1;
                    }

                    if (endLine == TextArea.Text.Length - 1)
                    {
                        startLine -= 2;
                    }
                }

                TextArea.DeleteRange(startLine, endLine - startLine + 1);

                TextArea.SetSelection(startLine, startLine);
            }
        }

        private void ShowDirectoryNavigator()
        {
            if (firstDirectoryNavigatorToggle)
            {
                ControlHelper.SuspendDrawing(kryptonSplitContainer);
            }

            ToggleDirectoryNavigator(kryptonSplitContainer.Panel1Collapsed);

            if (!kryptonSplitContainer.Panel1Collapsed)
            {
                directoryNavigationTreeView.Focus();
            }

            if (openedFolder == null)
            {
                NavigateToPath("C:\\");
            }

            if (firstDirectoryNavigatorToggle)
            {
                ControlHelper.ResumeDrawing(kryptonSplitContainer);
                this.RedrawNonClient();
                firstDirectoryNavigatorToggle = false;
            }
        }

        // [Event] Triggered when clicking "Show Terminal Tab" from the context menu
        private void kryptonContextMenuItem15_Click(object sender, EventArgs e)
        {
            ToggleTerminalTabControlWithInitialization(!terminalTabControl.Visible);
        }

        // [Event] Triggered when Form is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool closeStatus = CloseApp();

            if (closeStatus)
            {
                Cef.Shutdown();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        // [Event] Triggered when clicking "Show Build Tab" from the upper menu
        private void showBuildToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleTerminalTabControlWithInitialization(!terminalTabControl.Visible);
        }

        // [Event] Triggered by multiple senders, when building (upper menu -> Build)
        private void BuildCommandTrigger_Click(object sender, EventArgs e)
        {
            int tag = (sender is ToolStripMenuItem) ? (int)((ToolStripMenuItem)sender).Tag
                    : (sender is KryptonContextMenuItem) ? (int)((KryptonContextMenuItem)sender).Tag : -1;

            if (tag >= 0)
            {
                if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    BuildCommand buildCommand = buildCommands[tag];

                    string scriptName = parsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                    string command = buildCommand.Command;
                    command = command.Replace("$FILE", scriptName);
                    ToggleTerminalTabControlWithInitialization(true);

                    int insertPosition = terminalTabControl.Pages.Count;

                    for (int i = 0; i < terminalTabControl.Pages.Count; i++)
                    {
                        if (terminalTabControl.Pages[i].Text.Equals(command))
                        {
                            insertPosition = i;
                            terminalTabControl.Pages.Remove(terminalTabControl.Pages[i]);
                            break;
                        }
                    }

                    NewTerminalTab(command, insertPosition);
                }
                else
                {
                    ShowNotification("Please open a file before launching any build command.");
                }
            }
        }

        // [Event] Triggered when a tab is changed
        private void tabControl_SelectedPageChanged(object sender, EventArgs e)
        {
            int indexToMoveTo = deletesLastTab ? tabControl.SelectedIndex : (deletesTab ? tabControl.SelectedIndex - 1 : tabControl.SelectedIndex);

            if (indexToMoveTo >= 0)
            {
                if (tabInfos[indexToMoveTo].getTabType() == TabType.CODE)
                {
                    formatToolStripMenuItem.Enabled = true;

                    tabControl.KryptonContextMenu = codeContextMenu;

                    DeactivateBuildAndRunOptions();
                    if (tabControl.SelectedIndex != -1 && tabInfos[indexToMoveTo].getOpenedFilePath() != null)
                    {
                        ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(tabInfos[indexToMoveTo].getOpenedFilePath()));
                        UpdateFormTitle(indexToMoveTo);
                    }
                    else
                    {
                        UpdateFormTitle("Untitled");
                    }
                }
                else
                {
                    formatToolStripMenuItem.Enabled = false;

                    tabControl.KryptonContextMenu = renderContextMenu;

                    ToggleTerminalTabControl(false);
                    ToggleDirectoryNavigator(false);

                    UpdateFormTitle(indexToMoveTo);

                    DeactivateBuildAndRunOptions();
                }
            }
            else if (showGitTabPressed)
            {
                tabControl.KryptonContextMenu = gitContextMenu;
                showGitTabPressed = false;
            }
            else
            {
                tabControl.KryptonContextMenu = null;
                DeactivateBuildAndRunOptions();
                UpdateFormTitle("Untitled");
            }
        }

        private void UpdateFormTitle(int index)
        {
            this.Text = tabInfos[index].getOpenedFilePath() + " - pie";
        }

        private void UpdateFormTitle(String customTitle)
        {
            this.Text = customTitle + " - pie";
        }

        private void buildTabControl_TabCountChanged(object sender, EventArgs e)
        {
            if (terminalTabControl.Pages.Count == 0)
            {
                kryptonContextMenuItem18.Enabled = false;
            }
            else
            {
                kryptonContextMenuItem18.Enabled = true;
            }
        }

        // [Event] Closes the currently opened terminal (accessed via Terminal tab control)
        private void kryptonContextMenuItem18_Click(object sender, EventArgs e)
        {
            CloseTerminalTab();
        }

        // [Event] Opens a new cmd terminal (accessed via Terminal tab control)
        private void kryptonContextMenuItem16_Click(object sender, EventArgs e)
        {
            NewTerminalTab("cmd.exe", terminalTabControl.Pages.Count);
        }

        // [Event] Opens a new PowerShell terminal (accessed via Terminal tab control)
        private void kryptonContextMenuItem17_Click(object sender, EventArgs e)
        {
            NewTerminalTab("powershell.exe", terminalTabControl.Pages.Count);
        }

        // [Event] Close remaining processes after form gets closed
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        // [Event] Triggers when user clicks the tab control
        private void tabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    //int index = lastSelectedTabIndex;
                    //tabControl.SelectedIndex = index;
                }
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildCommandsForm buildCommandsForm = new BuildCommandsForm();

            BuildCommandsFormInput buildCommandsFormInput = new BuildCommandsFormInput();
            buildCommandsFormInput.Palette = KryptonCustomPaletteBase;
            buildCommandsFormInput.ActiveTheme = activeTheme;
            buildCommandsFormInput.EditorProperties = editorProperties;
            buildCommandsFormInput.BuildCommands = buildCommands;

            buildCommandsForm.Input = buildCommandsFormInput;

            buildCommandsForm.ShowDialog();

            if (buildCommandsForm.Output.Saved)
            {
                ProcessBuildCommands();
            }
        }

        public void StartFind(string input, bool regex, bool matchCase, bool wholeWord)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            ClearHighlights(TextArea);
            HighlightWord(input, TextArea, regex, matchCase, wholeWord);
            Find(input, TextArea, lastSelectedIndex, regex, matchCase, wholeWord);
        }

        private bool Find(string input, Scintilla scintilla, int indexStart, bool regex, bool matchCase, bool matchWholeWord)
        {
            canUpdateUI = false;

            if (string.IsNullOrEmpty(input))
                return false;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = 9;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            scintilla.TargetStart = indexStart;
            scintilla.TargetEnd = scintilla.TextLength;

            scintilla.SearchFlags = SearchFlags.None;

            if (regex)
            {
                scintilla.SearchFlags |= SearchFlags.Regex;
            }

            if (matchCase)
            {
                scintilla.SearchFlags |= SearchFlags.MatchCase;
            }

            if (matchWholeWord)
            {
                scintilla.SearchFlags |= SearchFlags.WholeWord;
            }

            int index;

            if ((index = scintilla.SearchInTarget(input)) != -1)
            {
                scintilla.SetSelection(index, index + scintilla.TargetEnd - scintilla.TargetStart);
                lastSelectedIndex = index + scintilla.TargetEnd - scintilla.TargetStart + 1;

                scintilla.ScrollCaret();
            }
            else
            {
                if (indexStart == 0)
                {
                    //ShowNotification("No more occurences found.");
                    return false;
                }

                lastSelectedIndex = 0;
                return Find(input, scintilla, 0, regex, matchCase, matchWholeWord);
            }

            return true;
        }

        private void HighlightWord(string text, Scintilla scintilla, bool regex, bool matchCase, bool matchWholeWord)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = 8;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[8].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[8].Under = true;
            scintilla.Indicators[8].ForeColor = activeTheme.Selection;
            scintilla.Indicators[8].OutlineAlpha = 255;
            scintilla.Indicators[8].Alpha = 100;

            // Search the document
            scintilla.TargetStart = 0;
            scintilla.TargetEnd = scintilla.TextLength;

            scintilla.SearchFlags = SearchFlags.None;

            if (regex)
            {
                scintilla.SearchFlags |= SearchFlags.Regex;
            }

            if (matchCase)
            {
                scintilla.SearchFlags |= SearchFlags.MatchCase;
            }

            if (matchWholeWord)
            {
                scintilla.SearchFlags |= SearchFlags.WholeWord;
            }

            while (scintilla.SearchInTarget(text) != -1)
            {
                // Mark the search results with the current indicator
                scintilla.IndicatorFillRange(scintilla.TargetStart, scintilla.TargetEnd - scintilla.TargetStart);

                // Search the remainder of the document
                scintilla.TargetStart = scintilla.TargetEnd;
                scintilla.TargetEnd = scintilla.TextLength;
            }
        }

        public void ClearHighlights()
        {
            if (tabInfos[tabControl.SelectedIndex].getTabType().Equals(TabType.CODE))
            {
                Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
                ClearHighlights(scintilla);
            }
        }

        public void ClearHighlights(Scintilla scintilla)
        {
            scintilla.IndicatorCurrent = 8;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);
        }

        public bool Replace(string from, string to, bool regex, bool matchCase, bool wholeWord)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];

            HighlightWord(from, scintilla, regex, matchCase, wholeWord);
            bool found = Find(from, scintilla, lastSelectedIndex, regex, matchCase, wholeWord);

            if (found)
            {
                scintilla.ReplaceSelection(to);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ReplaceAll(string from, string to, bool regex, bool matchCase, bool wholeWord)
        {
            bool status;

            do
            {
                status = Replace(from, to, regex, matchCase, wholeWord);
            } while (status);
        }

        private void aboutPieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();

            AboutFormInput aboutFormInput = new AboutFormInput();
            aboutFormInput.Palette = KryptonCustomPaletteBase;
            aboutFormInput.EditorProperties = editorProperties;

            aboutForm.Input = aboutFormInput;

            aboutForm.ShowDialog();
        }

        private void kryptonContextMenuItem6_Click(object sender, EventArgs e)
        {
            ShowFindReplacePanel();
        }

        private void kryptonContextMenuItem10_Click(object sender, EventArgs e)
        {
            if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_HTML)
            {
                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.SelectedPage.Controls[0];
                chromiumWebBrowser.Load(chromiumWebBrowser.Address);
            }
            else if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_MD)
            {
                string htmlContent = ConvertMarkdownToHtml(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());

                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.Pages[tabControl.SelectedIndex].Controls[0];
                chromiumWebBrowser.LoadHtml(htmlContent);
            }
        }

        private void kryptonContextMenuItem7_Click(object sender, EventArgs e)
        {
            NewTab(TabType.CODE, null);
        }

        private void kryptonContextMenuItem8_Click(object sender, EventArgs e)
        {
            CloseTab();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            OpenRepository();
        }

        private void OpenRepository()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult dialogResult = dialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string selectedPath = dialog.SelectedPath;

                OpenRepository(selectedPath);
            }
        }

        private void OpenRepository(string path)
        {
            string repo = Repository.Discover(path);

            if (repo == null)
            {
                NotificationYesNoCancelFormOutput notificationYesNoCancelFormOutput = ShowYesNoCancelNotification("No repository found at specified path. Would you like to initialize a new repository?");

                if (notificationYesNoCancelFormOutput.NotificationButton == NotificationButton.YES)
                {
                    InitializeRepository(path);

                    if (gitBranchesComboBox.Items.Count > 0)
                    {
                        gitBranchesComboBox.SelectedIndex = selectedBranchIndex;
                    }
                }
            }
            else
            {
                openedGitRepository = true;
                gitTabControl.SelectedIndex = 1;
                doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo();

                if (gitBranchesComboBox.Items.Count > 0)
                {
                    gitBranchesComboBox.SelectedIndex = selectedBranchIndex;

                    if (selectedBranch == null)
                    {
                        foreach (var branch in repository.Branches)
                        {
                            if (branch.FriendlyName == gitBranchesComboBox.Text)
                            {
                                selectedBranch = branch;
                                Commands.Checkout(repository, branch);
                            }
                        }
                    }
                }

                try
                {
                    Commands.Fetch(repository, "origin", new string[0], new FetchOptions
                    {
                        CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                        {
                            Username = gitCredentials.Username,
                            Password = gitCredentials.Password
                        }
                    }, null);
                } catch(Exception ex)
                {
                    // Do nothing, as the repo may not be remote
                }
            }
        }

        private void InitializeRepository(string path)
        {
            try
            {
                Repository.Init(path);
                openedGitRepository = true;
                gitTabControl.SelectedIndex = 1;
                doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo();
            }
            catch (Exception)
            {
                ShowNotification("Could not initialize repository at the specified path. Please check if you have the necessary permissions.");
            }
        }

        private void UpdateGitRepositoryInfo()
        {
            UpdateGitRepositoryInfo(true);
        }

        private void UpdateGitRepositoryInfo(bool updateDirNav)
        {
            if (!openedGitRepository)
            {
                return;
            }

            try
            {
                repository = new Repository(openedFolder);
            }
            catch (Exception)
            {
                openedGitRepository = false;
                gitTabControl.SelectedIndex = 0;
                ShowNotification("Could not open repository at the specified path. Please check if you have the necessary permissions.");
                return;
            }

            RetrieveGitItemsForCurrentBranch();

            string selectedBranch = null;
            if (gitBranchesComboBox.SelectedItem != null)
            {
                selectedBranch = gitBranchesComboBox.SelectedItem.ToString();
            }

            gitBranchesComboBox.Items.Clear();

            foreach (var branch in repository.Branches)
            {
                gitBranchesComboBox.Items.Add(branch.FriendlyName);

                if (selectedBranch != null && branch.FriendlyName == selectedBranch)
                {
                    if (!updateDirNav)
                    {
                        doNotUpdateDirNav = true;
                    }

                    gitBranchesComboBox.SelectedItem = gitBranchesComboBox.Items[gitBranchesComboBox.Items.Count - 1];
                }
            }

            if (gitBranchesComboBox.Items.Count > 0 && gitBranchesComboBox.SelectedItem == null)
            {
                if (!updateDirNav)
                {
                    doNotUpdateDirNav = true;
                }

                gitBranchesComboBox.SelectedIndex = 0;
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            OpenRepository();
        }

        private void gitStagingAreaListView_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedGitFiles();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            if (repository != null)
            {
                doNotTriggerBranchChangeEvent = true;
                UpdateGitRepositoryInfo();
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void RetrieveGitItemsForCurrentBranch()
        {
            StatusOptions statusOptions = new StatusOptions();
            statusOptions.IncludeUnaltered = true;

            List<GitFile> gitFileList = new List<GitFile>();

            foreach (var item in repository.RetrieveStatus(statusOptions))
            {
                GitFile gitFile = new GitFile();
                gitFile.Name = item.FilePath;

                if (item.State == FileStatus.DeletedFromWorkdir)
                {
                    gitFile.Status = "Deleted";
                    gitFileList.Add(gitFile);
                }
                else if (item.State == FileStatus.ModifiedInIndex || item.State == FileStatus.ModifiedInWorkdir)
                {
                    gitFile.Status = "Modified";
                    gitFileList.Add(gitFile);
                }
                else if (item.State == FileStatus.NewInIndex || item.State == FileStatus.NewInWorkdir)
                {
                    gitFile.Status = "New";
                    gitFileList.Add(gitFile);
                }
            }

            gitStagingAreaListView.SetObjects(gitFileList);
        }

        private void GitCommit(string items)
        {
            if (repository != null)
            {
                RepositoryStatus status = repository.RetrieveStatus();

                if (status.IsDirty)
                {
                    if (items != "*")
                    {
                        List<string> files = items.Split(' ').ToList();

                        if (files[files.Count - 1] == "")
                        {
                            files.RemoveAt(files.Count - 1);
                        }

                        Commands.Stage(repository, files);
                    }
                    else
                    {
                        Commands.Stage(repository, items);
                    }

                    if (string.IsNullOrEmpty(gitCredentials.Name) || string.IsNullOrEmpty(gitCredentials.Email))
                    {
                        GitCommitCredentialsForm gitCommitCredentialsForm = new GitCommitCredentialsForm();

                        GitCommitCredentialsFormInput gitCommitCredentialsFormInput = new GitCommitCredentialsFormInput();
                        gitCommitCredentialsFormInput.GitCredentials = gitCredentials;
                        gitCommitCredentialsFormInput.Palette = KryptonCustomPaletteBase;
                        gitCommitCredentialsFormInput.EditorProperties = editorProperties;

                        gitCommitCredentialsForm.Input = gitCommitCredentialsFormInput;

                        gitCommitCredentialsForm.ShowDialog();

                        if (gitCommitCredentialsForm.Output.Saved)
                        {
                            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
                            GitCommit(items);
                        }
                    }
                    else
                    {
                        Signature signature = new Signature(gitCredentials.Name, gitCredentials.Email, DateTime.Now);

                        GitCommitMessageForm gitCommitMessageForm = new GitCommitMessageForm();

                        GitCommitMessageFormInput gitCommitMessageFormInput = new GitCommitMessageFormInput();
                        gitCommitMessageFormInput.Palette = KryptonCustomPaletteBase;
                        gitCommitMessageFormInput.EditorProperties = editorProperties;
                        gitCommitMessageForm.Input = gitCommitMessageFormInput;

                        gitCommitMessageForm.ShowDialog();

                        string commitText = gitCommitMessageForm.Output.Message;

                        if (!string.IsNullOrEmpty(commitText))
                        {
                            Task.Run(() =>
                            {
                                repository.Commit(commitText, signature, signature);
                            }).Wait();

                            doNotShowBranchChangeNotification = true;
                            UpdateGitRepositoryInfo();
                            ShowNotification("Successfully commited.");
                        }
                    }
                }
                else
                {
                    ShowNotification("You have nothing to commit.");
                }
            }
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (repository != null)
            {
                GitCommit("*");
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void kryptonContextMenuItem9_Click(object sender, EventArgs e)
        {
            NewTab(TabType.CODE, null);
        }

        private void kryptonContextMenuItem11_Click(object sender, EventArgs e)
        {
            CloseTab();
        }

        private void RemoveGitActions()
        {
            if (gitContextMenu.Items.Count >= 3 && ((KryptonContextMenuHeading)gitContextMenu.Items[2]).Text == "Git")
            {
                gitContextMenu.Items.RemoveAt(3);
                gitContextMenu.Items.RemoveAt(2);
            }
        }

        private void gitContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemoveGitActions();

            if (gitStagingAreaListView.SelectedItems.Count != 0)
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Git";

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();

                KryptonContextMenuItem kryptonContextMenuItem = new KryptonContextMenuItem();
                kryptonContextMenuItem.Text = "Open selected files";
                kryptonContextMenuItem.Image = Properties.Resources.git;
                kryptonContextMenuItem.Click += KryptonContextMenuItem_OpenSelectedGitFiles;
                kryptonContextMenuItems.Items.Add(kryptonContextMenuItem);

                kryptonContextMenuItem = new KryptonContextMenuItem();
                kryptonContextMenuItem.Text = "Commit selected files";
                kryptonContextMenuItem.Image = Properties.Resources.git;
                kryptonContextMenuItem.Click += KryptonContextMenuItem_CommitSelectedFiles;
                kryptonContextMenuItems.Items.Add(kryptonContextMenuItem);

                kryptonContextMenuItem = new KryptonContextMenuItem();
                kryptonContextMenuItem.Text = "Rollback selected files";
                kryptonContextMenuItem.Image = Properties.Resources.git;
                kryptonContextMenuItem.Click += KryptonContextMenuItem_Click2;
                kryptonContextMenuItems.Items.Add(kryptonContextMenuItem);

                gitContextMenu.Items.Add(kryptonContextMenuHeading);
                gitContextMenu.Items.Add(kryptonContextMenuItems);
            }
        }

        private void KryptonContextMenuItem_Click2(object sender, EventArgs e)
        {
            GitRollback();
        }

        private void GitRollback()
        {
            if (repository != null)
            {
                Commit headCommit = repository.Head.Tip;
                List<string> paths = new List<string>();

                for (int i = 0; i < gitStagingAreaListView.SelectedObjects.Count; i++)
                {
                    paths.Add(((GitFile)gitStagingAreaListView.SelectedObjects[i]).Name);
                }

                try
                {
                    repository.CheckoutPaths(selectedBranch.FriendlyName, paths, new CheckoutOptions { CheckoutModifiers = CheckoutModifiers.Force });
                    doNotShowBranchChangeNotification = true;
                    UpdateGitRepositoryInfo();

                    // Files changed (where reverted). They don't need to keep their inconsistent state in the opened code tabs.
                    CloseRevertedGitFiles(paths);
                }
                catch (Exception)
                {
                    ShowNotification("Could not rollback files.");
                }
            }
            else
            {
                ShowNotification("No repository is opened.");
            }
        }

        private void CloseRevertedGitFiles(List<string> paths)
        {
            foreach (string path in paths)
            {
                for (int i = 0; i < tabInfos.Count; i++)
                {
                    if (tabInfos[i].getTabType().Equals(TabType.CODE) && tabInfos[i].getOpenedFilePath() != null && tabInfos[i].getOpenedFilePath().Equals(openedFolder + "\\" + path.Replace("/", "\\")))
                    {
                        tabControl.SelectedIndex = i;
                        CloseTab();
                    }
                }
            }
        }

        private void KryptonContextMenuItem_CommitSelectedFiles(object sender, EventArgs e)
        {
            string items = "";

            foreach (var gitFile in gitStagingAreaListView.SelectedObjects)
            {
                items += ((GitFile)gitFile).Name;
                items += " ";
            }

            GitCommit(items);
        }

        private void KryptonContextMenuItem_OpenSelectedGitFiles(object sender, EventArgs e)
        {
            OpenSelectedGitFiles();
        }

        private void OpenSelectedGitFiles()
        {
            if (gitStagingAreaListView.SelectedItems.Count != 0)
            {
                foreach (var gitFile in gitStagingAreaListView.SelectedObjects)
                {
                    string fileName = ((GitFile)gitFile).Name;
                    OpenFileIfNotAlreadyOpened(openedFolder + "\\" + fileName.Replace("/", "\\"));
                }
            }
        }

        private void GitPush()
        {
            if (repository != null)
            {
                var branch = selectedBranch;

                if (repository.Network.Remotes.Any())
                {
                    if (string.IsNullOrEmpty(gitCredentials.Username) || string.IsNullOrEmpty(gitCredentials.Password))
                    {
                        GitPushCredentialsForm gitPushCredentialsForm = new GitPushCredentialsForm();

                        GitPushCredentialsFormInput gitPushCredentialsFormInput = new GitPushCredentialsFormInput();
                        gitPushCredentialsFormInput.GitCredentials = gitCredentials;
                        gitPushCredentialsFormInput.Palette = KryptonCustomPaletteBase;
                        gitPushCredentialsFormInput.EditorProperties = editorProperties;

                        gitPushCredentialsForm.Input = gitPushCredentialsFormInput;

                        gitPushCredentialsForm.ShowDialog();

                        if (gitPushCredentialsForm.Output.Saved)
                        {
                            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
                            GitPush();
                        }
                    }
                    else
                    {
                        Remote remote = repository.Network.Remotes["origin"];

                        if (remote == null)
                        {
                            ShowNotification("No remote repository exists.");
                            return;
                        }

                        string branchName = gitBranchesComboBox.SelectedItem.ToString();
                        Branch remoteBranch = repository.Branches[$"origin/{branchName}"];

                        if (selectedBranch != null && remoteBranch != null && !selectedBranch.IsTracking) {
                        repository.Branches.Update(repository.Branches[gitBranchesComboBox.SelectedItem.ToString()],
                            b => b.Remote = remote.Name,
                            b => b.UpstreamBranch = remoteBranch.CanonicalName);
                        }

                        // Push the branch to the remote                        
                        var pushOptions = new PushOptions();
                        pushOptions.CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                           (url, usernameFromUrl, types) =>
                               new UsernamePasswordCredentials()
                               {
                                   Username = gitCredentials.Username,
                                   Password = gitCredentials.Password
                               });

                        try
                        {
                            doNotShowBranchChangeNotification = true;

                            try
                            {
                                Task.Run(() =>
                                {
                                    repository.Network.Push(remote, $"refs/heads/{branchName}:refs/heads/{branchName}", pushOptions);
                                }).Wait();
                            } catch(Exception ex)
                            {
                                ShowNotification("Error interacting with the remote repository. Check your credentials or reinitialize the local repository.");
                                return;
                            }

                            ShowNotification("Successfully pushed to remote.");
                            UpdateGitRepositoryInfo();
                            doNotShowBranchChangeNotification = false;
                        }
                        catch (Exception ex)
                        {
                            ShowNotification("Authentication failed. If pushing on GitHub, generate an access token (with proper permissions) instead of using the password.");
                        }
                    }
                }
                else
                {
                    GitConnectToRemoteForm gitConnectToRemoteForm = new GitConnectToRemoteForm();

                    GitConnectToRemoteFormInput gitConnectToRemoteFormInput = new GitConnectToRemoteFormInput();
                    gitConnectToRemoteFormInput.Palette = KryptonCustomPaletteBase;
                    gitConnectToRemoteFormInput.EditorProperties = editorProperties;
                    gitConnectToRemoteForm.Input = gitConnectToRemoteFormInput;

                    gitConnectToRemoteForm.ShowDialog();

                    if (gitConnectToRemoteForm.Output != null)
                    {
                        try
                        {
                            repository.Network.Remotes.Add("origin", gitConnectToRemoteForm.Output.RepositoryUrl);
                            GitPush();
                        }
                        catch (Exception)
                        {
                            ShowNotification("There was a problem connecting your repository to remote. Please check the URL and try again.");
                        }
                    }
                }
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            GitPush();
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            NotificationYesNoCancelFormOutput notificationYesNoCancelFormOutput = null;

            if (doNotReselect)
            {
                doNotReselect = false;
                return;
            }

            if (doNotTriggerBranchChangeEvent)
            {
                doNotTriggerBranchChangeEvent = false;
            }
            else if (repository != null)
            {
                if (doNotShowBranchChangeNotification)
                {
                    doNotShowBranchChangeNotification = false;
                }
                else if (selectedBranchIndex != gitBranchesComboBox.SelectedIndex)
                {
                    notificationYesNoCancelFormOutput = ShowYesNoCancelNotification("Checking out another branch will discard your current changes. Would you like to continue?");
                }
                else
                {
                    notificationYesNoCancelFormOutput = new NotificationYesNoCancelFormOutput() { NotificationButton = NotificationButton.YES };
                }

                if (notificationYesNoCancelFormOutput != null && notificationYesNoCancelFormOutput.NotificationButton == NotificationButton.YES)
                {
                    foreach (var branch in repository.Branches)
                    {
                        if (branch.FriendlyName == gitBranchesComboBox.Text)
                        {
                            string branchNameToCheckout;

                            if (gitBranchesComboBox.SelectedItem.ToString().StartsWith("origin/"))
                            {
                                // Check if current local branch already exists
                                string localBranchFriendlyName = gitBranchesComboBox.SelectedItem.ToString().Remove(0, 7);

                                if (repository.Branches[localBranchFriendlyName] == null)
                                {
                                    // Remove the "origin/" from remote branch name
                                    doNotShowBranchChangeNotification = true;
                                    var remoteBranch = repository.Branches[gitBranchesComboBox.SelectedItem.ToString()];
                                    var newBranch = repository.CreateBranch(localBranchFriendlyName, remoteBranch.Tip);
                                    repository.Branches.Update(newBranch, b => b.TrackedBranch = remoteBranch.CanonicalName);
                                }

                                int index = 0;
                                foreach (string b in gitBranchesComboBox.Items)
                                {
                                    if (b == localBranchFriendlyName)
                                    {
                                        doNotReselect = true;
                                        gitBranchesComboBox.SelectedIndex = index;
                                        break;
                                    }
                                    index++;
                                }

                                branchNameToCheckout = localBranchFriendlyName;
                            }
                            else
                            {
                                branchNameToCheckout = gitBranchesComboBox.SelectedItem.ToString();
                            }

                            CheckoutOptions checkoutOptions = new CheckoutOptions();
                            Branch currentBranch = Commands.Checkout(repository, repository.Branches[branchNameToCheckout], new CheckoutOptions() { CheckoutModifiers = CheckoutModifiers.Force });
                            selectedBranch = repository.Branches[branchNameToCheckout];
                            selectedBranchIndex = gitBranchesComboBox.SelectedIndex;
                            selectedRepositoryIndex = gitBranchesComboBox.SelectedIndex;
                        }
                    }

                    RetrieveGitItemsForCurrentBranch();
                }
                else
                {
                    doNotTriggerBranchChangeEvent = true;
                    gitBranchesComboBox.SelectedIndex = selectedRepositoryIndex;
                    doNotTriggerBranchChangeEvent = false;
                }

                if (doNotUpdateDirNav)
                {
                    doNotUpdateDirNav = false;
                }
                else
                {
                    NavigateToPathAndExpand();
                }
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void manageCommitCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GitSettingsForm gitSettingsForm = new GitSettingsForm();

            GitSettingsFormInput gitSettingsFormInput = new GitSettingsFormInput();
            gitSettingsFormInput.GitCredentials = gitCredentials;
            gitSettingsFormInput.Palette = KryptonCustomPaletteBase;
            gitSettingsFormInput.EditorProperties = editorProperties;

            gitSettingsForm.Input = gitSettingsFormInput;

            gitSettingsForm.ShowDialog();

            if (gitSettingsForm.Output.Saved)
            {
                configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
            }
        }

        private void managePushCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GitPushCredentialsForm gitPushCredentialsForm = new GitPushCredentialsForm();

            GitPushCredentialsFormInput gitPushCredentialsFormInput = new GitPushCredentialsFormInput();
            gitPushCredentialsFormInput.GitCredentials = gitCredentials;
            gitPushCredentialsFormInput.Palette = KryptonCustomPaletteBase;
            gitPushCredentialsFormInput.EditorProperties = editorProperties;

            gitPushCredentialsForm.Input = gitPushCredentialsFormInput;

            gitPushCredentialsForm.ShowDialog();

            if (gitPushCredentialsForm.Output.Saved)
            {
                configurationService.WriteToFile<GitCredentials>(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
            }
        }

        private void CloneRepository()
        {
            GitCloneForm gitCloneForm = new GitCloneForm();

            GitCloneFormInput gitCloneFormInput = new GitCloneFormInput();

            gitCloneFormInput.Palette = KryptonCustomPaletteBase;
            gitCloneFormInput.EditorProperties = editorProperties;
            gitCloneFormInput.GitCredentials = gitCredentials;

            gitCloneForm.Input = gitCloneFormInput;

            gitCloneForm.ShowDialog();

            if (gitCloneForm.Output.ClonePath != null)
            {
                NavigateToPath(gitCloneForm.Output.ClonePath);
            }
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            GitPull();
        }

        private void GitPull()
        {
            if (repository != null)
            {
                if (string.IsNullOrEmpty(gitCredentials.Name) || string.IsNullOrEmpty(gitCredentials.Email))
                {
                    GitCommitCredentialsForm gitCommitCredentialsForm = new GitCommitCredentialsForm();

                    GitCommitCredentialsFormInput gitCommitCredentialsFormInput = new GitCommitCredentialsFormInput();
                    gitCommitCredentialsFormInput.GitCredentials = gitCredentials;
                    gitCommitCredentialsFormInput.Palette = KryptonCustomPaletteBase;
                    gitCommitCredentialsFormInput.EditorProperties = editorProperties;

                    gitCommitCredentialsForm.Input = gitCommitCredentialsFormInput;

                    gitCommitCredentialsForm.ShowDialog();

                    if (gitCommitCredentialsForm.Output.Saved)
                    {
                        configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
                        GitPull();
                    }
                }
                else if (string.IsNullOrEmpty(gitCredentials.Username) || string.IsNullOrEmpty(gitCredentials.Password))
                {
                    GitPushCredentialsForm gitPushCredentialsForm = new GitPushCredentialsForm();

                    GitPushCredentialsFormInput gitPushCredentialsFormInput = new GitPushCredentialsFormInput();
                    gitPushCredentialsFormInput.GitCredentials = gitCredentials;
                    gitPushCredentialsFormInput.Palette = KryptonCustomPaletteBase;
                    gitPushCredentialsFormInput.EditorProperties = editorProperties;

                    gitPushCredentialsForm.Input = gitPushCredentialsFormInput;

                    gitPushCredentialsForm.ShowDialog();

                    if (gitPushCredentialsForm.Output.Saved)
                    {
                        configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "git.json"), new List<GitCredentials>() { gitCredentials });
                        GitPull();
                    }
                }
                else if (!repository.Network.Remotes.Any())
                {
                    GitConnectToRemoteForm gitConnectToRemoteForm = new GitConnectToRemoteForm();

                    GitConnectToRemoteFormInput gitConnectToRemoteFormInput = new GitConnectToRemoteFormInput();
                    gitConnectToRemoteFormInput.Palette = KryptonCustomPaletteBase;
                    gitConnectToRemoteFormInput.EditorProperties = editorProperties;
                    gitConnectToRemoteForm.Input = gitConnectToRemoteFormInput;

                    gitConnectToRemoteForm.ShowDialog();

                    if (gitConnectToRemoteForm.Output != null)
                    {
                        try
                        {
                            repository.Network.Remotes.Add("origin", gitConnectToRemoteForm.Output.RepositoryUrl);
                            GitPull();
                        }
                        catch (Exception)
                        {
                            ShowNotification("There was a problem connecting your repository to remote. Please check the URL and try again.");
                        }
                    }
                }
                else 
                {
                    var pullOptions = new PullOptions();

                    pullOptions.FetchOptions = new FetchOptions();

                    pullOptions.FetchOptions.CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                        (_url, _user, _cred) => new UsernamePasswordCredentials()
                        {
                            Username = gitCredentials.Username,
                            Password = gitCredentials.Password
                        });

                    Signature signature = new Signature(gitCredentials.Name, gitCredentials.Email, DateTime.Now);

                    Remote remote = repository.Network.Remotes["origin"];
                    string branchName = gitBranchesComboBox.SelectedItem.ToString();

                    var checkoutOptions = new CheckoutOptions();

                    Branch currentBranch = repository.Head;
                    Branch remoteBranch = repository.Branches[$"origin/{branchName}"];

                    if (currentBranch != null && remoteBranch != null && !currentBranch.IsTracking)
                    {
                        repository.Branches.Update(currentBranch,
                            b => b.UpstreamBranch = remoteBranch.CanonicalName);
                    }

                    try
                    {
                        Task.Run(() =>
                        {
                            Commands.Fetch(repository, "origin", new string[0], pullOptions.FetchOptions, null);
                            repository.Merge(remoteBranch, signature);
                        }).Wait();

                        UpdateGitRepositoryInfo();
                        ShowNotification("Pull successful.");
                        NavigateToPath(openedFolder);
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("There was an error while trying to pull from remote.");
                    }
                }
                
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            if (repository != null)
            {
                GitCommitLogForm gitCommitLogForm = new GitCommitLogForm();

                GitCommitLogFormInput gitCommitLogFormInput = new GitCommitLogFormInput();
                gitCommitLogFormInput.ActiveTheme = activeTheme;
                gitCommitLogFormInput.Repository = repository;
                gitCommitLogFormInput.EditorProperties = editorProperties;
                gitCommitLogFormInput.Palette = KryptonCustomPaletteBase;

                gitCommitLogForm.Input = gitCommitLogFormInput;

                gitCommitLogForm.ShowDialog();
            }
            else
            {
                ShowNotification("No repository opened.");
            }
        }

        private void SynchronizeMainFormComponentsWithTheme()
        {
            // MenuStrip & Children
            mainMenuStrip.BackColor = activeTheme.Primary;
            mainMenuStrip.ForeColor = activeTheme.Fore;

            foreach (ToolStripMenuItem toolStripMenuItem in mainMenuStrip.Items)
            {
                toolStripMenuItem.DropDown.BackColor = activeTheme.Primary;
                toolStripMenuItem.DropDown.ForeColor = activeTheme.Fore;
                toolStripMenuItem.ImageTransparentColor = activeTheme.Primary;

                if (toolStripMenuItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem toolStripMenuItemChild in toolStripMenuItem.DropDownItems)
                    {
                        toolStripMenuItemChild.BackColor = activeTheme.Primary;
                        toolStripMenuItemChild.ForeColor = activeTheme.Fore;

                        if (toolStripMenuItemChild.HasDropDownItems)
                        {
                            foreach (ToolStripMenuItem toolStripMenuItemChild2 in toolStripMenuItemChild.DropDownItems)
                            {
                                toolStripMenuItemChild2.BackColor = activeTheme.Primary;
                                toolStripMenuItemChild2.ForeColor = activeTheme.Fore;
                            }
                        }
                    }
                }
            }

            // TabControl
            tabControl.StateCommon.Panel.Color1 = activeTheme.Primary;
            tabControl.StateCommon.Panel.Color2 = activeTheme.Primary;

            // ObjectListView
            gitStagingAreaListView.UseCustomSelectionColors = true;
            gitStagingAreaListView.FullRowSelect = true;
            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.HighlightBackgroundColor = activeTheme.Secondary;
            gitStagingAreaListView.HighlightForegroundColor = activeTheme.Fore;
            gitStagingAreaListView.UnfocusedHighlightBackgroundColor = activeTheme.Secondary;
            gitStagingAreaListView.UnfocusedHighlightForegroundColor = activeTheme.Fore;
            gitStagingAreaListView.BackColor = activeTheme.Primary;
            gitStagingAreaListView.ForeColor = activeTheme.Fore;
            gitStagingAreaListView.BorderStyle = System.Windows.Forms.BorderStyle.None;

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = activeTheme.Secondary;
            headerstyle.Normal.ForeColor = activeTheme.Fore;

            headerstyle.Hot.BackColor = activeTheme.ButtonHover;
            headerstyle.Hot.ForeColor = activeTheme.Fore;

            headerstyle.Pressed.BackColor = activeTheme.ButtonFrame;
            headerstyle.Pressed.ForeColor = activeTheme.Fore;

            gitStagingAreaListView.HeaderFormatStyle = headerstyle;

            // ComboBox
            gitBranchesComboBox.StateCommon.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.DropBack.Color1 = activeTheme.Primary;
            gitBranchesComboBox.StateCommon.DropBack.Color2 = activeTheme.Primary;

            gitBranchesComboBox.StateTracking.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Back.Color1 = activeTheme.Secondary;
            gitBranchesComboBox.StateTracking.Item.Back.Color2 = activeTheme.Secondary;

            // Git Buttons
            if (activeTheme.IconType == "dark")
            {
                newBranchButton.Values.Image = Properties.Resources.plus_black;
                refreshStatusButton.Values.Image = Properties.Resources.refresh_black;
                commitButton.Values.Image = Properties.Resources.commit_black;
                pullButton.Values.Image = Properties.Resources.pull_black;
                pushButton.Values.Image = Properties.Resources.push_black;
                logButton.Values.Image = Properties.Resources.log_black;
            }
            else if (activeTheme.IconType == "light")
            {
                newBranchButton.Values.Image = Properties.Resources.plus_white;
                refreshStatusButton.Values.Image = Properties.Resources.refresh_white;
                commitButton.Values.Image = Properties.Resources.commit_white;
                pullButton.Values.Image = Properties.Resources.pull_white;
                pushButton.Values.Image = Properties.Resources.push_white;
                logButton.Values.Image = Properties.Resources.log_white;
            }

            // KryptonTableLayoutPanel
            kryptonTableLayoutPanel1.BackColor = activeTheme.Primary;

            CustomColorTable customColorTable = new CustomColorTable();
            customColorTable.InputThemeInfo = activeTheme;

            CustomToolStripRenderer customToolStripRenderer = new CustomToolStripRenderer(customColorTable);
            customToolStripRenderer.InputThemeInfo = activeTheme;
            mainMenuStrip.Renderer = customToolStripRenderer;
        }

        private void NavigateToPath(string path, bool openRepository)
        {
            ToggleDirectoryNavigator(true);

            directoryNavigationTreeView.Nodes.Clear();

            KryptonTreeNode rootNode = new KryptonTreeNode();
            rootNode.Text = "root";
            rootNode.Tag = path;
            rootNode.ImageKey = "folder.png";
            rootNode.SelectedImageKey = "folder.png";
            openedFolder = path;
            directoryNavigationTreeView.Nodes.Add(rootNode);

            AddItemsToDirectory(rootNode);

            if (Directory.GetDirectories(path).Any(d => parsingService.GetFileName(d).Equals(".git")))
            {
                if (openRepository)
                {
                    OpenRepository(path);
                }
            }
            else
            {
                openedGitRepository = false;
                gitTabControl.SelectedIndex = 0;
            }
        }

        private void FillExpandedNodes(List<KryptonTreeNode> expandedNodes, KryptonTreeNode rootNode)
        {
            foreach (KryptonTreeNode node in rootNode.Nodes)
            {
                if (node.IsExpanded)
                {
                    expandedNodes.Add(node);
                    FillExpandedNodes(expandedNodes, node);
                }
            }
        }

        private void ExpandRefreshedNodes(List<KryptonTreeNode> expandedNodes, KryptonTreeNode rootNode)
        {
            foreach (KryptonTreeNode node in rootNode.Nodes)
            {
                int position = GetPositionOfNode(expandedNodes, node);

                if (position != -1)
                {
                    expandedNodes.RemoveAt(position);
                    node.Expand();
                    ExpandRefreshedNodes(expandedNodes, node);
                }
            }
        }

        private int GetPositionOfNode(List<KryptonTreeNode> expandedNodes, KryptonTreeNode node)
        {
            for (int i = 0; i<expandedNodes.Count; i++)
            {
                KryptonTreeNode n = expandedNodes[i];
                
                if (n.Tag.ToString().Equals(node.Tag.ToString()) && n.Text.ToString().Equals(node.Text.ToString()))
                {
                    return i;
                }
            }

            return -1;
        }

        private void NavigateToPathAndExpand()
        {
            bool rootNodeExpanded = directoryNavigationTreeView.Nodes[0].IsExpanded;

            List<KryptonTreeNode> expandedNodes = new List<KryptonTreeNode>();
            FillExpandedNodes(expandedNodes, (KryptonTreeNode)directoryNavigationTreeView.Nodes[0]);

            NavigateToPath(openedFolder, false);

            if (rootNodeExpanded)
            {
                ((KryptonTreeNode)directoryNavigationTreeView.Nodes[0]).Expand();
            }

            ExpandRefreshedNodes(expandedNodes, (KryptonTreeNode)directoryNavigationTreeView.Nodes[0]);
        }

        private void NavigateToPath(string path)
        {
            NavigateToPath(path, true);
        }

        private void AddItemsToDirectory(KryptonTreeNode node)
        {
            string[] files;
            string[] directories;

            try
            {
                files = Directory.GetFiles(node.Tag.ToString());
                directories = Directory.GetDirectories(node.Tag.ToString());
            }
            catch (Exception ex)
            {
                ShowNotification("An error was encountered while trying to access the given path.");
                NavigateToPath("C:\\");
                return;
            }

            directories = directories.Where(d => !d.StartsWith("$")).ToArray();

            foreach (string directoryString in directories)
            {
                try
                {
                    KryptonTreeNode nodeToAdd = new KryptonTreeNode();
                    nodeToAdd.Text = parsingService.GetFileName(directoryString);
                    nodeToAdd.Tag = directoryString;
                    nodeToAdd.ImageKey = "folder.png";
                    nodeToAdd.SelectedImageKey = "folder.png";

                    if (Directory.EnumerateFiles(directoryString).Count() > 0 || Directory.EnumerateDirectories(directoryString).Count() > 0)
                    {
                        KryptonTreeNode dummyNode = new KryptonTreeNode();
                        dummyNode.Text = "Dummy";
                        dummyNode.Tag = "Pie:DummyNode";
                        nodeToAdd.Nodes.Add(dummyNode);
                    }

                    node.Nodes.Add(nodeToAdd);
                }
                catch (Exception ex) { }
            }

            foreach (string fileString in files)
            {
                KryptonTreeNode nodeToAdd = new KryptonTreeNode();
                nodeToAdd.Text = parsingService.GetFileName(fileString);
                nodeToAdd.Tag = fileString;
                nodeToAdd.ImageKey = "file.png";
                nodeToAdd.SelectedImageKey = "file.png";
                node.Nodes.Add(nodeToAdd);
            }

            win32APIService.HideScrollBars(directoryNavigationTreeView);
        }

        public object NavigationImageGetter(object rowObject)
        {
            NavigatorFile navigatorFile = (NavigatorFile)rowObject;
            if (navigatorFile.Type == "Directory")
                return "directory";
            else
                return "folder";
        }

        private void kryptonContextMenuItem12_Click(object sender, EventArgs e)
        {
            ShowDirectoryNavigator();
        }

        private void databasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabasesForm databasesForm = new DatabasesForm();

            DatabasesFormInput databasesFormInput = new DatabasesFormInput();
            databasesFormInput.Databases = databases;
            databasesFormInput.Palette = KryptonCustomPaletteBase;
            databasesFormInput.EditorProperties = editorProperties;
            databasesFormInput.ActiveTheme = activeTheme;

            databasesForm.Input = databasesFormInput;

            databasesForm.ShowDialog();

            if (databasesForm.Output.Saved)
            {
                ProcessDatabaseConnections();

                if (tabControl.SelectedIndex != -1 && tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    DeactivateBuildAndRunOptions();
                    ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                }
            }
        }

        private void ToolStripMenuItem_Click1(object sender, EventArgs e)
        {
            ChangeTheme(themeInfos.Find(theme => theme.Name == ((ToolStripMenuItem)sender).Text));
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorProperties.Wordwrap = !editorProperties.Wordwrap;
            ToggleWordWrap(editorProperties.Wordwrap);
            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "scintilla.json"), editorProperties);
        }

        private void ToggleWordWrap(bool status)
        {
            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (tabInfos[i].getTabType() == TabType.CODE)
                {
                    KryptonPage kryptonPage = tabControl.Pages[i];
                    Scintilla scintilla = (Scintilla)kryptonPage.Controls[0];

                    if (status)
                    {
                        scintilla.WrapMode = WrapMode.Word;
                        enableWordWrapToolStripMenuItem.Text = "Disable Word Wrap";
                    }
                    else
                    {
                        scintilla.WrapMode = WrapMode.None;
                        enableWordWrapToolStripMenuItem.Text = "Enable Word Wrap";
                    }
                }
            }
        }

        private void enableAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorProperties.Autosave = !editorProperties.Autosave;

            if (editorProperties.Autosave)
            {
                enableAutosaveToolStripMenuItem.Text = "Disable Autosave";
                PerformFirstSaveWhenAutosaveTriggered();
            }
            else
            {
                enableAutosaveToolStripMenuItem.Text = "Enable Autosave";
            }

            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "scintilla.json"), editorProperties);
        }

        private void PerformFirstSaveWhenAutosaveTriggered()
        {
            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (tabInfos[i].getTabType() == TabType.CODE)
                {
                    if (tabInfos[i].getOpenedFilePath() != null)
                    {
                        Save(i);
                    }
                }
            }
        }

        private void smartFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

            FormatForm formatForm = new FormatForm();

            FormatFormInput formatFormInput = new FormatFormInput();
            formatFormInput.Text = TextArea.Text;
            formatFormInput.ActiveTheme = activeTheme;
            formatFormInput.Palette = KryptonCustomPaletteBase;
            formatFormInput.EditorProperties = editorProperties;
            formatFormInput.Formatters = formatters;

            formatForm.Input = formatFormInput;

            formatForm.ShowDialog();

            if (formatForm.Output.Saved)
            {
                int currPos = TextArea.CurrentPosition;
                TextArea.Text = formatForm.Output.Text;
                TextArea.SelectionStart = currPos;
                TextArea.SelectionEnd = currPos;
            }
        }

        private void glassModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorProperties.Glass = !editorProperties.Glass;

            if (editorProperties.Glass)
            {
                enableGlassEffectToolStripMenuItem.Text = "Disable Glass Effect";
                this.Opacity = 0.90;
            }
            else
            {
                enableGlassEffectToolStripMenuItem.Text = "Enable Glass Effect";
                this.Opacity = 1;
            }

            configurationService.WriteToFile(System.IO.Path.Combine(SpecialPaths.Config, "scintilla.json"), editorProperties);
        }

        private void cheatsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheatsheetForm cheatsheetForm = new CheatsheetForm();

            CheatsheetFormInput cheatsheetFormInput = new CheatsheetFormInput();
            cheatsheetFormInput.Palette = KryptonCustomPaletteBase;
            cheatsheetFormInput.EditorProperties = editorProperties;
            cheatsheetFormInput.ActiveTheme = activeTheme;

            cheatsheetForm.Input = cheatsheetFormInput;

            cheatsheetForm.ShowDialog();
        }

        private void themeDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesignerForm designerForm = new DesignerForm();

            DesignerFormInput designerFormInput = new DesignerFormInput();
            designerFormInput.ThemeInfos = themeInfos;
            designerFormInput.EditorProperties = editorProperties;
            designerFormInput.Palette = KryptonCustomPaletteBase;
            designerFormInput.ActiveTheme = activeTheme;

            designerForm.Input = designerFormInput;

            designerForm.ShowDialog();

            ProcessCustomThemes();
            ChangeTheme(activeTheme);
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                Console.WriteLine(file);
                NewTab(TabType.CODE, null);
                Open(file);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("piesync.exe");
            Environment.Exit(0);
        }

        private void codeTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeTemplatesForm codeTemplatesForm = new CodeTemplatesForm();

            CodeTemplatesFormInput codeTemplatesFormInput = new CodeTemplatesFormInput();
            codeTemplatesFormInput.EditorProperties = editorProperties;
            codeTemplatesFormInput.Palette = KryptonCustomPaletteBase;
            codeTemplatesFormInput.ActiveTheme = activeTheme;
            codeTemplatesFormInput.CodeTemplates = codeTemplates;

            codeTemplatesForm.Input = codeTemplatesFormInput;

            codeTemplatesForm.ShowDialog();

            ProcessCodeTemplates();
        }

        private void directoryNavigationTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (doNotExpand)
            {
                doNotExpand = false;
                return;
            }

            KryptonTreeNode node = (KryptonTreeNode)e.Node;

            if (!node.Tag.Equals(openedFolder))
            {
                node.Nodes.Clear();
                AddItemsToDirectory(node);
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                NavigateToPath(folderBrowserDialog.SelectedPath);
            }
        }

        private void directoryNavigationTreeView_DoubleClick(object sender, EventArgs e)
        {
            KryptonTreeView kryptonTreeView = (KryptonTreeView)sender;

            win32APIService.HideScrollBars(kryptonTreeView);
            if (kryptonTreeView.SelectedNode != null)
            {
                string selectedPath = kryptonTreeView.SelectedNode.Tag.ToString();

                OpenFileIfNotAlreadyOpened(selectedPath);
            }
        }

        private void OpenFileIfNotAlreadyOpened(string selectedPath)
        {
            if (File.Exists(selectedPath))
            {
                foreach (TabInfo tabInfo in tabInfos)
                {
                    if (tabInfo.getOpenedFilePath() != null && tabInfo.getOpenedFilePath().Equals(selectedPath) && tabInfo.getTabType().Equals(TabType.CODE))
                    {
                        tabControl.SelectedIndex = tabInfos.IndexOf(tabInfo);
                        return;
                    }
                }

                NewTab(TabType.CODE, null);
                Open(selectedPath);
            }
        }

        private void initializeANewRepoButton_Click_1(object sender, EventArgs e)
        {
            InitializeRepository(openedFolder);
        }

        private void cloneARepoButton_Click(object sender, EventArgs e)
        {
            CloneRepository();
        }

        private int FindBranchIndexWithName(string branchName)
        {
            for (int i = 0; i < gitBranchesComboBox.Items.Count; i++)
            {
                if (gitBranchesComboBox.Items[i].ToString().Equals(branchName))
                {
                    return i;
                }
            }

            return -1;
        }

        private void NewBranch()
        {
            NewBranchForm newBranchForm = new NewBranchForm();

            NewBranchFormInput newBranchFormInput = new NewBranchFormInput();
            newBranchFormInput.Palette = KryptonCustomPaletteBase;
            newBranchFormInput.EditorProperties = editorProperties;
            newBranchFormInput.ActiveTheme = activeTheme;
            newBranchForm.Input = newBranchFormInput;

            newBranchForm.ShowDialog();

            if (newBranchForm.Output != null)
            {
                if (repository != null)
                {
                    if (string.IsNullOrWhiteSpace(newBranchForm.Output.BranchName.Trim()))
                    {
                        ShowNotification("Please enter a valid branch name.");
                        return;
                    }

                    try
                    {
                        Branch newBranch = repository.CreateBranch(newBranchForm.Output.BranchName);
                        Commands.Checkout(repository, newBranch);

                        UpdateGitRepositoryInfo();

                        selectedBranchIndex = FindBranchIndexWithName(newBranch.FriendlyName);
                        gitBranchesComboBox.SelectedIndex = selectedBranchIndex;
                    }
                    catch (NameConflictException ex)
                    {
                        ShowNotification("Could not create branch because a reference with that name already exists.");
                    }
                    catch (Exception ex)
                    {
                        ShowNotification("Could not create branch.");
                    }

                }
                else
                {
                    ShowNotification("No repository opened.");
                }
            }
        }

        private void newBranchButton_Click(object sender, EventArgs e)
        {
            NewBranch();
        }

        private void directoryNavigationTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (dirNavModificationType.Equals(DirNavModificationType.CREATE_FILE))
            {
                try
                {
                    dirNavModificationType = DirNavModificationType.NONE;

                    string path = newFolderName + "\\" + e.Label;

                    if (File.Exists(path))
                    {
                        ShowNotification("A file with the same name already exists.");
                        e.Node.Remove();
                        return;
                    }
                    else if (e.Label == null || string.IsNullOrEmpty(e.Label.Trim()))
                    {
                        ShowNotification("File name cannot be empty.");
                        e.Node.Remove();
                        return;
                    }

                    CreateNewFile(path);
                    e.Node.Tag = path;
                }
                catch (Exception ex)
                {
                    ShowNotification("An error occurred while creating the file. Make sure you have the appropiate permissions and a file with the same name doesn't already exist.");
                    e.Node.Remove();
                }
            }
            else if (dirNavModificationType.Equals(DirNavModificationType.CREATE_FOLDER))
            {
                try
                {
                    dirNavModificationType = DirNavModificationType.NONE;

                    string path = newFolderName + "\\" + e.Label;

                    if (Directory.Exists(path))
                    {
                        ShowNotification("A folder with the same name already exists.");
                        e.Node.Remove();
                        return;
                    }
                    else if (string.IsNullOrEmpty(e.Label.Trim()))
                    {
                        ShowNotification("Folder name cannot be empty.");
                        e.Node.Remove();
                        return;
                    }

                    CreateNewFolder(path);
                    e.Node.Tag = path;
                }
                catch (Exception ex)
                {
                    ShowNotification("An error occurred while creating the folder. Make sure you have the appropiate permissions and a folder with the same name doesn't already exist.");
                    e.Node.Remove();
                }
            }
            else if (dirNavModificationType.Equals(DirNavModificationType.RENAME_FILE))
            {
                dirNavModificationType = DirNavModificationType.NONE;

                string path = newFolderName + e.Label;

                if (File.Exists(path))
                {
                    ShowNotification("A file with the same name already exists.");
                    e.Node.EndEdit(true);
                    e.CancelEdit = true;
                    return;
                }
                else if (e.Node.Tag.Equals(path) || string.IsNullOrEmpty(e.Label.Trim()))
                {
                    ShowNotification("File name cannot be empty.");
                    e.Node.EndEdit(true);
                    e.CancelEdit = true;
                    return;
                }
                else if (path.Equals(newFolderName))
                {
                    return;
                }

                RenameFile(e.Node.Tag.ToString(), path);
                e.Node.Tag = path;
            }
            else if (dirNavModificationType.Equals(DirNavModificationType.RENAME_DIRECTORY))
            {
                if (IsFileFromDirectoryOpened(newFolderName))
                {
                    ShowNotification("Please close all opened files from the directory before renaming it.");
                    e.CancelEdit = true;
                    return;
                }

                dirNavModificationType = DirNavModificationType.NONE;

                string path = parsingService.GetFolderName(newFolderName) + e.Label;

                if (Directory.Exists(path))
                {
                    ShowNotification("A folder with the same name already exists.");
                    e.Node.EndEdit(true);
                    e.CancelEdit = true;
                    return;
                }
                else if (e.Node.Tag.Equals(path) || string.IsNullOrEmpty(e.Label.Trim()))
                {
                    ShowNotification("Folder name cannot be empty.");
                    e.Node.EndEdit(true);
                    e.CancelEdit = true;
                    return;
                }
                else if (path.Equals(newFolderName))
                {
                    return;
                }
                RenameDirectory(e.Node.Tag.ToString(), path);
                e.Node.Tag = path;
                e.Node.Nodes.Clear();
                AddItemsToDirectory((KryptonTreeNode)e.Node);
            }

            directoryNavigationTreeView.Refresh();
        }
        private void CreateNewFolder(string src)
        {
            Directory.CreateDirectory(src);
            doNotShowBranchChangeNotification = true;
            UpdateGitRepositoryInfo(false);
        }

        private void CreateNewFile(string src)
        {
            File.Create(src).Close();
            doNotShowBranchChangeNotification = true;
            UpdateGitRepositoryInfo(false);
        }

        private void RenameFile(string src, string dest)
        {
            File.Move(src, dest);
            doNotShowBranchChangeNotification = true;
            UpdateGitRepositoryInfo(false);
        }

        private void RenameDirectory(string src, string dest)
        {
            Directory.Move(src, dest);
            doNotShowBranchChangeNotification = true;
            UpdateGitRepositoryInfo(false);
        }

        private void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo(false);
            }
            catch (Exception)
            {
                ShowNotification("Could not delete specified file. Make sure you have the appropiate permissions and the file is not being used by another process.");
            }
        }

        private void directoryNavigationTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                TreeNode clickedNode = directoryNavigationTreeView.GetNodeAt(e.X, e.Y);
                if (clickedNode != null)
                {
                    directoryNavigationTreeView.SelectedNode = clickedNode;
                }
            }
        }

        private void initializeANewRepoButton_Click(object sender, EventArgs e)
        {
            InitializeRepository(openedFolder);
        }

        private void cloneARepoButton_Click_1(object sender, EventArgs e)
        {
            CloneRepository();
        }

        private void directoryNavigationTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (dirNavModificationType == DirNavModificationType.NONE)
            {
                e.CancelEdit = true;
            }
        }
    }
}