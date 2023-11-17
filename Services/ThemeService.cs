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
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;

/**
 * This namespace provides access to the ObjectListView control.
 * Licensed under GNU General Public License (GPL 3.0). For more info, see https://www.gnu.org/licenses/gpl-3.0.html 
 *
 * Author of the library: Phillip Piper
 * Copyright 2006-2016 Bright Ideas Software
 */
using BrightIdeasSoftware;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;

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
            lightColorDictionary["Selection"] = Color.FromArgb(184, 184, 184);
            lightColorDictionary["CaretLineBack"] = Color.FromArgb(230, 230, 230);
            lightColorDictionary["NumberMargin"] = Color.FromArgb(225, 225, 225);
            lightColorDictionary["Folding"] = Color.FromArgb(235, 235, 235);

            darkColorDictionary["Primary"] = Color.FromArgb(40, 40, 40);
            darkColorDictionary["Secondary"] = Color.FromArgb(50, 50, 50);
            darkColorDictionary["Button"] = Color.FromArgb(70, 70, 70);
            darkColorDictionary["ButtonFrame"] = Color.FromArgb(100, 100, 100);
            darkColorDictionary["ButtonHover"] = Color.FromArgb(85, 85, 85);
            darkColorDictionary["Fore"] = Color.White;
            darkColorDictionary["FormBorder"] = Color.FromArgb(90, 90, 90);
            darkColorDictionary["Selection"] = Color.FromArgb(92, 92, 92);
            darkColorDictionary["ButtonHover"] = Color.FromArgb(85, 85, 85);
            darkColorDictionary["CaretLineBack"] = Color.FromArgb(50, 50, 50);
            darkColorDictionary["NumberMargin"] = Color.FromArgb(46, 46, 46);
            darkColorDictionary["Folding"] = Color.FromArgb(50, 50, 50);
        }

        public static Color GetColor(string color)
        {
            if (Globals.theme == 1)
            {
                return darkColorDictionary[color];
            }
            else
            {
                return lightColorDictionary[color];
            }
        }

        public static void SetPaletteToTheme(KryptonPalette kryptonPalette, int theme)
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

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color1 = colorDictionary["FormBorder"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color2 = colorDictionary["FormBorder"];

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
        }

        public static void GetTheme(string file)
        {
            using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
            {
                JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                string token = null;

                while (jsonTextReader.Read())
                {
                    if (jsonTextReader.Value != null)
                    {
                        if (jsonTextReader.TokenType == JsonToken.PropertyName)
                        {
                            token = jsonTextReader.Value.ToString();
                        }
                        else if (jsonTextReader.TokenType == JsonToken.Integer)
                        {
                            if (token == "theme")
                            {
                                int.TryParse(jsonTextReader.Value.ToString(), out Globals.theme);
                            }
                        }
                    }
                }
            }
        }

        internal static void WriteThemeToFile(string file, int theme)
        {
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + file, "");

            TextWriter textWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + file);

            using (JsonWriter writer = new JsonTextWriter(textWriter))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();
                writer.WritePropertyName("theme");
                writer.WriteValue(Globals.theme);
                writer.WriteEndObject();
            }
        }
    }

}
