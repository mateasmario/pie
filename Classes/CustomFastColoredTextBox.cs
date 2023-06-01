using FastColoredTextBoxNS;

namespace pie.Classes
{
    internal class CustomFastColoredTextBox : FastColoredTextBox
    {
        public CustomFastColoredTextBox() : base()
        {
            this.SyntaxHighlighter = new CustomSyntaxHighlighter(this);
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // CustomFastColoredTextBox
            // 
            this.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "CustomFastColoredTextBox";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
