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

using BrightIdeasSoftware;
using ComponentFactory.Krypton.Toolkit;
using pie.Classes;
using pie.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace pie.Forms.Format
{
    public partial class FormatForm : KryptonForm
    {
        public FormatForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
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
            SynchronizeObjectListViewWithTheme();
            List<FormatOption> formatOptions = FetchPredefinedFormatOptions();
            formatOptionsListView.SetObjects(formatOptions);
        }

        private List<FormatOption> FetchPredefinedFormatOptions()
        {
            List<FormatOption> formatOptions = new List<FormatOption>();

            formatOptions.Add(new FormatOption("LINE_DUPLICATE", "Line Processing", "Duplicates every line"));
            formatOptions.Add(new FormatOption("LINE_ADD_EMPTY", "Line Processing", "Adds empty row between each line"));
            formatOptions.Add(new FormatOption("LINE_CAPITALIZE_FIRST", "Line Processing", "Capitalizes first character from every line"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_EMPTY", "Line Processing", "Removes empty lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_WHITESPACE", "Line Processing", "Removes whitespace lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_DUPLICATE", "Line Processing", "Removes duplicate lines"));
            formatOptions.Add(new FormatOption("LINE_REMOVE_DUPLICATE_CONSEC", "Line Processing", "Removes consecutive duplicate lines"));
            formatOptions.Add(new FormatOption("LINE_TRIM", "Line Processing", "Trims lines (removes trailing and leading whitespaces)"));
            formatOptions.Add(new FormatOption("CHAR_CAPITALIZE", "Character Processing", "Capitalizes every word"));
            formatOptions.Add(new FormatOption("CHAR_CASE_UPPER", "Character Processing", "Converts text to uppercase"));
            formatOptions.Add(new FormatOption("CHAR_CASE_LOWER", "Character Processing", "Converts text to lowercase"));
            formatOptions.Add(new FormatOption("CHAR_CASE_SWAP", "Character Processing", "Swaps lowercase and uppercase characters"));
            formatOptions.Add(new FormatOption("CHAR_REMOVE_WHITESPACE", "Character Processing", "Removes all whitespaces (except CR/LF)"));
            formatOptions.Add(new FormatOption("CHAR_REMOVE_WHITESPACE_CONSEC", "Character Processing", "Removes consecutive whitespaces (except CR/LF)"));
            formatOptions.Add(new FormatOption("CHAR_CONV_NEWLINE_COMMA", "Character Processing", "Converts \'\n\' to comma"));
            formatOptions.Add(new FormatOption("CHAR_CONV_NEWLINE_SPACE", "Character Processing", "Converts \'\n\' to space"));
            formatOptions.Add(new FormatOption("SORT_ASC", "Sorting", "Sorts lines ascending"));
            formatOptions.Add(new FormatOption("SORT_DESC", "Sorting", "Sorts lines descending"));
            formatOptions.Add(new FormatOption("SORT_REVERSE", "Sorting", "Reverses line order"));

            return formatOptions;
        }

        private void formatOptionsListView_DoubleClick(object sender, EventArgs e)
        {
            if (formatOptionsListView.SelectedObjects.Count == 1)
            {
                Globals.chosenFormatOption = ((FormatOption)formatOptionsListView.SelectedObject).FormatOptionName;
                this.Close();
            }
        }
    }
}
