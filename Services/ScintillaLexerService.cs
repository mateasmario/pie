using System;
using System.Collections.Generic;
using System.Drawing;
using ScintillaNET;
using System.IO;

namespace pie.Services
{
    internal class ScintillaLexerService
    {
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

        public static void ConfigureLexer(string language, Scintilla scintilla)
        {
            Dictionary<string, string> configColorDictionary = Globals.getConfigColorDictionary();

            if (language == "c")
            {
                scintilla.Lexer = Lexer.Cpp;
                scintilla.Styles[ScintillaNET.Style.Cpp.Default].ForeColor = ConvertHexToColor(configColorDictionary["Default"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = ConvertHexToColor(configColorDictionary["Number"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = ConvertHexToColor(configColorDictionary["Word"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = ConvertHexToColor(configColorDictionary["String"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = ConvertHexToColor(configColorDictionary["Character"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = ConvertHexToColor(configColorDictionary["Verbatim"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = ConvertHexToColor(configColorDictionary["StringEol"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = ConvertHexToColor(configColorDictionary["Operator"]);
                scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = ConvertHexToColor(configColorDictionary["Preprocessor"]);

                scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                scintilla.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
            }
            else if (language == "lua")
            {
                // Extracted from the Lua Scintilla lexer and SciTE .properties file

                var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var numericChars = "0123456789";
                var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

                // Configure the Lua lexer styles
                scintilla.Styles[Style.Lua.Default].ForeColor = ConvertHexToColor(configColorDictionary["Default"]);
                scintilla.Styles[Style.Lua.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[Style.Lua.CommentLine].ForeColor = ConvertHexToColor(configColorDictionary["CommentLine"]);
                scintilla.Styles[Style.Lua.Number].ForeColor = ConvertHexToColor(configColorDictionary["Number"]);
                scintilla.Styles[Style.Lua.Word].ForeColor = ConvertHexToColor(configColorDictionary["Word"]);
                scintilla.Styles[Style.Lua.Word2].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);
                scintilla.Styles[Style.Lua.Word3].ForeColor = ConvertHexToColor(configColorDictionary["Word3"]);
                scintilla.Styles[Style.Lua.Word4].ForeColor = ConvertHexToColor(configColorDictionary["Word4"]);
                scintilla.Styles[Style.Lua.String].ForeColor = ConvertHexToColor(configColorDictionary["String"]);
                scintilla.Styles[Style.Lua.Character].ForeColor = ConvertHexToColor(configColorDictionary["Character"]);
                scintilla.Styles[Style.Lua.LiteralString].ForeColor = ConvertHexToColor(configColorDictionary["LiteralString"]);
                scintilla.Styles[Style.Lua.StringEol].BackColor = ConvertHexToColor(configColorDictionary["StringEol"]);
                scintilla.Styles[Style.Lua.Operator].ForeColor = ConvertHexToColor(configColorDictionary["Operator"]);
                scintilla.Styles[Style.Lua.Preprocessor].ForeColor = ConvertHexToColor(configColorDictionary["Preprocessor"]);
                scintilla.Lexer = Lexer.Lua;
                scintilla.WordChars = alphaChars + numericChars + accentedChars;

                // Console.WriteLine(scintilla.DescribeKeywordSets());

                // Keywords
                scintilla.SetKeywords(0, "and break do else elseif end for function if in local nil not or repeat return then until while" + " false true" + " goto");
                // Basic Functions
                scintilla.SetKeywords(1, "assert collectgarbage dofile error _G getmetatable ipairs loadfile next pairs pcall print rawequal rawget rawset setmetatable tonumber tostring type _VERSION xpcall string table math coroutine io os debug" + " getfenv gcinfo load loadlib loadstring require select setfenv unpack _LOADED LUA_PATH _REQUIREDNAME package rawlen package bit32 utf8 _ENV");
                // String Manipulation & Mathematical
                scintilla.SetKeywords(2, "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" + " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" + " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" + " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset");
                // Input and Output Facilities and System Facilities
                scintilla.SetKeywords(3, "coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload");

                // Instruct the lexer to calculate folding
                scintilla.SetProperty("fold", "1");
                scintilla.SetProperty("fold.compact", "1");

                /*
                // Configure a margin to display folding symbols
                scintilla.Margins[2].Type = MarginType.Symbol;
                scintilla.Margins[2].Mask = Marker.MaskFolders;
                scintilla.Margins[2].Sensitive = true;
                scintilla.Margins[2].Width = 20;

                // Set colors for all folding markers
                for (int i = 25; i <= 31; i++)
                {
                    scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                    scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
                }
                */

                // Configure folding markers with respective symbols
                scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
                scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
                scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
                scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
                scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
                scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
                scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

                // Enable automatic folding
                scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
            }
            else if (language == "python")
            {
                // Set the lexer
                scintilla.Lexer = Lexer.Python;

                // Known lexer properties:
                // "tab.timmy.whinge.level",
                // "lexer.python.literals.binary",
                // "lexer.python.strings.u",
                // "lexer.python.strings.b",
                // "lexer.python.strings.over.newline",
                // "lexer.python.keywords2.no.sub.identifiers",
                // "fold.quotes.python",
                // "fold.compact",
                // "fold"

                // Some properties we like
                scintilla.SetProperty("tab.timmy.whinge.level", "1");
                scintilla.SetProperty("fold", "1");

                /*
                // Use margin 2 for fold markers
                scintilla.Margins[2].Type = MarginType.Symbol;
                scintilla.Margins[2].Mask = Marker.MaskFolders;
                scintilla.Margins[2].Sensitive = true;
                scintilla.Margins[2].Width = 20;

                // Reset folder markers
                for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
                {
                    scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                    scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
                }
                */

                // Style the folder markers
                scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
                scintilla.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
                scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
                scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
                scintilla.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
                scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
                scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
                scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
                scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

                // Enable automatic folding
                scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

                // Set the styles
                scintilla.Styles[Style.Python.Default].ForeColor = ConvertHexToColor(configColorDictionary["Default"]);
                scintilla.Styles[Style.Python.CommentLine].ForeColor = ConvertHexToColor(configColorDictionary["CommentLine"]);
                scintilla.Styles[Style.Python.CommentLine].Italic = true;
                scintilla.Styles[Style.Python.Number].ForeColor = ConvertHexToColor(configColorDictionary["Number"]);
                scintilla.Styles[Style.Python.String].ForeColor = ConvertHexToColor(configColorDictionary["String"]);
                scintilla.Styles[Style.Python.Character].ForeColor = ConvertHexToColor(configColorDictionary["Character"]);
                scintilla.Styles[Style.Python.Word].ForeColor = ConvertHexToColor(configColorDictionary["Word"]);
                scintilla.Styles[Style.Python.Word].Bold = true;
                scintilla.Styles[Style.Python.Triple].ForeColor = ConvertHexToColor(configColorDictionary["Triple"]);
                scintilla.Styles[Style.Python.TripleDouble].ForeColor = ConvertHexToColor(configColorDictionary["TripleDouble"]);
                scintilla.Styles[Style.Python.ClassName].ForeColor = ConvertHexToColor(configColorDictionary["ClassName"]);
                scintilla.Styles[Style.Python.ClassName].Bold = true;
                scintilla.Styles[Style.Python.DefName].ForeColor = ConvertHexToColor(configColorDictionary["DefName"]);
                scintilla.Styles[Style.Python.DefName].Bold = true;
                scintilla.Styles[Style.Python.Operator].Bold = true;
                // scintilla.Styles[Style.Python.Identifier] ... your keywords styled here
                scintilla.Styles[Style.Python.CommentBlock].ForeColor = ConvertHexToColor(configColorDictionary["CommentBlock"]);
                scintilla.Styles[Style.Python.CommentBlock].Italic = true;
                scintilla.Styles[Style.Python.StringEol].ForeColor = ConvertHexToColor(configColorDictionary["StringEol"]);
                scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
                scintilla.Styles[Style.Python.StringEol].FillLine = true;
                scintilla.Styles[Style.Python.Word2].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);
                scintilla.Styles[Style.Python.Decorator].ForeColor = ConvertHexToColor(configColorDictionary["Decorator"]);

                // Important for Python
                scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;

                // Keyword lists:
                // 0 "Keywords",
                // 1 "Highlighted identifiers"

                var python2 = "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
                var python3 = "False None True and as assert break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield";
                var cython = "cdef cimport cpdef";

                scintilla.SetKeywords(0, python2 + " " + cython);
                // scintilla.SetKeywords(1, "add your own keywords here");
            }
            else if (language == "xml")
            {
                // Set the XML Lexer
                scintilla.Lexer = Lexer.Xml;

                // Enable folding
                scintilla.SetProperty("fold", "1");
                scintilla.SetProperty("fold.compact", "1");
                scintilla.SetProperty("fold.html", "1");

                /*
                // Use Margin 2 for fold markers
                scintilla.Margins[2].Type = MarginType.Symbol;
                scintilla.Margins[2].Mask = Marker.MaskFolders;
                scintilla.Margins[2].Sensitive = true;
                scintilla.Margins[2].Width = 20;

                // Reset folder markers
                for (int i = Marker.FolderEnd; i <= Marker.FolderOpen; i++)
                {
                    scintilla.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                    scintilla.Markers[i].SetBackColor(SystemColors.ControlDark);
                }
                */

                // Style the folder markers
                scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
                scintilla.Markers[Marker.Folder].SetBackColor(SystemColors.ControlText);
                scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
                scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
                scintilla.Markers[Marker.FolderEnd].SetBackColor(SystemColors.ControlText);
                scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
                scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
                scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
                scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

                // Enable automatic folding
                scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

                // Set the Styles
                scintilla.Styles[Style.Xml.Attribute].ForeColor = ConvertHexToColor(configColorDictionary["Attribute"]);
                scintilla.Styles[Style.Xml.Entity].ForeColor = ConvertHexToColor(configColorDictionary["Entity"]);
                scintilla.Styles[Style.Xml.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[Style.Xml.Tag].ForeColor = ConvertHexToColor(configColorDictionary["Tag"]);
                scintilla.Styles[Style.Xml.TagEnd].ForeColor = ConvertHexToColor(configColorDictionary["TagEnd"]);
                scintilla.Styles[Style.Xml.DoubleString].ForeColor = ConvertHexToColor(configColorDictionary["DoubleString"]);
                scintilla.Styles[Style.Xml.SingleString].ForeColor = ConvertHexToColor(configColorDictionary["SingleString"]);
            }
            else if (language == "mssql")
            {
                // Set the SQL Lexer
                scintilla.Lexer = Lexer.Sql;

                // Set the Styles
                scintilla.Styles[Style.Sql.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[Style.Sql.CommentLine].ForeColor = ConvertHexToColor(configColorDictionary["CommentLine"]);
                scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = ConvertHexToColor(configColorDictionary["CommentLineDoc"]);
                scintilla.Styles[Style.Sql.Number].ForeColor = ConvertHexToColor(configColorDictionary["Number"]);
                scintilla.Styles[Style.Sql.Word].ForeColor = ConvertHexToColor(configColorDictionary["Word"]);
                scintilla.Styles[Style.Sql.Word2].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);
                scintilla.Styles[Style.Sql.User1].ForeColor = ConvertHexToColor(configColorDictionary["User1"]);
                scintilla.Styles[Style.Sql.User2].ForeColor = ConvertHexToColor(configColorDictionary["User2"]);
                scintilla.Styles[Style.Sql.String].ForeColor = ConvertHexToColor(configColorDictionary["String"]);
                scintilla.Styles[Style.Sql.Character].ForeColor = ConvertHexToColor(configColorDictionary["Character"]);
                scintilla.Styles[Style.Sql.Operator].ForeColor = ConvertHexToColor(configColorDictionary["Operator"]);

                // Set keyword lists
                // Word = 0
                scintilla.SetKeywords(0, @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ");
                // Word2 = 1
                scintilla.SetKeywords(1, @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ");
                // User1 = 4
                scintilla.SetKeywords(4, @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ");
                // User2 = 5
                scintilla.SetKeywords(5, @"sys objects sysobjects ");
            }
        }

        public static void SetLexer(String extension, Scintilla scintilla)
        {
            switch (extension)
            {
                case "c":
                    ConfigureLexer("c", scintilla);
                    break;
                case "cpp":
                    ConfigureLexer("c", scintilla);
                    break;
                case "cs":
                    ConfigureLexer("c", scintilla);
                    break;
                case "java":
                    ConfigureLexer("c", scintilla);
                    break;
                case "lua":
                    ConfigureLexer("lua", scintilla);
                    break;
                case "py":
                    ConfigureLexer("python", scintilla);
                    break;
                case "xml":
                    ConfigureLexer("xml", scintilla);
                    break;
                case "html":
                    ConfigureLexer("xml", scintilla);
                    break;
                case "sql":
                    ConfigureLexer("mssql", scintilla);
                    break;
            }
        }

        public static void InitializeDefaultColorDictionary()
        {
            Dictionary<string, string> colorDictionary = new Dictionary<string, string>();

            colorDictionary["Default"] = "C0C0C0";
            colorDictionary["Comment"] = "008000";
            colorDictionary["CommentLine"] = "008000";
            colorDictionary["CommentLineDoc"] = "008000";
            colorDictionary["Number"] = "808000";
            colorDictionary["Word"] = "0000FF";
            colorDictionary["Word2"] = "8A2BE2";
            colorDictionary["Word3"] = "483D8B";
            colorDictionary["Word4"] = "48ED8B";
            colorDictionary["String"] = "A31515";
            colorDictionary["LiteralString"] = "A31515";
            colorDictionary["Character"] = "A31515";
            colorDictionary["Verbatim"] = "A31515";
            colorDictionary["StringEol"] = "FFC0CB";
            colorDictionary["Operator"] = "800080";
            colorDictionary["Preprocessor"] = "800080";
            colorDictionary["Triple"] = "7f0000";
            colorDictionary["TripleDouble"] = "7f0000";
            colorDictionary["ClassName"] = "0000FF";
            colorDictionary["DefName"] = "007F7F";
            colorDictionary["CommentBlock"] = "7F7F7F";
            colorDictionary["Decorator"] = "805000";
            colorDictionary["Attribute"] = "FF0000";
            colorDictionary["Entity"] = "FF0000";
            colorDictionary["Tag"] = "0000FF";
            colorDictionary["TagEnd"] = "0000FF";
            colorDictionary["DoubleString"] = "FF1493";
            colorDictionary["SingleString"] = "FF1493";
            colorDictionary["User1"] = "808080";
            colorDictionary["User2"] = "FF0080";
            colorDictionary["Background"] = "FFFFFF";
            colorDictionary["Fore"] = "000000";
            colorDictionary["CaretLine"] = "E6EFFA";
            colorDictionary["Selection"] = "FFE6A2";

            Globals.setDefaultColorDictionary(colorDictionary);
        }

        public static void InitializeConfigColorDictionary(string file)
        {
            Dictionary<string, string> configColorDictionary = new Dictionary<string, string>();
            IEnumerable<string> lines = File.ReadLines(file);

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(':');
                configColorDictionary[splitLine[0]] = splitLine[1];
            }

            Globals.setConfigColorDictionary(configColorDictionary);
        }
    }
}
