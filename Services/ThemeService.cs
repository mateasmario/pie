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

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;
using pie.Classes;

namespace pie.Services
{
    internal class ThemeService
    {
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
            return Globals.colorDictionary[color];
        }

        public static string GetIconType(string theme)
        {
            foreach (ThemeInfo themeInfo in Globals.themeInfos)
            {
                if (themeInfo.Name == theme)
                {
                    if (themeInfo.IconType != null)
                    {
                        return themeInfo.IconType;
                    }
                    else
                    {
                        return "dark";
                    }
                }
            }

            return "dark";
        }

        public static Dictionary<string, Color> SetColorDictionary(String theme)
        {
            Dictionary<string, Color> colorDictionary = null;

            InitializeDictionaries();

            if (theme == "light")
            {
                colorDictionary = lightColorDictionary;
            }
            else if (theme == "dark")
            {
                colorDictionary = darkColorDictionary;
            }
            else
            {
                foreach (ThemeInfo themeInfo in Globals.themeInfos)
                {
                    if (themeInfo.Name == theme)
                    {
                        colorDictionary = themeInfo.ColorDictionary;
                        break;
                    }
                }

                if (colorDictionary == null)
                {
                    colorDictionary = lightColorDictionary;
                }
            }

            return colorDictionary;
        }

        public static void SetPaletteToTheme(KryptonPalette kryptonPalette, string theme)
        {
            // Common
            kryptonPalette.Common.StateCommon.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.Common.StateCommon.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];
            kryptonPalette.Common.StateCommon.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Back.Color2 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Border.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateCommon.Border.Color2 = Globals.colorDictionary["Secondary"];

            kryptonPalette.Common.StateOthers.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.Common.StateOthers.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];
            kryptonPalette.Common.StateOthers.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Back.Color2 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Border.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.Common.StateOthers.Border.Color2 = Globals.colorDictionary["Secondary"];

            // Buttons
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color1 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Back.Color2 = Globals.colorDictionary["Button"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color1 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.Color2 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color1 = Globals.colorDictionary["ButtonFrame"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.Color2 = Globals.colorDictionary["ButtonFrame"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color1 = Globals.colorDictionary["ButtonFrame"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.Color2 = Globals.colorDictionary["ButtonFrame"];

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideDefault.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Border.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color1 = Globals.colorDictionary["Button"];
            kryptonPalette.ButtonStyles.ButtonStandalone.OverrideFocus.Back.Color2 = Globals.colorDictionary["Button"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateCommon.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color1 = Globals.colorDictionary["ButtonHover"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.Color2 = Globals.colorDictionary["ButtonHover"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color1 = Globals.colorDictionary["ButtonHover"];
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.Color2 = Globals.colorDictionary["ButtonHover"];

            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Back.ColorStyle = PaletteColorStyle.Solid;
            kryptonPalette.ButtonStyles.ButtonStandalone.StateTracking.Border.ColorStyle = PaletteColorStyle.Solid;

            // Form
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Back.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color1 = Globals.colorDictionary["FormBorder"];
            kryptonPalette.FormStyles.FormCommon.StateCommon.Border.Color2 = Globals.colorDictionary["FormBorder"];

            // HeaderStyle
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.HeaderStyles.HeaderDockInactive.StateCommon.Back.Color2 = Globals.colorDictionary["Secondary"];

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = Globals.colorDictionary["Primary"];

            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];

            // InputControl
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Back.Color2 = Globals.colorDictionary["Primary"];

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color1 = Globals.colorDictionary["FormBorder"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Border.Color2 = Globals.colorDictionary["FormBorder"];

            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.InputControlStyles.InputControlCommon.StateCommon.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];

            // Label
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.LabelStyles.LabelCommon.StateCommon.ShortText.Color2 = Globals.colorDictionary["Fore"];

            // PanelStyle
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.PanelStyles.PanelCommon.StateCommon.Color2 = Globals.colorDictionary["Primary"];

            // Tabs
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Back.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.TabStyles.TabCommon.StateSelected.Back.Color2 = Globals.colorDictionary["Secondary"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.TabStyles.TabCommon.StateCommon.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];

            // ContextMenu
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlInner.Back.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.ContextMenu.StateHighlight.ItemHighlight.Back.ColorStyle = PaletteColorStyle.Solid;

            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Back.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color1 = Globals.colorDictionary["FormBorder"];
            kryptonPalette.ContextMenu.StateCommon.ControlOuter.Border.Color2 = Globals.colorDictionary["FormBorder"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Border.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Border.Color2 = Globals.colorDictionary["Primary"];

            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Back.Color2 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateCommon.Heading.Content.ShortText.Color2 = Globals.colorDictionary["Fore"];

            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateCommon.ItemTextStandard.ShortText.Color2 = Globals.colorDictionary["Fore"];

            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color1 = Globals.colorDictionary["Primary"];
            kryptonPalette.ContextMenu.StateCommon.ItemImageColumn.Back.Color2 = Globals.colorDictionary["Primary"];

            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color1 = Globals.colorDictionary["Secondary"];
            kryptonPalette.ContextMenu.StateCommon.ItemHighlight.Back.Color2 = Globals.colorDictionary["Secondary"];

            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color1 = Globals.colorDictionary["Fore"];
            kryptonPalette.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color2 = Globals.colorDictionary["Fore"];
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
                        else if (jsonTextReader.TokenType == JsonToken.String)
                        {
                            if (token == "theme")
                            {
                                Globals.theme = jsonTextReader.Value.ToString();
                            }
                        }
                    }
                }
            }
        }

        internal static void WriteThemeToFile(string file, string theme)
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

        public static List<ThemeInfo> LoadThemesFromFolder(string directory)
        {
            List<ThemeInfo> themeInfos = new List<ThemeInfo>();

            ThemeInfo themeInfo = null;

            string token = null;

            string[] files = null;

            try
            {
                files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    if (ParsingService.GetFileExtension(file) == "json")
                    {
                        themeInfo = new ThemeInfo();
                        themeInfo.Name = ParsingService.RemoveFileExtension(ParsingService.GetFileName(file));
                        themeInfo.ColorDictionary = new Dictionary<string, Color>();

                        using (var textReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + file))
                        {
                            JsonTextReader jsonTextReader = new JsonTextReader(textReader);

                            while (jsonTextReader.Read())
                            {
                                if (jsonTextReader.Value != null)
                                {
                                    if (jsonTextReader.TokenType == JsonToken.PropertyName)
                                    {
                                        token = jsonTextReader.Value.ToString();
                                    }
                                    else if (jsonTextReader.TokenType == JsonToken.String)
                                    {
                                        if (token == "IconType")
                                        {
                                            themeInfo.IconType = jsonTextReader.Value.ToString();
                                        }
                                        else
                                        {
                                            string colorValue = jsonTextReader.Value.ToString();

                                            string[] rgbValues = colorValue.Split(',');

                                            int red, green, blue;
                                            int.TryParse(rgbValues[0].Trim(), out red);
                                            int.TryParse(rgbValues[1].Trim(), out green);
                                            int.TryParse(rgbValues[2].Trim(), out blue);

                                            themeInfo.ColorDictionary[token] = Color.FromArgb(red, green, blue);
                                        }
                                    }
                                }
                            }
                        }
                        themeInfos.Add(themeInfo);
                    }
                }

                return themeInfos;
            }
            catch (DirectoryNotFoundException ex)
            {
                return themeInfos;
            }
        }
    }
}
