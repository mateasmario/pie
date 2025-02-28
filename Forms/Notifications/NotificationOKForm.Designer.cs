namespace pie
{
    partial class NotificationOKForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationOKForm));
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.contentRichTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.okButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.contentRichTextBox);
            this.mainPanel.Controls.Add(this.okButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(366, 100);
            this.mainPanel.TabIndex = 7;
            // 
            // contentRichTextBox
            // 
            this.contentRichTextBox.Location = new System.Drawing.Point(3, 8);
            this.contentRichTextBox.Name = "contentRichTextBox";
            this.contentRichTextBox.ReadOnly = true;
            this.contentRichTextBox.Size = new System.Drawing.Size(360, 54);
            this.contentRichTextBox.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.contentRichTextBox.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.contentRichTextBox.TabIndex = 5;
            this.contentRichTextBox.Text = "";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(295, 68);
            this.okButton.Name = "okButton";
            this.okButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.okButton.Size = new System.Drawing.Size(63, 25);
            this.okButton.TabIndex = 4;
            this.okButton.Values.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // NotificationOKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 100);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NotificationOKForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.Load += new System.EventHandler(this.CustomMessageBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonButton okButton;
        private Krypton.Toolkit.KryptonRichTextBox contentRichTextBox;
    }
}