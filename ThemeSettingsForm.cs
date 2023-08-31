using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pie.Services;
using ComponentFactory.Krypton.Toolkit;
using ScintillaNET;

namespace pie
{
    public partial class ThemeSettingsForm : Form
    {
        public ThemeSettingsForm()
        {
            InitializeComponent();
        }

        private bool ProcessColors(bool finalResult, string key, string value)
        {
            if (!finalResult)
            {
                return false;
            }

            if (value.Length != 6)
            {
                MessageBox.Show("Invalid color format for " + key + ". HEX value should have a length of 6 characters.", "Theme Settings - pie");
                return false;
            }

            foreach(char c in value)
            {
                if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                {
                    MessageBox.Show("Invalid color format for " + key + ". HEX value should only include digits and letters from A to F.", "Theme Settings - pie");
                    return false;
                }
            }

            return true;
        }

        private void ProcessFileWithNewColors()
        {
            string content = "";

            content += "Default:" + ScintillaLexerService.ConvertColorToHex(defaultButton.SelectedColor) + "\n";
            content += "Comment:" + ScintillaLexerService.ConvertColorToHex(commentButton.SelectedColor) + "\n";
            content += "CommentLine:" + ScintillaLexerService.ConvertColorToHex(commentButton.SelectedColor) + "\n";
            content += "CommentLineDoc:" + ScintillaLexerService.ConvertColorToHex(commentButton.SelectedColor) + "\n";
            content += "Number:" + ScintillaLexerService.ConvertColorToHex(numberButton.SelectedColor) + "\n";
            content += "Word:" + ScintillaLexerService.ConvertColorToHex(wordButton.SelectedColor) + "\n";
            content += "Word2:" + ScintillaLexerService.ConvertColorToHex(word2Button.SelectedColor) + "\n";
            content += "Word3:" + ScintillaLexerService.ConvertColorToHex(word3Button.SelectedColor) + "\n";
            content += "Word4:" + ScintillaLexerService.ConvertColorToHex(word4Button.SelectedColor) + "\n";
            content += "String:" + ScintillaLexerService.ConvertColorToHex(stringButton.SelectedColor) + "\n";
            content += "LiteralString:" + ScintillaLexerService.ConvertColorToHex(stringButton.SelectedColor) + "\n";
            content += "Character:" + ScintillaLexerService.ConvertColorToHex(characterButton.SelectedColor) + "\n";
            content += "Verbatim:" + ScintillaLexerService.ConvertColorToHex(characterButton.SelectedColor) + "\n";
            content += "StringEol:" + ScintillaLexerService.ConvertColorToHex(stringEolButton.SelectedColor) + "\n";
            content += "Operator:" + ScintillaLexerService.ConvertColorToHex(operatorButton.SelectedColor) + "\n";
            content += "Preprocessor:" + ScintillaLexerService.ConvertColorToHex(preprocessorButton.SelectedColor) + "\n";
            content += "Triple:" + ScintillaLexerService.ConvertColorToHex(tripleButton.SelectedColor) + "\n";
            content += "TripleDouble:" + ScintillaLexerService.ConvertColorToHex(tripleButton.SelectedColor) + "\n";
            content += "ClassName:" + ScintillaLexerService.ConvertColorToHex(classNameButton.SelectedColor) + "\n";
            content += "DefName:" + ScintillaLexerService.ConvertColorToHex(defNameButton.SelectedColor) + "\n";
            content += "CommentBlock:" + ScintillaLexerService.ConvertColorToHex(commentBlockButton.SelectedColor) + "\n";
            content += "Decorator:" + ScintillaLexerService.ConvertColorToHex(decoratorButton.SelectedColor) + "\n";
            content += "Attribute:" + ScintillaLexerService.ConvertColorToHex(entityButton.SelectedColor) + "\n";
            content += "Entity:" + ScintillaLexerService.ConvertColorToHex(entityButton.SelectedColor) + "\n";
            content += "Tag:" + ScintillaLexerService.ConvertColorToHex(tagButton.SelectedColor) + "\n";
            content += "TagEnd:" + ScintillaLexerService.ConvertColorToHex(tagButton.SelectedColor) + "\n";
            content += "DoubleString:" + ScintillaLexerService.ConvertColorToHex(stringButton.SelectedColor) + "\n";
            content += "SingleString:" + ScintillaLexerService.ConvertColorToHex(stringButton.SelectedColor) + "\n";
            content += "User1:" + ScintillaLexerService.ConvertColorToHex(user1Button.SelectedColor) + "\n";
            content += "User2:" + ScintillaLexerService.ConvertColorToHex(user2Button.SelectedColor) + "\n";
            content += "Background:" + ScintillaLexerService.ConvertColorToHex(backgroundButton.SelectedColor) + "\n";
            content += "Fore:" + ScintillaLexerService.ConvertColorToHex(foreButton.SelectedColor) + "\n";
            content += "CaretLine:" + ScintillaLexerService.ConvertColorToHex(caretLineButton.SelectedColor) + "\n";
            content += "Selection:" + ScintillaLexerService.ConvertColorToHex(selectionButton.SelectedColor) + "\n";

            File.WriteAllText("theme-settings.config", content);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            ProcessFileWithNewColors();

            DialogResult dialogResult = MessageBox.Show("Close pie and reopen it manually, in order for the changes to take effect?", "Theme Settings", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Globals.setCloseAfterApplyingChanges(true);
            }

            this.Close();
        }

