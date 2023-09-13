using ComponentFactory.Krypton.Navigator;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScintillaNET;
using pie.Classes;
using ComponentFactory.Krypton.Toolkit;
using Markdig;
using pie.Enums;
using CefSharp.WinForms;
using CefSharp;

namespace pie
{
    public partial class MainForm : Form
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


        // [Field] Paths of the opened files
        public List<TabInfo> tabInfos;
        public bool darkMode;
        public bool terminalTabControlOpened;

        // [Constructor] Field initializations occur here 
        public MainForm()
        {
            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(1036, 634);

            tabInfos = new List<TabInfo>();
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
            TextArea.SetSelectionBackColor(true, ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Selection"]));
            TextArea.CaretLineBackColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["CaretLine"]);
            TextArea.UsePopup(false);

            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Fore"]);
            TextArea.CaretForeColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Fore"]);
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Background"]);
            TextArea.StyleClearAll();

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;

            InitNumberMargin(TextArea);
            //InitBookmarkMargin(TextArea);
            InitCodeFolding(TextArea);

            TextArea.Dock = DockStyle.Fill;

            return TextArea;
        }

        private Scintilla CreateNewRenderTab()
        {
            Scintilla TextArea = new Scintilla();
            TextArea.KeyDown += keyDownEvents;
            TextArea.KeyPress += TextArea_KeyPress;
            TextArea.MouseDown += TextArea_MouseDown;
            TextArea.TextChanged += TextArea_TextChanged;
            TextArea.UpdateUI += TextArea_UpdateUI;
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;
            TextArea.SetSelectionBackColor(true, ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Selection"]));
            TextArea.CaretLineBackColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["CaretLine"]);
            TextArea.UsePopup(false);

            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Fore"]);
            TextArea.CaretForeColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Fore"]);
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Background"]);
            TextArea.StyleClearAll();

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;

            InitNumberMargin(TextArea);
            InitCodeFolding(TextArea);

            TextArea.Dock = DockStyle.Fill;

            return TextArea;
        }

