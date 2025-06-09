namespace pie.Forms.Git
{
    partial class GitSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitSettingsForm));
            mainPanel = new Krypton.Toolkit.KryptonPanel();
            proxyTipLabel = new Krypton.Toolkit.KryptonLabel();
            proxyTextBox = new Krypton.Toolkit.KryptonTextBox();
            proxyLabel = new Krypton.Toolkit.KryptonLabel();
            remoteServerPasswordTextBox = new Krypton.Toolkit.KryptonTextBox();
            remoteServerUsernameTextBox = new Krypton.Toolkit.KryptonTextBox();
            remoteServerUsernameLabel = new Krypton.Toolkit.KryptonLabel();
            remoteServerPasswordLabel = new Krypton.Toolkit.KryptonLabel();
            authorEmailTextBox = new Krypton.Toolkit.KryptonTextBox();
            authorNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            saveButton = new Krypton.Toolkit.KryptonButton();
            authorNameLabel = new Krypton.Toolkit.KryptonLabel();
            authorEmailLabel = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)mainPanel).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(proxyTipLabel);
            mainPanel.Controls.Add(proxyTextBox);
            mainPanel.Controls.Add(proxyLabel);
            mainPanel.Controls.Add(remoteServerPasswordTextBox);
            mainPanel.Controls.Add(remoteServerUsernameTextBox);
            mainPanel.Controls.Add(remoteServerUsernameLabel);
            mainPanel.Controls.Add(remoteServerPasswordLabel);
            mainPanel.Controls.Add(authorEmailTextBox);
            mainPanel.Controls.Add(authorNameTextBox);
            mainPanel.Controls.Add(saveButton);
            mainPanel.Controls.Add(authorNameLabel);
            mainPanel.Controls.Add(authorEmailLabel);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new System.Drawing.Size(480, 366);
            mainPanel.TabIndex = 8;
            // 
            // proxyTipLabel
            // 
            proxyTipLabel.Location = new System.Drawing.Point(5, 293);
            proxyTipLabel.Name = "proxyTipLabel";
            proxyTipLabel.Size = new System.Drawing.Size(175, 14);
            proxyTipLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            proxyTipLabel.TabIndex = 22;
            proxyTipLabel.Values.Text = "Leave empty if no proxy should be used.";
            // 
            // proxyTextBox
            // 
            proxyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            proxyTextBox.Location = new System.Drawing.Point(9, 268);
            proxyTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            proxyTextBox.Name = "proxyTextBox";
            proxyTextBox.Size = new System.Drawing.Size(461, 23);
            proxyTextBox.TabIndex = 20;
            // 
            // proxyLabel
            // 
            proxyLabel.Location = new System.Drawing.Point(5, 244);
            proxyLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            proxyLabel.Name = "proxyLabel";
            proxyLabel.Size = new System.Drawing.Size(104, 20);
            proxyLabel.TabIndex = 21;
            proxyLabel.Values.Text = "Proxy URL + Port";
            // 
            // remoteServerPasswordTextBox
            // 
            remoteServerPasswordTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            remoteServerPasswordTextBox.Location = new System.Drawing.Point(9, 210);
            remoteServerPasswordTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            remoteServerPasswordTextBox.Name = "remoteServerPasswordTextBox";
            remoteServerPasswordTextBox.PasswordChar = '●';
            remoteServerPasswordTextBox.Size = new System.Drawing.Size(461, 23);
            remoteServerPasswordTextBox.TabIndex = 3;
            // 
            // remoteServerUsernameTextBox
            // 
            remoteServerUsernameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            remoteServerUsernameTextBox.Location = new System.Drawing.Point(9, 152);
            remoteServerUsernameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            remoteServerUsernameTextBox.Name = "remoteServerUsernameTextBox";
            remoteServerUsernameTextBox.Size = new System.Drawing.Size(462, 23);
            remoteServerUsernameTextBox.TabIndex = 2;
            // 
            // remoteServerUsernameLabel
            // 
            remoteServerUsernameLabel.Location = new System.Drawing.Point(5, 128);
            remoteServerUsernameLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            remoteServerUsernameLabel.Name = "remoteServerUsernameLabel";
            remoteServerUsernameLabel.Size = new System.Drawing.Size(149, 20);
            remoteServerUsernameLabel.TabIndex = 18;
            remoteServerUsernameLabel.Values.Text = "Remote Server Username";
            // 
            // remoteServerPasswordLabel
            // 
            remoteServerPasswordLabel.Location = new System.Drawing.Point(5, 186);
            remoteServerPasswordLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            remoteServerPasswordLabel.Name = "remoteServerPasswordLabel";
            remoteServerPasswordLabel.Size = new System.Drawing.Size(230, 20);
            remoteServerPasswordLabel.TabIndex = 19;
            remoteServerPasswordLabel.Values.Text = "Remote Server Password / Access Token";
            // 
            // authorEmailTextBox
            // 
            authorEmailTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            authorEmailTextBox.Location = new System.Drawing.Point(9, 95);
            authorEmailTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            authorEmailTextBox.Name = "authorEmailTextBox";
            authorEmailTextBox.Size = new System.Drawing.Size(461, 23);
            authorEmailTextBox.TabIndex = 1;
            // 
            // authorNameTextBox
            // 
            authorNameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            authorNameTextBox.Location = new System.Drawing.Point(9, 37);
            authorNameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            authorNameTextBox.Name = "authorNameTextBox";
            authorNameTextBox.Size = new System.Drawing.Size(462, 23);
            authorNameTextBox.TabIndex = 0;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(378, 330);
            saveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            saveButton.Name = "saveButton";
            saveButton.OverrideFocus.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            saveButton.Size = new System.Drawing.Size(92, 29);
            saveButton.TabIndex = 4;
            saveButton.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            saveButton.Values.Text = "Save";
            saveButton.Click += kryptonButton1_Click;
            // 
            // authorNameLabel
            // 
            authorNameLabel.Location = new System.Drawing.Point(5, 13);
            authorNameLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            authorNameLabel.Name = "authorNameLabel";
            authorNameLabel.Size = new System.Drawing.Size(84, 20);
            authorNameLabel.TabIndex = 2;
            authorNameLabel.Values.Text = "Author Name";
            // 
            // authorEmailLabel
            // 
            authorEmailLabel.Location = new System.Drawing.Point(5, 70);
            authorEmailLabel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            authorEmailLabel.Name = "authorEmailLabel";
            authorEmailLabel.Size = new System.Drawing.Size(81, 20);
            authorEmailLabel.TabIndex = 3;
            authorEmailLabel.Values.Text = "Author Email";
            // 
            // GitSettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(480, 366);
            Controls.Add(mainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "GitSettingsForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Git Settings";
            Load += GitSettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)mainPanel).EndInit();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonTextBox authorEmailTextBox;
        private Krypton.Toolkit.KryptonTextBox authorNameTextBox;
        private Krypton.Toolkit.KryptonButton saveButton;
        private Krypton.Toolkit.KryptonLabel authorNameLabel;
        private Krypton.Toolkit.KryptonLabel authorEmailLabel;
        private Krypton.Toolkit.KryptonTextBox remoteServerPasswordTextBox;
        private Krypton.Toolkit.KryptonTextBox remoteServerUsernameTextBox;
        private Krypton.Toolkit.KryptonLabel remoteServerUsernameLabel;
        private Krypton.Toolkit.KryptonLabel remoteServerPasswordLabel;
        private Krypton.Toolkit.KryptonTextBox proxyTextBox;
        private Krypton.Toolkit.KryptonLabel proxyLabel;
        private Krypton.Toolkit.KryptonLabel proxyTipLabel;
    }
}