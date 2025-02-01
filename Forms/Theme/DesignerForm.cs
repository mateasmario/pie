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

namespace pie.Forms.Theme
{
    public partial class DesignerForm : KryptonForm
    {
        private ThemeService themeService = new ThemeService();
        private ConfigurationService configurationService = new ConfigurationService();

        public DesignerFormInput Input { get; set; }

        private ThemeInfo themeInfoToEdit;

        public DesignerForm()
        {
            InitializeComponent();
        }

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            themeListView.SetObjects(Input.ThemeInfos);

            generalAspectsGroupBox.Visible = false;
            scintillaGroupBox.Visible = false;
        }

        private void SynchronizeObjectListViewWithTheme()
        {
            themeListView.ShowGroups = false;
            themeListView.UseCustomSelectionColors = true;
            themeListView.FullRowSelect = true;
            themeListView.MultiSelect = false;
            themeListView.HeaderStyle = ColumnHeaderStyle.None;

            themeListView.BackColor = Input.ActiveTheme.Primary;
            themeListView.ForeColor = Input.ActiveTheme.Fore;
            themeListView.HighlightBackgroundColor = Input.ActiveTheme.Secondary;
            themeListView.HighlightForegroundColor = Input.ActiveTheme.Fore;
            themeListView.UnfocusedHighlightBackgroundColor = Input.ActiveTheme.Secondary;
            themeListView.UnfocusedHighlightForegroundColor = Input.ActiveTheme.Fore;

            ThemeNameColumn.FillsFreeSpace = true;
        }

        private void themeListView_DoubleClick(object sender, EventArgs e)
        {
            generalAspectsGroupBox.Visible = true;
            scintillaGroupBox.Visible = true;

            if (themeListView.SelectedItems.Count == 1)
            {
                foreach(ThemeInfo themeInfo in Input.ThemeInfos)
                {
                    if (themeInfo.Name.Equals(themeListView.SelectedItem.Text))
                    {
                        themeInfoToEdit = themeInfo;
                        break;
                    }
                }
            }

            primaryColorButton.SelectedColor = themeInfoToEdit.Primary;
            secondaryColorButton.SelectedColor = themeInfoToEdit.Secondary;
            buttonColorButton.SelectedColor = themeInfoToEdit.Button;
            buttonFrameColorButton.SelectedColor = themeInfoToEdit.ButtonFrame;
            buttonHoverColorButton.SelectedColor = themeInfoToEdit.ButtonHover;
            foreColorButton.SelectedColor = themeInfoToEdit.Fore;
            formBorderColorButton.SelectedColor = themeInfoToEdit.FormBorder;
            selectionColorButton.SelectedColor = themeInfoToEdit.Selection;
            caretLineBackColorButton.SelectedColor = themeInfoToEdit.CaretLineBack;
            numberMarginColorButton.SelectedColor = themeInfoToEdit.NumberMargin;
            foldingColorButton.SelectedColor = themeInfoToEdit.Folding;

            if (themeInfoToEdit.IconType == null || themeInfoToEdit.IconType.ToLower().Equals("dark"))
            {
                iconTypeComboBox.SelectedIndex = 0;
            }
            else
            {
                iconTypeComboBox.SelectedIndex = 1;
            }

            commentColorButton.SelectedColor = themeInfoToEdit.Comment;
            commentLineColorButton.SelectedColor = themeInfoToEdit.CommentLine;
            commentBlockColorButton.SelectedColor = themeInfoToEdit.CommentBlock;
            wordColorButton.SelectedColor = themeInfoToEdit.Word;
            stringColorButton.SelectedColor = themeInfoToEdit.String;
            numberColorButton.SelectedColor = themeInfoToEdit.Number;
            operatorColorButton.SelectedColor = themeInfoToEdit.Operator;
            preprocessorColorButton.SelectedColor = themeInfoToEdit.Preprocessor;
            tripleColorButton.SelectedColor = themeInfoToEdit.Triple;
            decoratorColorButton.SelectedColor = themeInfoToEdit.Decorator;
            attributeColorButton.SelectedColor = themeInfoToEdit.Attribute;
            entityColorButton.SelectedColor = themeInfoToEdit.Entity;
            user1ColorButton.SelectedColor = themeInfoToEdit.User1;
            user2ColorButton.SelectedColor = themeInfoToEdit.User2;
        }

        private void kryptonColorButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            themeInfoToEdit.GetType().GetProperty(kryptonColorButton.Text).SetValue(themeInfoToEdit, kryptonColorButton.SelectedColor);
        }

        private void kryptonComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iconTypeComboBox.SelectedIndex == 0)
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
            configurationService.WriteFilesToDirectory("config/themes", Input.ThemeInfos, "json");
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (themeInfoToEdit != null)
            {
                Input.ThemeInfos.Remove(themeInfoToEdit);
                themeListView.RemoveObject(themeInfoToEdit);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            NewThemeForm newThemeForm = new NewThemeForm();

            NewThemeFormInput newThemeFormInput = new NewThemeFormInput();
            newThemeFormInput.Palette = Input.Palette;
            newThemeFormInput.ThemeInfos = Input.ThemeInfos;
            newThemeFormInput.EditorProperties = Input.EditorProperties;

            newThemeForm.Input = newThemeFormInput;

            newThemeForm.ShowDialog();

            if (newThemeForm.Output.NewThemeName != null)
            {
                ThemeInfo themeInfo = new ThemeInfo();
                themeInfo.Name = newThemeForm.Output.NewThemeName;

                Input.ThemeInfos.Add(themeInfo);
                themeListView.AddObject(themeInfo);
            }
        }
    }
}
