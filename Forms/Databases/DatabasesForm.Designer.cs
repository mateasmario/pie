namespace pie.Forms.Databases
{
    partial class DatabasesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabasesForm));
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.databasesListView = new BrightIdeasSoftware.ObjectListView();
            this.ConnectionNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.applyButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.databaseConnectionsLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.removeButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.addButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databasesListView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.databasesListView);
            this.mainPanel.Controls.Add(this.applyButton);
            this.mainPanel.Controls.Add(this.databaseConnectionsLabel);
            this.mainPanel.Controls.Add(this.removeButton);
            this.mainPanel.Controls.Add(this.addButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(543, 320);
            this.mainPanel.TabIndex = 7;
            // 
            // databasesListView
            // 
            this.databasesListView.AllColumns.Add(this.ConnectionNameColumn);
            this.databasesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databasesListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.databasesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ConnectionNameColumn});
            this.databasesListView.HideSelection = false;
            this.databasesListView.Location = new System.Drawing.Point(10, 29);
            this.databasesListView.MultiSelect = false;
            this.databasesListView.Name = "databasesListView";
            this.databasesListView.Size = new System.Drawing.Size(523, 244);
            this.databasesListView.TabIndex = 5;
            this.databasesListView.UseCompatibleStateImageBehavior = false;
            this.databasesListView.View = System.Windows.Forms.View.Details;
            this.databasesListView.DoubleClick += new System.EventHandler(this.databasesListView_DoubleClick);
            // 
            // ConnectionNameColumn
            // 
            this.ConnectionNameColumn.AspectName = "Name";
            this.ConnectionNameColumn.CellPadding = null;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(467, 282);
            this.applyButton.Name = "applyButton";
            this.applyButton.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.applyButton.Size = new System.Drawing.Size(66, 25);
            this.applyButton.TabIndex = 4;
            this.applyButton.Values.Text = "Apply";
            this.applyButton.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // databaseConnectionsLabel
            // 
            this.databaseConnectionsLabel.Location = new System.Drawing.Point(5, 6);
            this.databaseConnectionsLabel.Name = "databaseConnectionsLabel";
            this.databaseConnectionsLabel.Size = new System.Drawing.Size(132, 20);
            this.databaseConnectionsLabel.TabIndex = 0;
            this.databaseConnectionsLabel.Values.Text = "Database Connections";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(82, 282);
            this.removeButton.Name = "removeButton";
            this.removeButton.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.removeButton.Size = new System.Drawing.Size(66, 25);
            this.removeButton.TabIndex = 2;
            this.removeButton.Values.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(10, 282);
            this.addButton.Name = "addButton";
            this.addButton.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.addButton.Size = new System.Drawing.Size(66, 25);
            this.addButton.TabIndex = 3;
            this.addButton.Values.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // DatabasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 320);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DatabasesForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Databases";
            this.Load += new System.EventHandler(this.DatabasesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.databasesListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel mainPanel;
        private BrightIdeasSoftware.ObjectListView databasesListView;
        private BrightIdeasSoftware.OLVColumn ConnectionNameColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonButton applyButton;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel databaseConnectionsLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton removeButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton addButton;
    }
}