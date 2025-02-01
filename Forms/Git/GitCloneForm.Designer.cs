namespace pie
{
    partial class GitCloneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitCloneForm));
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.cloneButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.cloneIntoLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.repositoryURLLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cloneIntoTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.repositoryURLTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.browseButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.cloneButton);
            this.mainPanel.Controls.Add(this.cloneIntoLabel);
            this.mainPanel.Controls.Add(this.repositoryURLLabel);
            this.mainPanel.Controls.Add(this.cloneIntoTextBox);
            this.mainPanel.Controls.Add(this.repositoryURLTextBox);
            this.mainPanel.Controls.Add(this.browseButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(550, 148);
            this.mainPanel.TabIndex = 3;
            // 
            // cloneButton
            // 
            this.cloneButton.Location = new System.Drawing.Point(467, 115);
            this.cloneButton.Name = "cloneButton";
            this.cloneButton.Size = new System.Drawing.Size(76, 25);
            this.cloneButton.TabIndex = 6;
            this.cloneButton.Values.Text = "Clone";
            this.cloneButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // cloneIntoLabel
            // 
            this.cloneIntoLabel.Location = new System.Drawing.Point(7, 52);
            this.cloneIntoLabel.Name = "cloneIntoLabel";
            this.cloneIntoLabel.Size = new System.Drawing.Size(67, 20);
            this.cloneIntoLabel.TabIndex = 5;
            this.cloneIntoLabel.Values.Text = "Clone into";
            // 
            // repositoryURLLabel
            // 
            this.repositoryURLLabel.Location = new System.Drawing.Point(7, 5);
            this.repositoryURLLabel.Name = "repositoryURLLabel";
            this.repositoryURLLabel.Size = new System.Drawing.Size(94, 20);
            this.repositoryURLLabel.TabIndex = 4;
            this.repositoryURLLabel.Values.Text = "Repository URL";
            // 
            // cloneIntoTextBox
            // 
            this.cloneIntoTextBox.Location = new System.Drawing.Point(12, 72);
            this.cloneIntoTextBox.Name = "cloneIntoTextBox";
            this.cloneIntoTextBox.Size = new System.Drawing.Size(449, 23);
            this.cloneIntoTextBox.TabIndex = 3;
            // 
            // repositoryURLTextBox
            // 
            this.repositoryURLTextBox.Location = new System.Drawing.Point(12, 25);
            this.repositoryURLTextBox.Name = "repositoryURLTextBox";
            this.repositoryURLTextBox.Size = new System.Drawing.Size(449, 23);
            this.repositoryURLTextBox.TabIndex = 2;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(467, 72);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(76, 25);
            this.browseButton.TabIndex = 1;
            this.browseButton.Values.Text = "Browse";
            this.browseButton.Click += new System.EventHandler(this.kryptonButton1_Click_1);
            // 
            // GitCloneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 148);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GitCloneForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Git Clone";
            this.Load += new System.EventHandler(this.GitCloneForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel mainPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton cloneButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel cloneIntoLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel repositoryURLLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox cloneIntoTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox repositoryURLTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton browseButton;
    }
}