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
using System.Windows.Forms;
using pie.Services;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;
using System.Reflection;

namespace pie.Forms.Theme
{
    public partial class DesignerForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();
        private ConfigurationService configurationService = new ConfigurationService();

        private ThemeInfo themeInfoToEdit;

        public DesignerForm()
        {
            InitializeComponent();
            themeService.SetPaletteToObjects(this, Globals.kryptonPalette);
        }

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            if (Globals.editorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            themeListView.SetObjects(Globals.themeInfos);

            kryptonGroupBox1.Visible = false;
            kryptonGroupBox2.Visible = false;
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            themeListView.ShowGroups = false;
            themeListView.UseCustomSelectionColors = true;
            themeListView.FullRowSelect = true;
            themeListView.MultiSelect = false;
            themeListView.HeaderStyle = ColumnHeaderStyle.None;

            themeListView.BackColor = Globals.theme.Primary;
            themeListView.ForeColor = Globals.theme.Fore;
            themeListView.HighlightBackgroundColor = Globals.theme.Secondary;
            themeListView.HighlightForegroundColor = Globals.theme.Fore;
            themeListView.UnfocusedHighlightBackgroundColor = Globals.theme.Secondary;
            themeListView.UnfocusedHighlightForegroundColor = Globals.theme.Fore;

            ThemeNameColumn.FillsFreeSpace = true;
        }

        private void themeListView_DoubleClick(object sender, EventArgs e)
        {
            kryptonGroupBox1.Visible = true;
            kryptonGroupBox2.Visible = true;

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

            kryptonColorButton1.SelectedColor = themeInfoToEdit.Primary;
            kryptonColorButton3.SelectedColor = themeInfoToEdit.Secondary;
            kryptonColorButton4.SelectedColor = themeInfoToEdit.Button;
            kryptonColorButton5.SelectedColor = themeInfoToEdit.ButtonFrame;
            kryptonColorButton6.SelectedColor = themeInfoToEdit.ButtonHover;
            kryptonColorButton7.SelectedColor = themeInfoToEdit.Fore;
            kryptonColorButton8.SelectedColor = themeInfoToEdit.FormBorder;
            kryptonColorButton9.SelectedColor = themeInfoToEdit.Selection;
            kryptonColorButton10.SelectedColor = themeInfoToEdit.CaretLineBack;
            kryptonColorButton11.SelectedColor = themeInfoToEdit.NumberMargin;
            kryptonColorButton12.SelectedColor = themeInfoToEdit.Folding;

            if (themeInfoToEdit.IconType == null || themeInfoToEdit.IconType.ToLower().Equals("dark"))
            {
                kryptonComboBox1.SelectedIndex = 0;
            }
            else
            {
                kryptonComboBox1.SelectedIndex = 1;
            }

            kryptonColorButton22.SelectedColor = themeInfoToEdit.Comment;
            kryptonColorButton21.SelectedColor = themeInfoToEdit.CommentLine;
            kryptonColorButton20.SelectedColor = themeInfoToEdit.CommentBlock;
            kryptonColorButton19.SelectedColor = themeInfoToEdit.Word;
            kryptonColorButton18.SelectedColor = themeInfoToEdit.String;
            kryptonColorButton17.SelectedColor = themeInfoToEdit.Number;
            kryptonColorButton16.SelectedColor = themeInfoToEdit.Operator;
            kryptonColorButton15.SelectedColor = themeInfoToEdit.Preprocessor;
            kryptonColorButton14.SelectedColor = themeInfoToEdit.Triple;
            kryptonColorButton13.SelectedColor = themeInfoToEdit.Decorator;
            kryptonColorButton2.SelectedColor = themeInfoToEdit.Attribute;
            kryptonColorButton23.SelectedColor = themeInfoToEdit.Entity;
            kryptonColorButton24.SelectedColor = themeInfoToEdit.User1;
            kryptonColorButton25.SelectedColor = themeInfoToEdit.User2;
        }

        private void kryptonColorButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            themeInfoToEdit.GetType().GetProperty(kryptonColorButton.Text).SetValue(themeInfoToEdit, kryptonColorButton.SelectedColor);
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
            configurationService.WriteFilesToDirectory("config/themes", Globals.themeInfos);
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (themeInfoToEdit != null)
            {
                Globals.themeInfos.Remove(themeInfoToEdit);
                themeListView.RemoveObject(themeInfoToEdit);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            NewThemeForm newThemeForm = new NewThemeForm();
            newThemeForm.ShowDialog();

            if (Globals.newThemeName != null)
            {
                ThemeInfo themeInfo = new ThemeInfo();
                themeInfo.Name = Globals.newThemeName;

                Globals.themeInfos.Add(themeInfo);
                themeListView.AddObject(themeInfo);
            }
        }
    }
}
