using pie.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
