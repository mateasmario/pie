namespace pie
{
    partial class NotificationYesNoCancelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationYesNoCancelForm));
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.contentRichTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            this.yesButton = new Krypton.Toolkit.KryptonButton();
            this.noButton = new Krypton.Toolkit.KryptonButton();
            this.cancelButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.contentRichTextBox);
            this.mainPanel.Controls.Add(this.yesButton);
            this.mainPanel.Controls.Add(this.noButton);
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(366, 95);
            this.mainPanel.TabIndex = 8;
            // 
            // contentRichTextBox
            // 
            this.contentRichTextBox.Location = new System.Drawing.Point(3, 8);
            this.contentRichTextBox.Name = "contentRichTextBox";
            this.contentRichTextBox.ReadOnly = true;
            this.contentRichTextBox.Size = new System.Drawing.Size(360, 45);
            this.contentRichTextBox.StateCommon.Border.Draw = Krypton.Toolkit.InheritBool.False;
            this.contentRichTextBox.StateCommon.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.contentRichTextBox.TabIndex = 7;
            this.contentRichTextBox.Text = "";
            // 
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(157, 64);
            this.yesButton.Name = "yesButton";
            this.yesButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.yesButton.Size = new System.Drawing.Size(63, 25);
            this.yesButton.TabIndex = 6;
            this.yesButton.Values.Text = "Yes";
            this.yesButton.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // noButton
            // 
            this.noButton.Location = new System.Drawing.Point(226, 64);
            this.noButton.Name = "noButton";
            this.noButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.noButton.Size = new System.Drawing.Size(63, 25);
            this.noButton.TabIndex = 5;
            this.noButton.Values.Text = "No";
            this.noButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(295, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cancelButton.Size = new System.Drawing.Size(63, 25);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Values.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // NotificationYesNoCancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 95);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotificationYesNoCancelForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notification";
            this.Load += new System.EventHandler(this.NotificationYesNoCancel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonButton yesButton;
        private Krypton.Toolkit.KryptonButton noButton;
        private Krypton.Toolkit.KryptonButton cancelButton;
        private Krypton.Toolkit.KryptonRichTextBox contentRichTextBox;
    }
}