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

using pie.Services;
using System.Drawing;
using System.Windows.Forms;

namespace pie.Classes
{
    internal class CustomColorTable : ProfessionalColorTable
    {
        public CustomColorTable()
        {
            base.UseSystemColors = false;
        }
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return ThemeService.GetSecondaryColor();
            }
        }
        public override System.Drawing.Color MenuBorder
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override System.Drawing.Color MenuItemBorder
        {
            get { return ThemeService.GetPrimaryColor(); }
        }
        public override Color MenuItemSelected
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return ThemeService.GetSecondaryColor(); }
        }
        public override Color MenuStripGradientBegin
        {
            get { return ThemeService.GetPrimaryColor(); }
        }
        public override Color MenuStripGradientEnd
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return ThemeService.GetPrimaryColor();  }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ButtonCheckedGradientBegin
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ButtonCheckedGradientMiddle
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return ThemeService.GetPrimaryColor(); }
        }

        public override Color ButtonCheckedHighlight
        {
            get { return ThemeService.GetPrimaryColor(); }
        }
    }
}
