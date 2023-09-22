using BrightIdeasSoftware;
using ComponentFactory.Krypton.Toolkit;
using LibGit2Sharp;
using pie.Classes;
using pie.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class GitCommitLogForm : KryptonForm
    {
        public GitCommitLogForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
        }

        private HeaderFormatStyle CustomizeHeaderStyle()
        {
            HeaderFormatStyle headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = ThemeService.GetSecondaryColor();
            headerstyle.Normal.ForeColor = ThemeService.GetForeColor();

            headerstyle.Hot.BackColor = ThemeService.GetButtonHoverColor();
            headerstyle.Hot.ForeColor = ThemeService.GetForeColor();

            headerstyle.Pressed.BackColor = ThemeService.GetButtonFrameColor();
            headerstyle.Pressed.ForeColor = ThemeService.GetForeColor();

            return headerstyle;
        }

        private void GitCommitLogForm_Load(object sender, EventArgs e)
        {
            gitCommitLogListView.ShowGroups = false;
            gitCommitLogListView.BackColor = ThemeService.GetPrimaryColor();
            gitCommitLogListView.ForeColor = ThemeService.GetForeColor();

            gitCommitLogListView.HeaderFormatStyle = CustomizeHeaderStyle();

            List<GitCommit> gitCommitList = new List<GitCommit>();

            var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

            foreach (Commit c in Globals.repo.Commits.Take(15))
            {
                GitCommit gitCommit = new GitCommit();

                gitCommit.Id = c.Id.ToString();
                gitCommit.Author = c.Author.ToString();
                gitCommit.Date = c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture);
                gitCommit.Message = c.Message;

                gitCommitList.Add(gitCommit);
            }

            gitCommitLogListView.SetObjects(gitCommitList);
        }
    }
}
