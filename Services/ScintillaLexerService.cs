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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.IO;
using pie.Classes;

/**
 * ScintillaNET provides the text editors used in pie.
 * 
 * Copyright (c) 2017, Jacob Slusser, https://github.com/jacobslusser
*/
using ScintillaNET;

/**
 * AutocompleteMenuNS is a namespace that comes from AutoCompleteMenu-ScintillaNet. It is used for various Autocomplete suggestions while writing code.
 * 
 * AutoCompleteMenu-ScintillaNet is licensed under the GNU Lesser General Public License (LGPLv3).
 * For more information related to the license, navigate to Licenses/gpl-3.0.txt
 * 
 */
using AutocompleteMenuNS;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Docking;

/**
 * Used for reading and writing to JSON config files
 * 
 * Copyright (c) 2007 James Newton-King
 */
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace pie.Services
{
    internal class ScintillaLexerService
    {
        private static Dictionary<string, Color> parserColorDictionary;
        private static bool dictionaryInitialized = false;

        public static string ConvertColorToHex(Color c)
        {
            return $"{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        public static Color ConvertHexToColor(string value)
        {
            ColorConverter converter = new ColorConverter();
            Color color = (Color)converter.ConvertFromString("#FF" + value);
            return color;
        }

        public static void InitializeParserDictionary()
        {
            parserColorDictionary = new Dictionary<string, Color>();

            parserColorDictionary["Default"] = ThemeService.GetColor("Fore");
            parserColorDictionary["Background"] = ThemeService.GetColor("Primary");
            parserColorDictionary["Fore"] = ThemeService.GetColor("Fore");
            parserColorDictionary["CaretLine"] = ThemeService.GetColor("CaretLineBack");
            parserColorDictionary["Selection"] = ThemeService.GetColor("Selection");

            if (Globals.theme == "dark")
            {
                parserColorDictionary["Comment"] = Color.FromArgb(192, 192, 192);
                parserColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
                parserColorDictionary["Number"] = Color.FromArgb(242, 161, 39);
                parserColorDictionary["Word"] = Color.FromArgb(60, 170, 232);
                parserColorDictionary["String"] = Color.FromArgb(56, 207, 172);
                parserColorDictionary["Operator"] = Color.FromArgb(222, 2, 222);
                parserColorDictionary["Preprocessor"] = Color.FromArgb(222, 2, 222);
                parserColorDictionary["Triple"] = Color.FromArgb(207, 2, 2);
                parserColorDictionary["CommentBlock"] = Color.FromArgb(153, 153, 153);
                parserColorDictionary["Decorator"] = Color.FromArgb(230, 222, 5);
                parserColorDictionary["Attribute"] = Color.FromArgb(222, 2, 222);
                parserColorDictionary["Entity"] = Color.FromArgb(222, 2, 222);
                parserColorDictionary["User1"] = Color.FromArgb(128, 128, 128);
                parserColorDictionary["User2"] = Color.FromArgb(255, 0, 128);

            }
            else if (Globals.theme == "light")
            {
                parserColorDictionary["Comment"] = Color.FromArgb(180, 180, 180);
                parserColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
                parserColorDictionary["Number"] = Color.FromArgb(194, 127, 25);
                parserColorDictionary["Word"] = Color.FromArgb(50, 125, 168);
                parserColorDictionary["String"] = Color.FromArgb(43, 158, 131);
                parserColorDictionary["Operator"] = Color.FromArgb(128, 0, 128);
                parserColorDictionary["Preprocessor"] = Color.FromArgb(128, 0, 128);
                parserColorDictionary["Triple"] = Color.FromArgb(127, 0, 0);
                parserColorDictionary["CommentBlock"] = Color.FromArgb(127, 127, 127);
                parserColorDictionary["Decorator"] = Color.FromArgb(186, 119, 2);
                parserColorDictionary["Attribute"] = Color.FromArgb(128, 0, 128);
                parserColorDictionary["Entity"] = Color.FromArgb(128, 0, 128);
                parserColorDictionary["User1"] = Color.FromArgb(128, 128, 128);
                parserColorDictionary["User2"] = Color.FromArgb(255, 0, 128);
            }
            else
            {
                parserColorDictionary["Comment"] = ThemeService.GetColor("Comment");
                parserColorDictionary["CommentLine"] = ThemeService.GetColor("CommentLine");
                parserColorDictionary["Number"] = ThemeService.GetColor("Number");
                parserColorDictionary["Word"] = ThemeService.GetColor("Word");
                parserColorDictionary["String"] = ThemeService.GetColor("String");
                parserColorDictionary["Operator"] = ThemeService.GetColor("Operator");
                parserColorDictionary["Preprocessor"] = ThemeService.GetColor("Preprocessor");
                parserColorDictionary["Triple"] = ThemeService.GetColor("Triple");
                parserColorDictionary["CommentBlock"] = ThemeService.GetColor("CommentBlock");
                parserColorDictionary["Decorator"] = ThemeService.GetColor("Decorator");
                parserColorDictionary["Attribute"] = ThemeService.GetColor("Attribute");
                parserColorDictionary["Entity"] = ThemeService.GetColor("Entity");
                parserColorDictionary["User1"] = ThemeService.GetColor("User1");
                parserColorDictionary["User2"] = ThemeService.GetColor("User2");
            }
        }

        public static void SetAutocompleteMenuKeywords(AutocompleteMenu autocompleteMenu, List<string> keywords)
        {
            autocompleteMenu.AppearInterval = 1;
            autocompleteMenu.SetAutocompleteItems(keywords);
        }

        public static void ConfigureLexer(LanguageDefinition languageDefinition, Scintilla scintilla, KryptonDockableNavigator tabControl, int index)
        {
            if (!dictionaryInitialized)
            {
                InitializeParserDictionary();
                dictionaryInitialized = true;
            }

            Lexer lexer = (Lexer)Enum.Parse(typeof(Lexer), languageDefinition.Lexer);
            scintilla.Lexer = lexer;

            if (languageDefinition.Keywords != null)
            {
                scintilla.SetKeywords(0, languageDefinition.Keywords);
            }

            SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), languageDefinition.Keywords.Split(' ').ToList());
            EnableFolding(scintilla);

            if (languageDefinition.WordChars != null)
            {
                scintilla.WordChars = languageDefinition.WordChars;
            }

            if (languageDefinition.Properties != null && languageDefinition.Properties.Count > 0)
            {
                foreach (LanguageProperty property in languageDefinition.Properties)
                {
                    if (property != null && property.Name != null && property.Value != null)
                    {
                        scintilla.SetProperty(property.Name, property.Value);
                    }
                }
            }

            SetDefaultStyles(scintilla, lexer);
        }

        private static void SetDefaultStyles(Scintilla scintilla, Lexer lexer)
        {
            if (lexer == Lexer.Cpp)
            {
                AddCppStyles(scintilla);
            }
            else if (lexer == Lexer.Json)
            {
                AddJsonStyles(scintilla);
            }
            else if (lexer == Lexer.Lua)
            {
                AddLuaStyles(scintilla);
            }
            else if (lexer == Lexer.Python)
            {
                AddPythonStyles(scintilla);
            }
            else if (lexer == Lexer.Xml)
            {
                AddXmlStyles(scintilla);
            }
            else if (lexer == Lexer.Html)
            {
                AddHtmlStyles(scintilla);
            }
            else if (lexer == Lexer.Sql)
            {
                AddSqlStyles(scintilla);
            }
        }

        private static void EnableFolding(Scintilla scintilla)
        {
            // Enable folding
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");

            // Style the folder markers
            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.CirclePlus;
            scintilla.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.CircleMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.CirclePlusConnected;
            scintilla.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.CircleMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            for (int i = 25; i <= 31; i++)
            {
                scintilla.Markers[i].SetForeColor(ThemeService.GetColor("Folding")); // styles for [+] and [-]
                scintilla.Markers[i].SetBackColor(ThemeService.GetColor("Fore")); // styles for [+] and [-]
            }

            // Enable automatic folding
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
        }

        private static void AddCppStyles(Scintilla scintilla)
        {
            scintilla.Styles[ScintillaNET.Style.Cpp.Default].ForeColor = parserColorDictionary["Default"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = parserColorDictionary["Number"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = parserColorDictionary["Operator"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = parserColorDictionary["Preprocessor"];
        }

        private static void AddJsonStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Json.Default].ForeColor = parserColorDictionary["Default"];
            scintilla.Styles[Style.Json.Number].ForeColor = parserColorDictionary["Number"];
            scintilla.Styles[Style.Json.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Json.StringEol].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Json.LineComment].ForeColor = parserColorDictionary["CommentLine"];
            scintilla.Styles[Style.Json.BlockComment].ForeColor = parserColorDictionary["CommentBlock"];
            scintilla.Styles[Style.Json.Operator].ForeColor = parserColorDictionary["Operator"];
            scintilla.Styles[Style.Json.Keyword].ForeColor = parserColorDictionary["Word"];
        }

        private static void AddLuaStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Lua.Default].ForeColor = parserColorDictionary["Default"];
            scintilla.Styles[Style.Lua.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Lua.CommentLine].ForeColor = parserColorDictionary["CommentLine"];
            scintilla.Styles[Style.Lua.Number].ForeColor = parserColorDictionary["Number"];
            scintilla.Styles[Style.Lua.Word].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Lua.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Lua.Character].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Lua.LiteralString].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Lua.StringEol].BackColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Lua.Operator].ForeColor = parserColorDictionary["Operator"];
            scintilla.Styles[Style.Lua.Preprocessor].ForeColor = parserColorDictionary["Preprocessor"];
        }

        private static void AddPythonStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Python.Default].ForeColor = parserColorDictionary["Default"];
            scintilla.Styles[Style.Python.CommentLine].ForeColor = parserColorDictionary["CommentLine"];
            scintilla.Styles[Style.Python.CommentLine].Italic = true;
            scintilla.Styles[Style.Python.Number].ForeColor = parserColorDictionary["Number"];
            scintilla.Styles[Style.Python.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Python.Character].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Python.Word].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Python.Word].Bold = true;
            scintilla.Styles[Style.Python.Triple].ForeColor = parserColorDictionary["Triple"];
            scintilla.Styles[Style.Python.TripleDouble].ForeColor = parserColorDictionary["Triple"];
            scintilla.Styles[Style.Python.ClassName].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Python.ClassName].Bold = true;
            scintilla.Styles[Style.Python.DefName].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Python.DefName].Bold = true;
            scintilla.Styles[Style.Python.Operator].Bold = true;
            scintilla.Styles[Style.Python.CommentBlock].ForeColor = parserColorDictionary["CommentBlock"];
            scintilla.Styles[Style.Python.CommentBlock].Italic = true;
            scintilla.Styles[Style.Python.StringEol].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
            scintilla.Styles[Style.Python.StringEol].FillLine = true;
            scintilla.Styles[Style.Python.Decorator].ForeColor = parserColorDictionary["Decorator"];
        }

        private static void AddXmlStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Xml.Attribute].ForeColor = parserColorDictionary["Attribute"];
            scintilla.Styles[Style.Xml.Entity].ForeColor = parserColorDictionary["Entity"];
            scintilla.Styles[Style.Xml.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Xml.Tag].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Xml.TagEnd].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Xml.DoubleString].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Xml.SingleString].ForeColor = parserColorDictionary["String"];
        }

        private static void AddHtmlStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Html.Attribute].ForeColor = parserColorDictionary["Attribute"];
            scintilla.Styles[Style.Html.Entity].ForeColor = parserColorDictionary["Entity"];
            scintilla.Styles[Style.Html.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Html.Tag].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Html.TagEnd].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Html.DoubleString].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Html.SingleString].ForeColor = parserColorDictionary["String"];
        }

        private static void AddSqlStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Sql.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Sql.CommentLine].ForeColor = parserColorDictionary["CommentLine"];
            scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = parserColorDictionary["CommentLine"];
            scintilla.Styles[Style.Sql.Number].ForeColor = parserColorDictionary["Number"];
            scintilla.Styles[Style.Sql.Word].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Sql.User1].ForeColor = parserColorDictionary["User1"];
            scintilla.Styles[Style.Sql.User2].ForeColor = parserColorDictionary["User2"];
            scintilla.Styles[Style.Sql.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Sql.Character].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Sql.Operator].ForeColor = parserColorDictionary["Operator"];
        }

        public static void SetLexer(String extension, Scintilla scintilla, KryptonDockableNavigator tabControl, int index)
        {
            foreach(LanguageMapping languageMapping in Globals.languageMappings)
            {
                if (languageMapping.Extension.Equals(extension))
                {
                    foreach(LanguageDefinition languageDefinition in Globals.languageDefinitions)
                    {
                        if (languageDefinition.Name.Equals(languageMapping.Language))
                        {
                            ConfigureLexer(languageDefinition, scintilla, tabControl, index);
                            return;
                        }
                    }
                }
            }
        }

        public static List<LanguageDefinition> LoadDefinitionsFromFolder(string directory)
        {
            List <LanguageDefinition> languageDefinitions = new List<LanguageDefinition>();

            LanguageDefinition languageDefinition = null;

            string token = null;

            string[] files = null;

            try
            {
                files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    if (ParsingService.GetFileExtension(file) == "json")
                    {
                        languageDefinition = new LanguageDefinition();
                        languageDefinition.Name = ParsingService.RemoveFileExtension(ParsingService.GetFileName(file));

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
                                        if (token.ToLower() == "lexer")
                                        {
                                            languageDefinition.Lexer = jsonTextReader.Value.ToString();
                                        }
                                        else if (token.ToLower() == "keywords")
                                        {
                                            languageDefinition.Keywords = jsonTextReader.Value.ToString();
                                        }
                                        else if (token.ToLower() == "wordchars")
                                        {
                                            languageDefinition.WordChars = jsonTextReader.Value.ToString();
                                        }
                                        else if (token.ToLower() == "properties")
                                        {
                                            List<LanguageProperty> languageProperties = new List<LanguageProperty>();

                                            JObject propertyJson = JObject.Parse(jsonTextReader.Value.ToString());
                                            foreach (var x in propertyJson)
                                            {
                                                LanguageProperty languageProperty = new LanguageProperty();
                                                
                                                if (x.Key == "name")
                                                {
                                                    languageProperty.Name = x.ToString();
                                                }
                                                else if (x.Key == "value")
                                                {
                                                    languageProperty.Value = x.ToString();
                                                    languageProperties.Add(languageProperty);
                                                }
                                            }

                                            languageDefinition.Properties = languageProperties;

                                        }
                                    }
                                }
                            }
                        }
                        languageDefinitions.Add(languageDefinition);
                    }
                }

                return languageDefinitions;
            }
            catch (DirectoryNotFoundException ex)
            {
                return languageDefinitions;
            }
        }
    }
}
