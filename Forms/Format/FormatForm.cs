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
using pie.Enums;

namespace pie.Forms.Format
{
    public partial class FormatForm : KryptonForm
    {
        public FormatForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            formatOptionsListView.ShowGroups = false;
            formatOptionsListView.UseCustomSelectionColors = true;
            formatOptionsListView.FullRowSelect = true;
            formatOptionsListView.MultiSelect = false;

            formatOptionsListView.BackColor = ThemeService.GetColor("Primary");
            formatOptionsListView.ForeColor = ThemeService.GetColor("Fore");
            formatOptionsListView.HighlightBackgroundColor = ThemeService.GetColor("Secondary");
            formatOptionsListView.HighlightForegroundColor = ThemeService.GetColor("Fore");
            formatOptionsListView.UnfocusedHighlightBackgroundColor = ThemeService.GetColor("Secondary");
            formatOptionsListView.UnfocusedHighlightForegroundColor = ThemeService.GetColor("Fore");

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = ThemeService.GetColor("Secondary");
            headerstyle.Normal.ForeColor = ThemeService.GetColor("Fore");

            headerstyle.Hot.BackColor = ThemeService.GetColor("ButtonHover");
            headerstyle.Hot.ForeColor = ThemeService.GetColor("Fore");

            headerstyle.Pressed.BackColor = ThemeService.GetColor("ButtonFrame");
            headerstyle.Pressed.ForeColor = ThemeService.GetColor("Fore");

            formatOptionsListView.HeaderFormatStyle = headerstyle;

            FormatOptionDescriptionColumn.FillsFreeSpace = true;
        }

        private void FormatForm_Load(object sender, EventArgs e)
        {
            if (Globals.glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            List<FormatOption> formatOptions = FetchPredefinedFormatOptions();
            formatOptionsListView.SetObjects(formatOptions);

            kryptonTextBox1.Select();
            kryptonTextBox1.SelectAll();
        }

        private List<FormatOption> FetchPredefinedFormatOptions()
        {
            List<FormatOption> formatOptions = new List<FormatOption>();

            foreach(CustomFormatter customFormatter in Globals.customFormatters)
            {
                FormatOption formatOption = new FormatOption(customFormatter.Name, FormatOptionCategory.CUSTOM, "External formatter", customFormatter.Instance, customFormatter.MethodInfo);
                formatOptions.Add(formatOption);
            }

            formatOptions.Add(new FormatOption("LINE_DUPLICATE", FormatOptionCategory.LINE_PROCESSING, "Duplicates every line"));
            formatOptions.Add(new FormatOption("LINE_ADD_EMPTY", FormatOptionCategory.LINE_PROCESSING, "Adds empty row between each line"));
            formatOptions.Add(new FormatOption("LINE_CAPITALIZE_FIRST", FormatOptionCategory.LINE_PROCESSING, "Capitalizes first character from every line"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_EMPTY", FormatOptionCategory.LINE_PROCESSING, "Removes empty lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_WHITESPACE", FormatOptionCategory.LINE_PROCESSING, "Removes whitespace lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_DUPLICATE", FormatOptionCategory.LINE_PROCESSING, "Removes duplicate lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_DUPLICATE_CONSEC", FormatOptionCategory.LINE_PROCESSING, "Removes consecutive duplicate lines"));
            formatOptions.Add(new FormatOption("LINE_TRIM", FormatOptionCategory.LINE_PROCESSING, "Trims lines (removes trailing and leading whitespaces)"));
            formatOptions.Add(new FormatOption("CHAR_CAPITALIZE", FormatOptionCategory.CHARACTER_PROCESSING, "Capitalizes every word"));
            formatOptions.Add(new FormatOption("CHAR_CASE_UPPER", FormatOptionCategory.CHARACTER_PROCESSING, "Converts text to uppercase"));
            formatOptions.Add(new FormatOption("CHAR_CASE_LOWER", FormatOptionCategory.CHARACTER_PROCESSING, "Converts text to lowercase"));
            formatOptions.Add(new FormatOption("CHAR_CASE_SWAP", FormatOptionCategory.CHARACTER_PROCESSING, "Swaps lowercase and uppercase characters"));
            formatOptions.Add(new FormatOption("CHAR_REMOVE_WHITESPACE", FormatOptionCategory.CHARACTER_PROCESSING, "Removes all whitespaces (except CR/LF)"));
            formatOptions.Add(new FormatOption("CHAR_REMOVE_WHITESPACE_CONSEC", FormatOptionCategory.CHARACTER_PROCESSING, "Removes consecutive whitespaces (except CR/LF)"));
            formatOptions.Add(new FormatOption("CHAR_CONV_NEWLINE_COMMA", FormatOptionCategory.CHARACTER_PROCESSING, "Converts \'\\n\' to comma"));
            formatOptions.Add(new FormatOption("CHAR_CONV_NEWLINE_SPACE", FormatOptionCategory.CHARACTER_PROCESSING, "Converts \'\\n\' to space"));
            formatOptions.Add(new FormatOption("SORT_ASC", FormatOptionCategory.SORTING, "Sorts lines ascending"));
            formatOptions.Add(new FormatOption("SORT_DESC", FormatOptionCategory.SORTING, "Sorts lines descending"));
            formatOptions.Add(new FormatOption("SORT_REVERSE", FormatOptionCategory.SORTING, "Reverses line order"));

            return formatOptions;
        }

        private void formatOptionsListView_DoubleClick(object sender, EventArgs e)
        {
            if (formatOptionsListView.SelectedObjects.Count == 1)
            {
                Globals.chosenFormatOption = (FormatOption)formatOptionsListView.SelectedObject;
                this.Close();
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            List<FormatOption> formatOptions = FetchPredefinedFormatOptions();

            if (!kryptonTextBox1.Text.Trim().Equals(""))
            {
                formatOptions = formatOptions.FindAll(x => x.FormatOptionName.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || x.FormatOptionName.ToLower().Replace("_", " ").Contains(kryptonTextBox1.Text.ToLower().Trim()) || x.FormatOptionDescription.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || x.FormatOptionCategory.ToString().ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()));
            }
            
            formatOptionsListView.SetObjects(formatOptions);
        }
    }
}
