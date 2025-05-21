/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using pie.Classes;
using pie.Forms.Databases;
using pie.Services;
using pie.Enums;
using pie.Forms.Format;
using pie.Forms.Theme;
using pie.Forms.Other;
using pie.Forms.Git;
using pie.Forms.CodeTemplates;
using pie.Classes.Configuration.FileBased.Impl;
using plugin.Classes;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using Krypton.Navigator;

/** Markdig is used in order to allow users to render their Markdown (.md) code into HTML.
 * 
 * Copyright (c) 2018-2019, Alexandre Mutel
 */
using Markdig;

/**
 * CefSharp is used in order to integrate Chromium-Based Web Browsers inside pie.
 * 
 * Copyright © The CefSharp Authors. All rights reserved.
 */
using CefSharp.WinForms;
using CefSharp;

/**
 * AutocompleteMenuNS is a namespace that comes from AutoCompleteMenu-ScintillaNet. It is used for various Autocomplete suggestions while writing code.
 * 
 * AutoCompleteMenu-ScintillaNet is licensed under the GNU Lesser General Public License (LGPLv3).
 * For more information related to the license, see https://www.gnu.org/licenses/lgpl-3.0.html.
 * 
 */
using AutocompleteMenuNS;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

/**
 * This namespace provides access to the ObjectListView control.
 * Licensed under GNU General Public License (GPL 3.0). For more info, see https://www.gnu.org/licenses/gpl-3.0.html 
 * 
 * Author of the library: Phillip Piper
 * Copyright 2006-2016 Bright Ideas Software
 */
using BrightIdeasSoftware;

/**
 * ConEmu.Winforms is used for integrating terminal features inside the application.
 * 
 * Copyright (c) 2021, Maksim Moisiuk <ConEmu.Maximus5@gmail.com>
 */
using ConEmu.WinForms;

namespace pie
{
    public partial class MainForm : KryptonForm
    {
        private ConfigurationService configurationService = new ConfigurationService();
        private ThemingService themeService = new ThemingService();
        private ParsingService parsingService = new ParsingService();
        private UpdateService updateService = new UpdateService();
        private ScintillaLexerService scintillaLexerService = new ScintillaLexerService();

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
        private bool mouseDown;
        private Point lastLocation;
        private Branch selectedBranch;
        private bool gitTabOpened;
        private int selectedBranchIndex;
        private bool doNotTriggerBranchChangeEvent;
        private bool doNotShowBranchChangeNotification;
        private bool showGitTabPressed;

        public string[] Args;

