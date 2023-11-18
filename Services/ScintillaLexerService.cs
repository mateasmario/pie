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

            if (Globals.theme == 1)
            {
                parserColorDictionary["Comment"] = Color.FromArgb(192, 192, 192);
                parserColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
                parserColorDictionary["Number"] = Color.FromArgb(242, 161, 39);
                parserColorDictionary["Word"] = Color.FromArgb(60, 170, 232);
                parserColorDictionary["Word2"] = Color.FromArgb(60, 170, 232);
                parserColorDictionary["Word3"] = Color.FromArgb(145, 35, 247);
                parserColorDictionary["Word4"] = Color.FromArgb(145, 35, 247);
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
            else
            {
                parserColorDictionary["Comment"] = Color.FromArgb(180, 180, 180);
                parserColorDictionary["CommentLine"] = Color.FromArgb(0, 128, 0);
                parserColorDictionary["Number"] = Color.FromArgb(194, 127, 25);
                parserColorDictionary["Word"] = Color.FromArgb(50, 125, 168);
                parserColorDictionary["Word2"] = Color.FromArgb(50, 125, 168);
                parserColorDictionary["Word3"] = Color.FromArgb(138, 43, 226);
                parserColorDictionary["Word4"] = Color.FromArgb(138, 43, 226);
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
        }

        public static void SetAutocompleteMenuKeywords(AutocompleteMenu autocompleteMenu, List<string> keywords)
        {
            autocompleteMenu.AppearInterval = 1;
            autocompleteMenu.SetAutocompleteItems(keywords);
        }

        public static void ConfigureLexer(string language, Scintilla scintilla, KryptonDockableNavigator tabControl, int index)
        {
            if (!dictionaryInitialized) 
            { 
                InitializeParserDictionary();
                dictionaryInitialized = true;
            }

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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "json")
            {
                scintilla.Lexer = Lexer.Json;

                scintilla.Styles[Style.Json.Default].ForeColor = parserColorDictionary["Default"];
                scintilla.Styles[Style.Json.Number].ForeColor = parserColorDictionary["Number"];
                scintilla.Styles[Style.Json.String].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Json.StringEol].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Json.LineComment].ForeColor = parserColorDictionary["CommentLine"];
                scintilla.Styles[Style.Json.BlockComment].ForeColor = parserColorDictionary["CommentBlock"];
                scintilla.Styles[Style.Json.Operator].ForeColor = parserColorDictionary["Operator"];
                scintilla.Styles[Style.Json.Keyword].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Json.Uri].ForeColor = parserColorDictionary["Word2"];

                scintilla.SetProperty("lexer.json.allow.comments", "1");
                scintilla.SetProperty("lexer.json.escape.sequence", "1");
            }
            else if (language == "lua")
            {
                var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var numericChars = "0123456789";
                var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

                scintilla.Styles[Style.Lua.Default].ForeColor = parserColorDictionary["Default"];
                scintilla.Styles[Style.Lua.Comment].ForeColor = parserColorDictionary["Comment"];
                scintilla.Styles[Style.Lua.CommentLine].ForeColor = parserColorDictionary["CommentLine"];
                scintilla.Styles[Style.Lua.Number].ForeColor = parserColorDictionary["Number"];
                scintilla.Styles[Style.Lua.Word].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Lua.Word2].ForeColor = parserColorDictionary["Word2"];
                scintilla.Styles[Style.Lua.Word3].ForeColor = parserColorDictionary["Word3"];
                scintilla.Styles[Style.Lua.Word4].ForeColor = parserColorDictionary["Word4"];
                scintilla.Styles[Style.Lua.String].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Lua.Character].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Lua.LiteralString].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Lua.StringEol].BackColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Lua.Operator].ForeColor = parserColorDictionary["Operator"];
                scintilla.Styles[Style.Lua.Preprocessor].ForeColor = parserColorDictionary["Preprocessor"];
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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "python")
            {
                scintilla.Lexer = Lexer.Python;

                scintilla.SetProperty("tab.timmy.whinge.level", "1");

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
                scintilla.Styles[Style.Python.Word2].ForeColor = parserColorDictionary["Word2"];
                scintilla.Styles[Style.Python.Decorator].ForeColor = parserColorDictionary["Decorator"];

                scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;

                string keywordSet1 = "and as assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass print raise return try while with yield";
                string keywordSet2 = "False None True";
                string keywordSet3 = "cdef cimport cpdef";

                scintilla.SetKeywords(0, keywordSet1 + " " + keywordSet3);
                scintilla.SetKeywords(1, keywordSet2);

                string combined = keywordSet1 + " " + keywordSet2 + " " + keywordSet3;
                string[] combinedArray = combined.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());

                EnableFolding(scintilla);
            }
            else if (language == "xml")
            {
                scintilla.Lexer = Lexer.Xml;

                scintilla.SetProperty("fold.html", "1");

                EnableFolding(scintilla);

                scintilla.Styles[Style.Xml.Attribute].ForeColor = parserColorDictionary["Attribute"];
                scintilla.Styles[Style.Xml.Entity].ForeColor = parserColorDictionary["Entity"];
                scintilla.Styles[Style.Xml.Comment].ForeColor = parserColorDictionary["Comment"];
                scintilla.Styles[Style.Xml.Tag].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Xml.TagEnd].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Xml.DoubleString].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Xml.SingleString].ForeColor = parserColorDictionary["String"];
            }
            else if (language == "html")
            {
                scintilla.Lexer = Lexer.Html;

                scintilla.Styles[Style.Html.Attribute].ForeColor = parserColorDictionary["Attribute"];
                scintilla.Styles[Style.Html.Entity].ForeColor = parserColorDictionary["Entity"];
                scintilla.Styles[Style.Html.Comment].ForeColor = parserColorDictionary["Comment"];
                scintilla.Styles[Style.Html.Tag].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Html.TagEnd].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Html.DoubleString].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Html.SingleString].ForeColor = parserColorDictionary["String"];

                string keywordSet1 = "!doctype a abbr accept accept-charset accesskey acronym action address align alink alt applet archive area article aside async audio autocomplete autofocus axis b background base basefont bdi bdo bgcolor bgsound big blink blockquote body border br button canvas caption cellpadding cellspacing center char charoff charset checkbox checked cite class classid clear code codebase codetype col colgroup color cols colspan command compact content contenteditable contextmenu coords data datafld dataformatas datalist datapagesize datasrc datetime dd declare defer del details dfn dialog dir disabled div dl draggable dropzone dt element em embed enctype event face fieldset figcaption figure file font footer for form formaction formenctype formmethod formnovalidate formtarget frame frameborder frameset h1 h2 h3 h4 h5 h6 head header headers height hgroup hidden hr href hreflang hspace html http-equiv i id iframe image img input ins isindex ismap kbd keygen label lang language leftmargin legend li link list listing longdesc main manifest map marginheight marginwidth mark marquee max maxlength media menu menuitem meta meter method min multicol multiple name nav nobr noembed noframes nohref noresize noscript noshade novalidate nowrap object ol onabort onafterprint onautocomplete onautocompleteerror onbeforeonload onbeforeprint onblur oncancel oncanplay oncanplaythrough onchange onclick onclose oncontextmenu oncuechange ondblclick ondrag ondragend ondragenter ondragleave ondragover ondragstart ondrop ondurationchange onemptied onended onerror onfocus onhashchange oninput oninvalid onkeydown onkeypress onkeyup onload onloadeddata onloadedmetadata onloadstart onmessage onmousedown onmouseenter onmouseleave onmousemove onmouseout onmouseover onmouseup onmousewheel onoffline ononline onpagehide onpageshow onpause onplay onplaying onpointercancel onpointerdown onpointerenter onpointerleave onpointerlockchange onpointerlockerror onpointermove onpointerout onpointerover onpointerup onpopstate onprogress onratechange onreadystatechange onredo onreset onresize onscroll onseeked onseeking onselect onshow onsort onstalled onstorage onsubmit onsuspend ontimeupdate ontoggle onundo onunload onvolumechange onwaiting optgroup option output p param password pattern picture placeholder plaintext pre profile progress prompt public q radio readonly rel required reset rev reversed role rows rowspan rp rt rtc ruby rules s samp sandbox scheme scope scoped script seamless section select selected shadow shape size sizes small source spacer span spellcheck src srcdoc standby start step strike strong style sub submit summary sup svg svg:svg tabindex table target tbody td template text textarea tfoot th thead time title topmargin tr track tt type u ul usemap valign value valuetype var version video vlink vspace wbr width xml xmlns xmp";

                scintilla.SetKeywords(0, keywordSet1);

                string[] combinedArray = keywordSet1.Split(' ');

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());
            }
            else if (language == "mssql")
            {
                scintilla.Lexer = Lexer.Sql;

                scintilla.Styles[Style.Sql.Comment].ForeColor = parserColorDictionary["Comment"];
                scintilla.Styles[Style.Sql.CommentLine].ForeColor = parserColorDictionary["CommentLine"];
                scintilla.Styles[Style.Sql.CommentLineDoc].ForeColor = parserColorDictionary["CommentLine"];
                scintilla.Styles[Style.Sql.Number].ForeColor = parserColorDictionary["Number"];
                scintilla.Styles[Style.Sql.Word].ForeColor = parserColorDictionary["Word"];
                scintilla.Styles[Style.Sql.Word2].ForeColor = parserColorDictionary["Word2"];
                scintilla.Styles[Style.Sql.User1].ForeColor = parserColorDictionary["User1"];
                scintilla.Styles[Style.Sql.User2].ForeColor = parserColorDictionary["User2"];
                scintilla.Styles[Style.Sql.String].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Sql.Character].ForeColor = parserColorDictionary["String"];
                scintilla.Styles[Style.Sql.Operator].ForeColor = parserColorDictionary["Operator"];

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

                SetAutocompleteMenuKeywords(Globals.tabInfos[index].getAutocompleteMenu(), combinedArray.ToList());
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
            scintilla.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = parserColorDictionary["Word2"];
            scintilla.Styles[ScintillaNET.Style.Cpp.String].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Verbatim].ForeColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.StringEol].BackColor = parserColorDictionary["String"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = parserColorDictionary["Operator"];
            scintilla.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = parserColorDictionary["Preprocessor"];
        }

        public static void SetLexer(String extension, Scintilla scintilla, KryptonDockableNavigator tabControl, int index)
        {
            switch (extension)
            {
                case "c":
                    ConfigureLexer("c", scintilla, tabControl, index);
                    break;
                case "cpp":
                    ConfigureLexer("c", scintilla, tabControl, index);
                    break;
                case "cs":
                    ConfigureLexer("cs", scintilla, tabControl, index);
                    break;
                case "java":
                    ConfigureLexer("java", scintilla, tabControl, index);
                    break;
                case "js":
                    ConfigureLexer("js", scintilla, tabControl, index);
                    break;
                case "json":
                    ConfigureLexer("json", scintilla, tabControl, index);
                    break;
                case "lua":
                    ConfigureLexer("lua", scintilla, tabControl, index);
                    break;
                case "py":
                    ConfigureLexer("python", scintilla, tabControl, index);
                    break;
                case "xml":
                    ConfigureLexer("xml", scintilla, tabControl, index);
                    break;
                case "html":
                    ConfigureLexer("html", scintilla, tabControl, index);
                    break;
                case "sql":
                    ConfigureLexer("mssql", scintilla, tabControl, index);
                    break;
            }
        }
    }
}
