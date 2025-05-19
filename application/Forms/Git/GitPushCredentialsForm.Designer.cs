namespace pie
{
    partial class GitPushCredentialsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitPushCredentialsForm));
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.remoteServerPasswordLabel = new Krypton.Toolkit.KryptonLabel();
            this.remoteServerUsernameLabel = new Krypton.Toolkit.KryptonLabel();
            this.remoteServerPasswordTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.remoteServerUsernameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.saveButton = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.remoteServerPasswordLabel);
            this.mainPanel.Controls.Add(this.remoteServerUsernameLabel);
            this.mainPanel.Controls.Add(this.remoteServerPasswordTextBox);
            this.mainPanel.Controls.Add(this.remoteServerUsernameTextBox);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(474, 142);
            this.mainPanel.TabIndex = 3;
            // 
            // remoteServerPasswordLabel
            // 
            this.remoteServerPasswordLabel.Location = new System.Drawing.Point(9, 55);
            this.remoteServerPasswordLabel.Name = "remoteServerPasswordLabel";
            this.remoteServerPasswordLabel.Size = new System.Drawing.Size(230, 20);
            this.remoteServerPasswordLabel.TabIndex = 5;
            this.remoteServerPasswordLabel.Values.Text = "Remote Server Password / Access Token";
            // 
            // remoteServerUsernameLabel
            // 
            this.remoteServerUsernameLabel.Location = new System.Drawing.Point(9, 5);
            this.remoteServerUsernameLabel.Name = "remoteServerUsernameLabel";
            this.remoteServerUsernameLabel.Size = new System.Drawing.Size(149, 20);
            this.remoteServerUsernameLabel.TabIndex = 4;
            this.remoteServerUsernameLabel.Values.Text = "Remote Server Username";
            // 
            // remoteServerPasswordTextBox
            // 
            this.remoteServerPasswordTextBox.Location = new System.Drawing.Point(12, 75);
            this.remoteServerPasswordTextBox.Name = "remoteServerPasswordTextBox";
            this.remoteServerPasswordTextBox.PasswordChar = '●';
            this.remoteServerPasswordTextBox.Size = new System.Drawing.Size(449, 23);
            this.remoteServerPasswordTextBox.TabIndex = 3;
            // 
            // remoteServerUsernameTextBox
            // 
            this.remoteServerUsernameTextBox.Location = new System.Drawing.Point(12, 25);
            this.remoteServerUsernameTextBox.Name = "remoteServerUsernameTextBox";
            this.remoteServerUsernameTextBox.Size = new System.Drawing.Size(449, 23);
            this.remoteServerUsernameTextBox.TabIndex = 2;
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
            // GitPushCredentialsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 142);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GitPushCredentialsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Push Credentials";
            this.Load += new System.EventHandler(this.GitPushCredentialsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonLabel remoteServerPasswordLabel;
        private Krypton.Toolkit.KryptonLabel remoteServerUsernameLabel;
        private Krypton.Toolkit.KryptonTextBox remoteServerPasswordTextBox;
        private Krypton.Toolkit.KryptonTextBox remoteServerUsernameTextBox;
        private Krypton.Toolkit.KryptonButton saveButton;
    }
}