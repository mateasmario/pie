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
using System.Text.RegularExpressions;
using pie.Classes;
using pie.Services;

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
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Format
{
    public partial class FormatForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();

        public string Input { get; set; }
        public string Output { get; set; }

        public FormatForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            formatOptionsListView.ShowGroups = false;
            formatOptionsListView.UseCustomSelectionColors = true;
            formatOptionsListView.FullRowSelect = true;
            formatOptionsListView.MultiSelect = false;

            formatOptionsListView.BackColor = Globals.theme.Primary;
            formatOptionsListView.ForeColor = Globals.theme.Fore;
            formatOptionsListView.HighlightBackgroundColor = Globals.theme.Secondary;
            formatOptionsListView.HighlightForegroundColor = Globals.theme.Fore;
            formatOptionsListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            formatOptionsListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = Globals.theme.Secondary;
            headerstyle.Normal.ForeColor = Globals.theme.Fore;

            headerstyle.Hot.BackColor = Globals.theme.ButtonHover;
            headerstyle.Hot.ForeColor = Globals.theme.Fore;

            headerstyle.Pressed.BackColor = Globals.theme.ButtonFrame;
            headerstyle.Pressed.ForeColor = Globals.theme.Fore;

            formatOptionsListView.HeaderFormatStyle = headerstyle;

            FormatOptionDescriptionColumn.FillsFreeSpace = true;
        }

        private void FormatForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            formatOptionsListView.SetObjects(Globals.formatters);

            kryptonTextBox1.Select();
            kryptonTextBox1.SelectAll();
        }

        private void formatOptionsListView_DoubleClick(object sender, EventArgs e)
        {
            if (formatOptionsListView.SelectedObjects.Count == 1)
            {
                Formatter selectedFormatOption = Globals.formatters.Find(formatter => ((Formatter)formatOptionsListView.SelectedObject).Name == formatter.Name);
                Output = selectedFormatOption.InvokeMethod(Input);
                this.Close();
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            formatOptionsListView.SetObjects(Globals.formatters.FindAll(x => x.Name.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || Regex.Replace(x.Name, "([A-Z])", " $1", RegexOptions.Compiled).Trim().ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim())));
        }
    }
}
