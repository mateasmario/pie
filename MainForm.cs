using ComponentFactory.Krypton.Navigator;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScintillaNET;
using ComponentFactory.Krypton.Docking;
using pie.Classes;

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
            NewTab();

            if (args.Length == 2)
            {
                Open(args[1].ToString());
            }
        }


        // [Field] Paths of the opened files
        public List<String> openedFilePaths;
        public List<bool> openedFileChanges;
        public bool darkMode;
        public bool terminalTabControlOpened;

        // [Constructor] Field initializations occur here 
        public MainForm()
        {
            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(1036, 634);

            openedFilePaths = new List<String>();
            openedFileChanges = new List<bool>();
        }

        public Scintilla CreateNewTextArea()
        {
            Scintilla TextArea = new Scintilla();
            TextArea.KeyDown += keyDownEvents;
            TextArea.KeyPress += TextArea_KeyPress;
            TextArea.MouseDown += TextArea_MouseDown;
            TextArea.TextChanged += TextArea_TextChanged;
            TextArea.WrapMode = WrapMode.None;
            TextArea.IndentationGuides = IndentView.LookBoth;
            TextArea.SetSelectionBackColor(true, ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Selection"]));
            TextArea.CaretLineBackColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["CaretLine"]);
            TextArea.UsePopup(false);

            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Fore"]);
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Background"]);
            TextArea.StyleClearAll();

            TextArea.BorderStyle = ScintillaNET.BorderStyle.None;

            InitNumberMargin(TextArea);
            //InitBookmarkMargin(TextArea);
            InitCodeFolding(TextArea);

            TextArea.Dock = DockStyle.Fill;

            return TextArea;
        }

        private void TextArea_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla)sender;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == Globals.getMaxLineNumberCharLength())
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width = scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            Globals.setMaxLineNumberCharLength(maxLineNumberCharLength);

            openedFileChanges[tabControl.SelectedIndex] = true;
        }

        private void TextArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                kryptonContextMenu1.Show(sender);
            }
        }

        private void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < 32)
            {
                // Prevent control characters from getting inserted into the text buffer
                e.Handled = true;
                return;
            }

            openedFileChanges[tabControl.SelectedIndex] = true;
        }

        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 5;

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
        public void NewTab()
        {
            KryptonPage kryptonPage = new KryptonPage();
            kryptonPage.Text = "Untitled";

            Scintilla TextArea = CreateNewTextArea();

            kryptonPage.Controls.Add(TextArea);

            int index = 0;

            if (tabControl.Pages.Count > 1)
            {
                index = Globals.getLastSelectedTabIndex() + 1;
            }

            tabControl.Pages.Insert(index, kryptonPage);
            tabControl.SelectedPage = kryptonPage;

            openedFilePaths.Insert(index, null);
            openedFileChanges.Insert(index, false);
        }

        public void CloseApp()
        {
            int tabCount = tabControl.Pages.Count;

            while(tabCount > 1)
            {
                CloseTab();
                tabCount--;
            }
            Application.Exit();
        }

        // [Method] Closes the currently selected tab
        public void CloseTab()
        {
            if (tabControl.SelectedIndex != tabControl.Pages.Count - 1)
            {
                if (openedFileChanges[tabControl.SelectedIndex] == true)
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
                }
                else
                {
                    CloseTabAfterWarning();
                }
            }
        }

        public void CloseTabAfterWarning()
        {
            KryptonPage selectedKryptonPage = tabControl.SelectedPage;
            openedFilePaths.RemoveAt(tabControl.SelectedIndex);
            openedFileChanges.RemoveAt(tabControl.SelectedIndex);
            tabControl.Pages.Remove(selectedKryptonPage);

            if (tabControl.SelectedIndex >= 0 && tabControl.SelectedIndex < openedFilePaths.Count && openedFilePaths[tabControl.SelectedIndex] != null)
            {
                SetBuildAndRunOptions(true);
            }

            if (tabControl.Pages.Count > 1)
            {
                tabControl.SelectedIndex = Globals.getLastSelectedTabIndex() - 1;

                if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
                {
                    tabControl.SelectedIndex--;
                }

                Globals.setLastSelectedTabIndex(tabControl.SelectedIndex);
            }
            else
            {
                NewTab();
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
            if (openedFilePaths[openedTabIndex] == null)
            {
                SaveAs();
            }
            else
            {
                string chosenPath = openedFilePaths[tabControl.SelectedIndex];

                TextWriter txt = new StreamWriter(chosenPath);
                Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
                txt.Write(TextArea.Text);
                txt.Close();

                tabControl.SelectedPage.Text = chosenPath;

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]));
                ScintillaLexerService.SetLexer(extension, TextArea);
            }

            openedFileChanges[openedTabIndex] = false;
            SetBuildAndRunOptions(true);
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

                openedFilePaths[tabControl.SelectedIndex] = chosenPath;
                tabControl.SelectedPage.Text = chosenPath;

                string extension = ParsingService.GetFileExtension(ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]));
                ScintillaLexerService.SetLexer(extension, TextArea);
            }
        }

        // [Method] Opens a file (without openFileDialog), given just a path
        public void Open(string fileName)
        {
            if (tabControl.Pages.Count == 1)
            {
                NewTab();
            }

            int openedTabIndex = tabControl.SelectedIndex;

            string fileContent = System.IO.File.ReadAllText(fileName);

            Scintilla TextArea = (Scintilla)tabControl.SelectedPage.Controls[0];
            TextArea.Text = fileContent;

            openedFilePaths[openedTabIndex] = fileName;
            tabControl.SelectedPage.Text = fileName;

            string extension = ParsingService.GetFileExtension(fileName);
            ScintillaLexerService.SetLexer(extension, TextArea);

            SetBuildAndRunOptions(true);
        }

        // [Method] Opens a new file and replaces text and path (in openedFilePaths) for selected tab (with openFileDialog)
        public void OpenPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                NewTab();
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
                kryptonPage.ImageSmall = Properties.Resources.terminal__2_;
            }
            else if (process == "powershell.exe")
            {
                kryptonPage.Text = "Powershell";
                kryptonPage.ImageSmall = Properties.Resources.powershell__1_;
            }

            consoleControl.Dock = DockStyle.Fill;

            consoleControl.StartProcess(process, "");

            buildTabControl.SelectedPage = kryptonPage;
        }

        public void SetBuildAndRunOptions(bool status)
        {
            toolStripMenuItem2.Enabled = status;
            toolStripMenuItem3.Enabled = status;

            foreach (ToolStripMenuItem toolStripMenuItem in Globals.getBuildCommandsToolstripMenuItems())
            {
                toolStripMenuItem.Enabled = status;
            }

            javaClassclassToolStripMenuItem.Enabled = status;
            pythonScriptpyToolStripMenuItem.Enabled = status;
            perlScriptplToolStripMenuItem.Enabled = status;
            renderHTMLFilehtmlToolStripMenuItem.Enabled = status;

            workingDirectoryToolStripMenuItem.Enabled = status;
            stagingAreaToolStripMenuItem.Enabled = status;
            remoteToolStripMenuItem.Enabled = status;
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
                buildTabControl.Show();
                terminalTabControlOpened = true;
                kryptonContextMenuItem15.Checked = true;
                showBuildToolsToolStripMenuItem.Checked = true;
            }
            else
            {
                buildTabControl.Hide();
                terminalTabControlOpened = false;
                kryptonContextMenuItem15.Checked = false;
                showBuildToolsToolStripMenuItem.Checked = false;
            }
        }

        // [Method] Used for Git commands
        public void ExecuteGitCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/K " + "(" + command + ")&(pause)&(exit)";

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(openedFilePaths[tabControl.SelectedIndex]);
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
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(javac " + openedFilePaths[tabControl.SelectedIndex] + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
            }
            else if (type == "C/C++ Source (.c, .cpp) with GCC Compiler")
            {
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(gcc -Wall " + openedFilePaths[tabControl.SelectedIndex] + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
            }
            else if (type == "C/C++ Source (.c, .cpp)")
            {
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(gcc -Wall " + openedFilePaths[tabControl.SelectedIndex] + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
            }

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(openedFilePaths[tabControl.SelectedIndex]);
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
                string className = ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]);
                className = ParsingService.RemoveFileExtension(className);

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(java " + className + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }
            else if (type == "Python Script (.py)")
            {
                string scriptName = ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]);

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(py " + scriptName + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }
            else if (type == "Perl Script (.pl)")
            {
                string scriptName = ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]);

                startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(perl " + scriptName + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
            }

            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(openedFilePaths[tabControl.SelectedIndex]);
                Process.Start(startInfo);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please open a file before launching any run command.", "Build Error");
            }
        }

        // [Event] Form Loading
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load Build Commands from configuration file
            List<BuildCommand> buildCommands = BuildCommandService.GetBuildCommandsFromFile("build-commands.config");
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

            Globals.setBuildCommands(buildCommands);
            Globals.setBuildCommandsToolstripMenuItems(buildCommandToolStripMenuItems);

            ScintillaLexerService.InitializeDefaultColorDictionary();
            ScintillaLexerService.InitializeConfigColorDictionary("theme-settings.config");

            // Do other visual processing
            buildTabControl.Hide();

            tabControl.AllowPageDrag = false;
            tabControl.AllowPageReorder = false;

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length == 2)
            {
                string path = args[1];
                Open(path);
            }
            else
            {
                NewTab();
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            string scriptName = ParsingService.GetFileName(openedFilePaths[tabControl.SelectedIndex]);
            string command = (string)((ToolStripMenuItem)sender).Tag;
            command = command.Replace("$FILE", scriptName);
            startInfo.Arguments = "/K (echo ^[Pie^] Running started.)&(" + command + ")&(echo ^[Pie^] Running finished.)&(pause)&(exit)";
           
            try
            {
                startInfo.WorkingDirectory = ParsingService.GetFolderName(openedFilePaths[tabControl.SelectedIndex]);
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
            NewTab();
        }

        // [Event] "New" button pressed in upper menu => create new tab
        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab();
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
            if (e.KeyCode == Keys.T && e.Modifiers == Keys.Control)
            {
                NewTab();
            }
            else if (e.KeyCode == Keys.W && e.Modifiers == Keys.Control)
            {
                CloseTab();
            }
            else if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                Save();
            }
            else if (e.KeyCode == Keys.B && e.Modifiers == Keys.Control)
            {
                ShowTerminalTabControl();
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

            CloseApp();
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
                Globals.setLastSelectedTabIndex(tabControl.SelectedIndex);
            }

            if (tabControl.Pages.Count == openedFilePaths.Count)
            {
                if (tabControl.SelectedIndex == -1 || openedFilePaths[tabControl.SelectedIndex] == null)
                {
                    SetBuildAndRunOptions(false);
                }
                else
                {
                    SetBuildAndRunOptions(true);
                }
            }
            else
            {
                SetBuildAndRunOptions(false);
            }
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
                string path = ParsingService.GetFolderName(openedFilePaths[tabControl.SelectedIndex]);

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
            Globals.setCommitMessage(null);

            GitCommitMessageForm commitMessageForm = new GitCommitMessageForm();
            commitMessageForm.ShowDialog();

            if (Globals.getCommitMessage() != null)
            {
                ExecuteGitCommand("git commit -m \"" + Globals.getCommitMessage() + "\"");
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
                    int index = Globals.getLastSelectedTabIndex();
                    tabControl.SelectedIndex = index;
                }
            }
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Pages.Count - 1)
            {
                NewTab();
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildCommandsForm buildCommandsForm = new BuildCommandsForm();
            buildCommandsForm.ShowDialog();

            if (Globals.getCloseAfterApplyingChanges())
            {
                this.Close();
            }
        }

        private void themeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeSettingsForm themeSettingsForm = new ThemeSettingsForm();
            themeSettingsForm.ShowDialog();

            if (Globals.getCloseAfterApplyingChanges())
            {
                this.Close();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
