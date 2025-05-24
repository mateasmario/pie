/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Windows.Forms;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using Krypton.Docking;
using Krypton.Navigator;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

namespace pie.Services
{
    internal class ThemingService
    {
        public void SetPaletteToTheme(KryptonCustomPaletteBase KryptonCustomPaletteBase, ThemeInfo theme)
        {
            // Common
            KryptonCustomPaletteBase.Common.StateCommon.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.Common.StateCommon.Content.ShortText.Color2 = theme.Fore;
            KryptonCustomPaletteBase.Common.StateCommon.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateCommon.Back.Color2 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateCommon.Border.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateCommon.Border.Color2 = theme.Secondary;

            KryptonCustomPaletteBase.Common.StateOthers.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.Common.StateOthers.Content.ShortText.Color2 = theme.Fore;
            KryptonCustomPaletteBase.Common.StateOthers.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateOthers.Back.Color2 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateOthers.Border.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.Common.StateOthers.Border.Color2 = theme.Secondary;

            // Buttons
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = theme.Button;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color1 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color2 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color1 = theme.ButtonFrame;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color2 = theme.ButtonFrame;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color1 = theme.ButtonFrame;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color2 = theme.ButtonFrame;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Back.ColorStyle = PaletteColorStyle.Solid;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Border.ColorStyle = PaletteColorStyle.Solid;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = theme.Button;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = theme.Button;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color2 = theme.Fore;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = theme.ButtonHover;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = theme.ButtonHover;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = theme.ButtonHover;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Border.Color2 = theme.ButtonHover;

            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            KryptonCustomPaletteBase.ButtonStyles.ButtonStandalone.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;

            // Form
            KryptonCustomPaletteBase.FormStyles.FormCommon.StateCommon.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.FormStyles.FormCommon.StateCommon.Back.Color2 = theme.Primary;
            KryptonCustomPaletteBase.FormStyles.FormCommon.StateCommon.Border.Color1 = theme.FormBorder;
            KryptonCustomPaletteBase.FormStyles.FormCommon.StateCommon.Border.Color2 = theme.FormBorder;

            // HeaderStyle
            KryptonCustomPaletteBase.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color2 = theme.Secondary;

            KryptonCustomPaletteBase.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = theme.Primary;

            KryptonCustomPaletteBase.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // InputControl
            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Back.Color2 = theme.Primary;

            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Border.Color1 = theme.FormBorder;
            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Border.Color2 = theme.FormBorder;

            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // Label
            KryptonCustomPaletteBase.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.LabelStyles.LabelCommon.StateCommon.ShortText.Color2 = theme.Fore;

            // PanelStyle
            KryptonCustomPaletteBase.PanelStyles.PanelCommon.StateCommon.Color1 = theme.Primary;
            KryptonCustomPaletteBase.PanelStyles.PanelCommon.StateCommon.Color2 = theme.Primary;

            // Tabs
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateCommon.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateCommon.Back.Color2 = theme.Primary;
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateSelected.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateSelected.Back.Color2 = theme.Secondary;
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.TabStyles.TabCommon.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // ContextMenu
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlInner.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlInner.Back.Color2 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateHighlight.ItemHighlight.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.ContextMenu.StateHighlight.ItemHighlight.Back.ColorStyle = PaletteColorStyle.Solid;

            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlOuter.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlOuter.Back.Color2 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlOuter.Border.Color1 = theme.FormBorder;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ControlOuter.Border.Color2 = theme.FormBorder;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemImageColumn.Border.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemImageColumn.Border.Color2 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Border.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Border.Color2 = theme.Primary;

            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Back.Color2 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.Heading.Content.ShortText.Color2 = theme.Fore;

            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color2 = theme.Fore;

            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemImageColumn.Back.Color1 = theme.Primary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemImageColumn.Back.Color2 = theme.Primary;

            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemHighlight.Back.Color1 = theme.Secondary;
            KryptonCustomPaletteBase.ContextMenu.StateCommon.ItemHighlight.Back.Color2 = theme.Secondary;

            KryptonCustomPaletteBase.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color1 = theme.Fore;
            KryptonCustomPaletteBase.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color2 = theme.Fore;
        }

        public void ColorizeTextArea(Scintilla TextArea, ThemeInfo theme)
        {
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 15;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = theme.Fore;
            TextArea.CaretForeColor = theme.Fore;
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = theme.Primary;
            TextArea.SetSelectionBackColor(true, theme.Selection);
            TextArea.CaretLineBackColor = theme.CaretLineBack;
            TextArea.StyleClearAll();

            InitNumberMargin(TextArea, theme);
            InitCodeFolding(TextArea, theme);
        }

        private void InitNumberMargin(Scintilla TextArea, ThemeInfo theme)
        {

            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = theme.NumberMargin;
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = theme.Fore;
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = theme.Fore;
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = theme.NumberMargin;

            TextArea.Margins[0].Width = 24;
        }

        private void InitCodeFolding(Scintilla TextArea, ThemeInfo theme)
        {

            TextArea.SetFoldMarginColor(true, theme.Folding);
            TextArea.SetFoldMarginHighlightColor(true, theme.Folding);

            TextArea.Margins[3].Type = MarginType.Symbol;
            TextArea.Margins[3].Mask = Marker.MaskFolders;
            TextArea.Margins[3].Sensitive = true;
            TextArea.Margins[3].Width = 20;
        }

        public void SetPaletteToObjects(Control control, KryptonCustomPaletteBase palette)
        {
            if (control is KryptonForm)
            {
                ((KryptonForm)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonPanel)
            {
                ((KryptonPanel)control).Palette = palette;
            }
            else if (control is KryptonLabel)
            {
                ((KryptonLabel)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonButton)
            {
                ((KryptonButton)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonTextBox)
            {
                ((KryptonTextBox)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonRichTextBox)
            {
                ((KryptonRichTextBox)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonCheckButton)
            {
                ((KryptonCheckButton)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonColorButton)
            {
                ((KryptonColorButton)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonNavigator)
            {
                ((KryptonNavigator)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonDockableNavigator)
            {
                ((KryptonDockableNavigator)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonHeaderGroup)
            {
                ((KryptonHeaderGroup)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonGroupBox)
            {
                ((KryptonGroupBox)control).LocalCustomPalette = palette;
            }
            else if (control is KryptonComboBox)
            {
                ((KryptonComboBox)control).LocalCustomPalette = palette;
            }


            foreach (Control child in control.Controls)
            {
                SetPaletteToObjects(child, palette);
            }
        }
    }
}
