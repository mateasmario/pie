/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

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
using pie.Exceptions;
using pie.Forms.Git;
using pie.Forms.CodeTemplates;
using pie.Classes.Exceptions;

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
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;

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
        private ThemeService themeService = new ThemeService();
        private ParsingService parsingService = new ParsingService();
        private UpdateService updateService = new UpdateService();
        private ScintillaLexerService scintillaLexerService = new ScintillaLexerService();
        private FormattingService formattingService = new FormattingService();

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
            ProcessBuildCommands();
            ProcessDatabaseConnections();
            ProcessLanguageMappings();
            ProcessLanguageDefinitions();
        }

        private void ProcessGitCredentials()
        {
            try
            {
                Globals.gitCredentials = configurationService.GetFromFile<GitCredentials>("config/git.json")[0];
            }
            catch (FileNotFoundException)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/git.json", "{}");
            }
        }

        private void ProcessThemes()
        {
            try
            {
                ProcessCustomThemes();
                SelectedTheme selectedTheme = configurationService.GetSingleFromFile<SelectedTheme>("config/theme.json");

                if (selectedTheme.Name == "Dark")
                {
                    Globals.theme = ThemeService.darkTheme;
                }
                else if (selectedTheme.Name == "Light")
                {
                    Globals.theme = ThemeService.lightTheme;
                }
                else
                {
                    foreach (ThemeInfo t in Globals.themeInfos)
                    {
                        if (t.Name == selectedTheme.Name)
                        {
                            Globals.theme = t;
                        }
                    }
                }

                ScintillaLexerService.ResetDictionary();
            }
            catch (FileNotFoundException)
            {
                Globals.theme = ThemeService.lightTheme;
                configurationService.WriteToFile("config/theme.json", new SelectedTheme("light"));
            }
        }

        private void ProcessCustomThemes()
        {
            if (themeSettingsToolStripMenuItem.DropDownItems.Count > 2)
            {
                int removeCount = themeSettingsToolStripMenuItem.DropDownItems.Count - 2;

                while (removeCount > 0)
                {
                    themeSettingsToolStripMenuItem.DropDownItems.RemoveAt(2);
                    removeCount--;
                }
            }


            Globals.themeInfos = configurationService.LoadFromFolder<ThemeInfo>("config/themes", "json");

            foreach (ThemeInfo themeInfo in Globals.themeInfos)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = themeInfo.Name;

                toolStripMenuItem.Click += ToolStripMenuItem_Click1;

                themeSettingsToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }
        }

        public void ChangeTheme(ThemeInfo theme)
        {
            ControlHelper.SuspendDrawing(this);

            Globals.theme = theme;

            configurationService.WriteToFile("config/theme.json", new SelectedTheme(theme.Name));

            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (Globals.tabInfos[i].getTabType() == TabType.CODE)
                {
                    KryptonPage kryptonPage = tabControl.Pages[i];
                    Scintilla scintilla = (Scintilla)kryptonPage.Controls[0];

                    ScintillaLexerService.ResetDictionary();

                    if (Globals.tabInfos[i].getOpenedFilePath() != null)
                    {
                        string extension = parsingService.GetFileExtension(Globals.tabInfos[i].getOpenedFilePath());
                        themeService.ColorizeTextArea(scintilla, Globals.theme);
                        ColorizeAutocompleteMenu(Globals.tabInfos[i].getAutocompleteMenu());
                        scintillaLexerService.SetLexer(extension, scintilla, tabControl, i);
                        UpdateNumberMarginWidth(scintilla, true);
                    }
                    else
                    {
                        themeService.ColorizeTextArea(scintilla, Globals.theme);
                    }
                }
            }

            themeService.SetPaletteToTheme(kryptonPalette, Globals.theme);
            SynchronizeMainFormComponentsWithTheme();

            if (directoryNavigationTextBox.Text != "")
            {
                NavigateToPath(directoryNavigationTextBox.Text);
            }

            Globals.doNotShowBranchChangeNotification = true;
            Globals.doNotTriggerBranchChangeEvent = true;
            UpdateGitRepositoryInfo();
            Globals.doNotShowBranchChangeNotification = false;
            Globals.doNotTriggerBranchChangeEvent = false;

            ControlHelper.ResumeDrawing(this);
            this.RedrawNonClient();
        }

        private void ProcessEditorProperties()
        {
            try
            {
                EditorPropertiesService.GetEditorPropertiesFromFile("config/scintilla.json");

                if (Globals.wordwrap)
                {
                    wordWrapToolStripMenuItem.Text = "Disable Word Wrap";
                }

                if (Globals.autosave)
                {
                    enableAutosaveToolStripMenuItem.Text = "Disable Autosave";
                }

                if (Globals.glass)
                {
                    glassModeToolStripMenuItem.Text = "Disable Glass Effect";
                }

            }
            catch (FileNotFoundException ex)
            {
                EditorPropertiesService.WriteEditorPropertiesToFile("config/scintilla.json", false, false, false);
            }

            try
            {
                Globals.customFormatters = formattingService.LoadCustomFormattersFromFolder("formatters");
            }
            catch (IncorrectPublicMethodArgumentNumberException ex)
            {
                ShowNotification("Public method needs to have a single parameter.");
            }
            catch (IncorrectPublicMethodArgumentTypeException ex)
            {
                ShowNotification("Public method argument type needs to be string.");
            }
            catch (IncorrectPublicMethodCountException ex)
            {
                ShowNotification("Formatter class needs to have a single public method.");
            }
            catch (IncorrectPublicMethodNameException ex)
            {
                ShowNotification("Public method name needs to be 'format'.");
            }
            catch (IncorrectPublicMethodReturnTypeException ex)
            {
                ShowNotification("Public method return type needs to be string.");
            }
        }

        private void SetDynamicDesign()
        {
            Globals.kryptonPalette = kryptonPalette;

            this.MinimumSize = new System.Drawing.Size(1036, 634);

            mainMenuStrip.Renderer = new CustomToolStripRenderer(new CustomColorTable());

            ResetFindPanelLocation();
            ResetDirectoryPanelLocation();
            findReplaceHeaderGroup.Visible = false;
            ToggleDirectoryNavigator(false);

            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.FormatRow += GitStagingAreaListView_FormatRow;

            themeService.SetPaletteToTheme(kryptonPalette, Globals.theme);
            SynchronizeMainFormComponentsWithTheme();

            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
            codeContextMenu.Palette = kryptonPalette;
            terminalContextMenu.Palette = kryptonPalette;
            renderContextMenu.Palette = kryptonPalette;
            gitContextMenu.Palette = kryptonPalette;

            terminalTabControl.Hide();
            gitPanel.Hide();

            tabControl.AllowPageDrag = false;
            tabControl.AllowPageReorder = false;

            replaceTextBox.KeyDown += ReplaceTextBox_KeyDown;
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
            colors.BackColor = Globals.theme.Primary;
            colors.ForeColor = Globals.theme.Fore;
            colors.HighlightingColor = Globals.theme.Primary;
            colors.SelectedBackColor = Globals.theme.CaretLineBack;
            colors.SelectedBackColor2 = Globals.theme.CaretLineBack;
            colors.SelectedForeColor = Globals.theme.Fore;
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
        private void FillContextMenu(String extension)
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

            // Add new "Suggested Actions" header + items
            if (extension == "c" || extension == "cpp")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Build C/C++ Source";
                item1.Click += Item1_Click;
                kryptonContextMenuItems.Items.Add(item1);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "java")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Build Java Source";
                item1.Click += Item1_Click1;
                kryptonContextMenuItems.Items.Add(item1);

                KryptonContextMenuItem item2 = new KryptonContextMenuItem();
                item2.Text = "Run as Java Class";
                item2.Click += Item2_Click;
                kryptonContextMenuItems.Items.Add(item2);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "py")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Run Python Script";
                item1.Click += Item1_Click2;
                kryptonContextMenuItems.Items.Add(item1);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "pl")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Run Perl Script";
                item1.Click += Item1_Click3; ;
                kryptonContextMenuItems.Items.Add(item1);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "html")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Render HTML";
                item1.Click += Item1_Click4;
                kryptonContextMenuItems.Items.Add(item1);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "md")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Render Markdown";
                item1.Click += Item1_Click5;
                kryptonContextMenuItems.Items.Add(item1);
                codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "sql" && Globals.databases.Count > 0)
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                codeContextMenu.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();

                foreach (DatabaseConnection database in Globals.databases)
                {
                    KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                    item1.Text = "Run query against " + database.Name;
                    item1.Click += Item1_Click6;
                    kryptonContextMenuItems.Items.Add(item1);
                    codeContextMenu.Items.Insert(3, kryptonContextMenuItems);
                }
            }
        }

        private void FileActionsItem2_Click(object sender, EventArgs e)
        {
            string path = parsingService.GoBackInFilePath(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            Process.Start(path);
        }

        private void FileActionsItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
        }

        private void Item1_Click6(object sender, EventArgs e)
        {
            KryptonContextMenuItem kryptonContextMenuItem = (KryptonContextMenuItem)sender;

            string name = kryptonContextMenuItem.Text.Substring(18);

            DatabaseConnection database = null;

            foreach (DatabaseConnection tempDatabase in Globals.databases)
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
                DatabaseOutputForm databaseOutputForm = new DatabaseOutputForm(databaseResult.DataTable);
                databaseOutputForm.ShowDialog();
            }
        }

        private void Item1_Click5(object sender, EventArgs e)
        {
            ExecuteRunCommand("Render Markdown (.md)");
        }

        private void Item1_Click4(object sender, EventArgs e)
        {
            ExecuteRunCommand("Render HTML (.html)");
        }

        private void Item1_Click3(object sender, EventArgs e)
        {
            ExecuteRunCommand("Perl Script (.pl)");
        }

        private void Item1_Click2(object sender, EventArgs e)
        {
            ExecuteRunCommand("Python Script (.py)");
        }

        private void Item2_Click(object sender, EventArgs e)
        {
            ExecuteRunCommand("Java Class (.class)");
        }

        private void Item1_Click1(object sender, EventArgs e)
        {
            ExecuteBuildCommand("Java Source (.java)");
        }

        private void Item1_Click(object sender, EventArgs e)
        {
            ExecuteBuildCommand("C/C++ Source (.c, .cpp)");
        }

        private void TextArea_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            if (Globals.canUpdateUI)
            {
                Scintilla TextArea = (Scintilla)sender;

                Globals.lastSelectedIndex = TextArea.CurrentPosition;
            }
            else
            {
                Globals.canUpdateUI = true;
            }
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            UpdateNumberMarginWidth(scintilla, false);


            Globals.tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);

            if (Globals.autosave)
            {
                if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
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
            if (maxLineNumberCharLength == Globals.maxLineNumberCharLength && !updateTheme)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            Globals.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void TextArea_MouseDown(object sender, MouseEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            if (e.Button == MouseButtons.Right)
            {
                codeContextMenu.Show(sender);
            }

            Globals.lastSelectedIndex = TextArea.SelectionEnd;
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

            Globals.tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);
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

                if (Globals.gitTabOpened)
                {
                    for (int i = 0; i < tabControl.Pages.Count; i++)
                    {
                        if (Globals.tabInfos[i].getTabType() == TabType.GIT)
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
                    Globals.gitTabOpened = true;
                }
            }
            else
            {
                if (Globals.firstBrowserTab)
                {
                    Cef.Initialize(new CefSettings());
                    Globals.firstBrowserTab = false;
                }

                tabControl.KryptonContextMenu = renderContextMenu;
                ToggleTerminalTabControl(false);
                ToggleFindReplacePanel(false);
                ToggleDirectoryNavigator(false);

                kryptonPage.Text = parsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                kryptonPage.ToolTipTitle = Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();

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
            Globals.tabInfos.Insert(index, tabInfo);

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

            if (Globals.wordwrap)
            {
                TextArea.WrapMode = WrapMode.Word;
            }
            else
            {
                TextArea.WrapMode = WrapMode.None;
            }

            TextArea.UsePopup(false);

            themeService.ColorizeTextArea(TextArea, Globals.theme);

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
            Globals.deletesTab = true;

            if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_HTML || Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_MD)
            {
                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.SelectedPage.Controls[0];
                CloseTabAfterWarning();
            }
            else if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.GIT)
            {
                Globals.gitTabOpened = false;
                CloseTabAfterWarning();
            }
            else if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFileChanges())
            {
                ShowYesNoCancelNotification("Save file before closing it?");
                if (Globals.notificationButtonPressed == NotificationButton.YES)
                {
                    Save(tabControl.SelectedIndex);
                    CloseTabAfterWarning();
                }
                else if (Globals.notificationButtonPressed == NotificationButton.NO)
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

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                UpdateFormTitle(tabControl.SelectedIndex);
            }

            Globals.deletesTab = false;

            return true;
        }

        public void CloseTabAfterWarning()
        {
            KryptonPage selectedKryptonPage = tabControl.SelectedPage;

            if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
            {
                Globals.deletesLastTab = true;
            }
            else
            {
                Globals.deletesLastTab = false;
            }

            if (tabControl.SelectedIndex >= 0)
            {
                TabInfo tabInfo = Globals.tabInfos[tabControl.SelectedIndex];
                Globals.tabInfos.RemoveAt(tabControl.SelectedIndex);

                if (tabInfo.getTabType() != TabType.CODE && tabInfo.getTabType() != TabType.GIT)
                {
                    selectedKryptonPage.Dispose();
                }
                else
                {
                    tabControl.Pages.Remove(selectedKryptonPage);
                }
            }

            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < Globals.tabInfos.Count && Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                DeactivateBuildAndRunOptions();
                ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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
            if (Globals.tabInfos[openedTabIndex].getOpenedFilePath() == null)
            {
                SaveAs(openedTabIndex);
            }
            else
            {
                string chosenPath = Globals.tabInfos[openedTabIndex].getOpenedFilePath();

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.Pages[openedTabIndex].Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabControl.Pages[openedTabIndex].Text = parsingService.GetFileName(chosenPath);

                string extension = parsingService.GetFileExtension(parsingService.GetFileName(Globals.tabInfos[openedTabIndex].getOpenedFilePath()));
                scintillaLexerService.SetLexer(extension, TextArea, tabControl, openedTabIndex);
                UpdateFormTitle(tabControl.SelectedIndex);
                Globals.tabInfos[openedTabIndex].setOpenedFileChanges(false);
            }

            DeactivateBuildAndRunOptions();

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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

                Globals.tabInfos[selectedIndex].setOpenedFilePath(chosenPath);
                tabControl.Pages[selectedIndex].Text = parsingService.GetFileName(chosenPath);
                tabControl.Pages[selectedIndex].ToolTipTitle = chosenPath;

                string extension = parsingService.GetFileExtension(parsingService.GetFileName(Globals.tabInfos[selectedIndex].getOpenedFilePath()));
                scintillaLexerService.SetLexer(extension, TextArea, tabControl, selectedIndex);
                UpdateFormTitle(selectedIndex);
                Globals.tabInfos[selectedIndex].setOpenedFileChanges(false);
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

            Globals.tabInfos[openedTabIndex].setOpenedFilePath(fileName);
            tabControl.SelectedPage.Text = parsingService.GetFileName(fileName);
            tabControl.SelectedPage.ToolTipTitle = fileName;

            string extension = parsingService.GetFileExtension(fileName);

            scintillaLexerService.SetLexer(extension, TextArea, tabControl, tabControl.SelectedIndex);

            DeactivateBuildAndRunOptions();
            ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            UpdateFormTitle(tabControl.SelectedIndex);

            if (tabControl.Pages.Count >= 1)
            {
                Globals.tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(false);
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

        public void NewTerminalTab(string process, bool beginning)
        {
            KryptonPage kryptonPage = new KryptonPage();

            if (beginning)
            {
                terminalTabControl.Pages.Insert(0, kryptonPage);
            }
            else
            {
                terminalTabControl.Pages.Add(kryptonPage);
            }

            ConEmuControl conEmuControl = new ConEmuControl();

            ConEmuStartInfo conEmuStartInfo = new ConEmuStartInfo();
            conEmuStartInfo.ConsoleProcessCommandLine = process;

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                conEmuStartInfo.StartupDirectory = parsingService.GetFolderName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
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
                NewTerminalTab("cmd.exe", false);
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

        public void ActivateSpecificBuildAndRunOptions(String extension)
        {
            FillContextMenu(extension);

            if (extension == "c" || extension == "cpp")
            {
                toolStripMenuItem3.Enabled = true;
            }
            else if (extension == "java")
            {
                toolStripMenuItem2.Enabled = true;
                javaClassclassToolStripMenuItem.Enabled = true;
            }
            else if (extension == "class")
            {
                javaClassclassToolStripMenuItem.Enabled = true;
            }
            else if (extension == "py")
            {
                pythonScriptpyToolStripMenuItem.Enabled = true;
            }
            else if (extension == "pl")
            {
                perlScriptplToolStripMenuItem.Enabled = true;
            }
            else if (extension == "html")
            {
                renderHTMLFilehtmlToolStripMenuItem.Enabled = true;
            }
            else if (extension == "md")
            {
                renderMarkdownmdToolStripMenuItem.Enabled = true;
            }

            // Custom Build Commands
            foreach (ToolStripMenuItem toolStripMenuItem in Globals.buildCommandToolStripMenuItems)
            {
                toolStripMenuItem.Enabled = true;
            }
        }

        public void DeactivateBuildAndRunOptions()
        {
            DeleteFileActions();
            RemoveSuggestedActions();

            toolStripMenuItem2.Enabled = false;
            toolStripMenuItem3.Enabled = false;

            foreach (ToolStripMenuItem toolStripMenuItem in Globals.buildCommandToolStripMenuItems)
            {
                toolStripMenuItem.Enabled = false;
            }

            javaClassclassToolStripMenuItem.Enabled = false;
            pythonScriptpyToolStripMenuItem.Enabled = false;
            perlScriptplToolStripMenuItem.Enabled = false;
            renderHTMLFilehtmlToolStripMenuItem.Enabled = false;
            renderMarkdownmdToolStripMenuItem.Enabled = false;
        }

        // [Method] Opens terminal tab control
        public void ShowTerminalTabControl()
        {
            if (terminalTabControl.Pages.Count == 0)
            {
                NewTerminalTab("cmd.exe", false);
            }

            ToggleTerminalTabControl(!terminalTabControl.Visible);
        }

        public void ToggleTerminalTabControl(bool status)
        {
            terminalTabControl.Visible = status;

            if (status)
            {
                showBuildToolsToolStripMenuItem.Text = "Hide Terminal Tab";
                kryptonContextMenuItem15.Text = "Hide Terminal Tab";
            }
            else
            {
                showBuildToolsToolStripMenuItem.Text = "Show Terminal Tab";
                kryptonContextMenuItem15.Text = "Show Terminal Tab";
            }
        }

        // [Method] Used for Build commands
        public void ExecuteBuildCommand(string type)
        {
            if (terminalTabControl.Pages.Count == 0)
            {
                NewTerminalTab("cmd.exe", false);
            }

            ToggleTerminalTabControl(true);

            string process = null;

            if (type == "Java Source (.java)")
            {
                process = "javac.exe " + Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();
            }
            else if (type == "C/C++ Source (.c, .cpp)")
            {
                process = "gcc -Wall " + Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();
            }

            NewTerminalTab(process, false);
        }

        public static void ShowNotification(string text)
        {
            Globals.notificationText = text;

            NotificationOK notification = new NotificationOK();
            notification.ShowDialog();

            Globals.notificationText = null;
        }

        public static void ShowYesNoCancelNotification(string text)
        {
            Globals.notificationText = text;

            NotificationYesNoCancel notification = new NotificationYesNoCancel();
            notification.ShowDialog();

            Globals.notificationText = null;
        }

        // [Method] Used for Run commands
        public void ExecuteRunCommand(string type)
        {
            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                if (terminalTabControl.Pages.Count == 0)
                {
                    NewTerminalTab("cmd.exe", false);
                }

                ToggleTerminalTabControl(true);

                string process = null;

                if (type == "Java Class (.class)")
                {
                    string className = parsingService.GetFileName(parsingService.RemoveFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));

                    process = "java " + className;
                }
                else if (type == "Python Script (.py)")
                {
                    process = "python " + Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();
                }
                else if (type == "Perl Script (.pl)")
                {
                    process = "perl " + Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();
                }
                else if (type == "Render HTML (.html)")
                {
                    if (parsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "html")
                    {
                        NewTab(TabType.RENDER_HTML, Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                    }
                    else
                    {
                        ShowNotification("pie can only render files with the .html extension.");
                    }

                    return;
                }
                else if (type == "Render Markdown (.md)")
                {
                    int index = tabControl.SelectedIndex;

                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

                    if (parsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "md")
                    {
                        NewTab(TabType.RENDER_MD, Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                    }
                    else
                    {
                        ShowNotification("pie can only render files with the .md extension.");
                    }

                    return;
                }

                NewTerminalTab(process, false);
            }
            else
            {
                ShowNotification("Please open a file before launching any run command.");
            }
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
                if (tabControl.SelectedIndex < Globals.tabInfos.Count && Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
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
                if (indexToMoveTo < Globals.tabInfos.Count && Globals.tabInfos[indexToMoveTo].getTabType() == TabType.CODE)
                {
                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                    ClearHighlights(TextArea);
                }

                kryptonContextMenuItem6.Text = "Show Find and Replace";
            }
        }

        private void ProcessBuildCommands()
        {
            if (buildToolStripMenuItem1.DropDownItems.Count > 2)
            {
                int removeCount = buildToolStripMenuItem1.DropDownItems.Count - 2;

                while (removeCount > 0)
                {
                    buildToolStripMenuItem1.DropDownItems.RemoveAt(2);
                    removeCount--;
                }
            }

            try
            {
                Globals.buildCommands = configurationService.GetFromFile<BuildCommand>("config/build.json");

                foreach (BuildCommand buildCommand in Globals.buildCommands)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Text = buildCommand.Name;
                    toolStripMenuItem.Tag = buildCommand.Command;

                    toolStripMenuItem.Click += ToolStripMenuItem_Click;

                    buildToolStripMenuItem1.DropDownItems.Add(toolStripMenuItem);
                    Globals.buildCommandToolStripMenuItems.Add(toolStripMenuItem);


                    if (tabControl.Pages.Count >= 2 && Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() == null)
                    {
                        toolStripMenuItem.Enabled = false;
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/build.json", "[]");
            }
            catch (ConfigurationException ex)
            {
                ShowNotification("There was an error in reading the build commands. Please check the syntax of the .json configuration.");
            }
        }

        private void ProcessLanguageMappings()
        {
            try
            {
                Globals.languageMappings = configurationService.GetFromFile<LanguageMapping>("config/mappings.json");
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/mappings.json", "[]");
            }
            catch (ConfigurationException ex)
            {
                ShowNotification("There was an error in reading the build commands. Please check the syntax of the .json configuration.");
            }
        }

        private void ProcessLanguageDefinitions()
        {
            Globals.languageDefinitions = scintillaLexerService.LoadDefinitionsFromFolder(AppDomain.CurrentDomain.BaseDirectory + "config/languages");
        }

        // [Event] Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
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
                e.Item.ForeColor = Globals.theme.IconType == "dark" ? Color.FromArgb(179, 179, 179) : Color.FromArgb(100, 100, 100);
            else if (gitFile.Status == "Deleted")
                e.Item.ForeColor = Color.FromArgb(251, 77, 77);
            else if (gitFile.Status == "New")
                e.Item.ForeColor = Globals.theme.IconType == "dark" ? Color.FromArgb(60, 170, 232) : Color.FromArgb(40, 115, 158);
            else if (gitFile.Status == "Modified")
                e.Item.ForeColor = Globals.theme.IconType == "dark" ? Color.FromArgb(255, 199, 87) : Color.FromArgb(224, 165, 45);
            else
                e.Item.ForeColor = Globals.theme.Fore;
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

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            string scriptName = parsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            string command = (string)((ToolStripMenuItem)sender).Tag;
            command = command.Replace("$FILE", scriptName);

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                if (terminalTabControl.Pages.Count == 0)
                {
                    NewTerminalTab("cmd.exe", false);
                }

                ToggleTerminalTabControl(true);

                NewTerminalTab(command, false);
            }
            else
            {
                ShowNotification("Please open a file before launching any run command.");
            }
        }

        // [Event] Triggered when a new tab is opened/closed
        private void tabControl_TabCountChanged(object sender, EventArgs e)
        {
            if (tabControl.Pages.Count == 0)
            {
                kryptonContextMenuItem3.Enabled = false;

                saveFileToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;
            }
            else
            {
                kryptonContextMenuItem3.Enabled = true;

                saveFileToolStripMenuItem.Enabled = true;
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

                if (e.Modifiers == Keys.Control)
                {
                    e.SuppressKeyPress = true;
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
            if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
            {
                if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                {
                    Save(tabControl.SelectedIndex);
                }
                else if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
                {
                    ShowTerminalTabControl();
                }
                else if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                {
                    ShowFindReplacePanel();
                }
                else if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
                {
                    ShowDirectoryNavigator();
                }
                else if (e.KeyCode == Keys.X && e.Modifiers == Keys.Control)
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
                        int selectionStart = TextArea.SelectionStart  - 1;

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
                if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                {
                    NavigateToPath(parsingService.GetFolderName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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
            ShowTerminalTabControl();
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
            ShowTerminalTabControl();
        }

        // [Event] Triggered by multiple senders, when building (upper menu -> Build)
        private void buildEvent(object sender, EventArgs e)
        {
            string content = ((ToolStripMenuItem)sender).Text;
            try
            {
                ExecuteBuildCommand(content);
            }
            catch (NullReferenceException ex)
            {
                ShowNotification("Please open a file before launching any build commands.");
            }
        }

        // [Event] Triggered by multiple senders, when running (upper menu -> Run)
        private void runEvent(object sender, EventArgs e)
        {
            string content = ((ToolStripMenuItem)sender).Text;
            try
            {
                ExecuteRunCommand(content);
            }
            catch (NullReferenceException ex)
            {
                ShowNotification("Please open a file before launching any run commands.");
            }
        }

        // [Event] Triggered when a tab is changed
        private void tabControl_SelectedPageChanged(object sender, EventArgs e)
        {
            int indexToMoveTo = Globals.deletesLastTab ? tabControl.SelectedIndex : (Globals.deletesTab ? tabControl.SelectedIndex - 1 : tabControl.SelectedIndex);

            if (indexToMoveTo >= 0)
            {
                if (Globals.tabInfos[indexToMoveTo].getTabType() == TabType.CODE)
                {
                    smartFormatterToolStripMenuItem.Enabled = true;

                    tabControl.KryptonContextMenu = codeContextMenu;

                    DeactivateBuildAndRunOptions();
                    if (tabControl.SelectedIndex != -1 && Globals.tabInfos[indexToMoveTo].getOpenedFilePath() != null)
                    {
                        ActivateSpecificBuildAndRunOptions(parsingService.GetFileExtension(Globals.tabInfos[indexToMoveTo].getOpenedFilePath()));
                        UpdateFormTitle(indexToMoveTo);
                    }
                    else
                    {
                        UpdateFormTitle("Untitled");
                    }
                }
                else
                {
                    smartFormatterToolStripMenuItem.Enabled = false;

                    if (Globals.tabInfos[indexToMoveTo].getTabType() == TabType.GIT)
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

                    if (Globals.tabInfos[indexToMoveTo].getTabType() == TabType.GIT)
                    {
                        UpdateFormTitle("Git");
                    }
                    else
                    {
                        UpdateFormTitle(indexToMoveTo);
                    }
                }
            }
            else if (Globals.showGitTabPressed)
            {
                tabControl.KryptonContextMenu = gitContextMenu;
                Globals.showGitTabPressed = false;
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
            this.Text = Globals.tabInfos[index].getOpenedFilePath() + " - pie";
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
            NewTerminalTab("cmd.exe", false);
        }

        // [Event] Opens a new PowerShell terminal (accessed via Terminal tab control)
        private void kryptonContextMenuItem17_Click(object sender, EventArgs e)
        {
            NewTerminalTab("powershell.exe", false);
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
                    //int index = Globals.lastSelectedTabIndex;
                    //tabControl.SelectedIndex = index;
                }
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildCommandsForm buildCommandsForm = new BuildCommandsForm();
            buildCommandsForm.ShowDialog();

            if (Globals.closeAfterApplyingChanges)
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
            Find(TextArea, Globals.lastSelectedIndex, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
        }

        private bool Find(Scintilla scintilla, int indexStart, bool regex, bool matchCase, bool matchWholeWord)
        {
            Globals.canUpdateUI = false;

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
                Globals.lastSelectedIndex = index + scintilla.TargetEnd - scintilla.TargetStart + 1;

                scintilla.ScrollCaret();
            }
            else
            {
                if (indexStart == 0)
                {
                    ShowNotification("No more occurences found.");
                    return false;
                }

                Globals.lastSelectedIndex = 0;
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
            scintilla.Indicators[8].ForeColor = Globals.theme.Selection;
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
            Globals.mouseDown = true;
            Globals.lastLocation = e.Location;
        }

        private void findPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Globals.mouseDown)
            {
                findReplaceHeaderGroup.Location = new Point(
                    (findReplaceHeaderGroup.Location.X - Globals.lastLocation.X) + e.X, (findReplaceHeaderGroup.Location.Y - Globals.lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void findPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.mouseDown = false;
        }

        private bool Replace(Scintilla scintilla, String from, String to)
        {
            HighlightWord(from, scintilla, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
            bool found = Find(scintilla, Globals.lastSelectedIndex, regexCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);

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
            aboutForm.ShowDialog();
        }

        private void kryptonContextMenuItem6_Click(object sender, EventArgs e)
        {
            ShowFindReplacePanel();
        }

        private void kryptonContextMenuItem10_Click(object sender, EventArgs e)
        {
            if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_HTML)
            {
                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.SelectedPage.Controls[0];
                chromiumWebBrowser.Load(chromiumWebBrowser.Address);
            }
            else if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_MD)
            {
                string htmlContent = ConvertMarkdownToHtml(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());

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
            Globals.showGitTabPressed = true;
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
                ShowYesNoCancelNotification("No repository found at specified path. Would you like to initialize a new repository?");

                if (Globals.notificationButtonPressed == NotificationButton.YES)
                {
                    Repository.Init(path);
                    repositoryTextBox.Text = path;
                    Globals.doNotShowBranchChangeNotification = true;
                    UpdateGitRepositoryInfo();

                    if (gitBranchesComboBox.Items.Count > 0)
                    {
                        gitBranchesComboBox.SelectedIndex = Globals.selectedBranchIndex;
                    }
                }
            }
            else
            {
                repositoryTextBox.Text = path;
                Globals.doNotShowBranchChangeNotification = true;
                UpdateGitRepositoryInfo();

                if (gitBranchesComboBox.Items.Count > 0)
                {
                    gitBranchesComboBox.SelectedIndex = Globals.selectedBranchIndex;
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

            Globals.repo = new Repository(path);

            RetrieveGitItemsForCurrentBranch();

            string selectedBranch = null;
            if (gitBranchesComboBox.SelectedItem != null)
            {
                selectedBranch = gitBranchesComboBox.SelectedItem.ToString();
            }

            gitBranchesComboBox.Items.Clear();

            foreach (var branch in Globals.repo.Branches)
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
            if (Globals.repo != null)
            {
                Globals.doNotTriggerBranchChangeEvent = true;
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

            foreach (var item in Globals.repo.RetrieveStatus(statusOptions))
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
            if (Globals.repo != null)
            {
                RepositoryStatus status = Globals.repo.RetrieveStatus();

                if (status.IsDirty)
                {
                    if (items != "*")
                    {
                        List<string> files = items.Split(' ').ToList();

                        if (files[files.Count - 1] == "")
                        {
                            files.RemoveAt(files.Count - 1);
                        }

                        Commands.Stage(Globals.repo, files);
                    }
                    else
                    {
                        Commands.Stage(Globals.repo, items);
                    }

                    if (string.IsNullOrEmpty(Globals.gitCredentials.Name) || string.IsNullOrEmpty(Globals.gitCredentials.Email))
                    {
                        GitCommitCredentialsForm gitCredentialsForm = new GitCommitCredentialsForm();
                        Globals.gitFormClosedWithOk = false;
                        gitCredentialsForm.ShowDialog();

                        if (Globals.gitFormClosedWithOk)
                        {
                            configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials });
                            GitCommit(items);
                        }
                    }
                    else
                    {
                        Signature signature = new Signature(Globals.gitCredentials.Name, Globals.gitCredentials.Email, DateTime.Now);

                        string commitText = commitMessageRichTextBox.Text;

                        Task.Run(() =>
                        {
                            Globals.repo.Commit(commitText, signature, signature);
                        }).Wait();

                        Globals.doNotShowBranchChangeNotification = true;
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
            if (Globals.repo != null)
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
            if (Globals.repo != null)
            {
                var branch = Globals.selectedBranch;

                if (true)
                {
                    if (string.IsNullOrEmpty(Globals.gitCredentials.Username) || string.IsNullOrEmpty(Globals.gitCredentials.Password))
                    {
                        GitPushCredentialsForm gitCredentialsForm = new GitPushCredentialsForm();
                        Globals.gitFormClosedWithOk = false;
                        gitCredentialsForm.ShowDialog();

                        if (Globals.gitFormClosedWithOk)
                        {
                            configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials });
                            GitPush();
                        }
                    }
                    else
                    {
                        Remote remote = Globals.repo.Network.Remotes["origin"];

                        Globals.repo.Branches.Update(Globals.repo.Branches[gitBranchesComboBox.SelectedItem.ToString()],
                            b => b.Remote = remote.Name,
                            b => b.UpstreamBranch = Globals.repo.Branches[gitBranchesComboBox.SelectedItem.ToString()].CanonicalName);

                        // Push the branch to the remote                        
                        var pushOptions = new LibGit2Sharp.PushOptions();
                        pushOptions.CredentialsProvider = new LibGit2Sharp.Handlers.CredentialsHandler(
                           (url, usernameFromUrl, types) =>
                               new LibGit2Sharp.UsernamePasswordCredentials()
                               {
                                   Username = Globals.gitCredentials.Username,
                                   Password = Globals.gitCredentials.Password
                               });

                        try
                        {
                            Globals.doNotShowBranchChangeNotification = true;

                            string branchName = gitBranchesComboBox.SelectedItem.ToString();

                            Task.Run(() =>
                            {
                                Globals.repo.Network.Push(Globals.repo.Branches[branchName], pushOptions);
                            }).Wait();

                            ShowNotification("Successfully pushed to remote.");
                            UpdateGitRepositoryInfo();
                            Globals.doNotShowBranchChangeNotification = false;
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
            if (Globals.doNotTriggerBranchChangeEvent)
            {
                Globals.doNotTriggerBranchChangeEvent = false;
            }
            else if (Globals.repo != null)
            {
                int selectedIndex = gitBranchesComboBox.SelectedIndex;

                if (Globals.doNotShowBranchChangeNotification)
                {
                    Globals.doNotShowBranchChangeNotification = false;
                }
                else
                {
                    ShowYesNoCancelNotification("Checking out another branch will discard your current changes. Would you like to continue?");
                }

                if (Globals.notificationButtonPressed == NotificationButton.YES)
                {
                    foreach (var branch in Globals.repo.Branches)
                    {
                        if (branch.FriendlyName == gitBranchesComboBox.Text)
                        {
                            Globals.selectedBranch = branch;
                            Globals.selectedBranchIndex = gitBranchesComboBox.SelectedIndex;

                            CheckoutOptions checkoutOptions = new CheckoutOptions();
                            Branch currentBranch = Commands.Checkout(Globals.repo, branch, new CheckoutOptions() { CheckoutModifiers = CheckoutModifiers.Force });

                            if (gitBranchesComboBox.SelectedItem.ToString().StartsWith("origin/"))
                            {
                                // Check if current local branch already exists
                                string localBranchFriendlyName = gitBranchesComboBox.SelectedItem.ToString().Remove(0, 7);

                                if (localBranchFriendlyName == null)
                                {
                                    // Remove the "origin/" from remote branch name
                                    Globals.doNotShowBranchChangeNotification = true;
                                    var remoteBranch = Globals.repo.Branches[gitBranchesComboBox.SelectedItem.ToString()];
                                    var newBranch = Globals.repo.CreateBranch(localBranchFriendlyName, remoteBranch.Tip);
                                    Globals.repo.Branches.Update(newBranch, b => b.TrackedBranch = remoteBranch.CanonicalName);
                                }

                                int index = 0;
                                foreach (string branchName in gitBranchesComboBox.Items)
                                {
                                    if (branchName == localBranchFriendlyName)
                                    {
                                        Globals.doNotShowBranchChangeNotification = true;
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
                    Globals.doNotTriggerBranchChangeEvent = true;
                    gitBranchesComboBox.SelectedIndex = selectedIndex;
                    Globals.doNotTriggerBranchChangeEvent = false;
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
            Globals.gitFormClosedWithOk = false;
            gitSettingsForm.ShowDialog();

            if (Globals.gitFormClosedWithOk)
            {
                configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials });
            }
        }

        private void managePushCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GitPushCredentialsForm gitCredentialsForm = new GitPushCredentialsForm();
            Globals.gitFormClosedWithOk = false;
            gitCredentialsForm.ShowDialog();

            if (Globals.gitFormClosedWithOk)
            {
                configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials } );
            }
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            GitCloneForm gitCloneForm = new GitCloneForm();
            gitCloneForm.ShowDialog();

            if (Globals.clonePath != null)
            {
                OpenRepository(Globals.clonePath);
            }
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            GitPull();
        }

        private void GitPull()
        {
            if (Globals.repo != null)
            {
                if (string.IsNullOrEmpty(Globals.gitCredentials.Name) || string.IsNullOrEmpty(Globals.gitCredentials.Email))
                {
                    GitCommitCredentialsForm gitCredentialsForm = new GitCommitCredentialsForm();
                    Globals.gitFormClosedWithOk = false;
                    gitCredentialsForm.ShowDialog();

                    if (Globals.gitFormClosedWithOk)
                    {
                        configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials });
                        GitPull();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Globals.gitCredentials.Username) || string.IsNullOrEmpty(Globals.gitCredentials.Password))
                    {
                        GitPushCredentialsForm gitCredentialsForm = new GitPushCredentialsForm();
                        Globals.gitFormClosedWithOk = false;
                        gitCredentialsForm.ShowDialog();

                        if (Globals.gitFormClosedWithOk)
                        {
                            configurationService.WriteToFile<GitCredentials>("config/git.json", new List<GitCredentials>() { Globals.gitCredentials });
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
                                Username = Globals.gitCredentials.Username,
                                Password = Globals.gitCredentials.Password
                            });

                        Signature signature = new Signature(Globals.gitCredentials.Name, Globals.gitCredentials.Email, DateTime.Now);

                        try
                        {
                            Task.Run(() =>
                            {
                                Commands.Pull(Globals.repo, signature, pullOptions);
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
            if (Globals.repo != null)
            {
                GitCommitLogForm gitCommitLogForm = new GitCommitLogForm();
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
            mainMenuStrip.BackColor = Globals.theme.Primary;
            mainMenuStrip.ForeColor = Globals.theme.Fore;

            foreach (ToolStripMenuItem toolStripMenuItem in mainMenuStrip.Items)
            {
                toolStripMenuItem.DropDown.BackColor = Globals.theme.Primary;
                toolStripMenuItem.DropDown.ForeColor = Globals.theme.Fore;
                toolStripMenuItem.ImageTransparentColor = Globals.theme.Primary;

                if (toolStripMenuItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem toolStripMenuItemChild in toolStripMenuItem.DropDownItems)
                    {
                        toolStripMenuItemChild.BackColor = Globals.theme.Primary;
                        toolStripMenuItemChild.ForeColor = Globals.theme.Fore;

                        if (toolStripMenuItemChild.HasDropDownItems)
                        {
                            foreach (ToolStripMenuItem toolStripMenuItemChild2 in toolStripMenuItemChild.DropDownItems)
                            {
                                toolStripMenuItemChild2.BackColor = Globals.theme.Primary;
                                toolStripMenuItemChild2.ForeColor = Globals.theme.Fore;
                            }
                        }
                    }
                }
            }

            // HeaderGroup
            findReplaceHeaderGroup.StateCommon.Border.Color1 = Globals.theme.FormBorder;
            findReplaceHeaderGroup.StateCommon.Border.Color2 = Globals.theme.FormBorder;
            directoryNavigationHeaderGroup.StateCommon.Border.Color1 = Globals.theme.FormBorder;
            directoryNavigationHeaderGroup.StateCommon.Border.Color2 = Globals.theme.FormBorder;

            // TabControl
            tabControl.StateCommon.Panel.Color1 = Globals.theme.Primary;
            tabControl.StateCommon.Panel.Color2 = Globals.theme.Primary;

            // ObjectListView
            gitStagingAreaListView.UseCustomSelectionColors = true;
            gitStagingAreaListView.FullRowSelect = true;
            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.HighlightBackgroundColor = Globals.theme.Secondary;
            gitStagingAreaListView.HighlightForegroundColor = Globals.theme.Fore;
            gitStagingAreaListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            gitStagingAreaListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;
            gitStagingAreaListView.BackColor = Globals.theme.Primary;
            gitStagingAreaListView.ForeColor = Globals.theme.Fore;

            directoryNavigationObjectListView.ShowGroups = false;
            directoryNavigationObjectListView.UseCustomSelectionColors = true;
            directoryNavigationObjectListView.FullRowSelect = true;
            directoryNavigationObjectListView.MultiSelect = false;
            directoryNavigationObjectListView.HeaderStyle = ColumnHeaderStyle.None;
            directoryNavigationObjectListView.SmallImageList = new ImageList();
            directoryNavigationObjectListView.SmallImageList.Images.Add("directory", Properties.Resources.folder);
            olvColumn3.FillsFreeSpace = true;
            olvColumn3.ImageGetter = new ImageGetterDelegate(NavigationImageGetter);

            directoryNavigationObjectListView.BackColor = Globals.theme.Primary;
            directoryNavigationObjectListView.ForeColor = Globals.theme.Fore;
            directoryNavigationObjectListView.HighlightBackgroundColor = Globals.theme.Secondary;
            directoryNavigationObjectListView.HighlightForegroundColor = Globals.theme.Fore;
            directoryNavigationObjectListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            directoryNavigationObjectListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = Globals.theme.Secondary;
            headerstyle.Normal.ForeColor = Globals.theme.Fore;

            headerstyle.Hot.BackColor = Globals.theme.ButtonHover;
            headerstyle.Hot.ForeColor = Globals.theme.Fore;

            headerstyle.Pressed.BackColor = Globals.theme.ButtonFrame;
            headerstyle.Pressed.ForeColor = Globals.theme.Fore;

            gitStagingAreaListView.HeaderFormatStyle = headerstyle;

            regexCheckBox.StateCommon.ShortText.Color1 = Globals.theme.Fore;
            regexCheckBox.StateCommon.ShortText.Color2 = Globals.theme.Fore;

            matchCaseCheckBox.StateCommon.ShortText.Color1 = Globals.theme.Fore;
            matchCaseCheckBox.StateCommon.ShortText.Color2 = Globals.theme.Fore;

            matchWholeWordCheckBox.StateCommon.ShortText.Color1 = Globals.theme.Fore;
            matchWholeWordCheckBox.StateCommon.ShortText.Color2 = Globals.theme.Fore;

            // ComboBox
            gitBranchesComboBox.StateCommon.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.DropBack.Color1 = Globals.theme.Primary;
            gitBranchesComboBox.StateCommon.DropBack.Color2 = Globals.theme.Primary;

            gitBranchesComboBox.StateTracking.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Back.Color1 = Globals.theme.Secondary;
            gitBranchesComboBox.StateTracking.Item.Back.Color2 = Globals.theme.Secondary;

            // Git Buttons
            if (Globals.theme.IconType == "dark")
            {
                kryptonButton8.Values.Image = Properties.Resources.refresh_black;
                kryptonButton6.Values.Image = Properties.Resources.commit_black;
                kryptonButton10.Values.Image = Properties.Resources.pull_black;
                kryptonButton7.Values.Image = Properties.Resources.push_black;
                kryptonButton11.Values.Image = Properties.Resources.log_black;
            }
            else if (Globals.theme.IconType == "light")
            {
                kryptonButton8.Values.Image = Properties.Resources.refresh_white;
                kryptonButton6.Values.Image = Properties.Resources.commit_white;
                kryptonButton10.Values.Image = Properties.Resources.pull_white;
                kryptonButton7.Values.Image = Properties.Resources.push_white;
                kryptonButton11.Values.Image = Properties.Resources.log_white;
            }
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
            Globals.mouseDown = true;
            Globals.lastLocation = e.Location;
        }

        private void directoryNavigationHeaderGroup_Panel_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.mouseDown = false;
        }

        private void directoryNavigationHeaderGroup_Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (Globals.mouseDown)
            {
                directoryNavigationHeaderGroup.Location = new Point(
                    (directoryNavigationHeaderGroup.Location.X - Globals.lastLocation.X) + e.X, (directoryNavigationHeaderGroup.Location.Y - Globals.lastLocation.Y) + e.Y);

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
            databasesForm.ShowDialog();

            if (Globals.closeAfterApplyingChanges)
            {
                ProcessDatabaseConnections();
            }
        }

        private void ProcessDatabaseConnections()
        {
            try
            {
                Globals.databases = configurationService.GetFromFile<DatabaseConnection>("config/databases.json");
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/databases.json", "[]");
            }
            catch (ConfigurationException ex)
            {
                ShowNotification("There was an error in reading the database connections. Please check the syntax of the .json configuration.");
            }
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(ThemeService.lightTheme);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(ThemeService.darkTheme);
        }

        private void ToolStripMenuItem_Click1(object sender, EventArgs e)
        {
            ChangeTheme(Globals.themeInfos.Find(theme => theme.Name == ((ToolStripMenuItem)sender).Text));
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.wordwrap = !Globals.wordwrap;
            ToggleWordWrap(Globals.wordwrap);
            EditorPropertiesService.WriteEditorPropertiesToFile("config/scintilla.json", Globals.wordwrap, Globals.autosave, Globals.glass);
        }

        private void ToggleWordWrap(bool status)
        {
            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (Globals.tabInfos[i].getTabType() == TabType.CODE)
                {
                    KryptonPage kryptonPage = tabControl.Pages[i];
                    Scintilla scintilla = (Scintilla)kryptonPage.Controls[0];

                    if (status)
                    {
                        scintilla.WrapMode = WrapMode.Word;
                        wordWrapToolStripMenuItem.Text = "Disable Word Wrap";
                    }
                    else
                    {
                        scintilla.WrapMode = WrapMode.None;
                        wordWrapToolStripMenuItem.Text = "Enable Word Wrap";
                    }
                }
            }
        }

        private void enableAutosaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.autosave = !Globals.autosave;

            if (Globals.autosave)
            {
                enableAutosaveToolStripMenuItem.Text = "Disable Autosave";
                PerformFirstSaveWhenAutosaveTriggered();
            }
            else
            {
                enableAutosaveToolStripMenuItem.Text = "Enable Autosave";
            }

            EditorPropertiesService.WriteEditorPropertiesToFile("config/scintilla.json", Globals.wordwrap, Globals.autosave, Globals.glass);
        }

        private void PerformFirstSaveWhenAutosaveTriggered()
        {
            for (int i = 0; i < tabControl.Pages.Count; i++)
            {
                if (Globals.tabInfos[i].getTabType() == TabType.CODE)
                {
                    if (Globals.tabInfos[i].getOpenedFilePath() != null)
                    {
                        Save(i);
                    }
                }
            }
        }

        private void duplicateEveryLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.DuplicateLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void addEmptyRowBetweenEachLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.AddEmptyRowBetweenEachLine(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void capitalizeFirstLineCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.CapitalizeFirstCharacterFromEveryLine(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void emptyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveEmptyLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void removeWhitespaceLinesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveWhitespaceLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void removeDuplicateLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveDuplicateLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void consecutiveDuplicateLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveConsecutiveDuplicates(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void trimLineswhitespacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.TrimLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void capitalizeEveryWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.CapitalizeEveryWord(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void capitalizeEntireTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];

            int currPos = scintilla.CurrentPosition;

            string result = formattingService.ConvertTextToUppercase(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void lowercaseEntireContentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.ConvertTextToLowercase(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void switchLowercaseWithUppercaseAndTheOtherWayAroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.SwitchLowercaseWithUppercase(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void allWhitespaceCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveAllWhitespaces(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void trimWhitespaceCharactersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.RemoveAllConsecutiveWhitespaces(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void sortAscendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.SortLines(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void sortDescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.SortLines(scintilla.Text, false);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void revertLineOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.ReverseLineOrder(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void convertNewlineToCommaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.ConvertNewlineToComma(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void convertNewlinenToSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)tabControl.SelectedPage.Controls[0];
            int currPos = scintilla.CurrentPosition;

            string result = formattingService.ConvertNewlineToSpace(scintilla.Text);
            scintilla.Text = result;

            scintilla.SelectionStart = currPos;
            scintilla.SelectionEnd = currPos;
        }

        private void smartFormatterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

            FormatForm formatForm = new FormatForm();
            formatForm.Input = TextArea.Text;
            formatForm.ShowDialog();

            int currPos = TextArea.CurrentPosition;
            TextArea.Text = formatForm.Output;
            TextArea.SelectionStart = currPos;
            TextArea.SelectionEnd = currPos;
        }

        private void glassModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.glass = !Globals.glass;

            if (Globals.glass)
            {
                glassModeToolStripMenuItem.Text = "Disable Glass Effect";
                this.Opacity = 0.90;
            }
            else
            {
                glassModeToolStripMenuItem.Text = "Enable Glass Effect";
                this.Opacity = 1;
            }

            EditorPropertiesService.WriteEditorPropertiesToFile("config/scintilla.json", Globals.wordwrap, Globals.autosave, Globals.glass);
        }

        private void cheatsheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheatsheetForm cheatsheetForm = new CheatsheetForm();
            cheatsheetForm.ShowDialog();
        }

        private void themeDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesignerForm designerForm = new DesignerForm();
            designerForm.ShowDialog();
            ProcessCustomThemes();
            ChangeTheme(Globals.theme);
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
            codeTemplatesForm.ShowDialog();
        }
    }
}