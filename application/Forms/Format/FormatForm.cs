/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/**
 * This namespace provides access to the ObjectListView control.
 * Licensed under GNU General Public License (GPL 3.0). For more info, see https://www.gnu.org/licenses/gpl-3.0.html 
 * 
 * Author of the library: Phillip Piper
 * Copyright 2006-2016 Bright Ideas Software
 */
using BrightIdeasSoftware;
/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace pie.Forms.Format
{
    public partial class FormatForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public FormatFormInput Input { get; set; }
        public FormatFormOutput Output { get; set; }

        public FormatForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Output = new FormatFormOutput();
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

        private void FormatForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            formatOptionsListView.SetObjects(Input.Formatters);

            kryptonTextBox1.Select();
            kryptonTextBox1.SelectAll();
        }


        private void SynchronizeObjectListViewWithTheme()
        {
            formatOptionsListView.ShowGroups = false;
            formatOptionsListView.UseCustomSelectionColors = true;
            formatOptionsListView.FullRowSelect = true;
            formatOptionsListView.MultiSelect = false;

            formatOptionsListView.BackColor = Input.ActiveTheme.Primary;
            formatOptionsListView.ForeColor = Input.ActiveTheme.Fore;
            formatOptionsListView.HighlightBackgroundColor = Input.ActiveTheme.Secondary;
            formatOptionsListView.HighlightForegroundColor = Input.ActiveTheme.Fore;
            formatOptionsListView.UnfocusedHighlightBackgroundColor = Input.ActiveTheme.Secondary;
            formatOptionsListView.UnfocusedHighlightForegroundColor = Input.ActiveTheme.Fore;

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = Input.ActiveTheme.Secondary;
            headerstyle.Normal.ForeColor = Input.ActiveTheme.Fore;

            headerstyle.Hot.BackColor = Input.ActiveTheme.ButtonHover;
            headerstyle.Hot.ForeColor = Input.ActiveTheme.Fore;

            headerstyle.Pressed.BackColor = Input.ActiveTheme.ButtonFrame;
            headerstyle.Pressed.ForeColor = Input.ActiveTheme.Fore;

            formatOptionsListView.HeaderFormatStyle = headerstyle;

            FormatOptionDescriptionColumn.FillsFreeSpace = true;
        }

        private void formatOptionsListView_DoubleClick(object sender, EventArgs e)
        {
            if (formatOptionsListView.SelectedObjects.Count == 1)
            {
                Formatter selectedFormatOption = Input.Formatters.Find(formatter => ((Formatter)formatOptionsListView.SelectedObject).Name == formatter.Name);
                Output.Text = selectedFormatOption.InvokeMethod(Input.Text);
                Output.Saved = true;
                this.Close();
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            formatOptionsListView.SetObjects(Input.Formatters.FindAll(x => x.Name.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || Regex.Replace(x.Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim().ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim())));
        }
    }
}
