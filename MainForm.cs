﻿using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using FastColoredTextBoxNS;
using pie.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class MainForm : Form
    {
        // [Field] Paths of the opened files
        public List<String> openedFilePaths;
        public bool darkMode;
        public bool terminalTabControlOpened;

        // [Constructor] Field initializations occur here 
        public MainForm()
        {
            InitializeComponent();

            this.MinimumSize = new System.Drawing.Size(1036, 634);

            openedFilePaths = new List<String>();
        }

        // [Method] Creates a new tab and selects the new tab
        public void NewTab()
        {
            KryptonPage kryptonPage = new KryptonPage();
            kryptonPage.Text = "Untitled";

            FastColoredTextBox fastColoredTextBox = new FastColoredTextBox();
            fastColoredTextBox.Name = "FastColoredTextBox";
            fastColoredTextBox.KeyDown += keyDownEvents;
            fastColoredTextBox.AutoCompleteBrackets = true;

            kryptonPage.Controls.Add(fastColoredTextBox);
            fastColoredTextBox.Dock = DockStyle.Fill;

            if (darkMode == true)
            {
                fastColoredTextBox.BackColor = Color.FromArgb(23, 23, 23);
                fastColoredTextBox.ForeColor = Color.White;
            }

            tabControl.Pages.Add(kryptonPage);

            tabControl.SelectedPage = kryptonPage;

            openedFilePaths.Add(null);
        }

        // [Method] Closes the currently selected tab
        public void CloseTab()
        {
            if (tabControl.Pages.Count > 0)
            {
                KryptonPage selectedKryptonPage = tabControl.SelectedPage;
                openedFilePaths.RemoveAt(tabControl.SelectedIndex);
                tabControl.Pages.Remove(selectedKryptonPage);

                if (tabControl.SelectedIndex >= 0 && openedFilePaths[tabControl.SelectedIndex] != null)
                {
                    SetBuildAndRunOptions(true);
                }
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
                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl.SelectedPage.Controls["FastColoredTextBox"];
                txt.Write(fastColoredTextBox.Text);
                txt.Close();

                tabControl.SelectedPage.Text = chosenPath;
            }

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
                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl.SelectedPage.Controls["FastColoredTextBox"];
                txt.Write(fastColoredTextBox.Text);
                txt.Close();

                openedFilePaths[tabControl.SelectedIndex] = chosenPath;
                tabControl.SelectedPage.Text = chosenPath;
            }
        }

        // [Method] Opens a new file and replaces text and path (in openedFilePaths) for selected tab
        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                if (tabControl.Pages.Count == 0)
                    NewTab();

                int openedTabIndex = tabControl.SelectedIndex;

                string fileContent = System.IO.File.ReadAllText(openFileDialog.FileName);

                FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl.SelectedPage.Controls["FastColoredTextBox"];
                fastColoredTextBox.Text = fileContent;

                openedFilePaths[openedTabIndex] = openFileDialog.FileName;
                tabControl.SelectedPage.Text = openFileDialog.FileName;

                SetBuildAndRunOptions(true);
            }
        }

        public void SetBuildAndRunOptions(bool status)
        {
            if (status == true)
            {
                toolStripMenuItem2.Enabled = true;
                toolStripMenuItem3.Enabled = true;
                toolStripMenuItem4.Enabled = true;

                javaClassclassToolStripMenuItem.Enabled = true;
                pythonScriptpyToolStripMenuItem.Enabled = true;
                perlScriptplToolStripMenuItem.Enabled = true;
                renderHTMLFilehtmlToolStripMenuItem.Enabled = true;

                workingDirectoryToolStripMenuItem.Enabled = true;
                stagingAreaToolStripMenuItem.Enabled = true;
                remoteToolStripMenuItem.Enabled = true;
            }
            else
            {
                toolStripMenuItem2.Enabled = false;
                toolStripMenuItem3.Enabled = false;
                toolStripMenuItem4.Enabled = false;

                javaClassclassToolStripMenuItem.Enabled = false;
                pythonScriptpyToolStripMenuItem.Enabled = false;
                perlScriptplToolStripMenuItem.Enabled = false;
                renderHTMLFilehtmlToolStripMenuItem.Enabled = false;

                workingDirectoryToolStripMenuItem.Enabled = false;
                stagingAreaToolStripMenuItem.Enabled = false;
                remoteToolStripMenuItem.Enabled = false;
            }
        }

        // [Method] Turns Dark Mode on/off
        public void ToggleDarkMode()
        {
            if (darkMode == false)
            {
                darkMode = true;
                
                foreach (KryptonPage kryptonPage in tabControl.Pages)
                {
                    FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)kryptonPage.Controls["FastColoredTextBox"];
                    fastColoredTextBox.BackColor = Color.FromArgb(23, 23, 23);
                    fastColoredTextBox.ForeColor = Color.White;
                }

                darkModeToolStripMenuItem.Checked = true;
            }
            else
            {
                darkMode = false;

                foreach (KryptonPage kryptonPage in tabControl.Pages)
                {
                    FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)kryptonPage.Controls["FastColoredTextBox"];
                    fastColoredTextBox.BackColor = Color.White;
                    fastColoredTextBox.ForeColor = Color.Black;
                }

                darkModeToolStripMenuItem.Checked = false;
            }
        }

        // [Method] Opens terminal tab control
        public void ShowTerminalTabControl()
        {
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

            if (type == "Java Source (.java) with JDK")
            {
                startInfo.Arguments = "/K (echo ^[Pie^] Building started.)&(javac " + openedFilePaths[tabControl.SelectedIndex] + ")&(echo ^[Pie^] Build finished.)&(pause)&(exit)";
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
            buildTabControl.Hide();

            terminal.StartProcess("cmd.exe", "");
            powershell.StartProcess("powershell.exe", "");

            NewTab();
        }

        // [Event] Triggered when a new tab is opened/closed
        private void tabControl_TabCountChanged(object sender, EventArgs e)
        {
            if (tabControl.Pages.Count == 0)
            {
                kryptonContextMenuItem3.Enabled = false;

                saveFileToolStripMenuItem.Enabled = false;
                saveAsToolStripMenuItem.Enabled = false;

                kryptonContextMenuItem5.Enabled = false;
                kryptonContextMenuItem6.Enabled = false;
                kryptonContextMenuItem7.Enabled = false;
                kryptonContextMenuItem8.Enabled = false;
                kryptonContextMenuItem9.Enabled = false;
                kryptonContextMenuItem10.Enabled = false;
                kryptonContextMenuItem11.Enabled = false;
                kryptonContextMenuItem12.Enabled = false;
            }
            else
            {
                kryptonContextMenuItem3.Enabled = true;

                saveFileToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;

                kryptonContextMenuItem5.Enabled = true;
                kryptonContextMenuItem6.Enabled = true;
                kryptonContextMenuItem7.Enabled = true;
                kryptonContextMenuItem8.Enabled = true;
                kryptonContextMenuItem9.Enabled = true;
                kryptonContextMenuItem10.Enabled = true;
                kryptonContextMenuItem11.Enabled = true;
                kryptonContextMenuItem12.Enabled = true;
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
            Open();
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


        // [Event] "Dark Mode" button from "Design" button located in the upper menu
        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToggleDarkMode();
        }

        // [Generic Event] Used for changing the language of the FastColoredTextBox of the selected tab. Called by other event listeners
        private void codeFormatting(object sender, EventArgs e)
        {
            KryptonContextMenuItem kryptonContextMenuItem = (KryptonContextMenuItem)sender;
            FastColoredTextBox fastColoredTextBox = (FastColoredTextBox)tabControl.SelectedPage.Controls["FastColoredTextBox"];

            string content = fastColoredTextBox.Text;

            switch(kryptonContextMenuItem.Text)
            {
                case "JS":
                    fastColoredTextBox.Language = Language.JS;
                    break;
                case "HTML":
                    fastColoredTextBox.Language = Language.HTML;
                    break;
                case "C#":
                    fastColoredTextBox.Language = Language.CSharp;
                    break;
                case "XML":
                    fastColoredTextBox.Language = Language.XML;
                    break;
                case "VB":
                    fastColoredTextBox.Language = Language.VB;
                    break;
                case "Lua":
                    fastColoredTextBox.Language = Language.Lua;
                    break;
                case "PHP":
                    fastColoredTextBox.Language = Language.PHP;
                    break;
                case "SQL":
                    fastColoredTextBox.Language = Language.SQL;
                    break;
            }

            fastColoredTextBox.Clear();
            fastColoredTextBox.Text = content;
        }

        // [Event] Triggered when clicking "Show Terminal Tab" from the context menu
        private void kryptonContextMenuItem15_Click(object sender, EventArgs e)
        {
            ShowTerminalTabControl();
        }

        // [Event] Triggered when Form is closed
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            terminal.StopProcess();
            terminal.Dispose();

            powershell.StopProcess();
            powershell.Dispose();
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
    }
}