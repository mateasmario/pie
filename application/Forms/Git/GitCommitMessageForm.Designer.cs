namespace pie.Forms.Git
{
    partial class GitCommitMessageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitCommitMessageForm));
            mainPanel = new Krypton.Toolkit.KryptonPanel();
            commitMessageRichTextBox = new Krypton.Toolkit.KryptonRichTextBox();
            saveButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)mainPanel).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(commitMessageRichTextBox);
            mainPanel.Controls.Add(saveButton);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new System.Drawing.Size(411, 156);
            mainPanel.TabIndex = 3;
            // 
            // commitMessageRichTextBox
            // 
            commitMessageRichTextBox.Location = new System.Drawing.Point(3, 12);
            commitMessageRichTextBox.Name = "commitMessageRichTextBox";
            commitMessageRichTextBox.Size = new System.Drawing.Size(405, 96);
            commitMessageRichTextBox.TabIndex = 3;
            commitMessageRichTextBox.Text = "";
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(331, 126);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(76, 25);
            saveButton.TabIndex = 1;
            saveButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            saveButton.Values.Text = "Save";
            saveButton.Click += saveButton_Click;
            // 
            // GitCommitMessageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(411, 156);
            Controls.Add(mainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "GitCommitMessageForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Commit Message";
            Load += GitCommitMessageForm_Load;
            ((System.ComponentModel.ISupportInitialize)mainPanel).EndInit();
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonButton saveButton;
        private Krypton.Toolkit.KryptonRichTextBox commitMessageRichTextBox;
    }
}