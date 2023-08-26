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

            content += "Default:" + defaultTextBox.Text + "\n";
            content += "Comment:" + commentTextBox.Text + "\n";
            content += "CommentLine:" + commentTextBox.Text + "\n";
            content += "CommentLineDoc:" + commentTextBox.Text + "\n";
            content += "Number:" + numberTextBox.Text + "\n";
            content += "Word:" + wordTextBox.Text + "\n";
            content += "Word2:" + word2TextBox.Text + "\n";
            content += "Word3:" + word3TextBox.Text + "\n";
            content += "Word4:" + word4TextBox.Text + "\n";
            content += "String:" + stringTextBox.Text + "\n";
            content += "LiteralString:" + stringTextBox.Text + "\n";
            content += "Character:" + characterTextBox.Text + "\n";
            content += "Verbatim:" + characterTextBox.Text + "\n";
            content += "StringEol:" + stringEOLTextBox.Text + "\n";
            content += "Operator:" + operatorTextBox.Text + "\n";
            content += "Preprocessor:" + preprocessorTextBox.Text + "\n";
            content += "Triple:" + tripleTextBox.Text + "\n";
            content += "TripleDouble:" + tripleTextBox.Text + "\n";
            content += "ClassName:" + claassNameTextBox.Text + "\n";
            content += "DefName:" + defNameTextBox.Text + "\n";
            content += "CommentBlock:" + commentBlockTextBox.Text + "\n";
            content += "Decorator:" + decoratorTextBox.Text + "\n";
            content += "Attribute:" + attributeTextBox.Text + "\n";
            content += "Entity:" + attributeTextBox.Text + "\n";
            content += "Tag:" + tagTextBox.Text + "\n";
            content += "TagEnd:" + tagTextBox.Text + "\n";
            content += "DoubleString:" + singleStringTextBox.Text + "\n";
            content += "SingleString:" + singleStringTextBox.Text + "\n";
            content += "User1:" + user1TextBox.Text + "\n";
            content += "User2:" + user2TextBox.Text + "\n";
            content += "Background:" + backgroundTextBox.Text + "\n";
            content += "Fore:" + foreTextBox.Text + "\n";
            content += "CaretLine:" + caretLineTextBox.Text + "\n";

            File.WriteAllText("theme-settings.config", content);
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            bool finalResult = true;
            bool canSave = true;

            finalResult = ProcessColors(finalResult, "Default", defaultTextBox.Text);
            finalResult = ProcessColors(finalResult, "Comment", commentTextBox.Text);
            finalResult = ProcessColors(finalResult, "Number", numberTextBox.Text);
            finalResult = ProcessColors(finalResult, "Word", wordTextBox.Text);
            finalResult = ProcessColors(finalResult, "Word2", word2TextBox.Text);
            finalResult = ProcessColors(finalResult, "Word3", word3TextBox.Text);
            finalResult = ProcessColors(finalResult, "Word4", word4TextBox.Text);
            finalResult = ProcessColors(finalResult, "String", stringTextBox.Text);
            finalResult = ProcessColors(finalResult, "Character", characterTextBox.Text);
            finalResult = ProcessColors(finalResult, "StringEol", stringEOLTextBox.Text);
            finalResult = ProcessColors(finalResult, "Operator", operatorTextBox.Text);
            finalResult = ProcessColors(finalResult, "Preprocessor", preprocessorTextBox.Text);
            finalResult = ProcessColors(finalResult, "Triple", tripleTextBox.Text);
            finalResult = ProcessColors(finalResult, "ClassName", claassNameTextBox.Text);
            finalResult = ProcessColors(finalResult, "DefName", defNameTextBox.Text);
            finalResult = ProcessColors(finalResult, "CommentBlock", commentBlockTextBox.Text);
            finalResult = ProcessColors(finalResult, "Decorator", decoratorTextBox.Text);
            finalResult = ProcessColors(finalResult, "Attribute", attributeTextBox.Text);
            finalResult = ProcessColors(finalResult, "Tag", tagTextBox.Text);
            finalResult = ProcessColors(finalResult, "SingleString", singleStringTextBox.Text);
            finalResult = ProcessColors(finalResult, "User1", user1TextBox.Text);
            finalResult = ProcessColors(finalResult, "User2", user2TextBox.Text);
            finalResult = ProcessColors(finalResult, "Background", backgroundTextBox.Text);
            finalResult = ProcessColors(finalResult, "Fore", foreTextBox.Text);
            finalResult = ProcessColors(finalResult, "CaretLine", caretLineTextBox.Text);

            if (finalResult)
            {
                ProcessFileWithNewColors();

                DialogResult dialogResult = MessageBox.Show("Close pie and reopen it manually, in order for the changes to take effect?", "Build Commands", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Globals.setCloseAfterApplyingChanges(true);
                }

                this.Close();
            }
        }

        private void SetTextBoxes(Dictionary<string, string> colorDictionary)
        {
            defaultTextBox.Text = colorDictionary["Default"];
            commentTextBox.Text = colorDictionary["Comment"];
            numberTextBox.Text = colorDictionary["Number"];
            wordTextBox.Text = colorDictionary["Word"];
            word2TextBox.Text = colorDictionary["Word2"];
            word3TextBox.Text = colorDictionary["Word3"];
            word4TextBox.Text = colorDictionary["Word4"];
            stringTextBox.Text = colorDictionary["String"];
            characterTextBox.Text = colorDictionary["Character"];
            stringEOLTextBox.Text = colorDictionary["StringEol"];
            operatorTextBox.Text = colorDictionary["Operator"];
            preprocessorTextBox.Text = colorDictionary["Preprocessor"];
            tripleTextBox.Text = colorDictionary["Triple"];
            claassNameTextBox.Text = colorDictionary["ClassName"];
            defNameTextBox.Text = colorDictionary["DefName"];
            commentBlockTextBox.Text = colorDictionary["CommentBlock"];
            decoratorTextBox.Text = colorDictionary["Decorator"];
            attributeTextBox.Text = colorDictionary["Attribute"];
            tagTextBox.Text = colorDictionary["Tag"];
            singleStringTextBox.Text = colorDictionary["SingleString"];
            user1TextBox.Text = colorDictionary["User1"];
            user2TextBox.Text = colorDictionary["User2"];
            backgroundTextBox.Text = colorDictionary["Background"];
            foreTextBox.Text = colorDictionary["Fore"];
            caretLineTextBox.Text = colorDictionary["CaretLine"];
        }

        private void ThemeSettingsForm_Load(object sender, EventArgs e)
        {
            SetTextBoxes(Globals.getConfigColorDictionary());
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            SetTextBoxes(Globals.getDefaultColorDictionary());
        }
    }
}
