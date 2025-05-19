/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Drawing;
using System.Windows.Forms;

namespace pie.Classes
{
    internal class CustomColorTable : ProfessionalColorTable
    {
        public ThemeInfo InputThemeInfo { get; set; }

        public CustomColorTable()
        {
            base.UseSystemColors = false;
        }
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return InputThemeInfo.Secondary;
            }
        }
        public override System.Drawing.Color MenuBorder
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override System.Drawing.Color MenuItemBorder
        {
            get { return InputThemeInfo.Primary; }
        }
        public override Color MenuItemSelected
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return InputThemeInfo.Secondary; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return InputThemeInfo.Primary; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ButtonCheckedGradientBegin
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ButtonCheckedGradientMiddle
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return InputThemeInfo.Primary; }
        }

        public override Color ButtonCheckedHighlight
        {
            get { return InputThemeInfo.Primary; }
        }
    }
}
