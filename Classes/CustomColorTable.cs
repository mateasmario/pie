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
using pie.Services;

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
                return Globals.theme.Secondary;
            }
        }
        public override System.Drawing.Color MenuBorder
        {
            get { return Globals.theme.Secondary; }
        }
        public override System.Drawing.Color MenuItemBorder
        {
            get { return Globals.theme.Primary; }
        }
        public override Color MenuItemSelected
        {
            get { return Globals.theme.Secondary; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Globals.theme.Secondary; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Globals.theme.Secondary; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Globals.theme.Secondary; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Globals.theme.Secondary; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return Globals.theme.Primary; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ButtonCheckedGradientBegin
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ButtonCheckedGradientMiddle
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ButtonCheckedGradientEnd
        {
            get { return Globals.theme.Primary; }
        }

        public override Color ButtonCheckedHighlight
        {
            get { return Globals.theme.Primary; }
        }
    }
}