        private void RemoveSuggestedActions()
        {
            if (((KryptonContextMenuHeading)kryptonContextMenu1.Items[2]).Text == "Suggested Actions")
            {
                kryptonContextMenu1.Items.RemoveAt(3);
                kryptonContextMenu1.Items.RemoveAt(2);
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
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Build C/C++ Source";
                item1.Click += Item1_Click;
                kryptonContextMenuItems.Items.Add(item1);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "java")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Build Java Source";
                item1.Click += Item1_Click1;
                kryptonContextMenuItems.Items.Add(item1);

                KryptonContextMenuItem item2 = new KryptonContextMenuItem();
                item2.Text = "Run as Java Class";
                item2.Click += Item2_Click;
                kryptonContextMenuItems.Items.Add(item2);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "py")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Run Python Script";
                item1.Click += Item1_Click2;
                kryptonContextMenuItems.Items.Add(item1);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "pl")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Run Perl Script";
                item1.Click += Item1_Click3; ;
                kryptonContextMenuItems.Items.Add(item1);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "html")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Render HTML";
                item1.Click += Item1_Click4;
                kryptonContextMenuItems.Items.Add(item1);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
            }
            else if (extension == "md")
            {
                KryptonContextMenuHeading kryptonContextMenuHeading = new KryptonContextMenuHeading();
                kryptonContextMenuHeading.Text = "Suggested Actions";
                kryptonContextMenu1.Items.Insert(2, kryptonContextMenuHeading);

                KryptonContextMenuItems kryptonContextMenuItems = new KryptonContextMenuItems();
                KryptonContextMenuItem item1 = new KryptonContextMenuItem();
                item1.Text = "Render Markdown";
                item1.Click += Item1_Click5;
                kryptonContextMenuItems.Items.Add(item1);
                kryptonContextMenu1.Items.Insert(3, kryptonContextMenuItems);
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
                Console.WriteLine(Globals.lastSelectedIndex);
            }
            else
            {
                Globals.canUpdateUI = true;
            }
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == Globals.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            Globals.maxLineNumberCharLength = maxLineNumberCharLength;

            tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);
        }

        private void TextArea_MouseDown(object sender, MouseEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            if (e.Button == MouseButtons.Right) {
                kryptonContextMenu1.Show(sender);
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

            tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(true);
        }

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin(Scintilla TextArea)
        {

            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = ParsingService.IntToColor(0xbbcee6);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = ParsingService.IntToColor(0x000000);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = ParsingService.IntToColor(0x000000);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = ParsingService.IntToColor(0xbbcee6);

            TextArea.Margins[0].Width = 24;
            TextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin(Scintilla TextArea)
        {

            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = TextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);
            //margin.Cursor = MarginCursor.Arrow;

            var marker = TextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(ParsingService.IntToColor(0xFF003B));
            marker.SetForeColor(ParsingService.IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding(Scintilla TextArea)
        {

            TextArea.SetFoldMarginColor(true, ParsingService.IntToColor(0xd1dded));
            TextArea.SetFoldMarginHighlightColor(true, ParsingService.IntToColor(0xd1dded));

            // Enable code folding
            TextArea.SetProperty("fold", "1");
            TextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            TextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                TextArea.Markers[i].SetForeColor(ParsingService.IntToColor(0xd1dded)); // styles for [+] and [-]
                TextArea.Markers[i].SetBackColor(ParsingService.IntToColor(0x000000)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            Scintilla TextArea = (Scintilla)sender;

            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion


        // [Method] Creates a new tab and selects the new tab
        public void NewTab(TabType tabType, String path)
        {
            KryptonPage kryptonPage = new KryptonPage();
            kryptonPage.Text = "Untitled";
            kryptonPage.ToolTipTitle = kryptonPage.Text;

            string openedFilePath = null;

            if (tabType == TabType.CODE)
            {
                Scintilla TextArea = CreateNewTextArea();
                kryptonPage.Controls.Add(TextArea);
            }
            else
            {
                if (Globals.firstBrowserTab)
                {
                    Cef.Initialize(new CefSettings());
                    Globals.firstBrowserTab = false;
                }

                tabControl.KryptonContextMenu = kryptonContextMenu3;
                ToggleTerminalTabControl(false);
                ToggleFindReplacePanel(false);

                kryptonPage.Text = tabInfos[tabControl.SelectedIndex].getOpenedFilePath();
                kryptonPage.ToolTipTitle = kryptonPage.Text;

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
                IContextMenuHandler contextMenuHandler = new PieContextMenuHandler(this, kryptonContextMenu3);
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

            TabInfo tabInfo = new TabInfo(openedFilePath, false, tabType);
            tabInfos.Insert(index, tabInfo);
            
            if (openedFilePath != null)
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
                    kryptonContextMenu3.Close();
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
            if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_HTML || tabInfos[tabControl.SelectedIndex].getTabType() == TabType.RENDER_MD)
            {
                ChromiumWebBrowser chromiumWebBrowser = (ChromiumWebBrowser)tabControl.SelectedPage.Controls[0];
                CloseTabAfterWarning();
            }
            else if (tabInfos[tabControl.SelectedIndex].getOpenedFileChanges())
            {
                DialogResult dialogResult = MessageBox.Show("Save file before closing it?", "Save file", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    Save();
                    CloseTabAfterWarning();
                }
                else if (dialogResult == DialogResult.No)
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
                UpdateFormTitle();
            }

            return true;
        }

        public void CloseTabAfterWarning()
        {
            KryptonPage selectedKryptonPage = tabControl.SelectedPage;

            tabInfos.RemoveAt(tabControl.SelectedIndex);

            tabControl.Pages.Remove(selectedKryptonPage);
            selectedKryptonPage.Dispose();

            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < tabInfos.Count && tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
            {
                DeactivateBuildAndRunOptions();
                ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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
            KryptonPage selectedKryptonPage = buildTabControl.SelectedPage;

            ConsoleControl.ConsoleControl consoleControl = (ConsoleControl.ConsoleControl)selectedKryptonPage.Controls["ConsoleControl"];
            consoleControl.StopProcess();

            buildTabControl.Pages.Remove(selectedKryptonPage);

            if (buildTabControl.Pages.Count == 0)
            {
                ShowTerminalTabControl();
            }
        }

        // [Method] Saves text stored in selected tab
        // Additional: If no file is open, lets the user decide the output path and replaces the position marked at selected tab index in the openFilePaths list (SaveAs)
        public void Save()
        {
            int openedTabIndex = tabControl.SelectedIndex;
            if (tabInfos[openedTabIndex].getOpenedFilePath() == null)
            {
                SaveAs();
            }
            else
            {
                string chosenPath = tabInfos[tabControl.SelectedIndex].getOpenedFilePath();

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabControl.SelectedPage.Text = chosenPath;

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                ScintillaLexerService.SetLexer(extension, TextArea);
                UpdateFormTitle();
            }

            tabInfos[openedTabIndex].setOpenedFileChanges(false);
            DeactivateBuildAndRunOptions();

            if (tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null) {
                ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
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

                tabInfos[tabControl.SelectedIndex].setOpenedFilePath(chosenPath);
                tabControl.SelectedPage.Text = chosenPath;
                tabControl.SelectedPage.ToolTipTitle = tabControl.SelectedPage.Text;

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                ScintillaLexerService.SetLexer(extension, TextArea);
                UpdateFormTitle();
            }
        }

        // [Method] Opens a file (without openFileDialog), given just a path
        public void Open(string fileName)
        {
            if (tabControl.Pages.Count == 1)
            {
                NewTab(TabType.CODE, null);
            }

            int openedTabIndex = tabControl.SelectedIndex;

            string fileContent = System.IO.File.ReadAllText(fileName);

            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            TextArea.Text = fileContent;

            tabInfos[openedTabIndex].setOpenedFilePath(fileName);
            tabControl.SelectedPage.Text = fileName;
            tabControl.SelectedPage.ToolTipTitle = tabControl.SelectedPage.Text;

            string extension = ParsingService.GetFileExtension(fileName);

            ScintillaLexerService.SetLexer(extension, TextArea);

            DeactivateBuildAndRunOptions();
            ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
            UpdateFormTitle();

            if (tabControl.Pages.Count >= 2)
            {
                tabInfos[tabControl.SelectedIndex].setOpenedFileChanges(false);
            }
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

        public void NewTerminalTab(string process)
        {
            KryptonPage kryptonPage = new KryptonPage();

            buildTabControl.Pages.Add(kryptonPage);
            
            ConsoleControl.ConsoleControl consoleControl = new ConsoleControl.ConsoleControl();
            consoleControl.Name = "ConsoleControl";

            consoleControl.Parent = kryptonPage;

            if (process == "cmd.exe")
            {
                kryptonPage.Text = "Command Prompt";
                kryptonPage.ImageSmall = Properties.Resources.terminal;
            }
            else if (process == "powershell.exe")
            {
                kryptonPage.Text = "Powershell";
                kryptonPage.ImageSmall = Properties.Resources.powershell;
            }

            consoleControl.Dock = DockStyle.Fill;

            consoleControl.StartProcess(process, "");

            buildTabControl.SelectedPage = kryptonPage;
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

            workingDirectoryToolStripMenuItem.Enabled = true;
            stagingAreaToolStripMenuItem.Enabled = true;
            remoteToolStripMenuItem.Enabled = true;

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

            workingDirectoryToolStripMenuItem.Enabled = false;
            stagingAreaToolStripMenuItem.Enabled = false;
            remoteToolStripMenuItem.Enabled = false;
        }

        // [Method] Opens terminal tab control
        public void ShowTerminalTabControl()
        {
            if (buildTabControl.Pages.Count == 0)
            {
                NewTerminalTab("cmd.exe");
            }

            if (terminalTabControlOpened == false)
            {
                ToggleTerminalTabControl(true);
            }
            else
            {
                ToggleTerminalTabControl(false);
            }
        }

        public void ToggleTerminalTabControl(bool status)
        {
            if (status)
            {
                buildTabControl.Show();
            }
            else
            {
                buildTabControl.Hide();
            }
            terminalTabControlOpened = status;
            kryptonContextMenuItem15.Checked = status;
            showBuildToolsToolStripMenuItem.Checked = status;
        }

        // [Method] Used for Git commands
        public void ExecuteGitCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/K " + "(" + command + ")&(pause)&(exit)";

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                Process.Start(startInfo);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please open a file before launching any Git command.", "Git Error");
            }
        }

        // [Method] Used for Build commands
        public void ExecuteBuildCommand(string type)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";

            if (type == "Java Source (.java)")
            {
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(javac " + tabInfos[tabControl.SelectedIndex].getOpenedFilePath() + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
            }
            else if (type == "C/C++ Source (.c, .cpp)")
            {
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(gcc -Wall " + tabInfos[tabControl.SelectedIndex].getOpenedFilePath() + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
            }

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                Process.Start(startInfo);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please open a file before launching any build command.", "Build Error");
            }
        }

        // [Method] Used for Run commands
        public void ExecuteRunCommand(string type)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";

            if (type == "Java Class (.class)")
            {
                string className = ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                className = ParsingService.RemoveFileExtension(className);

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(java " + className + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }
            else if (type == "Python Script (.py)")
            {
                string scriptName = ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(py " + scriptName + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }
            else if (type == "Perl Script (.pl)")
            {
                string scriptName = ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(perl " + scriptName + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }
            else if (type == "Render HTML (.html)")
            {
                if (ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "html")
                {
                    NewTab(TabType.RENDER_HTML, tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                }
                else
                {
                    MessageBox.Show("pie can only render files with the .html extension.", "pie");
                }

                return;
            }
            else if (type == "Render Markdown (.md)")
            {
                int index = tabControl.SelectedIndex;

                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

                if (ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()) == "md")
                {
                    NewTab(TabType.RENDER_MD, tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                }
                else
                {
                    MessageBox.Show("pie can only render files with the .md extension.", "pie");
                }

                return;
            }

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                Process.Start(startInfo);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please open a file before launching any run command.", "Build Error");
            }
        }

        private string ConvertMarkdownToHtml(string path)
        {
            string content = ParsingService.GetContentFromFile(path);

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string result = Markdown.ToHtml(content, pipeline);

            return result;
        }

        private void ShowFindReplacePanel()
        {
            if (Globals.findReplacePanelToggled)
            {
                ToggleFindReplacePanel(false);
            }
            else
            {
                ToggleFindReplacePanel(true);
            }
        }

        private void ToggleFindReplacePanel(bool status)
        {
            if (status)
            {
                ResetFindPanelLocation();
                findPanel.Show();
                findTextBox.Focus();
            }
            else
            {
                findPanel.Hide();

                if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                    ClearHighlights(TextArea);
                }
            }

            kryptonContextMenuItem6.Checked = status;
            Globals.findReplacePanelToggled = status;
        }

        // [Event] Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load Build Commands from configuration file
            List<BuildCommand> buildCommands = null;

            try
            {
                buildCommands = BuildCommandService.GetBuildCommandsFromFile("build-commands.config");
            } catch(FileNotFoundException ex)
            {
                MessageBox.Show("Cannot access Build Commands config file. Check if 'build-commands.config' exists in your main pie directory. If not, try reinstalling the app.", "pie");
                Application.Exit();
                return;
            }

            List<ToolStripMenuItem> buildCommandToolStripMenuItems = new List<ToolStripMenuItem>();

            foreach (BuildCommand buildCommand in buildCommands)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Text = buildCommand.getName();
                toolStripMenuItem.Tag = buildCommand.getCommand();

                toolStripMenuItem.Click += ToolStripMenuItem_Click;

                buildToolStripMenuItem1.DropDownItems.Add(toolStripMenuItem);
                buildCommandToolStripMenuItems.Add(toolStripMenuItem);
            }

            Globals.buildCommands = buildCommands;
            Globals.buildCommandToolStripMenuItems = buildCommandToolStripMenuItems;
            Globals.firstBrowserTab = true;

            ScintillaLexerService.InitializeDefaultColorDictionary();

            try {
                ScintillaLexerService.InitializeConfigColorDictionary("theme-settings.config");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Cannot access Theme Settings config file. Check if 'theme-settings.config' exists in your main pie directory. If not, try reinstalling the app.", "pie");
                Application.Exit();
                return;
            }

            ResetFindPanelLocation();
            findPanel.Hide();
            Globals.findReplacePanelToggled = false;

            // Do other visual processing
            buildTabControl.Hide();

            tabControl.AllowPageDrag = false;
            tabControl.AllowPageReorder = false;

            findTextBox.KeyDown += FindTextBox_KeyDown;
            replaceTextBox.KeyDown += ReplaceTextBox_KeyDown;

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
                    MessageBox.Show("pie could not open " + path + ". Are you sure the file exists?", "pie");
                    Application.Exit();
                    return;
                }
            }
            else
            {
                NewTab(TabType.CODE, null);
            }
        }

        private void ReplaceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];

                bool status = Replace(TextArea, findTextBox.Text, replaceTextBox.Text);

                if (!status)
                {
                    MessageBox.Show("No occurences found.", "pie");
                }
            }
        }

        private void FindTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                HighlightWord(findTextBox.Text, TextArea);
                Find(TextArea, Globals.lastSelectedIndex);
            }
        }

        private void ResetFindPanelLocation()
        {
            findPanel.Location = new Point((this.Width - findPanel.Width) / 2, (this.Height - findPanel.Height) / 4);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            string scriptName = ParsingService.GetFileName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
            string command = (string)((ToolStripMenuItem)sender).Tag;
            command = command.Replace("$FILE", scriptName);
            startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(" + command + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
           
            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());
                Process.Start(startInfo);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please open a file before launching any run command.", "Build Error");
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
                else if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE) {
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
                }
            }
        }

        // [Event] Triggered when clicking "Show Terminal Tab" from the context menu
        private void kryptonContextMenuItem15_Click(object sender, EventArgs e)
        {
            ShowTerminalTabControl();
        }

        // [Event] Triggered when Form is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose terminals from opened terminal tabs
            foreach (KryptonPage kryptonPage in buildTabControl.Pages)
            {
                ConsoleControl.ConsoleControl consoleControl = (ConsoleControl.ConsoleControl)kryptonPage.Controls["ConsoleControl"];
                consoleControl.StopProcess();
            }

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
                MessageBox.Show("Please open a file before launching any build commands.", "Build Error");
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
                MessageBox.Show("Please open a file before launching any run commands.", "Run Error");
            }
        }

        // [Event] Triggered when a tab is changed
        private void tabControl_SelectedPageChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex != tabControl.Pages.Count-1)
            {
                Globals.lastSelectedTabIndex = tabControl.SelectedIndex;
            }

            if (tabControl.SelectedIndex != tabControl.Pages.Count-1 && (tabControl.Pages.Count == tabInfos.Count+1))
            {
                if (tabInfos[tabControl.SelectedIndex].getTabType() == TabType.CODE)
                {
                    tabControl.KryptonContextMenu = kryptonContextMenu1;

                    DeactivateBuildAndRunOptions();
                    if (tabControl.SelectedIndex != -1 && tabInfos[tabControl.SelectedIndex].getOpenedFilePath() != null)
                    {
                        ActivateSpecificBuildAndRunOptions(ParsingService.GetFileExtension(tabInfos[tabControl.SelectedIndex].getOpenedFilePath()));
                        UpdateFormTitle();
                    }
                    else
                    {
                        UpdateFormTitle("Untitled");
                    }
                }
                else
                {
                    tabControl.KryptonContextMenu = kryptonContextMenu3;
                    ToggleTerminalTabControl(false);
                    ToggleFindReplacePanel(false);

                    UpdateFormTitle();

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
            this.Text = "pie [" + tabInfos[tabControl.SelectedIndex].getOpenedFilePath() + "]";
        }

        private void UpdateFormTitle(String customTitle)
        {
            this.Text = "pie [" + customTitle + "]";
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void createARepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("git init");
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void checkRepositoryStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("git status");
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void deleteRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = ParsingService.GetFolderName(tabInfos[tabControl.SelectedIndex].getOpenedFilePath());

                var dir = new DirectoryInfo(path + ".git");
                if (dir.Exists)
                {
                    dir.Delete(true);
                    MessageBox.Show("Local repository was successfully deleted.", "Git Info");
                }
                else
                {
                    MessageBox.Show("Repository doesn't exist.", "Git Error");
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Please open a file before launching any git commands.", "Git Error");
            }
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void commitYourChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.commitMessage = null;

            GitCommitMessageForm commitMessageForm = new GitCommitMessageForm();
            commitMessageForm.ShowDialog();

            if (Globals.commitMessage != null)
            {
                ExecuteGitCommand("git commit -m \"" + Globals.commitMessage + "\"");
            }
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void addChangesToStagingAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("git add .");
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void pushToRemoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("git push");
        }

        // [Event] Triggered when user presses one of the buttons in the "Git" tab
        private void pullFromRemoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteGitCommand("git pull");
        }

        private void buildTabControl_TabCountChanged(object sender, EventArgs e)
        {
            if (buildTabControl.Pages.Count == 0)
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
            NewTerminalTab("cmd.exe");
        }

        // [Event] Opens a new PowerShell terminal (accessed via Terminal tab control)
        private void kryptonContextMenuItem17_Click(object sender, EventArgs e)
        {
            NewTerminalTab("powershell.exe");
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
                this.Close();
            }
        }

        private void themeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeSettingsForm themeSettingsForm = new ThemeSettingsForm();
            themeSettingsForm.ShowDialog();

            if (Globals.closeAfterApplyingChanges)
            {
                this.Close();
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResetFindPanelLocation();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            HighlightWord(findTextBox.Text, TextArea);
            Find(TextArea, Globals.lastSelectedIndex);
        }

        private bool Find(Scintilla scintilla, int indexStart)
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

            int index = -1;

            if ((index = scintilla.SearchInTarget(text)) != -1)
            {
                scintilla.SetSelection(index, index + findTextBox.Text.Length);
                scintilla.SetSelectionBackColor(false, ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Selection"]));
                Globals.lastSelectedIndex = index + findTextBox.Text.Length + 1;
            }
            else
            {
                if (indexStart == 0)
                {
                    return false;
                }

                Globals.lastSelectedIndex = 0;
                return Find(scintilla, 0);
            }

            return true;
        }

        private void HighlightWord(string text, Scintilla scintilla)
        {
            if (string.IsNullOrEmpty(text))
                return;

            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = 8;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);

            // Update indicator appearance
            scintilla.Indicators[8].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[8].Under = true;
            scintilla.Indicators[8].ForeColor = ScintillaLexerService.ConvertHexToColor(Globals.configColorDictionary["Selection"]);
            scintilla.Indicators[8].OutlineAlpha = 255;
            scintilla.Indicators[8].Alpha = 100;

            // Search the document
            scintilla.TargetStart = 0;
            scintilla.TargetEnd = scintilla.TextLength;
            scintilla.SearchFlags = SearchFlags.None;
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
                findPanel.Location = new Point(
                    (findPanel.Location.X - Globals.lastLocation.X) + e.X, (findPanel.Location.Y - Globals.lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void findPanel_MouseUp(object sender, MouseEventArgs e)
        {
            Globals.mouseDown = false;
        }

        private bool Replace(Scintilla scintilla, String from, String to)
        {
            HighlightWord(from, scintilla);
            bool found = Find(scintilla, Globals.lastSelectedIndex);

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

            if (!status)
            {
                MessageBox.Show("No occurences found.", "pie");
            }
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
    }
}
