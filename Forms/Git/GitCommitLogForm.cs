﻿using pie.Classes;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

/**
 * This namespace provides access to the ObjectListView control.
 * Licensed under GNU General Public License (GPL 3.0). For more info, see https://www.gnu.org/licenses/gpl-3.0.html 
 * 
 * Author of the library: Phillip Piper
 * Copyright 2006-2016 Bright Ideas Software
 */
using BrightIdeasSoftware;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

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