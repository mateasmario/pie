﻿/** Copyright (C) 2023  Mario-Mihai Mateas
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

using pie.Classes;
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
        private ThemeService themeService = new ThemeService();

        public GitCommitLogForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private HeaderFormatStyle CustomizeHeaderStyle()
        {
            HeaderFormatStyle headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = Globals.theme.Secondary;
            headerstyle.Normal.ForeColor = Globals.theme.Fore;

            headerstyle.Hot.BackColor = Globals.theme.ButtonHover;
            headerstyle.Hot.ForeColor = Globals.theme.Fore;

            headerstyle.Pressed.BackColor = Globals.theme.ButtonFrame;
            headerstyle.Pressed.ForeColor = Globals.theme.Fore;

            return headerstyle;
        }

        private void GitCommitLogForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            gitCommitLogListView.ShowGroups = false;
            gitCommitLogListView.BackColor = Globals.theme.Primary;
            gitCommitLogListView.ForeColor = Globals.theme.Fore;
            gitCommitLogListView.UseCustomSelectionColors = true;
            gitCommitLogListView.FullRowSelect = true;
            gitCommitLogListView.HighlightBackgroundColor = Globals.theme.Secondary;
            gitCommitLogListView.HighlightForegroundColor = Globals.theme.Fore;
            gitCommitLogListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            gitCommitLogListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;
            gitCommitLogListView.BackColor = Globals.theme.Primary;
            gitCommitLogListView.ForeColor = Globals.theme.Fore;

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
