namespace pie
{
    partial class GitCommitCredentialsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitCommitCredentialsForm));
            this.saveButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.authorEmailLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.authorNameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.authorEmailTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.authorNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(385, 108);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(76, 25);
            this.saveButton.TabIndex = 1;
            this.saveButton.Values.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.authorEmailLabel);
            this.mainPanel.Controls.Add(this.authorNameLabel);
            this.mainPanel.Controls.Add(this.authorEmailTextBox);
            this.mainPanel.Controls.Add(this.authorNameTextBox);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(474, 142);
            this.mainPanel.TabIndex = 2;
            // 
            // authorEmailLabel
            // 
            this.authorEmailLabel.Location = new System.Drawing.Point(9, 55);
            this.authorEmailLabel.Name = "authorEmailLabel";
            this.authorEmailLabel.Size = new System.Drawing.Size(81, 20);
            this.authorEmailLabel.TabIndex = 5;
            this.authorEmailLabel.Values.Text = "Author Email";
            // 
            // authorNameLabel
            // 
            this.authorNameLabel.Location = new System.Drawing.Point(9, 5);
            this.authorNameLabel.Name = "authorNameLabel";
            this.authorNameLabel.Size = new System.Drawing.Size(84, 20);
            this.authorNameLabel.TabIndex = 4;
            this.authorNameLabel.Values.Text = "Author Name";
            // 
            // authorEmailTextBox
            // 
            this.authorEmailTextBox.Location = new System.Drawing.Point(12, 75);
            this.authorEmailTextBox.Name = "authorEmailTextBox";
            this.authorEmailTextBox.Size = new System.Drawing.Size(449, 23);
            this.authorEmailTextBox.TabIndex = 3;
            // 
            // authorNameTextBox
            // 
            this.authorNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.authorNameTextBox.Name = "authorNameTextBox";
            this.authorNameTextBox.Size = new System.Drawing.Size(449, 23);
            this.authorNameTextBox.TabIndex = 2;
            // 
            // GitCommitCredentialsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(474, 142);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GitCommitCredentialsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commit Credentials";
            this.Load += new System.EventHandler(this.GitCommitCredentialsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonButton saveButton;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel mainPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox authorEmailTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox authorNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel authorEmailLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel authorNameLabel;
    }
}