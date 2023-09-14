using System;
using System.Collections.Generic;
using System.Drawing;
using ScintillaNET;
using System.IO;
using AutocompleteMenuNS;
using System.Linq;
using ComponentFactory.Krypton.Docking;

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

        public static void SetAutocompleteMenuKeywords(AutocompleteMenu autocompleteMenu, List<string> keywords)
        {
            autocompleteMenu.AppearInterval = 1;
            autocompleteMenu.SetAutocompleteItems(keywords);
        }

        public static void ConfigureLexer(string language, Scintilla scintilla, KryptonDockableNavigator tabControl)
        {
            Dictionary<string, string> configColorDictionary = Globals.configColorDictionary;

            if (language == "c")
            {
                scintilla.Lexer = Lexer.Cpp;
                AddCppStyles(scintilla);

                string keywordSet1 = "alignof and and_eq bitand bitor break case catch compl const_cast continue default delete do dynamic_cast else false for goto if namespace new not not_eq nullptr operator or or_eq reinterpret_cast return sizeof static_assert static_cast switch this throw true try typedef typeid using while xor xor_eq NULL";
                string keywordSet2 = "alignas asm auto bool char char16_t char32_t class clock_t const constexpr decltype double enum explicit export extern final float friend inline int int8_t int16_t int32_t int64_t int_fast8_t int_fast16_t int_fast32_t int_fast64_t intmax_t intptr_t long mutable noexcept override private protected ptrdiff_t public register short signed size_t ssize_t static struct template thread_local time_t typename uint8_t uint16_t uint32_t uint64_t uint_fast8_t uint_fast16_t uint_fast32_t uint_fast64_t uintmax_t uintptr_t union unsigned virtual void volatile wchar_t";
                string keywordSet3 = "a addindex addtogroup anchor arg attention author authors b brief bug c callergraph callgraph category cite class code cond copybrief copydetails copydoc copyright date def defgroup deprecated details diafile dir docbookonly dontinclude dot dotfile e else elseif em endcode endcond enddocbookonly enddot endhtmlonly endif endinternal endlatexonly endlink endmanonly endmsc endparblock endrtfonly endsecreflist enduml endverbatim endxmlonly enum example exception extends f$ f[f] file fn f { f } headerfile hidecallergraph hidecallgraph hideinitializer htmlinclude htmlonly idlexcept if ifnot image implements include includelineno ingroup interface internal invariant latexinclude latexonly li line link mainpage manonly memberof msc mscfile n name namespace nosubgrouping note overload p package page par paragraph param parblock post pre private privatesection property protected protectedsection protocol public publicsection pure ref refitem related relatedalso relates relatesalso remark remarks result returns retval rtfonly sa secreflist section see short showinitializer since skip skipline snippet startuml struct subpage subsection subsubsection tableofcontents test throw throws todo tparam typedef union until var verbatim verbinclude version vhdlflow warning weakgroup xmlonly xrefitem";
                
                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);
                scintilla.SetKeywords(2, keywordSet3);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "cs")
            {
                scintilla.Lexer = Lexer.Cpp;
                AddCppStyles(scintilla);

                string keywordSet1 = "abstract add alias as ascending async await base break case catch checked continue default delegate descending do dynamic else event explicit extern false finally fixed for foreach from get global goto group if implicit in interface internal into is join let lock namespace new null object operator orderby out override params partial private protected public readonly ref remove return sealed select set sizeof stackalloc switch this throw true try typeof unchecked unsafe using value virtual where while yield";
                string keywordSet2 = "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort var void";

                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);

                string combined = keywordSet1 + " " + keywordSet2;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "java")
            {
                scintilla.Lexer = Lexer.Cpp;
                AddCppStyles(scintilla);

                string keywordSet1 = "abstract break case catch continue default do else extern false finally for native super extends final native transient volatile implements synchronized if instanceof import package interface new null private protected public record return sizeof switch this throw throws true try while";
                string keywordSet2 = "boolean Boolean byte Byte char Character class double Double enum float Float int Integer long Long short Short static String void";

                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);

                string combined = keywordSet1 + " " + keywordSet2;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "js")
            {
                scintilla.Lexer = Lexer.Cpp;
                AddCppStyles(scintilla);

                string keywordSet1 = "abstract async await boolean break byte case catch char class const continue debugger default delete do double else enum export extends final finally float for from function goto if implements import in instanceof int interface let long native new null of package private protected public return short static super switch synchronized this throw throws transient try typeof var void volatile while with true false prototype yield";
                string keywordSet2 = "Array Date eval hasOwnProperty Infinity isFinite isNaN isPrototypeOf Math NaN Number Object prototype String toString undefined valueOf";
                string keywordSet3 =  "alert all anchor anchors area assign blur button checkbox clearInterval clearTimeout clientInformation close closed confirm constructor crypto decodeURI decodeURIComponent defaultStatus document element elements embed embeds encodeURI encodeURIComponent escape event fileUpload focus form forms frame innerHeight innerWidth layer layers link location mimeTypes navigate navigator frames frameRate hidden history image images offscreenBuffering onblur onclick onerror onfocus onkeydown onkeypress onkeyup onmouseover onload onmouseup onmousedown onsubmit open opener option outerHeight outerWidth packages pageXOffset pageYOffset parent parseFloat parseInt password pkcs11 plugin prompt propertyIsEnum radio reset screenX screenY scroll secure select self setInterval setTimeout status submit taint text textarea top unescape untaint window";

                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);
                scintilla.SetKeywords(2, keywordSet3);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "json")
            {
                scintilla.Lexer = Lexer.Json;

                scintilla.Styles[Style.Json.Default].ForeColor = ConvertHexToColor(configColorDictionary["Default"]);
                scintilla.Styles[Style.Json.Number].ForeColor = ConvertHexToColor(configColorDictionary["Number"]);
                scintilla.Styles[Style.Json.String].ForeColor = ConvertHexToColor(configColorDictionary["String"]);
                scintilla.Styles[Style.Json.StringEol].ForeColor = ConvertHexToColor(configColorDictionary["StringEol"]);
                scintilla.Styles[Style.Json.LineComment].ForeColor = ConvertHexToColor(configColorDictionary["CommentLine"]);
                scintilla.Styles[Style.Json.BlockComment].ForeColor = ConvertHexToColor(configColorDictionary["CommentBlock"]);
                scintilla.Styles[Style.Json.Operator].ForeColor = ConvertHexToColor(configColorDictionary["Operator"]);
                scintilla.Styles[Style.Json.Keyword].ForeColor = ConvertHexToColor(configColorDictionary["Word"]);
                scintilla.Styles[Style.Json.Uri].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);

                scintilla.SetProperty("lexer.json.allow.comments", "1");
                scintilla.SetProperty("lexer.json.escape.sequence", "1");
            }
            else if (language == "lua")
            {
                var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var numericChars = "0123456789";
                var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

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

                string keywordSet1 = "and break do else elseif end for function if in local nil not or repeat return then until while" + " false true" + " goto";
                string keywordSet2 = "assert collectgarbage dofile error _G getmetatable ipairs loadfile next pairs pcall print rawequal rawget rawset setmetatable tonumber tostring type _VERSION xpcall string table math coroutine io os debug" + " getfenv gcinfo load loadlib loadstring require select setfenv unpack _LOADED LUA_PATH _REQUIREDNAME package rawlen package bit32 utf8 _ENV";
                string keywordSet3 = "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" + " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" + " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" + " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset";
                string keywordSet4 = "coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload";

                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);
                scintilla.SetKeywords(2, keywordSet3);
                scintilla.SetKeywords(3, keywordSet4);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3 + " " + keywordSet4;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "python")
            {
                scintilla.Lexer = Lexer.Python;

                scintilla.SetProperty("tab.timmy.whinge.level", "1");

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
                scintilla.Styles[Style.Python.CommentBlock].ForeColor = ConvertHexToColor(configColorDictionary["CommentBlock"]);
                scintilla.Styles[Style.Python.CommentBlock].Italic = true;
                scintilla.Styles[Style.Python.StringEol].ForeColor = ConvertHexToColor(configColorDictionary["StringEol"]);
                scintilla.Styles[Style.Python.StringEol].BackColor = Color.FromArgb(0xE0, 0xC0, 0xE0);
                scintilla.Styles[Style.Python.StringEol].FillLine = true;
                scintilla.Styles[Style.Python.Word2].ForeColor = ConvertHexToColor(configColorDictionary["Word2"]);
                scintilla.Styles[Style.Python.Decorator].ForeColor = ConvertHexToColor(configColorDictionary["Decorator"]);

                scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;

                string keywordSet1 = "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
                string keywordSet2 = "False None True and as assert break class continue def del elif else except finally for from global if import in is lambda nonlocal not or pass raise return try while with yield";
                string keywordSet3 = "cdef cimport cpdef";

                scintilla.SetKeywords(0, keywordSet1 + " " + keywordSet3);
                scintilla.SetKeywords(1, keywordSet2);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "xml")
            {
                scintilla.Lexer = Lexer.Xml;

                scintilla.SetProperty("fold.html", "1");

                EnableFolding(scintilla);

                scintilla.Styles[Style.Xml.Attribute].ForeColor = ConvertHexToColor(configColorDictionary["Attribute"]);
                scintilla.Styles[Style.Xml.Entity].ForeColor = ConvertHexToColor(configColorDictionary["Entity"]);
                scintilla.Styles[Style.Xml.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[Style.Xml.Tag].ForeColor = ConvertHexToColor(configColorDictionary["Tag"]);
                scintilla.Styles[Style.Xml.TagEnd].ForeColor = ConvertHexToColor(configColorDictionary["TagEnd"]);
                scintilla.Styles[Style.Xml.DoubleString].ForeColor = ConvertHexToColor(configColorDictionary["DoubleString"]);
                scintilla.Styles[Style.Xml.SingleString].ForeColor = ConvertHexToColor(configColorDictionary["SingleString"]);
            }
            else if (language == "html")
            {
                scintilla.Lexer = Lexer.Html;

                scintilla.Styles[Style.Html.Attribute].ForeColor = ConvertHexToColor(configColorDictionary["Attribute"]);
                scintilla.Styles[Style.Html.Entity].ForeColor = ConvertHexToColor(configColorDictionary["Entity"]);
                scintilla.Styles[Style.Html.Comment].ForeColor = ConvertHexToColor(configColorDictionary["Comment"]);
                scintilla.Styles[Style.Html.Tag].ForeColor = ConvertHexToColor(configColorDictionary["Tag"]);
                scintilla.Styles[Style.Html.TagEnd].ForeColor = ConvertHexToColor(configColorDictionary["TagEnd"]);
                scintilla.Styles[Style.Html.DoubleString].ForeColor = ConvertHexToColor(configColorDictionary["DoubleString"]);
                scintilla.Styles[Style.Html.SingleString].ForeColor = ConvertHexToColor(configColorDictionary["SingleString"]);

                string keywordSet1 = "!doctype a abbr accept accept-charset accesskey acronym action address align alink alt applet archive area article aside async audio autocomplete autofocus axis b background base basefont bdi bdo bgcolor bgsound big blink blockquote body border br button canvas caption cellpadding cellspacing center char charoff charset checkbox checked cite class classid clear code codebase codetype col colgroup color cols colspan command compact content contenteditable contextmenu coords data datafld dataformatas datalist datapagesize datasrc datetime dd declare defer del details dfn dialog dir disabled div dl draggable dropzone dt element em embed enctype event face fieldset figcaption figure file font footer for form formaction formenctype formmethod formnovalidate formtarget frame frameborder frameset h1 h2 h3 h4 h5 h6 head header headers height hgroup hidden hr href hreflang hspace html http-equiv i id iframe image img input ins isindex ismap kbd keygen label lang language leftmargin legend li link list listing longdesc main manifest map marginheight marginwidth mark marquee max maxlength media menu menuitem meta meter method min multicol multiple name nav nobr noembed noframes nohref noresize noscript noshade novalidate nowrap object ol onabort onafterprint onautocomplete onautocompleteerror onbeforeonload onbeforeprint onblur oncancel oncanplay oncanplaythrough onchange onclick onclose oncontextmenu oncuechange ondblclick ondrag ondragend ondragenter ondragleave ondragover ondragstart ondrop ondurationchange onemptied onended onerror onfocus onhashchange oninput oninvalid onkeydown onkeypress onkeyup onload onloadeddata onloadedmetadata onloadstart onmessage onmousedown onmouseenter onmouseleave onmousemove onmouseout onmouseover onmouseup onmousewheel onoffline ononline onpagehide onpageshow onpause onplay onplaying onpointercancel onpointerdown onpointerenter onpointerleave onpointerlockchange onpointerlockerror onpointermove onpointerout onpointerover onpointerup onpopstate onprogress onratechange onreadystatechange onredo onreset onresize onscroll onseeked onseeking onselect onshow onsort onstalled onstorage onsubmit onsuspend ontimeupdate ontoggle onundo onunload onvolumechange onwaiting optgroup option output p param password pattern picture placeholder plaintext pre profile progress prompt public q radio readonly rel required reset rev reversed role rows rowspan rp rt rtc ruby rules s samp sandbox scheme scope scoped script seamless section select selected shadow shape size sizes small source spacer span spellcheck src srcdoc standby start step strike strong style sub submit summary sup svg svg:svg tabindex table target tbody td template text textarea tfoot th thead time title topmargin tr track tt type u ul usemap valign value valuetype var version video vlink vspace wbr width xml xmlns xmp";

                scintilla.SetKeywords(0, keywordSet1);

                string[] combinedArray = keywordSet1.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());
            }
            else if (language == "mssql")
            {
                scintilla.Lexer = Lexer.Sql;

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

                string keywordSet1 = @"add alter as authorization backup begin bigint binary bit break browse bulk by cascade case catch check checkpoint close clustered column commit compute constraint containstable continue create current cursor cursor database date datetime datetime2 datetimeoffset dbcc deallocate decimal declare default delete deny desc disk distinct distributed double drop dump else end errlvl escape except exec execute exit external fetch file fillfactor float for foreign freetext freetexttable from full function goto grant group having hierarchyid holdlock identity identity_insert identitycol if image index insert int intersect into key kill lineno load merge money national nchar nocheck nocount nolock nonclustered ntext numeric nvarchar of off offsets on open opendatasource openquery openrowset openxml option order over percent plan precision primary print proc procedure public raiserror read readtext real reconfigure references replication restore restrict return revert revoke rollback rowcount rowguidcol rule save schema securityaudit select set setuser shutdown smalldatetime smallint smallmoney sql_variant statistics table table tablesample text textsize then time timestamp tinyint to top tran transaction trigger truncate try union unique uniqueidentifier update updatetext use user values varbinary varchar varying view waitfor when where while with writetext xml go ";
                string keywordSet2 = @"ascii cast char charindex ceiling coalesce collate contains convert current_date current_time current_timestamp current_user floor isnull max min nullif object_id session_user substring system_user tsequal ";
                string keywordSet3 = @"all and any between cross exists in inner is join left like not null or outer pivot right some unpivot ( ) * ";
                string keywordSet4 = @"sys objects sysobjects ";

                scintilla.SetKeywords(0, keywordSet1);
                scintilla.SetKeywords(1, keywordSet2);
                scintilla.SetKeywords(4, keywordSet3);
                scintilla.SetKeywords(5, keywordSet4);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3 + " " + keywordSet4;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[tabControl.SelectedIndex].getAutocompleteMenu(), combinedArray.ToList());
            }
        }

        private static void EnableFolding(Scintilla scintilla)
        {
            // Enable folding
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");

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
        }

        private static void AddCppStyles(Scintilla scintilla)
        {
            Dictionary<string, string> configColorDictionary = Globals.configColorDictionary;

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
        }

        public static void SetLexer(String extension, Scintilla scintilla, KryptonDockableNavigator tabControl)
        {
            switch (extension)
            {
                case "c":
                    ConfigureLexer("c", scintilla, tabControl);
                    break;
                case "cpp":
                    ConfigureLexer("c", scintilla, tabControl);
                    break;
                case "cs":
                    ConfigureLexer("cs", scintilla, tabControl);
                    break;
                case "java":
                    ConfigureLexer("java", scintilla, tabControl);
                    break;
                case "js":
                    ConfigureLexer("js", scintilla, tabControl);
                    break;
                case "json":
                    ConfigureLexer("json", scintilla, tabControl);
                    break;
                case "lua":
                    ConfigureLexer("lua", scintilla, tabControl);
                    break;
                case "py":
                    ConfigureLexer("python", scintilla, tabControl);
                    break;
                case "xml":
                    ConfigureLexer("xml", scintilla, tabControl);
                    break;
                case "html":
                    ConfigureLexer("html", scintilla, tabControl);
                    break;
                case "sql":
                    ConfigureLexer("mssql", scintilla, tabControl);
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

            Globals.defaultColorDictionary = colorDictionary;
        }

        public static void InitializeConfigColorDictionary(string file)
        {
            Dictionary<string, string> configColorDictionary = new Dictionary<string, string>();
            IEnumerable<string> lines = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + file);

            foreach (string line in lines)
            {
                string[] splitLine = line.Split(':');
                configColorDictionary[splitLine[0]] = splitLine[1];
            }

            Globals.configColorDictionary = configColorDictionary;
        }
    }
}
