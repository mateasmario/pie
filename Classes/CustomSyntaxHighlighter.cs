using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie.Classes
{
    internal class CustomSyntaxHighlighter : SyntaxHighlighter
    {
        public readonly Style CustomStringStyle = new TextStyle(Brushes.Gold, null, FontStyle.Regular);
        public readonly Style CustomClassNameStyle = new TextStyle((Brush)new SolidBrush(Color.AliceBlue), null, FontStyle.Regular);

        public CustomSyntaxHighlighter(FastColoredTextBox currentTb) : base(currentTb)
        {
        }

        public void InitStyleSchema(Language lang)
        {
            switch (lang)
            {
                case Language.CSharp:
                    StringStyle = CustomStringStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = MagentaStyle;
                    AttributeStyle = GreenStyle;
                    ClassNameStyle = CustomClassNameStyle;
                    KeywordStyle = BlueStyle;
                    CommentTagStyle = GrayStyle;
                    break;
                case Language.VB:
                    StringStyle = BrownStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = MagentaStyle;
                    ClassNameStyle = BoldStyle;
                    KeywordStyle = BlueStyle;
                    break;
                case Language.HTML:
                    CommentStyle = GreenStyle;
                    TagBracketStyle = BlueStyle;
                    TagNameStyle = MaroonStyle;
                    AttributeStyle = RedStyle;
                    AttributeValueStyle = BlueStyle;
                    HtmlEntityStyle = RedStyle;
                    break;
                case Language.XML:
                    CommentStyle = GreenStyle;
                    XmlTagBracketStyle = BlueStyle;
                    XmlTagNameStyle = MaroonStyle;
                    XmlAttributeStyle = RedStyle;
                    XmlAttributeValueStyle = BlueStyle;
                    XmlEntityStyle = RedStyle;
                    XmlCDataStyle = BlackStyle;
                    break;
                case Language.JS:
                    StringStyle = BrownStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = MagentaStyle;
                    KeywordStyle = BlueStyle;
                    break;
                case Language.Lua:
                    StringStyle = BrownStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = MagentaStyle;
                    KeywordStyle = BlueBoldStyle;
                    FunctionsStyle = MaroonStyle;
                    break;
                case Language.PHP:
                    StringStyle = RedStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = RedStyle;
                    VariableStyle = MaroonStyle;
                    KeywordStyle = MagentaStyle;
                    KeywordStyle2 = BlueStyle;
                    KeywordStyle3 = GrayStyle;
                    break;
                case Language.SQL:
                    StringStyle = RedStyle;
                    CommentStyle = GreenStyle;
                    NumberStyle = MagentaStyle;
                    KeywordStyle = BlueBoldStyle;
                    StatementsStyle = BlueBoldStyle;
                    FunctionsStyle = MaroonStyle;
                    VariableStyle = MaroonStyle;
                    TypesStyle = BrownStyle;
                    break;
            }
        }

        public virtual void HighlightSyntax(Language language, Range range)
        {
            switch (language)
            {
                case Language.CSharp:
                    CSharpSyntaxHighlight(range);
                    break;
                case Language.VB:
                    VBSyntaxHighlight(range);
                    break;
                case Language.HTML:
                    HTMLSyntaxHighlight(range);
                    break;
                case Language.XML:
                    XMLSyntaxHighlight(range);
                    break;
                case Language.SQL:
                    SQLSyntaxHighlight(range);
                    break;
                case Language.PHP:
                    PHPSyntaxHighlight(range);
                    break;
                case Language.JS:
                    JScriptSyntaxHighlight(range);
                    break;
                case Language.Lua:
                    LuaSyntaxHighlight(range);
                    break;
            }
        }

        public virtual void CSharpSyntaxHighlight(Range range)
        {
            range.tb.CommentPrefix = "//";
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            range.tb.LeftBracket2 = '{';
            range.tb.RightBracket2 = '}';
            range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
            range.tb.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            range.ClearStyle(StringStyle, CommentStyle, NumberStyle, AttributeStyle, CustomClassNameStyle, KeywordStyle);
            if (CSharpStringRegex == null)
            {
                InitCShaprRegex();
            }

            range.SetStyle(StringStyle, CSharpStringRegex);
            range.SetStyle(CommentStyle, CSharpCommentRegex1);
            range.SetStyle(CommentStyle, CSharpCommentRegex2);
            range.SetStyle(CommentStyle, CSharpCommentRegex3);
            range.SetStyle(NumberStyle, CSharpNumberRegex);
            range.SetStyle(AttributeStyle, CSharpAttributeRegex);
            range.SetStyle(CustomClassNameStyle, CSharpClassNameRegex);
            range.SetStyle(KeywordStyle, CSharpKeywordRegex);
            foreach (Range range2 in range.GetRanges("^\\s*///.*$", RegexOptions.Multiline))
            {
                range2.ClearStyle(StyleIndex.All);
                if (HTMLTagRegex == null)
                {
                    InitHTMLRegex();
                }

                range2.SetStyle(CommentStyle);
                foreach (Range range3 in range2.GetRanges(HTMLTagContentRegex))
                {
                    range3.ClearStyle(StyleIndex.All);
                    range3.SetStyle(CommentTagStyle);
                }

                foreach (Range range4 in range2.GetRanges("^\\s*///", RegexOptions.Multiline))
                {
                    range4.ClearStyle(StyleIndex.All);
                    range4.SetStyle(CommentTagStyle);
                }
            }

            range.ClearFoldingMarkers();
            range.SetFoldingMarkers("{", "}");
            range.SetFoldingMarkers("#region\\b", "#endregion\\b");
            range.SetFoldingMarkers("/\\*", "\\*/");
        }
    }
}