        private void SetTextBoxes(Dictionary<string, string> colorDictionary)
        {
            defaultButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Default"]);
            commentButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Comment"]);
            numberButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Number"]);
            wordButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Word"]);
            word2Button.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Word2"]);
            word3Button.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Word3"]);
            word4Button.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Word4"]);
            stringButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["String"]);
            characterButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Character"]);
            stringEolButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["StringEol"]);
            operatorButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Operator"]);
            preprocessorButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Preprocessor"]);
            tripleButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Triple"]);
            classNameButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["ClassName"]);
            defNameButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["DefName"]);
            commentBlockButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["CommentBlock"]);
            decoratorButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Decorator"]);
            entityButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Attribute"]);
            tagButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Tag"]);
            user1Button.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["User1"]);
            user2Button.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["User2"]);
            backgroundButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Background"]);
            foreButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Fore"]);
            caretLineButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["CaretLine"]);
            selectionButton.SelectedColor = ScintillaLexerService.ConvertHexToColor(colorDictionary["Selection"]);
        }

        private void ThemeSettingsForm_Load(object sender, EventArgs e)
        {
            kryptonDockableNavigator1.AllowPageDrag = false;
            kryptonDockableNavigator1.AllowPageReorder = false;

            ScintillaNET.Scintilla TextArea = new ScintillaNET.Scintilla();
            TextArea.Parent = kryptonPanel1;
            kryptonPanel1.Controls.Add(TextArea);
            TextArea.Dock = DockStyle.Fill;
            TextArea.Text = "This is how your text is going to look like.\nClick on a row or highlight a certain part of the text.";
            TextArea.StyleResetDefault();
            TextArea.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            TextArea.Styles[ScintillaNET.Style.Default].Size = 10;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Fore"]);
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Background"]);
            TextArea.StyleClearAll();
            TextArea.BorderStyle = ScintillaNET.BorderStyle.FixedSingle;
            TextArea.ReadOnly = true;
            TextArea.Margins[0].Width = 24;
            TextArea.SetSelectionBackColor(true, ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["Selection"]));
            TextArea.CaretLineBackColor = ScintillaLexerService.ConvertHexToColor(Globals.getConfigColorDictionary()["CaretLine"]);
            TextArea.WrapMode = ScintillaNET.WrapMode.Char;
            SetTextBoxes(Globals.getConfigColorDictionary());
            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = ParsingService.IntToColor(0xbbcee6);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = ParsingService.IntToColor(0x000000);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = ParsingService.IntToColor(0xbbcee6);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = ParsingService.IntToColor(0x000000);
            TextArea.Margins[3].Type = MarginType.Symbol;
            TextArea.Margins[3].Mask = Marker.MaskFolders;
            TextArea.Margins[3].Sensitive = true;
            TextArea.Margins[3].Width = 20;
            TextArea.SetFoldMarginColor(true, ParsingService.IntToColor(0xd1dded));
            TextArea.SetFoldMarginHighlightColor(true, ParsingService.IntToColor(0xd1dded));
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SetTextBoxes(Globals.getDefaultColorDictionary());
        }

        private void user1TextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ResetMargins(Scintilla TextArea)
        {
            TextArea.Styles[ScintillaNET.Style.LineNumber].BackColor = ParsingService.IntToColor(0xbbcee6);
            TextArea.Styles[ScintillaNET.Style.LineNumber].ForeColor = ParsingService.IntToColor(0x000000);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].ForeColor = ParsingService.IntToColor(0xbbcee6);
            TextArea.Styles[ScintillaNET.Style.IndentGuide].BackColor = ParsingService.IntToColor(0x000000);
            TextArea.Margins[3].Type = MarginType.Symbol;
            TextArea.Margins[3].Mask = Marker.MaskFolders;
            TextArea.Margins[3].Sensitive = true;
            TextArea.Margins[3].Width = 20;
            TextArea.SetFoldMarginColor(true, ParsingService.IntToColor(0xd1dded));
            TextArea.SetFoldMarginHighlightColor(true, ParsingService.IntToColor(0xd1dded));
        }

        private void foreButton_SelectedColorChanged(object sender, ComponentFactory.Krypton.Toolkit.ColorEventArgs e)
        {
            ScintillaNET.Scintilla TextArea = (ScintillaNET.Scintilla)kryptonPanel1.Controls[0];
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            TextArea.Styles[ScintillaNET.Style.Default].ForeColor = kryptonColorButton.SelectedColor;
            TextArea.StyleClearAll();

            ResetMargins(TextArea);
        }

        private void backgroundButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            ScintillaNET.Scintilla TextArea = (ScintillaNET.Scintilla)kryptonPanel1.Controls[0];
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            TextArea.Styles[ScintillaNET.Style.Default].BackColor = kryptonColorButton.SelectedColor;
            TextArea.StyleClearAll();

            ResetMargins(TextArea);
        }

        private void selectionButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            ScintillaNET.Scintilla TextArea = (ScintillaNET.Scintilla)kryptonPanel1.Controls[0];
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            TextArea.SetSelectionBackColor(true, kryptonColorButton.SelectedColor);
            TextArea.StyleClearAll();

            ResetMargins(TextArea);
        }

        private void caretLineButton_SelectedColorChanged(object sender, ColorEventArgs e)
        {
            ScintillaNET.Scintilla TextArea = (ScintillaNET.Scintilla)kryptonPanel1.Controls[0];
            KryptonColorButton kryptonColorButton = (KryptonColorButton)sender;
            TextArea.CaretLineBackColor = kryptonColorButton.SelectedColor;
            TextArea.StyleClearAll();

            ResetMargins(TextArea);
        }
    }
}
