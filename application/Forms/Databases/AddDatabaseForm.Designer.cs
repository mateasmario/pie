namespace pie.Forms.Databases
{
    partial class AddDatabaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDatabaseForm));
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.postgresqlCheckButton = new Krypton.Toolkit.KryptonCheckButton();
            this.mssqlCheckButton = new Krypton.Toolkit.KryptonCheckButton();
            this.mysqlCheckButton = new Krypton.Toolkit.KryptonCheckButton();
            this.typeLabel = new Krypton.Toolkit.KryptonLabel();
            this.hostnameTipLabel = new Krypton.Toolkit.KryptonLabel();
            this.databaseConnectionNameTipLabel = new Krypton.Toolkit.KryptonLabel();
            this.databasePasswordTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.passwordLabel = new Krypton.Toolkit.KryptonLabel();
            this.databaseUsernameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.usernameLabel = new Krypton.Toolkit.KryptonLabel();
            this.testConnectionButton = new Krypton.Toolkit.KryptonButton();
            this.databaseNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.databaseNameLabel = new Krypton.Toolkit.KryptonLabel();
            this.databasePortTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.portLabel = new Krypton.Toolkit.KryptonLabel();
            this.databaseHostnameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.databaseConnectionNameTextBox = new Krypton.Toolkit.KryptonTextBox();
            this.saveButton = new Krypton.Toolkit.KryptonButton();
            this.databaseConnectionNameLabel = new Krypton.Toolkit.KryptonLabel();
            this.hostnameLabel = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.postgresqlCheckButton);
            this.mainPanel.Controls.Add(this.mssqlCheckButton);
            this.mainPanel.Controls.Add(this.mysqlCheckButton);
            this.mainPanel.Controls.Add(this.typeLabel);
            this.mainPanel.Controls.Add(this.hostnameTipLabel);
            this.mainPanel.Controls.Add(this.databaseConnectionNameTipLabel);
            this.mainPanel.Controls.Add(this.databasePasswordTextBox);
            this.mainPanel.Controls.Add(this.passwordLabel);
            this.mainPanel.Controls.Add(this.databaseUsernameTextBox);
            this.mainPanel.Controls.Add(this.usernameLabel);
            this.mainPanel.Controls.Add(this.testConnectionButton);
            this.mainPanel.Controls.Add(this.databaseNameTextBox);
            this.mainPanel.Controls.Add(this.databaseNameLabel);
            this.mainPanel.Controls.Add(this.databasePortTextBox);
            this.mainPanel.Controls.Add(this.portLabel);
            this.mainPanel.Controls.Add(this.databaseHostnameTextBox);
            this.mainPanel.Controls.Add(this.databaseConnectionNameTextBox);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Controls.Add(this.databaseConnectionNameLabel);
            this.mainPanel.Controls.Add(this.hostnameLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(411, 416);
            this.mainPanel.TabIndex = 7;
            // 
            // postgresqlCheckButton
            // 
            this.postgresqlCheckButton.Location = new System.Drawing.Point(277, 27);
            this.postgresqlCheckButton.Name = "postgresqlCheckButton";
            this.postgresqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.postgresqlCheckButton.TabIndex = 20;
            this.postgresqlCheckButton.Values.Image = global::pie.Properties.Resources.postgresql_black;
            this.postgresqlCheckButton.Values.Text = "PostgreSQL";
            this.postgresqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // mssqlCheckButton
            // 
            this.mssqlCheckButton.Location = new System.Drawing.Point(143, 27);
            this.mssqlCheckButton.Name = "mssqlCheckButton";
            this.mssqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.mssqlCheckButton.TabIndex = 19;
            this.mssqlCheckButton.Values.Image = global::pie.Properties.Resources.microsoft_black;
            this.mssqlCheckButton.Values.Text = " Microsoft SQL";
            this.mssqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // mysqlCheckButton
            // 
            this.mysqlCheckButton.Location = new System.Drawing.Point(8, 27);
            this.mysqlCheckButton.Name = "mysqlCheckButton";
            this.mysqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.mysqlCheckButton.TabIndex = 18;
            this.mysqlCheckButton.Values.Image = global::pie.Properties.Resources.mysql_black;
            this.mysqlCheckButton.Values.Text = "MySQL";
            this.mysqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(5, 3);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(37, 20);
            this.typeLabel.TabIndex = 17;
            this.typeLabel.Values.Text = "Type";
            // 
            // hostnameTipLabel
            // 
            this.hostnameTipLabel.Location = new System.Drawing.Point(5, 194);
            this.hostnameTipLabel.Name = "hostnameTipLabel";
            this.hostnameTipLabel.Size = new System.Drawing.Size(393, 14);
            this.hostnameTipLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hostnameTipLabel.TabIndex = 15;
            this.hostnameTipLabel.Values.Text = "This is the server your database is hosted on. Try \'localhost\' if your database i" +
    "s hosted locally.";
            // 
            // databaseConnectionNameTipLabel
            // 
            this.databaseConnectionNameTipLabel.Location = new System.Drawing.Point(5, 131);
            this.databaseConnectionNameTipLabel.Name = "databaseConnectionNameTipLabel";
            this.databaseConnectionNameTipLabel.Size = new System.Drawing.Size(313, 14);
            this.databaseConnectionNameTipLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.databaseConnectionNameTipLabel.TabIndex = 14;
            this.databaseConnectionNameTipLabel.Values.Text = "This specifies only how you want the connection to be displayed in the list.";
            // 
            // databasePasswordTextBox
            // 
            this.databasePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasePasswordTextBox.Location = new System.Drawing.Point(7, 334);
            this.databasePasswordTextBox.Name = "databasePasswordTextBox";
            this.databasePasswordTextBox.PasswordChar = '*';
            this.databasePasswordTextBox.Size = new System.Drawing.Size(395, 23);
            this.databasePasswordTextBox.TabIndex = 6;
            // 
            // passwordLabel
            // 
            this.passwordLabel.Location = new System.Drawing.Point(3, 313);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(62, 20);
            this.passwordLabel.TabIndex = 13;
            this.passwordLabel.Values.Text = "Password";
            // 
            // databaseUsernameTextBox
            // 
            this.databaseUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseUsernameTextBox.Location = new System.Drawing.Point(7, 284);
            this.databaseUsernameTextBox.Name = "databaseUsernameTextBox";
            this.databaseUsernameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseUsernameTextBox.TabIndex = 5;
            // 
            // usernameLabel
            // 
            this.usernameLabel.Location = new System.Drawing.Point(3, 263);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(65, 20);
            this.usernameLabel.TabIndex = 11;
            this.usernameLabel.Values.Text = "Username";
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(208, 376);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.testConnectionButton.Size = new System.Drawing.Size(110, 25);
            this.testConnectionButton.TabIndex = 7;
            this.testConnectionButton.Values.Text = "Test Connection";
            this.testConnectionButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // databaseNameTextBox
            // 
            this.databaseNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseNameTextBox.Location = new System.Drawing.Point(8, 234);
            this.databaseNameTextBox.Name = "databaseNameTextBox";
            this.databaseNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseNameTextBox.TabIndex = 4;
            // 
            // databaseNameLabel
            // 
            this.databaseNameLabel.Location = new System.Drawing.Point(4, 213);
            this.databaseNameLabel.Name = "databaseNameLabel";
            this.databaseNameLabel.Size = new System.Drawing.Size(97, 20);
            this.databaseNameLabel.TabIndex = 8;
            this.databaseNameLabel.Values.Text = "Database Name";
            // 
            // databasePortTextBox
            // 
            this.databasePortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasePortTextBox.Location = new System.Drawing.Point(310, 170);
            this.databasePortTextBox.Name = "databasePortTextBox";
            this.databasePortTextBox.Size = new System.Drawing.Size(93, 23);
            this.databasePortTextBox.TabIndex = 3;
            // 
            // portLabel
            // 
            this.portLabel.Location = new System.Drawing.Point(306, 149);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(33, 20);
            this.portLabel.TabIndex = 6;
            this.portLabel.Values.Text = "Port";
            // 
            // databaseHostnameTextBox
            // 
            this.databaseHostnameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseHostnameTextBox.Location = new System.Drawing.Point(8, 170);
            this.databaseHostnameTextBox.Name = "databaseHostnameTextBox";
            this.databaseHostnameTextBox.Size = new System.Drawing.Size(296, 23);
            this.databaseHostnameTextBox.TabIndex = 2;
            // 
            // databaseConnectionNameTextBox
            // 
            this.databaseConnectionNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseConnectionNameTextBox.Location = new System.Drawing.Point(8, 106);
            this.databaseConnectionNameTextBox.Name = "databaseConnectionNameTextBox";
            this.databaseConnectionNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseConnectionNameTextBox.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(324, 376);
            this.saveButton.Name = "saveButton";
            this.saveButton.OverrideFocus.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.saveButton.Size = new System.Drawing.Size(79, 25);
            this.saveButton.TabIndex = 8;
            this.saveButton.Values.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // databaseConnectionNameLabel
            // 
            this.databaseConnectionNameLabel.Location = new System.Drawing.Point(4, 85);
            this.databaseConnectionNameLabel.Name = "databaseConnectionNameLabel";
            this.databaseConnectionNameLabel.Size = new System.Drawing.Size(86, 20);
            this.databaseConnectionNameLabel.TabIndex = 2;
            this.databaseConnectionNameLabel.Values.Text = "Display Name";
            // 
            // hostnameLabel
            // 
            this.hostnameLabel.Location = new System.Drawing.Point(4, 149);
            this.hostnameLabel.Name = "hostnameLabel";
            this.hostnameLabel.Size = new System.Drawing.Size(66, 20);
            this.hostnameLabel.TabIndex = 3;
            this.hostnameLabel.Values.Text = "Hostname";
            // 
            // AddDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 416);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddDatabaseForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Edit Database";
            this.Load += new System.EventHandler(this.AddDatabaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonTextBox databaseHostnameTextBox;
        private Krypton.Toolkit.KryptonTextBox databaseConnectionNameTextBox;
        private Krypton.Toolkit.KryptonButton saveButton;
        private Krypton.Toolkit.KryptonLabel databaseConnectionNameLabel;
        private Krypton.Toolkit.KryptonLabel hostnameLabel;
        private Krypton.Toolkit.KryptonTextBox databaseNameTextBox;
        private Krypton.Toolkit.KryptonLabel databaseNameLabel;
        private Krypton.Toolkit.KryptonTextBox databasePortTextBox;
        private Krypton.Toolkit.KryptonLabel portLabel;
        private Krypton.Toolkit.KryptonButton testConnectionButton;
        private Krypton.Toolkit.KryptonTextBox databaseUsernameTextBox;
        private Krypton.Toolkit.KryptonLabel usernameLabel;
        private Krypton.Toolkit.KryptonTextBox databasePasswordTextBox;
        private Krypton.Toolkit.KryptonLabel passwordLabel;
        private Krypton.Toolkit.KryptonLabel databaseConnectionNameTipLabel;
        private Krypton.Toolkit.KryptonLabel hostnameTipLabel;
        private Krypton.Toolkit.KryptonLabel typeLabel;
        private Krypton.Toolkit.KryptonCheckButton mysqlCheckButton;
        private Krypton.Toolkit.KryptonCheckButton mssqlCheckButton;
        private Krypton.Toolkit.KryptonCheckButton postgresqlCheckButton;
    }
}