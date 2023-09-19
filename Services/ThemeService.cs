using BrightIdeasSoftware;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie.Services
{
    internal class ThemeService
    {
        private static bool dictionariesInitialized = false;

        private static Dictionary<string, Color> lightColorDictionary;
        private static Dictionary<string, Color> darkColorDictionary;

        private static void InitializeDictionaries()
        {
            lightColorDictionary = new Dictionary<string, Color>();
            darkColorDictionary = new Dictionary<string, Color>();

            lightColorDictionary["Primary"] = Color.FromArgb(245, 245, 245);
            lightColorDictionary["Secondary"] = Color.FromArgb(235, 235, 235);
            lightColorDictionary["Button"] = Color.FromArgb(220, 220, 220);
            lightColorDictionary["ButtonFrame"] = Color.FromArgb(190, 190, 190);
            lightColorDictionary["ButtonHover"] = Color.FromArgb(205, 205, 205);
            lightColorDictionary["Fore"] = Color.Black;
            lightColorDictionary["FormBorder"] = Color.FromArgb(175, 175, 175);

            darkColorDictionary["Primary"] = Color.FromArgb(40, 40, 40);
            darkColorDictionary["Secondary"] = Color.FromArgb(50, 50, 50);
            darkColorDictionary["Button"] = Color.FromArgb(70, 70, 70);
            darkColorDictionary["ButtonFrame"] = Color.FromArgb(100, 100, 100);
            darkColorDictionary["ButtonHover"] = Color.FromArgb(85, 85, 85);
            darkColorDictionary["Fore"] = Color.White;
            darkColorDictionary["FormBorder"] = Color.FromArgb(90, 90, 90);
        }

        public static Color GetSelectionColor()
        {
            if (Globals.theme == 1)
            {
                return Color.FromArgb(84, 84, 84);
            }
            else
            {
                return Color.FromArgb(192, 192, 192);
            }
        }

        public static Color GetCaretLineBackColor()
        {
            if (Globals.theme == 1)
            {
                return Color.FromArgb(64, 64, 64);
            }
            else
            {
                return Color.FromArgb(225, 225, 225);
            }
        }

        public static Color GetForeColor()
        {
            if (Globals.theme == 1)
            {
                return darkColorDictionary["Fore"];
            }
            else
            {
                return lightColorDictionary["Fore"];
            }
        }

        public static Color GetTextAreaBackColor()
        {
            if (Globals.theme == 1)
            {
                return darkColorDictionary["Primary"];
            }
            else
            {
                return lightColorDictionary["Primary"];
            }
        }

        public static Color GetNumberMarginColor()
        {
            if (Globals.theme == 1)
            {
                return Color.FromArgb(46, 46, 46);
            }
            else
            {
                return Color.FromArgb(225, 225, 225);
            }
        }

        public static Color GetFoldingColor()
        {
            if (Globals.theme == 1)
            {
                return Color.FromArgb(50, 50, 50);
            }
            else
            {
                return Color.FromArgb(229, 229, 229);
            }
        }

        public static Color GetPrimaryColor()
        {
            if (Globals.theme == 1)
            {
                return darkColorDictionary["Primary"];
            }
            else
            {
                return lightColorDictionary["Primary"];
            }
        }

        public static Color GetSecondaryColor()
        {
            if (Globals.theme == 1)
            {
                return darkColorDictionary["Secondary"];
            }
            else
            {
                return lightColorDictionary["Secondary"];
            }
        }

        public static void SetPaletteToTheme(KryptonDockableNavigator tabControl, KryptonPage kryptonPage, MenuStrip menuStrip, KryptonPalette kryptonPalette, ObjectListView objectListView, int theme)
        {
            if (!dictionariesInitialized)
            {
                dictionariesInitialized = true;
                InitializeDictionaries();
            }

            Dictionary<string, Color> colorDictionary;

            if (theme == 0)
            {
                colorDictionary = lightColorDictionary;
            }
            else
            {
                colorDictionary = darkColorDictionary;
            }

            // MenuStrip & Children
            if (menuStrip != null)
            {
                menuStrip.BackColor = colorDictionary["Primary"];
                menuStrip.ForeColor = colorDictionary["Fore"];

                foreach (ToolStripMenuItem toolStripMenuItem in menuStrip.Items)
                {
                    toolStripMenuItem.DropDown.BackColor = colorDictionary["Primary"];
                    toolStripMenuItem.DropDown.ForeColor = colorDictionary["Fore"];
                    toolStripMenuItem.ImageTransparentColor = colorDictionary["Primary"];

                    if (toolStripMenuItem.HasDropDownItems)
                    {
                        foreach (ToolStripMenuItem toolStripMenuItemChild in toolStripMenuItem.DropDownItems)
                        {
                            toolStripMenuItemChild.BackColor = colorDictionary["Primary"];
                            toolStripMenuItemChild.ForeColor = colorDictionary["Fore"];

                            if (toolStripMenuItemChild.HasDropDownItems)
                            {
                                foreach (ToolStripMenuItem toolStripMenuItemChild2 in toolStripMenuItemChild.DropDownItems)
                                {
                                    toolStripMenuItemChild2.BackColor = colorDictionary["Primary"];
                                    toolStripMenuItemChild2.ForeColor = colorDictionary["Fore"];
                                }
                            }
                        }
                    }
                }
            }

            // Common
            kryptonPalette.Common.StateCommon.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.Common.StateCommon.Content.ShortText.Color2 = colorDictionary["Fore"];
            kryptonPalette.Common.StateCommon.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Back.Color2 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Border.Color1 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Border.Color2 = colorDictionary["Secondary"];

            kryptonPalette.Common.StateOthers.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.Common.StateOthers.Content.ShortText.Color2 = colorDictionary["Fore"];
            kryptonPalette.Common.StateOthers.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Back.Color2 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Border.Color1 = colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Border.Color2 = colorDictionary["Secondary"];

            // Buttons
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = colorDictionary["Button"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color1 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color2 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color1 = colorDictionary["ButtonFrame"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color2 = colorDictionary["ButtonFrame"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color1 = colorDictionary["ButtonFrame"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color2 = colorDictionary["ButtonFrame"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = colorDictionary["Button"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color2 = colorDictionary["Fore"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = colorDictionary["ButtonHover"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = colorDictionary["ButtonHover"];            
            
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = colorDictionary["ButtonHover"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color2 = colorDictionary["ButtonHover"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;

            // Form
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color2 = colorDictionary["Primary"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color1 = colorDictionary["FormBorder"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color2 = colorDictionary["FormBorder"];

            // HeaderStyle
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color2 = colorDictionary["Secondary"];

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = colorDictionary["Primary"];

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = colorDictionary["Fore"];

            // InputControl
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color2 = colorDictionary["Primary"];

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color2 = colorDictionary["Fore"];

            // Label
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color2 = colorDictionary["Fore"];

            // PanelStyle
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color1 = colorDictionary["Primary"];
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color2 = colorDictionary["Primary"];

            // Tabs
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color2 = colorDictionary["Primary"];
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color2 = colorDictionary["Secondary"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color2 = colorDictionary["Fore"];

            if (tabControl != null)
            {
                tabControl.StateCommon.Panel.Color1 = colorDictionary["Primary"];
                tabControl.StateCommon.Panel.Color2 = colorDictionary["Primary"];
            }

            // ContextMenu
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color2 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color2 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color1 = colorDictionary["FormBorder"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color2 = colorDictionary["FormBorder"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color2 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color2 = colorDictionary["Primary"];

            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color2 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color2 = colorDictionary["Fore"];

            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color2 = colorDictionary["Fore"];

            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color1 = colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color2 = colorDictionary["Primary"];

            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color1 = colorDictionary["Secondary"];
            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color2 = colorDictionary["Secondary"];

            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color1 = colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color2 = colorDictionary["Fore"];

            // ObjectListView
            objectListView.BackColor = colorDictionary["Primary"];
            objectListView.ForeColor = colorDictionary["Fore"];

            var headerstyle = new HeaderFormatStyle();
            headerstyle.Normal.BackColor = colorDictionary["Secondary"];
            headerstyle.Normal.ForeColor = colorDictionary["Fore"];

            headerstyle.Hot.BackColor = colorDictionary["ButtonHover"];
            headerstyle.Hot.ForeColor = colorDictionary["Fore"];

            headerstyle.Pressed.BackColor = colorDictionary["ButtonFrame"];
            headerstyle.Pressed.ForeColor = colorDictionary["Fore"];            
           
            objectListView.HeaderFormatStyle = headerstyle;

            if (theme == 1)
            {
                kryptonPage.ImageSmall = Properties.Resources.plus_white;
            }
            else
            {
                kryptonPage.ImageSmall = Properties.Resources.plus_blue;
            }
        }
    }
}