        const int WM_CONTEXTMENU = 0x007B;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CONTEXTMENU)
            {
                m.Result = IntPtr.Zero;
                //Close();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public delegate void ProcessParametersDelegate(string[] args);

        public void ProcessParameters(string[] args)
        {
            NewTab(TabType.CODE, null);

            if (args.Length == 2)
            {
                Open(args[1].ToString());
            }
        }


        public MainForm()
        {
            InitializeComponent();
            GetConfigurationDataFromFiles();
            SetDynamicDesign();
            ProcessCommandLineArguments();
        }

        private void GetConfigurationDataFromFiles()
        {
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

        private void ProcessGitCredentials()
        {
            try
            {
                gitCredentials = configurationService.GetArrayFromFile<GitCredentials>("config/git.json")[0];
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/git.json", "{}");
            }
        }

        private void ProcessThemes()
        {
            ProcessCustomThemes();
            SelectedTheme selectedTheme = configurationService.GetObjectFromFile<SelectedTheme>(AppDomain.CurrentDomain.BaseDirectory + "config/theme.json");

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


            themeInfos = configurationService.GetArrayFromMultipleFiles<ThemeInfo>("config\\themes", "json");

            foreach (ThemeInfo themeInfo in themeInfos)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = themeInfo.Name;

                toolStripMenuItem.Click += ToolStripMenuItem_Click1;

                themesToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }
        }

        public void ChangeTheme(ThemeInfo theme)
        {
            ControlHelper.SuspendDrawing(this);

            activeTheme = theme;

            configurationService.WriteToFile("config/theme.json", new SelectedTheme(theme.Name));

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

            if (directoryNavigationTextBox.Text != "")
            {
                NavigateToPath(directoryNavigationTextBox.Text);
            }

            doNotShowBranchChangeNotification = true;
            doNotTriggerBranchChangeEvent = true;
            UpdateGitRepositoryInfo();
            doNotShowBranchChangeNotification = false;
            doNotTriggerBranchChangeEvent = false;

            ControlHelper.ResumeDrawing(this);
            this.RedrawNonClient();
        }

        private void ProcessEditorProperties()
        {
            editorProperties = configurationService.GetObjectFromFile<EditorProperties>(AppDomain.CurrentDomain.BaseDirectory + "config/scintilla.json");

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
            plugins = configurationService.LoadPluginsFromFolder<Plugin>(AppDomain.CurrentDomain.BaseDirectory + "plugins");
            
            foreach(Plugin plugin in plugins) {
                ToolStripMenuItem pluginItem = new ToolStripMenuItem();
                pluginItem.Text = plugin.Name;
                pluginsToolStripMenuItem.DropDownItems.Add(pluginItem);

                Dictionary<PluginTask, Func<PluginTaskInput, PluginTaskOutput>> pluginTasks = plugin.GetTasks();

                foreach(var pluginTask in pluginTasks)
                {
                    ToolStripMenuItem taskItem = new ToolStripMenuItem();
                    taskItem.Text = pluginTask.Key.Name;
                    taskItem.Click += TaskItem_Click;
                    pluginItem.DropDownItems.Add(taskItem);
                }
            }
        }

        private void TaskItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem taskItem = (ToolStripMenuItem)sender;
            ToolStrip pluginItem = taskItem.GetCurrentParent();

            foreach(Plugin plugin in plugins) {
                if (plugin.Name.Equals(pluginItem.Text))
                {
                    foreach(var task in plugin.GetTasks())
                    {
                        if (task.Key.Name.Equals(taskItem.Text))
                        {
                            PluginTaskInput pluginTaskInput = new PluginTaskInput();
                            PluginTaskOutput pluginTaskOutput = plugin.InvokeTask(task.Value, pluginTaskInput);
                            MessageBox.Show(pluginTaskOutput.Actions.Count.ToString());
                        }
                    }
                }
            }
        }

        private void SetDynamicDesign()
        {
            this.MinimumSize = new Size(1036, 634);

            SynchronizeCustomControls();

            ResetFindPanelLocation();
            ResetDirectoryPanelLocation();
            findReplaceHeaderGroup.Visible = false;
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
            gitPanel.Hide();

            tabControl.AllowPageDrag = false;
            tabControl.AllowPageReorder = false;

            replaceTextBox.KeyDown += ReplaceTextBox_KeyDown;
        }

