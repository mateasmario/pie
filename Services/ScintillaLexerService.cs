using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;

namespace pie.Services
{
    internal class ScintillaLexerService
    {
        public static void ConfigureLexer(string language, Scintilla scintilla)
        {
            if (language == "c")
            {
                scintilla.Lexer = Lexer.Cpp;
                scintilla.Styles[ScintillaNET.Style.Cpp.Default].ForeColor = Color.Silver;
                scintilla.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
                scintilla.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
                scintilla.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
                scintilla.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = Color.Olive;
                scintilla.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = Color.Blue;
                scintilla.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = Color.Blue;
                scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
                scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
                scintilla.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
                scintilla.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = Color.Pink;
                scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = Color.Purple;
                scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = Color.Maroon;

                scintilla.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
                scintilla.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
            }
            else if (language == "lua")
            {
                // Extracted from the Lua Scintilla lexer and SciTE .properties file

                var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var numericChars = "0123456789";
                var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

                // Configuring the default style with properties
                // we have common to every lexer style saves time.
                scintilla.StyleResetDefault();
                scintilla.Styles[Style.Default].Font = "Consolas";
                scintilla.Styles[Style.Default].Size = 10;
                scintilla.StyleClearAll();

                // Configure the Lua lexer styles
                scintilla.Styles[Style.Lua.Default].ForeColor = Color.Silver;
                scintilla.Styles[Style.Lua.Comment].ForeColor = Color.Green;
                scintilla.Styles[Style.Lua.CommentLine].ForeColor = Color.Green;
                scintilla.Styles[Style.Lua.Number].ForeColor = Color.Olive;
                scintilla.Styles[Style.Lua.Word].ForeColor = Color.Blue;
                scintilla.Styles[Style.Lua.Word2].ForeColor = Color.BlueViolet;
                scintilla.Styles[Style.Lua.Word3].ForeColor = Color.DarkSlateBlue;
                scintilla.Styles[Style.Lua.Word4].ForeColor = Color.DarkSlateBlue;
                scintilla.Styles[Style.Lua.String].ForeColor = Color.Red;
                scintilla.Styles[Style.Lua.Character].ForeColor = Color.Red;
                scintilla.Styles[Style.Lua.LiteralString].ForeColor = Color.Red;
                scintilla.Styles[Style.Lua.StringEol].BackColor = Color.Pink;
                scintilla.Styles[Style.Lua.Operator].ForeColor = Color.Purple;
                scintilla.Styles[Style.Lua.Preprocessor].ForeColor = Color.Maroon;
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
                // Reset the styles
                scintilla.StyleResetDefault();
                scintilla.Styles[Style.Default].Font = "Consolas";
                scintilla.Styles[Style.Default].Size = 10;
                scintilla.StyleClearAll(); // i.e. Apply to all

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
                scintilla.Styles[Style.Python.Default].ForeColor = Color.FromArgb(0x80, 0x80, 0x80);
                scintilla.Styles[Style.Python.CommentLine].ForeColor = Color.FromArgb(0x00, 0x7F, 0x00);
                scintilla.Styles[Style.Python.CommentLine].Italic = true;
                scintilla.Styles[Style.Python.Number].ForeColor = Color.FromArgb(0x00, 0x7F, 0x7F);
                scintilla.Styles[Style.Python.String].ForeColor = Color.FromArgb(0x7F, 0x00, 0x7F);
                scintilla.Styles[Style.Python.Character].ForeColor = Color.FromArgb(0x7F, 0x00, 0x7F);
                scintilla.Styles[Style.Python.Word].ForeColor = Color.FromArgb(0x00, 0x00, 0x7F);
                scintilla.Styles[Style.Python.Word].Bold = true;
                scintilla.Styles[Style.Python.Triple].ForeColor = Color.FromArgb(0x7F, 0x00, 0x00);
                scintilla.Styles[Style.Python.TripleDouble].ForeColor = Color.FromArgb(0x7F, 0x00, 0x00);
                scintilla.Styles[Style.Python.ClassName].ForeColor = Color.FromArgb(0x00, 0x00, 0xFF);
                scintilla.Styles[Style.Python.ClassName].Bold = true;
                scintilla.Styles[Style.Python.DefName].ForeColor = Color.FromArgb(0x00, 0x7F, 0x7F);
                scintilla.Styles[Style.Python.DefName].Bold = true;
                scintilla.Styles[Style.Python.Operator].Bold = true;
                // scintilla.Styles[Style.Python.Identifier] ... your keywords styled here
                scintilla.Styles[Style.Python.CommentBlock].ForeColor = Color.FromArgb(0x7F, 0x7F, 0x7F);
                scintilla.Styles[Style.Python.CommentBlock].Italic = true;
                scintilla.Styles[Style.Python.StringEol].ForeColor = Color.FromArgb(0x00, 0x00, 0x00);
                scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
                scintilla.Styles[Style.Python.StringEol].FillLine = true;
                scintilla.Styles[Style.Python.Word2].ForeColor = Color.FromArgb(0x40, 0x70, 0x90);
                scintilla.Styles[Style.Python.Decorator].ForeColor = Color.FromArgb(0x80, 0x50, 0x00);

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
                // Reset the styles
                scintilla.StyleResetDefault();
                scintilla.Styles[Style.Default].Font = "Consolas";
                scintilla.Styles[Style.Default].Size = 10;
                scintilla.StyleClearAll();

                // Set the XML Lexer
                scintilla.Lexer = Lexer.Xml;

                // Show line numbers
                scintilla.Margins[0].Width = 20;

                // Enable folding
                scintilla.SetProperty("fold", "1");
                scintilla.SetProperty("fold.compact", "1");
                scintilla.SetProperty("fold.html", "1");

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
                scintilla.StyleResetDefault();
                // I like fixed font for XML
                scintilla.Styles[Style.Default].Font = "Courier";
                scintilla.Styles[Style.Default].Size = 10;
                scintilla.StyleClearAll();
                scintilla.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
                scintilla.Styles[Style.Xml.Entity].ForeColor = Color.Red;
                scintilla.Styles[Style.Xml.Comment].ForeColor = Color.Green;
                scintilla.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
                scintilla.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
                scintilla.Styles[Style.Xml.DoubleString].ForeColor = Color.DeepPink;
                scintilla.Styles[Style.Xml.SingleString].ForeColor = Color.DeepPink;
            }
            else if (language == "mssql")
            {
                // Reset the styles
                scintilla.StyleResetDefault();
                scintilla.Styles[Style.Default].Font = "Courier New";
                scintilla.Styles[Style.Default].Size = 10;
                scintilla.StyleClearAll();

                // Set the SQL Lexer
                scintilla.Lexer = Lexer.Sql;

                // Show line numbers
                scintilla.Margins[0].Width = 20;

                // Set the Styles
                scintilla.Styles[Style.LineNumber].ForeColor = Color.FromArgb(255, 128, 128, 128);  //Dark Gray
                scintilla.Styles[Style.LineNumber].BackColor = Color.FromArgb(255, 228, 228, 228);  //Light Gray
                scintilla.Styles[Style.Sql.Comment].ForeColor = Color.Green;
                scintilla.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
                scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.Green;
                scintilla.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
                scintilla.Styles[Style.Sql.Word].ForeColor = Color.Blue;
                scintilla.Styles[Style.Sql.Word2].ForeColor = Color.Fuchsia;
                scintilla.Styles[Style.Sql.User1].ForeColor = Color.Gray;
                scintilla.Styles[Style.Sql.User2].ForeColor = Color.FromArgb(255, 00, 128, 192);    //Medium Blue-Green
                scintilla.Styles[Style.Sql.String].ForeColor = Color.Red;
                scintilla.Styles[Style.Sql.Character].ForeColor = Color.Red;
                scintilla.Styles[Style.Sql.Operator].ForeColor = Color.Black;

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
                    ConfigureLexer("", scintilla);
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
    }
}
