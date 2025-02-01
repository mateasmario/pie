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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.postgresqlCheckButton = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.mssqlCheckButton = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.mysqlCheckButton = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonLabel9 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel7 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.databasePasswordTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel6 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.databaseUsernameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton2 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.databaseNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.databasePortTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.databaseHostnameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.databaseConnectionNameTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.postgresqlCheckButton);
            this.kryptonPanel1.Controls.Add(this.mssqlCheckButton);
            this.kryptonPanel1.Controls.Add(this.mysqlCheckButton);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel9);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel7);
            this.kryptonPanel1.Controls.Add(this.databasePasswordTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel6);
            this.kryptonPanel1.Controls.Add(this.databaseUsernameTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.kryptonButton2);
            this.kryptonPanel1.Controls.Add(this.databaseNameTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.databasePortTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.databaseHostnameTextBox);
            this.kryptonPanel1.Controls.Add(this.databaseConnectionNameTextBox);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(411, 416);
            this.kryptonPanel1.TabIndex = 7;
            // 
            // kryptonCheckButton3
            // 
            this.postgresqlCheckButton.Location = new System.Drawing.Point(277, 27);
            this.postgresqlCheckButton.Name = "kryptonCheckButton3";
            this.postgresqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.postgresqlCheckButton.TabIndex = 20;
            this.postgresqlCheckButton.Values.Image = global::pie.Properties.Resources.postgresql_black;
            this.postgresqlCheckButton.Values.Text = "PostgreSQL";
            this.postgresqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // kryptonCheckButton2
            // 
            this.mssqlCheckButton.Location = new System.Drawing.Point(143, 27);
            this.mssqlCheckButton.Name = "kryptonCheckButton2";
            this.mssqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.mssqlCheckButton.TabIndex = 19;
            this.mssqlCheckButton.Values.Image = global::pie.Properties.Resources.microsoft_black;
            this.mssqlCheckButton.Values.Text = " Microsoft SQL";
            this.mssqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // kryptonCheckButton1
            // 
            this.mysqlCheckButton.Location = new System.Drawing.Point(8, 27);
            this.mysqlCheckButton.Name = "kryptonCheckButton1";
            this.mysqlCheckButton.Size = new System.Drawing.Size(128, 49);
            this.mysqlCheckButton.TabIndex = 18;
            this.mysqlCheckButton.Values.Image = global::pie.Properties.Resources.mysql_black;
            this.mysqlCheckButton.Values.Text = "MySQL";
            this.mysqlCheckButton.Click += new System.EventHandler(this.kryptonCheckButton1_Click);
            // 
            // kryptonLabel9
            // 
            this.kryptonLabel9.Location = new System.Drawing.Point(5, 3);
            this.kryptonLabel9.Name = "kryptonLabel9";
            this.kryptonLabel9.Size = new System.Drawing.Size(37, 20);
            this.kryptonLabel9.TabIndex = 17;
            this.kryptonLabel9.Values.Text = "Type";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(5, 194);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(393, 14);
            this.kryptonLabel8.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel8.TabIndex = 15;
            this.kryptonLabel8.Values.Text = "This is the server your database is hosted on. Try \'localhost\' if your database i" +
    "s hosted locally.";
            // 
            // kryptonLabel7
            // 
            this.kryptonLabel7.Location = new System.Drawing.Point(5, 131);
            this.kryptonLabel7.Name = "kryptonLabel7";
            this.kryptonLabel7.Size = new System.Drawing.Size(313, 14);
            this.kryptonLabel7.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabel7.TabIndex = 14;
            this.kryptonLabel7.Values.Text = "This specifies only how you want the connection to be displayed in the list.";
            // 
            // kryptonTextBox6
            // 
            this.databasePasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasePasswordTextBox.Location = new System.Drawing.Point(7, 334);
            this.databasePasswordTextBox.Name = "kryptonTextBox6";
            this.databasePasswordTextBox.PasswordChar = '*';
            this.databasePasswordTextBox.Size = new System.Drawing.Size(395, 23);
            this.databasePasswordTextBox.TabIndex = 6;
            // 
            // kryptonLabel6
            // 
            this.kryptonLabel6.Location = new System.Drawing.Point(3, 313);
            this.kryptonLabel6.Name = "kryptonLabel6";
            this.kryptonLabel6.Size = new System.Drawing.Size(62, 20);
            this.kryptonLabel6.TabIndex = 13;
            this.kryptonLabel6.Values.Text = "Password";
            // 
            // kryptonTextBox5
            // 
            this.databaseUsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseUsernameTextBox.Location = new System.Drawing.Point(7, 284);
            this.databaseUsernameTextBox.Name = "kryptonTextBox5";
            this.databaseUsernameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseUsernameTextBox.TabIndex = 5;
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(3, 263);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(65, 20);
            this.kryptonLabel5.TabIndex = 11;
            this.kryptonLabel5.Values.Text = "Username";
            // 
            // kryptonButton2
            // 
            this.kryptonButton2.Location = new System.Drawing.Point(208, 376);
            this.kryptonButton2.Name = "kryptonButton2";
            this.kryptonButton2.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton2.Size = new System.Drawing.Size(110, 25);
            this.kryptonButton2.TabIndex = 7;
            this.kryptonButton2.Values.Text = "Test Connection";
            this.kryptonButton2.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // kryptonTextBox4
            // 
            this.databaseNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseNameTextBox.Location = new System.Drawing.Point(8, 234);
            this.databaseNameTextBox.Name = "kryptonTextBox4";
            this.databaseNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseNameTextBox.TabIndex = 4;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(4, 213);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(97, 20);
            this.kryptonLabel4.TabIndex = 8;
            this.kryptonLabel4.Values.Text = "Database Name";
            // 
            // kryptonTextBox3
            // 
            this.databasePortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasePortTextBox.Location = new System.Drawing.Point(310, 170);
            this.databasePortTextBox.Name = "kryptonTextBox3";
            this.databasePortTextBox.Size = new System.Drawing.Size(93, 23);
            this.databasePortTextBox.TabIndex = 3;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(306, 149);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(33, 20);
            this.kryptonLabel3.TabIndex = 6;
            this.kryptonLabel3.Values.Text = "Port";
            // 
            // kryptonTextBox2
            // 
            this.databaseHostnameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseHostnameTextBox.Location = new System.Drawing.Point(8, 170);
            this.databaseHostnameTextBox.Name = "kryptonTextBox2";
            this.databaseHostnameTextBox.Size = new System.Drawing.Size(296, 23);
            this.databaseHostnameTextBox.TabIndex = 2;
            // 
            // kryptonTextBox1
            // 
            this.databaseConnectionNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseConnectionNameTextBox.Location = new System.Drawing.Point(8, 106);
            this.databaseConnectionNameTextBox.Name = "kryptonTextBox1";
            this.databaseConnectionNameTextBox.Size = new System.Drawing.Size(395, 23);
            this.databaseConnectionNameTextBox.TabIndex = 0;
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(324, 376);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.Size = new System.Drawing.Size(79, 25);
            this.kryptonButton1.TabIndex = 8;
            this.kryptonButton1.Values.Text = "Save";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(4, 85);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(86, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "Display Name";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(4, 149);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(66, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Hostname";
            // 
            // AddDatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 416);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddDatabaseForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add / Edit Database";
            this.Load += new System.EventHandler(this.AddDatabaseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databaseHostnameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databaseConnectionNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databaseNameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databasePortTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databaseUsernameTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox databasePasswordTextBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel7;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel9;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton mysqlCheckButton;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton mssqlCheckButton;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton postgresqlCheckButton;
    }
}