        private void SynchronizeCustomControls()
        {
            CustomColorTable customColorTable = new CustomColorTable();
            customColorTable.InputThemeInfo = activeTheme;

            CustomToolStripRenderer customToolStripRenderer = new CustomToolStripRenderer(customColorTable);
            customToolStripRenderer.InputThemeInfo = activeTheme;
            mainMenuStrip.Renderer = customToolStripRenderer;
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
            for(int i = 0; i<codeContextMenu.Items.Count; i++)
            {
                if (codeContextMenu.Items[i] is KryptonContextMenuHeading && ((KryptonContextMenuHeading)codeContextMenu.Items[i]).Text == "Suggested Actions")
                {
                    codeContextMenu.Items.RemoveAt(i+1);
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
            Process.Start(path);
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
            else if (tabType == TabType.GIT)
            {
                tabControl.KryptonContextMenu = gitContextMenu;

                ToggleTerminalTabControl(false);
                ToggleFindReplacePanel(false);
                directoryNavigationHeaderGroup.Visible = false;

                if (gitTabOpened)
                {
                    for (int i = 0; i < tabControl.Pages.Count; i++)
                    {
                        if (tabInfos[i].getTabType() == TabType.GIT)
                        {
                            tabControl.SelectedIndex = i;
                            return;
                        }
                    }
                }
                else
                {
                    kryptonPage.Text = "Git";
                    kryptonPage.Controls.Add(gitPanel);
                    gitPanel.Show();
                    gitPanel.Dock = DockStyle.Fill;
                    gitTabOpened = true;
                }
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
                ToggleFindReplacePanel(false);
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

            if (tabType == TabType.GIT)
            {
                UpdateFormTitle("Git");
            }
            else if (openedFilePath != null)
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
            else if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.GIT)
            {
                gitTabOpened = false;
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

                if (tabInfo.getTabType() != TabType.CODE && tabInfo.getTabType() != TabType.GIT)
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
                String chosenPath = saveFileDialog.FileName;

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

            foreach(ToolStripMenuItem toolStripMenuItem in buildAndRunToolstripMenuItem.DropDownItems)
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
                kryptonContextMenuItem12.Text = "Show Directory Navigator";
            }

            directoryNavigationHeaderGroup.Visible = status;
        }

        private void ShowFindReplacePanel()
        {
            ToggleDirectoryNavigator(false);
            ToggleFindReplacePanel(!findReplaceHeaderGroup.Visible);
        }

        private void ToggleFindReplacePanel(bool status)
        {
            findReplaceHeaderGroup.Visible = status;
            kryptonContextMenuItem6.Checked = status;

            if (status)
            {
                ResetFindPanelLocation();
                kryptonContextMenuItem6.Text = "Hide Find and Replace";
                findTextBox.Focus();
            }
            else
            {
                if (tabControl.SelectedIndex < tabInfos.Count && tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                    ClearHighlights(TextArea);
                }

                kryptonContextMenuItem6.Text = "Show Find and Replace";
            }
        }

        private void ToggleFindReplacePanel(bool status, int indexToMoveTo)
        {
            findReplaceHeaderGroup.Visible = status;
            kryptonContextMenuItem6.Checked = status;

            if (status)
            {
                ResetFindPanelLocation();
                kryptonContextMenuItem6.Text = "Hide Find and Replace";
                findTextBox.Focus();
            }
            else
            {
                if (indexToMoveTo < tabInfos.Count && tabInfos[indexToMoveTo].getTabType() == TabType.CODE)
                {
                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                    ClearHighlights(TextArea);
                }

                kryptonContextMenuItem6.Text = "Show Find and Replace";
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

            buildCommands = configurationService.GetArrayFromFile<BuildCommand>("config/build.json");

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

        private void ProcessLanguageMappings()
        {
           languageMappings = configurationService.GetArrayFromFile<LanguageMapping>("config\\mappings.json");
        }

        private void ProcessLanguageDefinitions()
        {
            languageDefinitions = configurationService.GetArrayFromMultipleFiles<LanguageDefinition>("config/languages", "json");
        }

        private void ProcessCodeTemplates()
        {
            codeTemplates = configurationService.GetArrayFromFile<CodeTemplate>("config\\templates.json");
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
            } catch(Exception)
            {
                // Update checker may fail due to missing network connection.
                updateToolStripMenuItem.Visible = false;
            }
        }

        private void GitStagingAreaListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            GitFile gitFile = (GitFile)e.Model;
            if (gitFile.Status == "Ignored")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(179, 179, 179) : Color.FromArgb(100, 100, 100);
            else if (gitFile.Status == "Deleted")
                e.Item.ForeColor = Color.FromArgb(251, 77, 77);
            else if (gitFile.Status == "New")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(60, 170, 232) : Color.FromArgb(40, 115, 158);
            else if (gitFile.Status == "Modified")
                e.Item.ForeColor = activeTheme.IconType == "dark" ? Color.FromArgb(255, 199, 87) : Color.FromArgb(224, 165, 45);
            else
                e.Item.ForeColor = activeTheme.Fore;
        }

        private void ReplaceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

                bool status = Replace(TextArea, findTextBox.Text, replaceTextBox.Text);

                if (!status)
                {
                    ShowNotification("No occurences found.");
                }
            }
        }

