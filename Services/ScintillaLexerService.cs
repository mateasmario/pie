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
using System.Reflection;
using System.Text.RegularExpressions;
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

namespace pie.Services
{
    public class ScintillaLexerService
    {
        private static Dictionary<string, Color> parserColorDictionary;

        public static void ResetDictionary(ThemeInfo activeTheme)
        {
            parserColorDictionary = new Dictionary<string, Color>();
            parserColorDictionary["Default"] = activeTheme.Fore;
            parserColorDictionary["Background"] = activeTheme.Primary;
            parserColorDictionary["Fore"] = activeTheme.Fore;
            parserColorDictionary["CaretLine"] = activeTheme.CaretLineBack;
            parserColorDictionary["Selection"] = activeTheme.Selection;
            parserColorDictionary["Comment"] = activeTheme.Comment;
            parserColorDictionary["CommentLine"] = activeTheme.CommentLine;
            parserColorDictionary["Number"] = activeTheme.Number;
            parserColorDictionary["Word"] = activeTheme.Word;
            parserColorDictionary["String"] = activeTheme.String;
            parserColorDictionary["Operator"] = activeTheme.Operator;
            parserColorDictionary["Preprocessor"] = activeTheme.Preprocessor;
            parserColorDictionary["Triple"] = activeTheme.Triple;
            parserColorDictionary["CommentBlock"] = activeTheme.CommentBlock;
            parserColorDictionary["Decorator"] = activeTheme.Decorator;
            parserColorDictionary["Attribute"] = activeTheme.Attribute;
            parserColorDictionary["Entity"] = activeTheme.Entity;
            parserColorDictionary["User1"] = activeTheme.User1;
            parserColorDictionary["User2"] = activeTheme.User2;
        }

        public string ConvertColorToHex(Color c)
        {
            return $"{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        public Color ConvertHexToColor(string value)
        {
            ColorConverter converter = new ColorConverter();
            Color color = (Color)converter.ConvertFromString("#FF" + value);
            return color;
        }

        public void SetAutocompleteMenuKeywords(AutocompleteMenu autocompleteMenu, List<string> keywords)
        {
            autocompleteMenu.AppearInterval = 1;
            autocompleteMenu.SetAutocompleteItems(keywords);
        }

        public void ConfigureLexer(AutocompleteMenu autocompleteMenu, LanguageDefinition languageDefinition, Scintilla scintilla, ThemeInfo activeTheme)
        {
            Lexer lexer = (Lexer)Enum.Parse(typeof(Lexer), languageDefinition.Lexer);
            scintilla.Lexer = lexer;

            if (languageDefinition.Keywords != null)
            {
                scintilla.SetKeywords(0, languageDefinition.Keywords);
            }

            SetAutocompleteMenuKeywords(autocompleteMenu, languageDefinition.Keywords.Split(' ').ToList());
            EnableFolding(scintilla, activeTheme);

            if (languageDefinition.WordChars != null)
            {
                scintilla.WordChars = languageDefinition.WordChars;
            }

            if (languageDefinition.Properties != null)
            {
                foreach (PropertyInfo propertyInfo in languageDefinition.Properties.GetType().GetProperties())
                {
                    string formattedPropertyName = Regex.Replace(propertyInfo.Name, "([A-Z])", ".$1", RegexOptions.Compiled).Trim().ToLower().Substring(1);
                    object propertyValue = propertyInfo.GetValue(languageDefinition.Properties);

                    if (propertyValue != null)
                    {
                        scintilla.SetProperty(formattedPropertyName, propertyValue.ToString());
                    }
                }
            }

            SetDefaultStyles(scintilla, lexer);
        }

        private void SetDefaultStyles(Scintilla scintilla, Lexer lexer)
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

        private void EnableFolding(Scintilla scintilla, ThemeInfo activeTheme)
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
                scintilla.Markers[i].SetForeColor(activeTheme.Folding); // styles for [+] and [-]
                scintilla.Markers[i].SetBackColor(activeTheme.Fore); // styles for [+] and [-]
            }

            // Enable automatic folding
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
        }

        private void AddCppStyles(Scintilla scintilla)
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

        private void AddJsonStyles(Scintilla scintilla)
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

        private void AddLuaStyles(Scintilla scintilla)
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

        private void AddPythonStyles(Scintilla scintilla)
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

        private void AddXmlStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Xml.Attribute].ForeColor = parserColorDictionary["Attribute"];
            scintilla.Styles[Style.Xml.Entity].ForeColor = parserColorDictionary["Entity"];
            scintilla.Styles[Style.Xml.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Xml.Tag].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Xml.TagEnd].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Xml.DoubleString].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Xml.SingleString].ForeColor = parserColorDictionary["String"];
        }

        private void AddHtmlStyles(Scintilla scintilla)
        {
            scintilla.Styles[Style.Html.Attribute].ForeColor = parserColorDictionary["Attribute"];
            scintilla.Styles[Style.Html.Entity].ForeColor = parserColorDictionary["Entity"];
            scintilla.Styles[Style.Html.Comment].ForeColor = parserColorDictionary["Comment"];
            scintilla.Styles[Style.Html.Tag].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Html.TagEnd].ForeColor = parserColorDictionary["Word"];
            scintilla.Styles[Style.Html.DoubleString].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[Style.Html.SingleString].ForeColor = parserColorDictionary["String"];
        }

        private void AddSqlStyles(Scintilla scintilla)
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

        public void SetLexer(AutocompleteMenu autocompleteMenu, string extension, Scintilla scintilla, ThemeInfo activeTheme)
        {
            foreach(LanguageMapping languageMapping in Globals.languageMappings)
            {
                if (languageMapping.Extension.Equals(extension))
                {
                    foreach(LanguageDefinition languageDefinition in Globals.languageDefinitions)
                    {
                        if (languageDefinition.Name.Equals(languageMapping.Language))
                        {
                            ConfigureLexer(autocompleteMenu, languageDefinition, scintilla, activeTheme);
                            return;
                        }
                    }
                }
            }
        }
    }
}
