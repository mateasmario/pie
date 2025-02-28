namespace pie.Forms.Theme
{
    partial class NewThemeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewThemeForm));
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.themeNameLabel = new Krypton.Toolkit.KryptonLabel();
            this.themeNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.createButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.themeNameLabel);
            this.mainPanel.Controls.Add(this.themeNameTextBox);
            this.mainPanel.Controls.Add(this.createButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(474, 94);
            this.mainPanel.TabIndex = 4;
            // 
            // themeNameLabel
            // 
            this.themeNameLabel.Location = new System.Drawing.Point(9, 5);
            this.themeNameLabel.Name = "themeNameLabel";
            this.themeNameLabel.Size = new System.Drawing.Size(84, 20);
            this.themeNameLabel.TabIndex = 4;
            this.themeNameLabel.Values.Text = "Theme Name";
            // 
            // themeNameTextBox
            // 
            this.themeNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.themeNameTextBox.Name = "themeNameTextBox";
            this.themeNameTextBox.Size = new System.Drawing.Size(449, 23);
            this.themeNameTextBox.TabIndex = 2;
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(385, 59);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(76, 25);
            this.createButton.TabIndex = 1;
            this.createButton.Values.Text = "Create";
            this.createButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // NewThemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 94);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewThemeForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Theme";
            this.Load += new System.EventHandler(this.NewThemeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonLabel themeNameLabel;
        private Krypton.Toolkit.KryptonTextBox themeNameTextBox;
        private Krypton.Toolkit.KryptonButton createButton;
    }
}