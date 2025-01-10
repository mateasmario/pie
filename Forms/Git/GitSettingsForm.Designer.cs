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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.remoteServerPasswordTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.remoteServerUsernameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.remoteServerUsernameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.remoteServerPasswordLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.authorEmailTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.authorNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.saveButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.authorNameLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.authorEmailLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.remoteServerPasswordTextBox);
            this.kryptonPanel1.Controls.Add(this.remoteServerUsernameTextBox);
            this.kryptonPanel1.Controls.Add(this.remoteServerUsernameLabel);
            this.kryptonPanel1.Controls.Add(this.remoteServerPasswordLabel);
            this.kryptonPanel1.Controls.Add(this.authorEmailTextBox);
            this.kryptonPanel1.Controls.Add(this.authorNameTextBox);
            this.kryptonPanel1.Controls.Add(this.saveButton);
            this.kryptonPanel1.Controls.Add(this.authorNameLabel);
            this.kryptonPanel1.Controls.Add(this.authorEmailLabel);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(411, 254);
            this.kryptonPanel1.TabIndex = 8;
            // 
            // remoteServerPasswordTextBox
            // 
            this.remoteServerPasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteServerPasswordTextBox.Location = new System.Drawing.Point(8, 182);
            this.remoteServerPasswordTextBox.Name = "remoteServerPasswordTextBox";
            this.remoteServerPasswordTextBox.PasswordChar = '●';
            this.remoteServerPasswordTextBox.Size = new System.Drawing.Size(394, 23);
            this.remoteServerPasswordTextBox.TabIndex = 3;
            // 
            // remoteServerUsernameTextBox
            // 
            this.remoteServerUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.remoteServerUsernameTextBox.Location = new System.Drawing.Point(8, 132);
            this.remoteServerUsernameTextBox.Name = "remoteServerUsernameTextBox";
            this.remoteServerUsernameTextBox.Size = new System.Drawing.Size(395, 23);
            this.remoteServerUsernameTextBox.TabIndex = 2;
            // 
            // remoteServerUsernameLabel
            // 
            this.remoteServerUsernameLabel.Location = new System.Drawing.Point(4, 111);
            this.remoteServerUsernameLabel.Name = "remoteServerUsernameLabel";
            this.remoteServerUsernameLabel.Size = new System.Drawing.Size(149, 20);
            this.remoteServerUsernameLabel.TabIndex = 18;
            this.remoteServerUsernameLabel.Values.Text = "Remote Server Username";
            // 
            // remoteServerPasswordLabel
            // 
            this.remoteServerPasswordLabel.Location = new System.Drawing.Point(4, 161);
            this.remoteServerPasswordLabel.Name = "remoteServerPasswordLabel";
            this.remoteServerPasswordLabel.Size = new System.Drawing.Size(230, 20);
            this.remoteServerPasswordLabel.TabIndex = 19;
            this.remoteServerPasswordLabel.Values.Text = "Remote Server Password / Access Token";
            // 
            // authorEmailTextBox
            // 
            this.authorEmailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorEmailTextBox.Location = new System.Drawing.Point(8, 82);
            this.authorEmailTextBox.Name = "authorEmailTextBox";
            this.authorEmailTextBox.Size = new System.Drawing.Size(394, 23);
            this.authorEmailTextBox.TabIndex = 1;
            // 
            // authorNameTextBox
            // 
            this.authorNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorNameTextBox.Location = new System.Drawing.Point(8, 32);
            this.authorNameTextBox.Name = "authorNameTextBox";
            this.authorNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.authorNameTextBox.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(324, 219);
            this.saveButton.Name = "saveButton";
            this.saveButton.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.saveButton.Size = new System.Drawing.Size(79, 25);
            this.saveButton.TabIndex = 4;
            this.saveButton.Values.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // authorNameLabel
            // 
            this.authorNameLabel.Location = new System.Drawing.Point(4, 11);
            this.authorNameLabel.Name = "authorNameLabel";
            this.authorNameLabel.Size = new System.Drawing.Size(84, 20);
            this.authorNameLabel.TabIndex = 2;
            this.authorNameLabel.Values.Text = "Author Name";
            // 
            // authorEmailLabel
            // 
            this.authorEmailLabel.Location = new System.Drawing.Point(4, 61);
            this.authorEmailLabel.Name = "authorEmailLabel";
            this.authorEmailLabel.Size = new System.Drawing.Size(81, 20);
            this.authorEmailLabel.TabIndex = 3;
            this.authorEmailLabel.Values.Text = "Author Email";
            // 
            // GitSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 254);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GitSettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Git Settings";
            this.Load += new System.EventHandler(this.GitSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox authorEmailTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox authorNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton saveButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel authorNameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel authorEmailLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox remoteServerPasswordTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox remoteServerUsernameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel remoteServerUsernameLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel remoteServerPasswordLabel;
    }
}