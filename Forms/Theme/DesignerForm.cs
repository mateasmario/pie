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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pie.Services;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Forms.Theme
{
    public partial class DesignerForm : KryptonForm
    {
        private ThemeInfo themeInfoToEdit;

        public DesignerForm()
        {
            InitializeComponent();
            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonPanel2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
            kryptonGroupBox1.Palette = Globals.kryptonPalette;
            kryptonGroupBox2.Palette = Globals.kryptonPalette;
            kryptonColorButton1.Palette = Globals.kryptonPalette;
            kryptonColorButton2.Palette = Globals.kryptonPalette;
            kryptonColorButton3.Palette = Globals.kryptonPalette;
            kryptonColorButton4.Palette = Globals.kryptonPalette;
            kryptonColorButton5.Palette = Globals.kryptonPalette;
            kryptonColorButton6.Palette = Globals.kryptonPalette;
            kryptonColorButton7.Palette = Globals.kryptonPalette;
            kryptonColorButton8.Palette = Globals.kryptonPalette;
            kryptonColorButton9.Palette = Globals.kryptonPalette;
            kryptonColorButton10.Palette = Globals.kryptonPalette;
            kryptonColorButton11.Palette = Globals.kryptonPalette;
            kryptonColorButton12.Palette = Globals.kryptonPalette;
            kryptonColorButton13.Palette = Globals.kryptonPalette;
            kryptonColorButton14.Palette = Globals.kryptonPalette;
            kryptonColorButton15.Palette = Globals.kryptonPalette;
            kryptonColorButton16.Palette = Globals.kryptonPalette;
            kryptonColorButton17.Palette = Globals.kryptonPalette;
            kryptonColorButton18.Palette = Globals.kryptonPalette;
            kryptonColorButton19.Palette = Globals.kryptonPalette;
            kryptonColorButton20.Palette = Globals.kryptonPalette;
            kryptonColorButton21.Palette = Globals.kryptonPalette;
            kryptonColorButton22.Palette = Globals.kryptonPalette;
            kryptonColorButton23.Palette = Globals.kryptonPalette;
            kryptonColorButton24.Palette = Globals.kryptonPalette;
            kryptonColorButton25.Palette = Globals.kryptonPalette;
            kryptonComboBox1.Palette = Globals.kryptonPalette;
        }

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            SynchronizeObjectListViewWithTheme();
            themeListView.SetObjects(Globals.themeInfos);
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            themeListView.ShowGroups = false;
            themeListView.UseCustomSelectionColors = true;
            themeListView.FullRowSelect = true;
            themeListView.MultiSelect = false;
            themeListView.HeaderStyle = ColumnHeaderStyle.None;

            themeListView.BackColor = ThemeService.GetColor("Primary");
            themeListView.ForeColor = ThemeService.GetColor("Fore");
            themeListView.HighlightBackgroundColor = ThemeService.GetColor("Secondary");
            themeListView.HighlightForegroundColor = ThemeService.GetColor("Fore");
            themeListView.UnfocusedHighlightBackgroundColor = ThemeService.GetColor("Secondary");
            themeListView.UnfocusedHighlightForegroundColor = ThemeService.GetColor("Fore");

            ThemeNameColumn.FillsFreeSpace = true;
        }

        private void themeListView_DoubleClick(object sender, EventArgs e)
        {
            if (themeListView.SelectedItems.Count == 1)
            {
                foreach(ThemeInfo themeInfo in Globals.themeInfos)
                {
                    if (themeInfo.Name.Equals(themeListView.SelectedItem.Text))
                    {
                        themeInfoToEdit = themeInfo;
                        break;
                    }
                }
            }

            kryptonColorButton1.SelectedColor = themeInfoToEdit.ColorDictionary["Primary"];
            kryptonColorButton3.SelectedColor = themeInfoToEdit.ColorDictionary["Secondary"];
            kryptonColorButton4.SelectedColor = themeInfoToEdit.ColorDictionary["Button"];
            kryptonColorButton5.SelectedColor = themeInfoToEdit.ColorDictionary["ButtonFrame"];
            kryptonColorButton6.SelectedColor = themeInfoToEdit.ColorDictionary["ButtonHover"];
            kryptonColorButton7.SelectedColor = themeInfoToEdit.ColorDictionary["Fore"];
            kryptonColorButton8.SelectedColor = themeInfoToEdit.ColorDictionary["FormBorder"];
            kryptonColorButton9.SelectedColor = themeInfoToEdit.ColorDictionary["Selection"];
            kryptonColorButton10.SelectedColor = themeInfoToEdit.ColorDictionary["CaretLineBack"];
            kryptonColorButton11.SelectedColor = themeInfoToEdit.ColorDictionary["NumberMargin"];
            kryptonColorButton12.SelectedColor = themeInfoToEdit.ColorDictionary["Folding"];
            if (themeInfoToEdit.IconType.ToLower().Equals("light"))
            {
                kryptonComboBox1.SelectedIndex = 0;
            }
            else
            {
                kryptonComboBox1.SelectedIndex = 1;
            }

            kryptonColorButton22.SelectedColor = themeInfoToEdit.ColorDictionary["Comment"];
            kryptonColorButton21.SelectedColor = themeInfoToEdit.ColorDictionary["CommentLine"];
            kryptonColorButton20.SelectedColor = themeInfoToEdit.ColorDictionary["CommentBlock"];
            kryptonColorButton19.SelectedColor = themeInfoToEdit.ColorDictionary["Word"];
            kryptonColorButton18.SelectedColor = themeInfoToEdit.ColorDictionary["String"];
            kryptonColorButton17.SelectedColor = themeInfoToEdit.ColorDictionary["Number"];
            kryptonColorButton16.SelectedColor = themeInfoToEdit.ColorDictionary["Operator"];
            kryptonColorButton15.SelectedColor = themeInfoToEdit.ColorDictionary["Preprocessor"];
            kryptonColorButton14.SelectedColor = themeInfoToEdit.ColorDictionary["Triple"];
            kryptonColorButton13.SelectedColor = themeInfoToEdit.ColorDictionary["Decorator"];
            kryptonColorButton2.SelectedColor = themeInfoToEdit.ColorDictionary["Attribute"];
            kryptonColorButton23.SelectedColor = themeInfoToEdit.ColorDictionary["Entity"];
            kryptonColorButton24.SelectedColor = themeInfoToEdit.ColorDictionary["User1"];
            kryptonColorButton25.SelectedColor = themeInfoToEdit.ColorDictionary["User2"];
        }

        private void kryptonColorButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            themeInfoToEdit.ColorDictionary[kryptonColorButton.Text] = kryptonColorButton.SelectedColor;
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (kryptonComboBox1.SelectedIndex == 0)
            {
                themeInfoToEdit.IconType = "dark";
            }
            else
            {
                themeInfoToEdit.IconType = "light";
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            ThemeService.WriteThemesToDirectory("config/themes", Globals.themeInfos);
        }
    }
}
