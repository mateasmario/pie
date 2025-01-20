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
using pie.Enums;

namespace pie.Forms.Format
{
    public partial class FormatForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();
        private FormattingService formattingService = new FormattingService();

        public string Input { get; set; }
        public string Output { get; set; }

        private List<FormatOption> formatOptions;

        public FormatForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);

            formatOptions = new List<FormatOption>();

            foreach (Formatter customFormatter in Globals.customFormatters)
            {
                FormatOption formatOption = new FormatOption(customFormatter.Name, FormatOptionCategory.Custom, "External formatter", customFormatter.Instance, customFormatter.MethodInfo);
                formatOptions.Add(formatOption);
            }

            formatOptions.Add(new FormatOption("LineDuplicate", FormatOptionCategory.Line, "Duplicates every line", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineAddEmpty", FormatOptionCategory.Line, "Adds empty row between each line", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineCapitalizeFirst", FormatOptionCategory.Line, "Capitalizes first character from every line", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineRemoveEmpty", FormatOptionCategory.Line, "Removes empty lines", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineRemoveWhitespace", FormatOptionCategory.Line, "Removes whitespace lines", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineRemoveDuplicate", FormatOptionCategory.Line, "Removes duplicate lines", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineRemoveDuplicateConsec", FormatOptionCategory.Line, "Removes consecutive duplicate lines", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("LineTrim", FormatOptionCategory.Line, "Trims lines (removes trailing and leading whitespaces)", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharCapitalize", FormatOptionCategory.Character, "Capitalizes every word", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharCaseUpper", FormatOptionCategory.Character, "Converts text to uppercase", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharCaseLower", FormatOptionCategory.Character, "Converts text to lowercase", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharCaseSwap", FormatOptionCategory.Character, "Swaps lowercase and uppercase characters", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharRemoveWhitespace", FormatOptionCategory.Character, "Removes all whitespaces (except CR/LF)", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharRemoveWhitespaceConsec", FormatOptionCategory.Character, "Removes consecutive whitespaces (except CR/LF)", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharConvNewlineComma", FormatOptionCategory.Character, "Converts \'\\n\' to comma", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("CharConvNewlineSpace", FormatOptionCategory.Character, "Converts \'\\n\' to space", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("SortAsc", FormatOptionCategory.Sorting, "Sorts lines ascending", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("SortDesc", FormatOptionCategory.Sorting, "Sorts lines descending", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
            formatOptions.Add(new FormatOption("SortReverse", FormatOptionCategory.Sorting, "Reverses line order", formattingService, formattingService.GetType().GetMethod("DuplicateLines")));
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
            formatOptionsListView.SetObjects(formatOptions);

            kryptonTextBox1.Select();
            kryptonTextBox1.SelectAll();
        }

        private void formatOptionsListView_DoubleClick(object sender, EventArgs e)
        {
            if (formatOptionsListView.SelectedObjects.Count == 1)
            {
                FormatOption selectedFormatOption = formatOptions.Find(formatOption => ((FormatOption)formatOptionsListView.SelectedObject).FormatOptionName == formatOption.FormatOptionName);
                Output = selectedFormatOption.InvokeMethod(Input);
                this.Close();
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!kryptonTextBox1.Text.Trim().Equals(""))
            {
                formatOptions = formatOptions.FindAll(x => x.FormatOptionName.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || Regex.Replace(x.FormatOptionName, "([A-Z])", " $1", RegexOptions.Compiled).Trim().ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || x.FormatOptionDescription.ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()) || x.FormatOptionCategory.ToString().ToLower().Contains(kryptonTextBox1.Text.ToLower().Trim()));
            }
            
            formatOptionsListView.SetObjects(formatOptions);
        }
    }
}
