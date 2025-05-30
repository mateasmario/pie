namespace pie.Forms.Git
{
    partial class GitConnectToRemoteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitConnectToRemoteForm));
            mainPanel = new Krypton.Toolkit.KryptonPanel();
            connectButton = new Krypton.Toolkit.KryptonButton();
            repositoryURLLabel = new Krypton.Toolkit.KryptonLabel();
            repositoryURLTextBox = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)mainPanel).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(connectButton);
            mainPanel.Controls.Add(repositoryURLLabel);
            mainPanel.Controls.Add(repositoryURLTextBox);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new System.Drawing.Size(456, 109);
            mainPanel.TabIndex = 4;
            // 
            // connectButton
            // 
            connectButton.Location = new System.Drawing.Point(349, 74);
            connectButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            connectButton.Name = "connectButton";
            connectButton.Size = new System.Drawing.Size(89, 29);
            connectButton.TabIndex = 6;
            connectButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            connectButton.Values.Text = "Connect";
            connectButton.Click += connectButton_Click;
            // 
            // repositoryURLLabel
            // 
            repositoryURLLabel.Location = new System.Drawing.Point(8, 6);
            repositoryURLLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            repositoryURLLabel.Name = "repositoryURLLabel";
            repositoryURLLabel.Size = new System.Drawing.Size(94, 20);
            repositoryURLLabel.TabIndex = 4;
            repositoryURLLabel.Values.Text = "Repository URL";
            // 
            // repositoryURLTextBox
            // 
            repositoryURLTextBox.Location = new System.Drawing.Point(14, 29);
            repositoryURLTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            repositoryURLTextBox.Name = "repositoryURLTextBox";
            repositoryURLTextBox.Size = new System.Drawing.Size(424, 23);
            repositoryURLTextBox.TabIndex = 2;
            // 
            // GitConnectToRemoteForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(456, 109);
            Controls.Add(mainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GitConnectToRemoteForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Connect to remote repository";
            Load += GitConnectToRemoteForm_Load;
            ((System.ComponentModel.ISupportInitialize)mainPanel).EndInit();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonButton connectButton;
        private Krypton.Toolkit.KryptonLabel repositoryURLLabel;
        private Krypton.Toolkit.KryptonTextBox repositoryURLTextBox;
    }
}