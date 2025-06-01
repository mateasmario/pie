/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Windows.Forms;
using pie.Services;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using System.IO;
using pie.Constants;

namespace pie.Forms.Theme
{
    public partial class DesignerForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();
        private ConfigurationService configurationService = new ConfigurationService();

        public DesignerFormInput Input { get; set; }

        private ThemeInfo themeInfoToEdit;

        public DesignerForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
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

        private void DesignerForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            SynchronizeObjectListViewWithTheme();
            themeListView.SetObjects(Input.ThemeInfos);

            themeTabControl.Visible = false;

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

            darkIconsRadioButton.StateCommon.ShortText.Color1 = Input.ActiveTheme.Fore;
            darkIconsRadioButton.StateCommon.ShortText.Color2 = Input.ActiveTheme.Fore;
            lightIconsRadioButton.StateCommon.ShortText.Color1 = Input.ActiveTheme.Fore;
            lightIconsRadioButton.StateCommon.ShortText.Color2 = Input.ActiveTheme.Fore;
        }

        private void themeListView_DoubleClick(object sender, EventArgs e)
        {
            if (themeListView.SelectedItems.Count == 1)
            {
                foreach (ThemeInfo themeInfo in Input.ThemeInfos)
                {
                    if (themeInfo.Name.Equals(themeListView.SelectedItem.Text))
                    {
                        themeInfoToEdit = themeInfo;
                        break;
                    }
                }
            }

            themeTabControl.Visible = true;


            if (themeInfoToEdit.IconType == null || themeInfoToEdit.IconType.ToLower().Equals("dark"))
            {
                darkIconsRadioButton.Checked = true;
            }
            else
            {
                lightIconsRadioButton.Checked = true;
            }

            primaryPanel.StateCommon.Color1 = themeInfoToEdit.Primary;
            secondaryPanel.StateCommon.Color1 = themeInfoToEdit.Secondary;
            buttonPanel.StateCommon.Color1 = themeInfoToEdit.Button;
            buttonFramePanel.StateCommon.Color1 = themeInfoToEdit.ButtonFrame;
            buttonHoverPanel.StateCommon.Color1 = themeInfoToEdit.ButtonHover;
            forePanel.StateCommon.Color1 = themeInfoToEdit.Fore;
            formBorderPanel.StateCommon.Color1 = themeInfoToEdit.FormBorder;
            selectionPanel.StateCommon.Color1 = themeInfoToEdit.Selection;
            caretLineBackPanel.StateCommon.Color1 = themeInfoToEdit.CaretLineBack;
            numberMarginPanel.StateCommon.Color1 = themeInfoToEdit.NumberMargin;
            foldingPanel.StateCommon.Color1 = themeInfoToEdit.Folding;
            commentPanel.StateCommon.Color1 = themeInfoToEdit.Comment;
            commentLinePanel.StateCommon.Color1 = themeInfoToEdit.CommentLine;
            commentBlockPanel.StateCommon.Color1 = themeInfoToEdit.CommentBlock;
            numberPanel.StateCommon.Color1 = themeInfoToEdit.Number;
            wordPanel.StateCommon.Color1 = themeInfoToEdit.Word;
            stringPanel.StateCommon.Color1 = themeInfoToEdit.String;
            operatorPanel.StateCommon.Color1 = themeInfoToEdit.Operator;
            preprocessorPanel.StateCommon.Color1 = themeInfoToEdit.Preprocessor;
            triplePanel.StateCommon.Color1 = themeInfoToEdit.Triple;
            decoratorPanel.StateCommon.Color1 = themeInfoToEdit.Decorator;
            attributePanel.StateCommon.Color1 = themeInfoToEdit.Attribute;
            entityPanel.StateCommon.Color1 = themeInfoToEdit.Entity;
            user1Panel.StateCommon.Color1 = themeInfoToEdit.User1;
            user2Panel.StateCommon.Color1 = themeInfoToEdit.User2;

            if (darkIconsRadioButton.Checked == true)
            {
                themeInfoToEdit.IconType = "dark";
            }
            else
            {
                themeInfoToEdit.IconType = "light";
            }
        }

        private void kryptonColorButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            themeInfoToEdit.GetType().GetProperty(kryptonColorButton.Text).SetValue(themeInfoToEdit, kryptonColorButton.SelectedColor);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            configurationService.WriteFilesToDirectory(SpecialPaths.Themes, Input.ThemeInfos, "json");
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

        private void panel_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            DialogResult dialogResult = colorDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                KryptonPanel kryptonPanel = (KryptonPanel)sender;
                kryptonPanel.StateCommon.Color1 = colorDialog.Color;

                int indexOfPanel = kryptonPanel.Name.IndexOf("Panel");
                string propertyName = kryptonPanel.Name.Remove(indexOfPanel, "Panel".Length);
                propertyName = propertyName.Substring(0, 1).ToUpper() + propertyName.Substring(1);
                themeInfoToEdit.GetType().GetProperty(propertyName).SetValue(themeInfoToEdit, kryptonPanel.StateCommon.Color1);
            }
        }

        private void darkIconsRadioButton_Click(object sender, EventArgs e)
        {
            lightIconsRadioButton.Checked = false;
            themeInfoToEdit.IconType = "dark";
        }

        private void lightIconsRadioButton_Click(object sender, EventArgs e)
        {
            darkIconsRadioButton.Checked = false;
            themeInfoToEdit.IconType = "light";
        }
    }
}
