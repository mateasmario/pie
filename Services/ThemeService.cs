﻿/** Copyright (C) 2023  Mario-Mihai Mateas
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
using pie.Classes;

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
            lightColorDictionary["Comment"] = Color.FromArgb(180, 180, 180);
            lightColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
            lightColorDictionary["Number"] = Color.FromArgb(194, 127, 25);
            lightColorDictionary["Word"] = Color.FromArgb(50, 125, 168);
            lightColorDictionary["Word2"] = Color.FromArgb(50, 125, 168);
            lightColorDictionary["Word3"] = Color.FromArgb(138, 43, 226);
            lightColorDictionary["Word4"] = Color.FromArgb(138, 43, 226);
            lightColorDictionary["String"] = Color.FromArgb(43, 158, 131);
            lightColorDictionary["Operator"] = Color.FromArgb(128, 0, 128);
            lightColorDictionary["Preprocessor"] = Color.FromArgb(128, 0, 128);
            lightColorDictionary["Triple"] = Color.FromArgb(127, 0, 0);
            lightColorDictionary["CommentBlock"] = Color.FromArgb(127, 127, 127);
            lightColorDictionary["Decorator"] = Color.FromArgb(186, 119, 2);
            lightColorDictionary["Attribute"] = Color.FromArgb(128, 0, 128);
            lightColorDictionary["Entity"] = Color.FromArgb(128, 0, 128);
            lightColorDictionary["User1"] = Color.FromArgb(128, 128, 128);
            lightColorDictionary["User2"] = Color.FromArgb(255, 0, 128);

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
            darkColorDictionary["Comment"] = Color.FromArgb(192, 192, 192);
            darkColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
            darkColorDictionary["Number"] = Color.FromArgb(242, 161, 39);
            darkColorDictionary["Word"] = Color.FromArgb(60, 170, 232);
            darkColorDictionary["Word2"] = Color.FromArgb(60, 170, 232);
            darkColorDictionary["Word3"] = Color.FromArgb(145, 35, 247);
            darkColorDictionary["Word4"] = Color.FromArgb(145, 35, 247);
            darkColorDictionary["String"] = Color.FromArgb(56, 207, 172);
            darkColorDictionary["Operator"] = Color.FromArgb(222, 2, 222);
            darkColorDictionary["Preprocessor"] = Color.FromArgb(222, 2, 222);
            darkColorDictionary["Triple"] = Color.FromArgb(207, 2, 2);
            darkColorDictionary["CommentBlock"] = Color.FromArgb(153, 153, 153);
            darkColorDictionary["Decorator"] = Color.FromArgb(230, 222, 5);
            darkColorDictionary["Attribute"] = Color.FromArgb(222, 2, 222);
            darkColorDictionary["Entity"] = Color.FromArgb(222, 2, 222);
            darkColorDictionary["User1"] = Color.FromArgb(128, 128, 128);
            darkColorDictionary["User2"] = Color.FromArgb(255, 0, 128);
        }

        public static Color GetColor(string color)
        {
            try
            {
                return Globals.colorDictionary[color];
            }
            catch(Exception ex)
            {
                NotificationExceptionHandlerService.handleException(ex);
                Environment.Exit(0);
                return Color.White;
            }
        }

        public static string GetIconType(string theme)
        {
            if (theme == "light")
            {
                return "dark";
            }
            else if (theme == "dark")
            {
                return "light";
            }
            else {
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
            }

            return "dark";
        }

        public static Dictionary<string, Color> GetColorDictionary(String theme)
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
            using (var textReader = File.OpenText(file))
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
                            if (token.ToLower() == "theme")
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

                        using (var textReader = File.OpenText(file))
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
                                        if (token.ToLower() == "icontype")
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

        internal static void WriteThemesToDirectory(string directory, List<ThemeInfo> themeInfos)
        {
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + directory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }


            foreach (ThemeInfo themeInfo in themeInfos)
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + directory + "/" + themeInfo.Name + ".json";
                var file = File.Create(path);
                file.Close();
                TextWriter textWriter = new StreamWriter(path);

                using (JsonWriter writer = new JsonTextWriter(textWriter))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();

                    foreach (KeyValuePair<string, Color> entry in themeInfo.ColorDictionary)
                    {
                        writer.WritePropertyName(entry.Key);
                        writer.WriteValue(ExtractRGBFromColor(entry.Value));
                    }

                    writer.WritePropertyName("IconType");

                    if (themeInfo.IconType == null)
                    {
                        themeInfo.IconType = "dark";
                    }

                    writer.WriteValue(themeInfo.IconType.ToString());

                    writer.WriteEndObject();
                }
            }
        }
        private static string ExtractRGBFromColor(Color color)
        {
            return color.R + ", " + color.G + ", " + color.B;
        }
    }
}
