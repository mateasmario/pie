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
