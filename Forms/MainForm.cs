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

using pie.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using pie.Enums;
using pie.Classes;

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
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace pie
{
    public partial class MainForm : KryptonForm
    {
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


        // [Constructor] Field initializations occur here 
        public MainForm()
        {
            InitializeComponent();
            InitializeGlobals();
            GetConfigurationDataFromFiles();
            SetDynamicDesign();
            ProcessCommandLineArguments();
        }

        private void InitializeGlobals()
        {
            Globals.tabInfos = new List<TabInfo>();
            Globals.gitCredentials = new GitCredentials();
            Globals.buildCommands = null;
            Globals.buildCommandToolStripMenuItems = new List<ToolStripMenuItem>();
            Globals.firstBrowserTab = true;
        }

        public Scintilla CreateNewTextArea()
        {
            Scintilla TextArea = new Scintilla();
            TextArea.KeyDown += keyDownEvents;
            TextArea.KeyPress += TextArea_KeyPress;
            TextArea.MouseDown += TextArea_MouseDown;
            TextArea.TextChanged += TextArea_TextChanged;
            TextArea.UpdateUI += TextArea_UpdateUI;
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;
            TextArea.ZoomChanged += TextArea_ZoomChanged;

            TextArea.UsePopup(false);

            ColorizeTextArea(TextArea);

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;

            TextArea.Dock = DockStyle.Fill;

            return TextArea;
        }

        private void TextArea_ZoomChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;

            const int padding = 2;

            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
        }

        private void ColorizeTextArea(Scintilla TextArea) 
        {
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ThemeService.GetColor("Fore");
            TextArea.CaretForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ThemeService.GetColor("Primary");
            TextArea.SetSelectionBackColor(true, ThemeService.GetColor("Selection"));
            TextArea.CaretLineBackColor = ThemeService.GetColor("CaretLineBack");
            TextArea.StyleClearAll();

            InitNumberMargin(TextArea);
            InitCodeFolding(TextArea);
        }

        private AutocompleteMenu InitializeAutocompleteMenu(Scintilla scintilla)
        {
            AutocompleteMenu autocompleteMenu = new AutocompleteMenu();

            ColorizeAutocompleteMenu(autocompleteMenu);

            autocompleteMenu.TargetControlWrapper = new ScintillaWrapper(scintilla);

            return autocompleteMenu;
        }

        private void ColorizeAutocompleteMenu(AutocompleteMenu autocompleteMenu)
        {
            Colors colors = new Colors();
            colors.BackColor = ThemeService.GetColor("Primary");
            colors.ForeColor = ThemeService.GetColor("Fore");
            colors.HighlightingColor = ThemeService.GetColor("Primary");
            colors.SelectedBackColor = ThemeService.GetColor("CaretLineBack");
            colors.SelectedBackColor2 = ThemeService.GetColor("CaretLineBack");
            colors.SelectedForeColor = ThemeService.GetColor("Fore");
            autocompleteMenu.Colors = colors;
            autocompleteMenu.LeftPadding = 0;
        }

        private void RemoveSuggestedActions()
        {
            if (((KryptonContextMenuHeading)codeContextMenu.Items[2]).Text == "Suggested Actions")
            {
                codeContextMenu.Items.RemoveAt(3);
                codeContextMenu.Items.RemoveAt(2);
            }
        }

        private void FillContextMenu(String extension)
        {
            // Remove old "Suggested Actions" item list
            RemoveSuggestedActions();

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

            if (e.Button == MouseButtons.Right) {
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

        private void InitNumberMargin(Scintilla TextArea)
        {

            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = ThemeService.GetColor("NumberMargin");
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = ThemeService.GetColor("Fore");
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = ThemeService.GetColor("NumberMargin");

            TextArea.Margins[0].Width = 24;
        }

        private void InitCodeFolding(Scintilla TextArea)
        {

            TextArea.SetFoldMarginColor(true, ThemeService.GetColor("Folding"));
            TextArea.SetFoldMarginHighlightColor(true, ThemeService.GetColor("Folding"));

            TextArea.Margins[3].Type = MarginType.Symbol;
            TextArea.Margins[3].Mask = Marker.MaskFolders;
            TextArea.Margins[3].Sensitive = true;
            TextArea.Margins[3].Width = 20;
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
                    for (int i = 0; i<tabControl.Pages.Count-1; i++)
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

                kryptonPage.Text = ParsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
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

            int index = 0;

            if (tabControl.Pages.Count > 1)
            {
                index = Globals.lastSelectedTabIndex + 1;
            }

            tabControl.Pages.Insert(index, kryptonPage);
            tabControl.SelectedPage = kryptonPage;

            TabInfo tabInfo = new TabInfo(openedFilePath, false, tabType, autocompleteMenu);
            Globals.tabInfos.Insert(index, tabInfo);
            
            if (tabType == TabType.GIT)
            {
                UpdateFormTitle("Git");
            }
            else if (openedFilePath != null)
            {
                UpdateFormTitle();
            }
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

            while(tabCount > 1)
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
                    Save();
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
                UpdateFormTitle();
            }

            return true;
        }

        public void CloseTabAfterWarning()
        {
            KryptonPage selectedKryptonPage = tabControl.SelectedPage;

            tabControl.Pages.Remove(selectedKryptonPage);

            if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() != TabType.CODE && Globals.tabInfos[tabControl.SelectedIndex].getTabType() != TabType.GIT)
            {
                selectedKryptonPage.Dispose();
            }

            Globals.tabInfos.RemoveAt(tabControl.SelectedIndex);


            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < Globals.tabInfos.Count && Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                DeactivateBuildAndRunOptions();
                ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            }

            if (tabControl.Pages.Count > 1)
            {
                tabControl.SelectedIndex = Globals.lastSelectedTabIndex - 1;

                if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
                {
                    tabControl.SelectedIndex--;
                }

                Globals.lastSelectedTabIndex = tabControl.SelectedIndex;
            }
            else
            {
                NewTab(TabType.CODE, null);
            }
        }

        // [Method] Closes the currently selected *terminal* tab
        public void CloseTerminalTab()
        {
            KryptonPage selectedKryptonPage = terminalTabControl.SelectedPage;
            ConEmuControl conEmuControl = (ConEmuControl)terminalTabControl.SelectedPage.Controls[0];

            bool nonBuildTabExists = false;

            if (selectedKryptonPage.Text != "Build")
            {
                // Check if there is at least one more tab page that isn't Build
                foreach(KryptonPage kryptonPage in terminalTabControl.Pages)
                {
                    if (kryptonPage != selectedKryptonPage && kryptonPage.Text != "Build")
                    {
                        nonBuildTabExists = true;
                    }
                }

                if (!nonBuildTabExists)
                {
                    NewTerminalTab("cmd.exe", true);
                }
            }

            terminalTabControl.Pages.Remove(selectedKryptonPage);

            conEmuControl.Dispose();

            if (terminalTabControl.Pages.Count == 0)
            {
                ShowTerminalTabControl();
            }
        }

        // [Method] Saves text stored in selected tab
        // Additional: If no file is open, lets the user decide the output path and replaces the position marked at selected tab index in the openFilePaths list (SaveAs)
        public void Save()
        {
            int openedTabIndex = tabControl.SelectedIndex;
            if (Globals.tabInfos[openedTabIndex].getOpenedFilePath() == null)
            {
                SaveAs();
            }
            else
            {
                string chosenPath = Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath();

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabControl.SelectedPage.Text = ParsingService.GetFileName(chosenPath);

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                ScintillaLexerService.SetLexer(extension, TextArea, tabControl, tabControl.SelectedIndex);
                UpdateFormTitle();
            }

            Globals.tabInfos[openedTabIndex].setOpenedFileChanges(false);
            DeactivateBuildAndRunOptions();

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null) {
                ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            }
        }

        // [Method] Saves text stored in selected tab at a user-specified location
        // Additional: If no tab is selected, outputs a MessageBox
        public void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Text documents (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                String chosenPath = saveFileDialog.FileName;

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                Globals.tabInfos[tabControl.SelectedIndex].setOpenedFilePath(chosenPath);
                tabControl.SelectedPage.Text = ParsingService.GetFileName(chosenPath);
                tabControl.SelectedPage.ToolTipTitle = chosenPath;

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                ScintillaLexerService.SetLexer(extension, TextArea, tabControl, tabControl.SelectedIndex);
                UpdateFormTitle();
            }
        }

        // [Method] Opens a file (without openFileDialog), given just a path
        public bool Open(string fileName)
        {
            if (tabControl.Pages.Count == 1)
            {
                NewTab(TabType.CODE, null);
            }

            int openedTabIndex = tabControl.SelectedIndex;

            string fileContent;

            try
            {
                fileContent = System.IO.File.ReadAllText(fileName);
            } catch(Exception ex)
            {
                ShowNotification("An error has been encountered while trying to open the file.");
                return false;
            }

            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            TextArea.Text = fileContent;

            Globals.tabInfos[openedTabIndex].setOpenedFilePath(fileName);
            tabControl.SelectedPage.Text = ParsingService.GetFileName(fileName);
            tabControl.SelectedPage.ToolTipTitle = fileName;

            string extension = ParsingService.GetFileExtension(fileName);

            ScintillaLexerService.SetLexer(extension, TextArea, tabControl, tabControl.SelectedIndex);

            DeactivateBuildAndRunOptions();
            ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            UpdateFormTitle();

            if (tabControl.Pages.Count >= 2)
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

        public void NewTerminalTab(string process,bool beginning)
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

            if (Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null) {
                conEmuStartInfo.StartupDirectory = ParsingService.GetFolderName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            }

            ConEmuSession conEmuSession = conEmuControl.Start(conEmuStartInfo);

            conEmuControl.BackColor = Color.White;
            conEmuControl.ForeColor = Color.Red;
            conEmuControl.Parent = kryptonPage;

            if (process == "cmd.exe")
            {
                kryptonPage.Text = "Command Prompt";
                kryptonPage.ImageSmall = Properties.Resources.terminal;
            }
            else if (process == "powershell.exe")
            {
                kryptonPage.Text = "PowerShell";
                kryptonPage.ImageSmall = Properties.Resources.powershell;
            }
            else
            {
                kryptonPage.Text = "Build";
                conEmuSession.ConsoleEmulatorClosed += ConEmuSession_ConsoleEmulatorClosed;
            }

            conEmuControl.Dock = DockStyle.Fill;

            terminalTabControl.SelectedPage = kryptonPage;
        }

        private void ConEmuSession_ConsoleEmulatorClosed(object sender, EventArgs e)
        {
            CloseTerminalTab();
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
                    string className = ParsingService.GetFileName(ParsingService.RemoveFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));

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
                    if (ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "html")
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

                    if (ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "md")
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
            string content = ParsingService.GetContentFromFile(path);

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
            if (status)
            {
                ResetFindPanelLocation();
                findTextBox.Focus();
                kryptonContextMenuItem6.Text = "Hide Find and Replace";
            }
            else
            {
                if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                    ClearHighlights(TextArea);
                }

                kryptonContextMenuItem6.Text = "Show Find and Replace";
            }

            findReplaceHeaderGroup.Visible = status;

            kryptonContextMenuItem6.Checked = status;
        }

        private void ProcessBuildCommands()
        {
            if (buildToolStripMenuItem1.DropDownItems.Count > 2)
            {
                int removeCount = buildToolStripMenuItem1.DropDownItems.Count - 2;

                while(removeCount > 0)
                {
                    buildToolStripMenuItem1.DropDownItems.RemoveAt(2);
                    removeCount--;
                }
            }

            try
            {
                Globals.buildCommands = BuildCommandService.GetBuildCommandsFromFile("config/build.json");

                foreach (BuildCommand buildCommand in Globals.buildCommands)
                {
                    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                    toolStripMenuItem.Text = buildCommand.getName();
                    toolStripMenuItem.Tag = buildCommand.getCommand();

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
        }

        private void GetConfigurationDataFromFiles()
        {
            ProcessBuildCommands();

            try
            {
                Globals.gitCredentials = GitService.ReadCredentialsFromFile("config/git.json");
            }
            catch (FileNotFoundException ex)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "config/git.json", "{}");
            }

            try
            {
                ThemeService.GetTheme("config/theme.json");
            }
            catch (FileNotFoundException ex)
            {
                Globals.theme = 0;
                ThemeService.WriteThemeToFile("config/theme.json", Globals.theme);
            }
        }

        private void SetDynamicDesign()
        {
            Globals.kryptonPalette = kryptonPalette;

            this.MinimumSize = new System.Drawing.Size(1036, 634);

            mainMenuStrip.Renderer = new ToolStripProfessionalRenderer(new CustomColorTable());

            ResetFindPanelLocation();
            ResetDirectoryPanelLocation();
            findReplaceHeaderGroup.Visible = false;
            ToggleDirectoryNavigator(false);

            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.FormatRow += GitStagingAreaListView_FormatRow;

            if (Globals.theme == 1)
            {
                themeSettingsToolStripMenuItem.Image = Properties.Resources.sun;
                themeSettingsToolStripMenuItem.Text = "Toggle Light Mode";
            }

            ThemeService.SetPaletteToTheme(kryptonPalette, Globals.theme);
            SynchronizeMainFormComponentsWithTheme();
            SynchronizeImagesWithTheme();
            
            this.Palette = kryptonPalette;
            tabControl.Palette = kryptonPalette;
            terminalTabControl.Palette = kryptonPalette;
            findReplaceHeaderGroup.Palette = kryptonPalette;
            directoryNavigationHeaderGroup.Palette = kryptonPalette;
            codeContextMenu.Palette = kryptonPalette;
            terminalContextMenu.Palette = kryptonPalette;
            renderContextMenu.Palette = kryptonPalette;
            gitContextMenu.Palette = kryptonPalette;
            kryptonLabel1.Palette = kryptonPalette;
            findTextBox.Palette = kryptonPalette;
            replaceTextBox.Palette = kryptonPalette;
            kryptonButton1.Palette = kryptonPalette;
            kryptonButton2.Palette = kryptonPalette;
            kryptonButton3.Palette = kryptonPalette;

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

        // [Event] Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void GitStagingAreaListView_FormatRow(object sender, FormatRowEventArgs e)
        {
            GitFile gitFile = (GitFile)e.Model;
            if (gitFile.Status == "Ignored")
                e.Item.ForeColor = Globals.theme == 1 ? Color.FromArgb(179, 179, 179) : Color.FromArgb(100, 100, 100);
            else if (gitFile.Status == "Deleted")
                e.Item.ForeColor = Color.FromArgb(251, 77, 77);
            else if (gitFile.Status == "New")
                e.Item.ForeColor = Globals.theme == 1 ? Color.FromArgb(60, 170, 232) : Color.FromArgb(40, 115, 158);
            else if (gitFile.Status == "Modified")
                e.Item.ForeColor = Globals.theme == 1 ? Color.FromArgb(255, 199, 87) : Color.FromArgb(224, 165, 45);
            else
                e.Item.ForeColor = ThemeService.GetColor("Fore");
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
            string scriptName = ParsingService.GetFileName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
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
            Save();
        }

        // [Event] "Save As" button pressed in upper menu
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
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
                else if (e.KeyCode == Keys.Enter)
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
                                NavigateToPath(ParsingService.GoBackInFilePath(directoryNavigationTextBox.Text));
                            }
                        }
                    }
                }
            }

            if (tabControl.SelectedIndex != tabControl.Pages.Count - 1)
            {
                if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
                {
                    NewTab(TabType.CODE, null);
                }
                else if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control)
                {
                    CloseTab();
                }
                else if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE) {
                    if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                    {
                        Save();
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
                }
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
                    NavigateToPath(ParsingService.GetFolderName(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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
            if (tabControl.SelectedIndex != tabControl.Pages.Count-1)
            {
                Globals.lastSelectedTabIndex = tabControl.SelectedIndex;
            }

            if (tabControl.SelectedIndex != tabControl.Pages.Count-1 && (tabControl.Pages.Count == Globals.tabInfos.Count+1))
            {
                if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    tabControl.KryptonContextMenu = codeContextMenu;

                    DeactivateBuildAndRunOptions();
                    if (tabControl.SelectedIndex != -1 && Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                    {
                        ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                        UpdateFormTitle();
                    }
                    else
                    {
                        UpdateFormTitle("Untitled");
                    }
                }
                else
                {
                    if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.GIT)
                    {
                        tabControl.KryptonContextMenu = gitContextMenu;
                    }
                    else
                    {
                        tabControl.KryptonContextMenu = renderContextMenu;
                    }

                    ToggleTerminalTabControl(false);
                    ToggleFindReplacePanel(false);
                    ToggleDirectoryNavigator(false);

                    if (Globals.tabInfos[tabControl.SelectedIndex].getTabType() == TabType.GIT)
                    {
                        UpdateFormTitle("Git");
                    }
                    else
                    {
                        UpdateFormTitle();
                    }
                }
            }
            else
            {
                DeactivateBuildAndRunOptions();
                UpdateFormTitle("Untitled");
            }
        }

        private void UpdateFormTitle()
        {
            this.Text = Globals.tabInfos[tabControl.SelectedIndex].getOpenedFilePath() + " - pie";
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
                ShowTerminalTabControl();
            }
            else
            {
                kryptonContextMenuItem18.Enabled = true;
            }
        }

        // [Event] Closes the currently opened terminal (accessed via Terminal tab control
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
                    int index = Globals.lastSelectedTabIndex;
                    tabControl.SelectedIndex = index;
                }
            }
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
            {
                NewTab(TabType.CODE, null);
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
            scintilla.Indicators[8].ForeColor = ThemeService.GetColor("Selection");
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

        private void themeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlHelper.SuspendDrawing(this);

            if (Globals.theme == 0)
            {
                Globals.theme = 1;
                ThemeService.WriteThemeToFile("config/theme.json", Globals.theme);
                themeSettingsToolStripMenuItem.Image = Properties.Resources.sun;
                themeSettingsToolStripMenuItem.Text = "Toggle Light Mode";
            }
            else
            {
                Globals.theme = 0;
                ThemeService.WriteThemeToFile("config/theme.json", Globals.theme);
                themeSettingsToolStripMenuItem.Image = Properties.Resources.crescent_moon;
                themeSettingsToolStripMenuItem.Text = "Toggle Dark Mode";
            }

            for (int i = 0; i<tabControl.Pages.Count-1; i++)
            {
                if (Globals.tabInfos[i].getTabType() == TabType.CODE)
                {
                    KryptonPage kryptonPage = tabControl.Pages[i];
                    Scintilla scintilla = (Scintilla)kryptonPage.Controls[0];

                    ScintillaLexerService.InitializeParserDictionary();

                    if (Globals.tabInfos[i].getOpenedFilePath() != null)
                    {
                        string extension = ParsingService.GetFileExtension(Globals.tabInfos[i].getOpenedFilePath());
                        ColorizeTextArea(scintilla);
                        ColorizeAutocompleteMenu(Globals.tabInfos[i].getAutocompleteMenu());
                        ScintillaLexerService.SetLexer(extension, scintilla, tabControl, i);
                        UpdateNumberMarginWidth(scintilla, true);
                    }
                    else
                    {
                        ColorizeTextArea(scintilla);
                    }
                }
            }

            ThemeService.SetPaletteToTheme(kryptonPalette, Globals.theme);
            SynchronizeMainFormComponentsWithTheme();
            SynchronizeImagesWithTheme();

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

        private void showGitTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

                        if (files[files.Count-1] == "")
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
                            GitService.WriteCredentials(Globals.gitCredentials);
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
                            GitService.WriteCredentials(Globals.gitCredentials);
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
                else {
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
            GitCommitCredentialsForm gitCredentialsForm = new GitCommitCredentialsForm();
            Globals.gitFormClosedWithOk = false;
            gitCredentialsForm.ShowDialog();

            if (Globals.gitFormClosedWithOk)
            {
                GitService.WriteCredentials(Globals.gitCredentials);
            }
        }

        private void managePushCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GitPushCredentialsForm gitCredentialsForm = new GitPushCredentialsForm();
            Globals.gitFormClosedWithOk = false;
            gitCredentialsForm.ShowDialog();

            if (Globals.gitFormClosedWithOk)
            {
                GitService.WriteCredentials(Globals.gitCredentials);
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
                        GitService.WriteCredentials(Globals.gitCredentials);
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
                            GitService.WriteCredentials(Globals.gitCredentials);
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
                            Task.Run(() => {
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
            mainMenuStrip.BackColor = ThemeService.GetColor("Primary");
            mainMenuStrip.ForeColor = ThemeService.GetColor("Fore");

            foreach (ToolStripMenuItem toolStripMenuItem in mainMenuStrip.Items)
            {
                toolStripMenuItem.DropDown.BackColor = ThemeService.GetColor("Primary");
                toolStripMenuItem.DropDown.ForeColor = ThemeService.GetColor("Fore");
                toolStripMenuItem.ImageTransparentColor = ThemeService.GetColor("Primary");

                if (toolStripMenuItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem toolStripMenuItemChild in toolStripMenuItem.DropDownItems)
                    {
                        toolStripMenuItemChild.BackColor = ThemeService.GetColor("Primary");
                        toolStripMenuItemChild.ForeColor = ThemeService.GetColor("Fore");

                        if (toolStripMenuItemChild.HasDropDownItems)
                        {
                            foreach (ToolStripMenuItem toolStripMenuItemChild2 in toolStripMenuItemChild.DropDownItems)
                            {
                                toolStripMenuItemChild2.BackColor = ThemeService.GetColor("Primary");
                                toolStripMenuItemChild2.ForeColor = ThemeService.GetColor("Fore");
                            }
                        }
                    }
                }
            }

            // HeaderGroup
            findReplaceHeaderGroup.StateCommon.Border.Color1 = ThemeService.GetColor("FormBorder");
            findReplaceHeaderGroup.StateCommon.Border.Color2 = ThemeService.GetColor("FormBorder");
            directoryNavigationHeaderGroup.StateCommon.Border.Color1 = ThemeService.GetColor("FormBorder");
            directoryNavigationHeaderGroup.StateCommon.Border.Color2 = ThemeService.GetColor("FormBorder");

            // TabControl
            tabControl.StateCommon.Panel.Color1 = ThemeService.GetColor("Primary");
            tabControl.StateCommon.Panel.Color2 = ThemeService.GetColor("Primary");

            // ObjectListView
            gitStagingAreaListView.UseCustomSelectionColors = true;
            gitStagingAreaListView.FullRowSelect = true;
            gitStagingAreaListView.ShowGroups = false;

            gitStagingAreaListView.HighlightBackgroundColor = ThemeService.GetColor("Secondary");
            gitStagingAreaListView.HighlightForegroundColor = ThemeService.GetColor("Fore");
            gitStagingAreaListView.UnfocusedHighlightBackgroundColor = ThemeService.GetColor("Secondary");
            gitStagingAreaListView.UnfocusedHighlightForegroundColor = ThemeService.GetColor("Fore");
            gitStagingAreaListView.BackColor = ThemeService.GetColor("Primary");
            gitStagingAreaListView.ForeColor = ThemeService.GetColor("Fore");

            directoryNavigationObjectListView.ShowGroups = false;
            directoryNavigationObjectListView.UseCustomSelectionColors = true;
            directoryNavigationObjectListView.FullRowSelect = true;
            directoryNavigationObjectListView.MultiSelect = false;
            directoryNavigationObjectListView.HeaderStyle = ColumnHeaderStyle.None;
            directoryNavigationObjectListView.SmallImageList = new ImageList();
            directoryNavigationObjectListView.SmallImageList.Images.Add("directory", Properties.Resources.folder);
            olvColumn3.FillsFreeSpace = true;
            olvColumn3.ImageGetter = new ImageGetterDelegate(NavigationImageGetter);

            directoryNavigationObjectListView.BackColor = ThemeService.GetColor("Primary");
            directoryNavigationObjectListView.ForeColor = ThemeService.GetColor("Fore");
            directoryNavigationObjectListView.HighlightBackgroundColor = ThemeService.GetColor("Secondary");
            directoryNavigationObjectListView.HighlightForegroundColor = ThemeService.GetColor("Fore");
            directoryNavigationObjectListView.UnfocusedHighlightBackgroundColor = ThemeService.GetColor("Secondary");
            directoryNavigationObjectListView.UnfocusedHighlightForegroundColor = ThemeService.GetColor("Fore");

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = ThemeService.GetColor("Secondary");
            headerstyle.Normal.ForeColor = ThemeService.GetColor("Fore");

            headerstyle.Hot.BackColor = ThemeService.GetColor("ButtonHover");
            headerstyle.Hot.ForeColor = ThemeService.GetColor("Fore");

            headerstyle.Pressed.BackColor = ThemeService.GetColor("ButtonFrame");
            headerstyle.Pressed.ForeColor = ThemeService.GetColor("Fore");

            gitStagingAreaListView.HeaderFormatStyle = headerstyle;

            regexCheckBox.StateCommon.ShortText.Color1 = ThemeService.GetColor("Fore");
            regexCheckBox.StateCommon.ShortText.Color2 = ThemeService.GetColor("Fore");

            matchCaseCheckBox.StateCommon.ShortText.Color1 = ThemeService.GetColor("Fore");
            matchCaseCheckBox.StateCommon.ShortText.Color2 = ThemeService.GetColor("Fore");

            matchWholeWordCheckBox.StateCommon.ShortText.Color1 = ThemeService.GetColor("Fore");
            matchWholeWordCheckBox.StateCommon.ShortText.Color2 = ThemeService.GetColor("Fore");

            // ComboBox
            gitBranchesComboBox.StateCommon.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateCommon.DropBack.Color1 = ThemeService.GetColor("Primary");
            gitBranchesComboBox.StateCommon.DropBack.Color2 = ThemeService.GetColor("Primary");

            gitBranchesComboBox.StateTracking.Item.Back.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Border.ColorStyle = PaletteColorStyle.Solid;
            gitBranchesComboBox.StateTracking.Item.Back.Color1 = ThemeService.GetColor("Secondary");
            gitBranchesComboBox.StateTracking.Item.Back.Color2 = ThemeService.GetColor("Secondary");
        }

        private void SynchronizeImagesWithTheme()
        {
            if (Globals.theme == 1)
            {
                kryptonPage1.ImageSmall = Properties.Resources.plus_white;
            }
            else
            {
                kryptonPage1.ImageSmall = Properties.Resources.plus_blue;
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
            } catch(Exception ex)
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
                        directoryNavigationHeaderGroup.Visible = false;
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
                        NavigateToPath(ParsingService.GoBackInFilePath(directoryNavigationTextBox.Text));
                    }
                }
            }
        }

        private void kryptonContextMenuItem12_Click(object sender, EventArgs e)
        {
            ShowDirectoryNavigator();
        }
    }
}