        private void ResetFindPanelLocation()
        {
            findReplaceHeaderGroup.Location = new Point((this.Width - findReplaceHeaderGroup.Width) / 2, (this.Height - findReplaceHeaderGroup.Height) / 4);
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
            if (directoryNavigationHeaderGroup.Visible)
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (directoryNavigationObjectListView.Items.Count > 0)
                    {
                        if (directoryNavigationObjectListView.SelectedIndex < directoryNavigationObjectListView.Items.Count - 1)
                        {
                            directoryNavigationObjectListView.Focus();
                            directoryNavigationObjectListView.SelectedIndex++;
                        }
                        else if (directoryNavigationObjectListView.SelectedItems.Count == 0)
                        {
                            directoryNavigationObjectListView.Focus();
                            directoryNavigationObjectListView.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (directoryNavigationObjectListView.SelectedItems.Count == 1)
                        {
                            NavigatorFile navigatorFile = (NavigatorFile)directoryNavigationObjectListView.SelectedObject;

                            if (navigatorFile.Type == "File")
                            {
                                NewTab(TabType.CODE, null);
                                bool state = Open(((NavigatorFile)navigatorFile).Path);

                                if (!state)
                                {
                                    CloseTab();
                                }
                                else
                                {
                                    ToggleDirectoryNavigator(false);
                                }
                            }
                            else
                            {
                                if (navigatorFile.Path != "..")
                                {
                                    NavigateToPath(navigatorFile.Path);
                                }
                                else
                                {
                                    NavigateToPath(parsingService.GoBackInFilePath(directoryNavigationTextBox.Text));
                                }
                            }
                        }
                    }
                }

                if (e.Modifiers == Keys.Control)
                {
                    e.SuppressKeyPress = true;
                }
            }
            else if (findReplaceHeaderGroup.Visible)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    StartFind();
                }
            }

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
            ToggleFindReplacePanel(false);
            ToggleDirectoryNavigator(!directoryNavigationHeaderGroup.Visible);

            if (directoryNavigationHeaderGroup.Visible)
            {
                directoryNavigationObjectListView.Focus();
            }

            ResetDirectoryPanelLocation();

            if (directoryNavigationTextBox.Text == "")
            {
                if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    NavigateToPath(parsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                }
                else
                {
                    NavigateToPath("C:\\");
                }
            }
        }

        private void ResetDirectoryPanelLocation()
        {
            directoryNavigationHeaderGroup.Location = new Point((this.Width - directoryNavigationHeaderGroup.Width) / 2, (this.Height - directoryNavigationHeaderGroup.Height) / 4);
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

            if (tag >= 0) {
                if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    BuildCommand buildCommand = buildCommands[tag];

                    string scriptName = parsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                    string command = buildCommand.Command;
                    command = command.Replace("$FILE", scriptName);
                    ToggleTerminalTabControlWithInitialization(true);

                    int insertPosition = terminalTabControl.Pages.Count;

                    for(int i = 0; i < terminalTabControl.Pages.Count; i++)
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

                    if (tabInfos[indexToMoveTo].getTabType() == TabType.GIT)
                    {
                        tabControl.KryptonContextMenu = gitContextMenu;
                    }
                    else
                    {
                        tabControl.KryptonContextMenu = renderContextMenu;
                    }

                    ToggleTerminalTabControl(false);
                    ToggleFindReplacePanel(false, indexToMoveTo);
                    ToggleDirectoryNavigator(false);

                    if (tabInfos[indexToMoveTo].getTabType() == TabType.GIT)
                    {
                        UpdateFormTitle("Git");
                    }
                    else
                    {
                        UpdateFormTitle(indexToMoveTo);
                    }

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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResetFindPanelLocation();
            ResetDirectoryPanelLocation();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            StartFind();
        }

        private void StartFind()
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            ClearHighlights(TextArea);
            HighlightWord(findTextBox.Text, TextArea, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
            Find(TextArea, lastSelectedIndex, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
        }

        private bool Find(Scintilla scintilla, int indexStart, bool regex, bool matchCase, bool matchWholeWord)
        {
            canUpdateUI = false;

            String text = findTextBox.Text;

            if (string.IsNullOrEmpty(text))
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

            if ((index = scintilla.SearchInTarget(text)) != -1)
            {
                scintilla.SetSelection(index, index + scintilla.TargetEnd - scintilla.TargetStart);
                lastSelectedIndex = index + scintilla.TargetEnd - scintilla.TargetStart + 1;

                scintilla.ScrollCaret();
            }
            else
            {
                if (indexStart == 0)
                {
                    ShowNotification("No more occurences found.");
                    return false;
                }

                lastSelectedIndex = 0;
                return Find(scintilla, 0, regex, matchCase, matchWholeWord);
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

        private void ClearHighlights(Scintilla scintilla)
        {
            scintilla.IndicatorCurrent = 8;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);
        }

        private void findPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void findPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                findReplaceHeaderGroup.Location = new Point(
                    (findReplaceHeaderGroup.Location.X - lastLocation.X) + e.X, (findReplaceHeaderGroup.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void findPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private bool Replace(Scintilla scintilla, String from, String to)
        {
            HighlightWord(from, scintilla, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
            bool found = Find(scintilla, lastSelectedIndex, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);

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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

            bool status = Replace(TextArea, findTextBox.Text, replaceTextBox.Text);
        }

        private void ReplaceAll(Scintilla scintilla, String from, String to)
        {
            bool status;

            do
            {
                status = Replace(scintilla, from, to);
            } while (status);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

            ReplaceAll(TextArea, findTextBox.Text, replaceTextBox.Text);
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

        private void showGitTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGitTabPressed = true;
            NewTab(TabType.GIT, null);
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
                    Repository.Init(path);
                    repositoryTextBox.Text = path;
                    doNotShowBranchChangeNotification = true;
                    UpdateGitRepositoryInfo();

                    if (gitBranchesComboBox.Items.Count > 0)
                    {
                        gitBranchesComboBox.SelectedIndex = selectedBranchIndex;
                    }
                }
            }
            else
            {
                repositoryTextBox.Text = path;
                doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo();

                if (gitBranchesComboBox.Items.Count > 0)
                {
                    gitBranchesComboBox.SelectedIndex = selectedBranchIndex;
                }
            }
        }

        private void UpdateGitRepositoryInfo()
        {
            string path = repositoryTextBox.Text;

            if (path == "")
            {
                return;
            }

            repository = new Repository(path);

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
                    gitBranchesComboBox.SelectedItem = gitBranchesComboBox.Items[gitBranchesComboBox.Items.Count - 1];
                }
            }

            if (gitBranchesComboBox.Items.Count > 0 && gitBranchesComboBox.SelectedItem == null)
            {
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
                }
                else if (item.State == FileStatus.Ignored)
                {
                    gitFile.Status = "Ignored";
                }
                else if (item.State == FileStatus.ModifiedInIndex || item.State == FileStatus.ModifiedInWorkdir)
                {
                    gitFile.Status = "Modified";
                }
                else if (item.State == FileStatus.NewInIndex || item.State == FileStatus.NewInWorkdir)
                {
                    gitFile.Status = "New";
                }
                else
                {
                    gitFile.Status = item.State.ToString();
                }

                gitFileList.Add(gitFile);
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
                            configurationService.WriteToFile("config/git.json", new List<GitCredentials>() { gitCredentials });
                            GitCommit(items);
                        }
                    }
                    else
                    {
                        Signature signature = new Signature(gitCredentials.Name, gitCredentials.Email, DateTime.Now);

                        string commitText = commitMessageRichTextBox.Text;

                        Task.Run(() =>
                        {
                            repository.Commit(commitText, signature, signature);
                        }).Wait();

                        doNotShowBranchChangeNotification = true;
                        UpdateGitRepositoryInfo();
                        ShowNotification("Successfully commited.");
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
                kryptonContextMenuItem.Click += KryptonContextMenuItem_Click;
                kryptonContextMenuItems.Items.Add(kryptonContextMenuItem);

                kryptonContextMenuItem = new KryptonContextMenuItem();
                kryptonContextMenuItem.Text = "Commit selected files";
                kryptonContextMenuItem.Image = Properties.Resources.git;
                kryptonContextMenuItem.Click += KryptonContextMenuItem_Click1;
                kryptonContextMenuItems.Items.Add(kryptonContextMenuItem);

                gitContextMenu.Items.Add(kryptonContextMenuHeading);
                gitContextMenu.Items.Add(kryptonContextMenuItems);
            }
        }

        private void KryptonContextMenuItem_Click1(object sender, EventArgs e)
        {
            string items = "";

            foreach (var gitFile in gitStagingAreaListView.SelectedObjects)
            {
                items += ((GitFile)gitFile).Name;
                items += " ";
            }

            GitCommit(items);
        }

        private void KryptonContextMenuItem_Click(object sender, EventArgs e)
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
                    NewTab(TabType.CODE, null);
                    Open(repositoryTextBox.Text + "\\" + fileName);
                }
            }
        }

        private void GitPush()
        {
            if (repository != null)
            {
                var branch = selectedBranch;

                if (true)
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
                            configurationService.WriteToFile("config/git.json", new List<GitCredentials>() { gitCredentials });
                            GitPush();
                        }
                    }
                    else
                    {
                        Remote remote = repository.Network.Remotes["origin"];

                        repository.Branches.Update(repository.Branches[gitBranchesComboBox.SelectedItem.ToString()],
                            b => b.Remote = remote.Name,
                            b => b.UpstreamBranch = repository.Branches[gitBranchesComboBox.SelectedItem.ToString()].CanonicalName);

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

                            string branchName = gitBranchesComboBox.SelectedItem.ToString();

                            Task.Run(() =>
                            {
                                repository.Network.Push(repository.Branches[branchName], pushOptions);
                            }).Wait();

                            ShowNotification("Successfully pushed to remote.");
                            UpdateGitRepositoryInfo();
                            doNotShowBranchChangeNotification = false;
                        }
                        catch (LibGit2SharpException ex)
                        {
                            ShowNotification("Authentication failed. If pushing on GitHub, generate an access token (with proper permissions) instead of using the password.");
                        }
                    }
                }
                else
                {
                    ShowNotification("Repository is not remote. Click 'OK' to see how to add the repository on GitHub");
                    Process.Start("https://docs.github.com/en/migrations/importing-source-code/using-the-command-line-to-import-source-code/adding-locally-hosted-code-to-github#adding-a-local-repository-to-github-using-git");
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

            if (doNotTriggerBranchChangeEvent)
            {
                doNotTriggerBranchChangeEvent = false;
            }
            else if (repository != null)
            {
                int selectedIndex = gitBranchesComboBox.SelectedIndex;

                if (doNotShowBranchChangeNotification)
                {
                    doNotShowBranchChangeNotification = false;
                }
                else
                {
                    notificationYesNoCancelFormOutput = ShowYesNoCancelNotification("Checking out another branch will discard your current changes. Would you like to continue?");
                }

                if (notificationYesNoCancelFormOutput != null && notificationYesNoCancelFormOutput.NotificationButton == NotificationButton.YES)
                {
                    foreach (var branch in repository.Branches)
                    {
                        if (branch.FriendlyName == gitBranchesComboBox.Text)
                        {
                            selectedBranch = branch;
                            selectedBranchIndex = gitBranchesComboBox.SelectedIndex;

                            CheckoutOptions checkoutOptions = new CheckoutOptions();
                            Branch currentBranch = Commands.Checkout(repository, branch, new CheckoutOptions() { CheckoutModifiers = CheckoutModifiers.Force });

                            if (gitBranchesComboBox.SelectedItem.ToString().StartsWith("origin/"))
                            {
                                // Check if current local branch already exists
                                string localBranchFriendlyName = gitBranchesComboBox.SelectedItem.ToString().Remove(0, 7);

                                if (localBranchFriendlyName == null)
                                {
                                    // Remove the "origin/" from remote branch name
                                    doNotShowBranchChangeNotification = true;
                                    var remoteBranch = repository.Branches[gitBranchesComboBox.SelectedItem.ToString()];
                                    var newBranch = repository.CreateBranch(localBranchFriendlyName, remoteBranch.Tip);
                                    repository.Branches.Update(newBranch, b => b.TrackedBranch = remoteBranch.CanonicalName);
                                }

                                int index = 0;
                                foreach (string branchName in gitBranchesComboBox.Items)
                                {
                                    if (branchName == localBranchFriendlyName)
                                    {
                                        doNotShowBranchChangeNotification = true;
                                        gitBranchesComboBox.SelectedIndex = index;
                                        break;
                                    }
                                    index++;
                                }

                            }
                        }
                    }

                    RetrieveGitItemsForCurrentBranch();
                }
                else
                {
                    doNotTriggerBranchChangeEvent = true;
                    gitBranchesComboBox.SelectedIndex = selectedIndex;
                    doNotTriggerBranchChangeEvent = false;
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
                configurationService.WriteToFile("config/git.json", new List<GitCredentials>() { gitCredentials });
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
                configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { gitCredentials } );
            }
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
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
                OpenRepository(gitCloneForm.Output.ClonePath);
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
                        configurationService.WriteToFile("config/git.json", new List<GitCredentials>() { gitCredentials });
                        GitPull();
                    }
                }
                else
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
                            configurationService.WriteToFile("config/git.json", new List<GitCredentials>() { gitCredentials });
                            GitPull();
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

                        try
                        {
                            Task.Run(() =>
                            {
                                Commands.Pull(repository, signature, pullOptions);
                            }).Wait();

                            UpdateGitRepositoryInfo();
                            ShowNotification("Pull successful.");
                        }
                        catch (LibGit2SharpException ex)
                        {
                            ShowNotification(ex.Message);
                        }
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

            // HeaderGroup
            findReplaceHeaderGroup.StateCommon.Border.Color1 = activeTheme.FormBorder;
            findReplaceHeaderGroup.StateCommon.Border.Color2 = activeTheme.FormBorder;
            directoryNavigationHeaderGroup.StateCommon.Border.Color1 = activeTheme.FormBorder;
            directoryNavigationHeaderGroup.StateCommon.Border.Color2 = activeTheme.FormBorder;

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

            directoryNavigationObjectListView.ShowGroups = false;
            directoryNavigationObjectListView.UseCustomSelectionColors = true;
            directoryNavigationObjectListView.FullRowSelect = true;
            directoryNavigationObjectListView.MultiSelect = false;
            directoryNavigationObjectListView.HeaderStyle = ColumnHeaderStyle.None;
            directoryNavigationObjectListView.SmallImageList = new ImageList();
            directoryNavigationObjectListView.SmallImageList.Images.Add("directory", Properties.Resources.folder);
            olvColumn3.FillsFreeSpace = true;
            olvColumn3.ImageGetter = new ImageGetterDelegate(NavigationImageGetter);

            directoryNavigationObjectListView.BackColor = activeTheme.Primary;
            directoryNavigationObjectListView.ForeColor = activeTheme.Fore;
            directoryNavigationObjectListView.HighlightBackgroundColor = activeTheme.Secondary;
            directoryNavigationObjectListView.HighlightForegroundColor = activeTheme.Fore;
            directoryNavigationObjectListView.UnfocusedHighlightBackgroundColor = activeTheme.Secondary;
            directoryNavigationObjectListView.UnfocusedHighlightForegroundColor = activeTheme.Fore;

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = activeTheme.Secondary;
            headerstyle.Normal.ForeColor = activeTheme.Fore;

            headerstyle.Hot.BackColor = activeTheme.ButtonHover;
            headerstyle.Hot.ForeColor = activeTheme.Fore;

            headerstyle.Pressed.BackColor = activeTheme.ButtonFrame;
            headerstyle.Pressed.ForeColor = activeTheme.Fore;

            gitStagingAreaListView.HeaderFormatStyle = headerstyle;

            regexCheckBox.StateCommon.ShortText.Color1 = activeTheme.Fore;
            regexCheckBox.StateCommon.ShortText.Color2 = activeTheme.Fore;

            matchCaseCheckBox.StateCommon.ShortText.Color1 = activeTheme.Fore;
            matchCaseCheckBox.StateCommon.ShortText.Color2 = activeTheme.Fore;

            matchWholeWordCheckBox.StateCommon.ShortText.Color1 = activeTheme.Fore;
            matchWholeWordCheckBox.StateCommon.ShortText.Color2 = activeTheme.Fore;

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
                kryptonButton8.Values.Image = Properties.Resources.refresh_black;
                kryptonButton6.Values.Image = Properties.Resources.commit_black;
                kryptonButton10.Values.Image = Properties.Resources.pull_black;
                kryptonButton7.Values.Image = Properties.Resources.push_black;
                kryptonButton11.Values.Image = Properties.Resources.log_black;
            }
            else if (activeTheme.IconType == "light")
            {
                kryptonButton8.Values.Image = Properties.Resources.refresh_white;
                kryptonButton6.Values.Image = Properties.Resources.commit_white;
                kryptonButton10.Values.Image = Properties.Resources.pull_white;
                kryptonButton7.Values.Image = Properties.Resources.push_white;
                kryptonButton11.Values.Image = Properties.Resources.log_white;
            }

            SynchronizeCustomControls();
        }

        private void regexCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            KryptonCheckBox kryptonCheckBox = (KryptonCheckBox)sender;

            if (kryptonCheckBox.Checked)
            {
                matchWholeWordCheckBox.Checked = false;
                matchWholeWordCheckBox.Enabled = false;
            }
            else
            {
                matchWholeWordCheckBox.Enabled = true;
            }
        }

        private void directoryNavigationHeaderGroup_Panel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void directoryNavigationHeaderGroup_Panel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void directoryNavigationHeaderGroup_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                directoryNavigationHeaderGroup.Location = new Point(
                    (directoryNavigationHeaderGroup.Location.X - lastLocation.X) + e.X, (directoryNavigationHeaderGroup.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            ChooseFolderForNavigator();
        }

        private void ChooseFolderForNavigator()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                NavigateToPath(folderBrowserDialog.SelectedPath);
            }
        }

        private void NavigateToPath(string path)
        {
            directoryNavigationObjectListView.Items.Clear();

            string[] files;
            string[] directories;

            try
            {
                files = Directory.GetFiles(path);
                directories = Directory.GetDirectories(path);
                directoryNavigationTextBox.Text = path;
            }
            catch (Exception ex)
            {
                ShowNotification("An error was encountered while trying to access the given path.");
                ChooseFolderForNavigator();
                return;
            }

            List<NavigatorFile> fileList = new List<NavigatorFile>();

            NavigatorFile back = new NavigatorFile();
            back.Path = "..";
            fileList.Add(back);

            foreach (string directoryString in directories)
            {
                NavigatorFile file = new NavigatorFile();
                file.Path = directoryString;
                file.Type = "Directory";
                fileList.Add(file);
            }

            foreach (string fileString in files)
            {
                NavigatorFile file = new NavigatorFile();
                file.Path = fileString;
                file.Type = "File";
                fileList.Add(file);
            }

            directoryNavigationObjectListView.SetObjects(fileList);
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

        private void ProcessDatabaseConnections()
        {
            databases = configurationService.GetArrayFromFile<DatabaseConnection>("config/databases.json");
        }

        private void ToolStripMenuItem_Click1(object sender, EventArgs e)
        {
            ChangeTheme(themeInfos.Find(theme => theme.Name == ((ToolStripMenuItem)sender).Text));
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editorProperties.Wordwrap = !editorProperties.Wordwrap;
            ToggleWordWrap(editorProperties.Wordwrap);
            configurationService.WriteToFile("config/scintilla.json", editorProperties);
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

            configurationService.WriteToFile("config/scintilla.json", editorProperties);
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

            configurationService.WriteToFile("config/scintilla.json", editorProperties);
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

        private void directoryNavigationObjectListView_DoubleClick(object sender, EventArgs e)
        {
            if (directoryNavigationObjectListView.SelectedItems.Count == 1)
            {
                NavigatorFile navigatorFile = (NavigatorFile)directoryNavigationObjectListView.SelectedObject;

                if (navigatorFile.Type == "File")
                {
                    NewTab(TabType.CODE, null);
                    bool state = Open(((NavigatorFile)navigatorFile).Path);

                    if (!state)
                    {
                        CloseTab();
                    }
                    else
                    {
                        ToggleDirectoryNavigator(false);
                    }
                }
                else
                {
                    if (navigatorFile.Path != "..")
                    {
                        NavigateToPath(navigatorFile.Path);
                    }
                    else
                    {
                        NavigateToPath(parsingService.GoBackInFilePath(directoryNavigationTextBox.Text));
                    }
                }
            }
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
            foreach(string file in files)
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
    }
}