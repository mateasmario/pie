namespace pie
{
    partial class AddBuildCommandForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBuildCommandForm));
            this.displayNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.commandTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.displayNameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.commandLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.saveButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.commandTipLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.fileExtensionsTipLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.fileExtensionsRichTextBox = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayNameTextBox
            // 
            this.displayNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayNameTextBox.Location = new System.Drawing.Point(8, 29);
            this.displayNameTextBox.Name = "displayNameTextBox";
            this.displayNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.displayNameTextBox.TabIndex = 0;
            // 
            // commandTextBox
            // 
            this.commandTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandTextBox.Location = new System.Drawing.Point(8, 79);
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(395, 23);
            this.commandTextBox.TabIndex = 1;
            // 
            // displayNameLabel
            // 
            this.displayNameLabel.Location = new System.Drawing.Point(4, 8);
            this.displayNameLabel.Name = "displayNameLabel";
            this.displayNameLabel.Size = new System.Drawing.Size(86, 20);
            this.displayNameLabel.TabIndex = 2;
            this.displayNameLabel.Values.Text = "Display Name";
            // 
            // commandLabel
            // 
            this.commandLabel.Location = new System.Drawing.Point(4, 58);
            this.commandLabel.Name = "commandLabel";
            this.commandLabel.Size = new System.Drawing.Size(67, 20);
            this.commandLabel.TabIndex = 3;
            this.commandLabel.Values.Text = "Command";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(324, 278);
            this.saveButton.Name = "saveButton";
            this.saveButton.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.saveButton.Size = new System.Drawing.Size(79, 25);
            this.saveButton.TabIndex = 3;
            this.saveButton.Values.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // commandTipLabel
            // 
            this.commandTipLabel.Location = new System.Drawing.Point(4, 105);
            this.commandTipLabel.Name = "commandTipLabel";
            this.commandTipLabel.Size = new System.Drawing.Size(274, 14);
            this.commandTipLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandTipLabel.TabIndex = 5;
            this.commandTipLabel.Values.Text = "$FILE can be used as a placeholder for the currently opened file.";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.fileExtensionsTipLabel);
            this.mainPanel.Controls.Add(this.fileExtensionsRichTextBox);
            this.mainPanel.Controls.Add(this.kryptonLabel5);
            this.mainPanel.Controls.Add(this.commandTextBox);
            this.mainPanel.Controls.Add(this.commandTipLabel);
            this.mainPanel.Controls.Add(this.displayNameTextBox);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Controls.Add(this.displayNameLabel);
            this.mainPanel.Controls.Add(this.commandLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(411, 311);
            this.mainPanel.TabIndex = 6;
            // 
            // fileExtensionsTipLabel
            // 
            this.fileExtensionsTipLabel.Location = new System.Drawing.Point(4, 246);
            this.fileExtensionsTipLabel.Name = "fileExtensionsTipLabel";
            this.fileExtensionsTipLabel.Size = new System.Drawing.Size(131, 14);
            this.fileExtensionsTipLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileExtensionsTipLabel.TabIndex = 10;
            this.fileExtensionsTipLabel.Values.Text = "Leave blank to match all files.";
            // 
            // fileExtensionsRichTextBox
            // 
            this.fileExtensionsRichTextBox.Location = new System.Drawing.Point(8, 146);
            this.fileExtensionsRichTextBox.Name = "fileExtensionsRichTextBox";
            this.fileExtensionsRichTextBox.Size = new System.Drawing.Size(395, 96);
            this.fileExtensionsRichTextBox.TabIndex = 2;
            this.fileExtensionsRichTextBox.Text = "";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(4, 125);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(152, 20);
            this.kryptonLabel5.TabIndex = 7;
            this.kryptonLabel5.Values.Text = "Associated File Extensions";
            // 
            // AddBuildCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(411, 311);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBuildCommandForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Edit Build Command";
            this.Load += new System.EventHandler(this.AddBuildCommandForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox displayNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox commandTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel displayNameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel commandLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton saveButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel commandTipLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel mainPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox fileExtensionsRichTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel fileExtensionsTipLabel;
    }
}