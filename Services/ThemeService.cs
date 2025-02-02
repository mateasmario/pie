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

using System.Windows.Forms;
using pie.Classes;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

namespace pie.Services
{
    internal class ThemeService
    {
        public void SetPaletteToTheme(KryptonPalette kryptonPalette, ThemeInfo theme)
        {
            // Common
            kryptonPalette.Common.StateCommon.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.Common.StateCommon.Content.ShortText.Color2 = theme.Fore;
            kryptonPalette.Common.StateCommon.Back.Color1 = theme.Secondary;
            kryptonPalette.Common.StateCommon.Back.Color2 = theme.Secondary;
            kryptonPalette.Common.StateCommon.Border.Color1 = theme.Secondary;
            kryptonPalette.Common.StateCommon.Border.Color2 = theme.Secondary;

            kryptonPalette.Common.StateOthers.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.Common.StateOthers.Content.ShortText.Color2 = theme.Fore;
            kryptonPalette.Common.StateOthers.Back.Color1 = theme.Secondary;
            kryptonPalette.Common.StateOthers.Back.Color2 = theme.Secondary;
            kryptonPalette.Common.StateOthers.Border.Color1 = theme.Secondary;
            kryptonPalette.Common.StateOthers.Border.Color2 = theme.Secondary;

            // Buttons
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = theme.Button;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color1 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color2 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color1 = theme.ButtonFrame;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color2 = theme.ButtonFrame;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color1 = theme.ButtonFrame;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color2 = theme.ButtonFrame;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = theme.Button;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = theme.Button;

            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color2 = theme.Fore;

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = theme.ButtonHover;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = theme.ButtonHover;

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = theme.ButtonHover;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color2 = theme.ButtonHover;

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;

            // Form
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color1 = theme.Primary;
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color2 = theme.Primary;
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color1 = theme.FormBorder;
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color2 = theme.FormBorder;

            // HeaderStyle
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color1 = theme.Secondary;
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color2 = theme.Secondary;

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = theme.Primary;
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = theme.Primary;

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // InputControl
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color1 = theme.Primary;
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color2 = theme.Primary;

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color1 = theme.FormBorder;
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color2 = theme.FormBorder;

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // Label
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = theme.Fore;
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color2 = theme.Fore;

            // PanelStyle
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color1 = theme.Primary;
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color2 = theme.Primary;

            // Tabs
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color1 = theme.Primary;
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color2 = theme.Primary;
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color1 = theme.Secondary;
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color2 = theme.Secondary;
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color2 = theme.Fore;

            // ContextMenu
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color2 = theme.Primary;
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.Color1 = theme.Secondary;
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color2 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color1 = theme.FormBorder;
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color2 = theme.FormBorder;
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color2 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color2 = theme.Primary;

            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color2 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = theme.Fore;
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color2 = theme.Fore;

            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color1 = theme.Fore;
            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color2 = theme.Fore;

            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color1 = theme.Primary;
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color2 = theme.Primary;

            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color1 = theme.Secondary;
            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color2 = theme.Secondary;

            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color1 = theme.Fore;
            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color2 = theme.Fore;
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

        public void SetPaletteToObjects(Control control, KryptonPalette palette)
        {
            if (control is KryptonForm)
            {
                ((KryptonForm)control).Palette = palette;
            }
            else if (control is KryptonPanel)
            {
                ((KryptonPanel)control).Palette = palette;
            }
            else if (control is KryptonLabel)
            {
                ((KryptonLabel)control).Palette = palette;
            }
            else if (control is KryptonButton)
            {
                ((KryptonButton)control).Palette = palette;
            }
            else if (control is KryptonTextBox)
            {
                ((KryptonTextBox)control).Palette = palette;
            }
            else if (control is KryptonRichTextBox)
            {
                ((KryptonRichTextBox)control).Palette = palette;
            }
            else if (control is KryptonCheckButton)
            {
                ((KryptonCheckButton)control).Palette = palette;
            }
            else if (control is KryptonColorButton)
            {
                ((KryptonColorButton)control).Palette = palette;
            }
            else if (control is KryptonNavigator)
            {
                ((KryptonNavigator)control).Palette = palette;
            }
            else if (control is KryptonDockableNavigator)
            {
                ((KryptonDockableNavigator)control).Palette = palette;
            }
            else if (control is KryptonHeaderGroup)
            {
                ((KryptonHeaderGroup)control).Palette = palette;
            }
            else if (control is KryptonGroupBox)
            {
                ((KryptonGroupBox)control).Palette = palette;
            }
            else if (control is KryptonComboBox)
            {
                ((KryptonComboBox)control).Palette = palette;
            }
            
            foreach (Control child in control.Controls)
            {
                SetPaletteToObjects(child, palette);
            }
        }
    }
}
