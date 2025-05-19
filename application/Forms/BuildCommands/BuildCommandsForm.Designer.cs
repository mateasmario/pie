namespace pie
{
    partial class BuildCommandsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildCommandsForm));
            this.kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            this.kryptonListBox1 = new Krypton.Toolkit.KryptonListBox();
            this.kryptonPage1 = new Krypton.Navigator.KryptonPage();
            this.buildCommandsLabel = new Krypton.Toolkit.KryptonLabel();
            this.removeButton = new Krypton.Toolkit.KryptonButton();
            this.addButton = new Krypton.Toolkit.KryptonButton();
            this.applyButton = new Krypton.Toolkit.KryptonButton();
            this.mainPanel = new Krypton.Toolkit.KryptonPanel();
            this.buildCommandsListView = new BrightIdeasSoftware.ObjectListView();
            this.BuildCommandNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).BeginInit();
            this.kryptonPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildCommandsListView)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(5, 9);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(149, 20);
            this.kryptonLabel1.TabIndex = 1;
            this.kryptonLabel1.Values.Text = "Custom Build Commands";
            // 
            // kryptonListBox1
            // 
            this.kryptonListBox1.Items.AddRange(new object[] {
            "sdasd"});
            this.kryptonListBox1.Location = new System.Drawing.Point(10, 30);
            this.kryptonListBox1.Name = "kryptonListBox1";
            this.kryptonListBox1.Size = new System.Drawing.Size(519, 184);
            this.kryptonListBox1.TabIndex = 0;
            // 
            // kryptonPage1
            // 
            this.kryptonPage1.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPage1.Controls.Add(this.kryptonLabel1);
            this.kryptonPage1.Controls.Add(this.kryptonListBox1);
            this.kryptonPage1.Flags = 65534;
            this.kryptonPage1.LastVisibleSet = true;
            this.kryptonPage1.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPage1.Name = "kryptonPage1";
            this.kryptonPage1.Size = new System.Drawing.Size(541, 445);
            this.kryptonPage1.Text = "Building";
            this.kryptonPage1.ToolTipTitle = "Page ToolTip";
            this.kryptonPage1.UniqueName = "26EE49FE8BF4430326BBCAF7F39C9913";
            // 
            // buildCommandsLabel
            // 
            this.buildCommandsLabel.Location = new System.Drawing.Point(5, 6);
            this.buildCommandsLabel.Name = "buildCommandsLabel";
            this.buildCommandsLabel.Size = new System.Drawing.Size(149, 20);
            this.buildCommandsLabel.TabIndex = 0;
            this.buildCommandsLabel.Values.Text = "Custom Build Commands";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(82, 282);
            this.removeButton.Name = "removeButton";
            this.removeButton.OverrideDefault.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.removeButton.Size = new System.Drawing.Size(66, 25);
            this.removeButton.TabIndex = 2;
            this.removeButton.Values.Text = "Remove";
            this.removeButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(10, 282);
            this.addButton.Name = "addButton";
            this.addButton.OverrideDefault.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.addButton.Size = new System.Drawing.Size(66, 25);
            this.addButton.TabIndex = 3;
            this.addButton.Values.Text = "Add";
            this.addButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(467, 282);
            this.applyButton.Name = "applyButton";
            this.applyButton.OverrideDefault.Border.DrawBorders = ((Krypton.Toolkit.PaletteDrawBorders)((((Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | Krypton.Toolkit.PaletteDrawBorders.Left) 
            | Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.applyButton.Size = new System.Drawing.Size(66, 25);
            this.applyButton.TabIndex = 4;
            this.applyButton.Values.Text = "Apply";
            this.applyButton.Click += new System.EventHandler(this.kryptonButton3_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.buildCommandsListView);
            this.mainPanel.Controls.Add(this.applyButton);
            this.mainPanel.Controls.Add(this.buildCommandsLabel);
            this.mainPanel.Controls.Add(this.removeButton);
            this.mainPanel.Controls.Add(this.addButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(543, 320);
            this.mainPanel.TabIndex = 6;
            // 
            // buildCommandsListView
            // 
            this.buildCommandsListView.AllColumns.Add(this.BuildCommandNameColumn);
            this.buildCommandsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildCommandsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buildCommandsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.BuildCommandNameColumn});
            this.buildCommandsListView.HideSelection = false;
            this.buildCommandsListView.Location = new System.Drawing.Point(10, 29);
            this.buildCommandsListView.MultiSelect = false;
            this.buildCommandsListView.Name = "buildCommandsListView";
            this.buildCommandsListView.Size = new System.Drawing.Size(523, 244);
            this.buildCommandsListView.TabIndex = 5;
            this.buildCommandsListView.UseCompatibleStateImageBehavior = false;
            this.buildCommandsListView.View = System.Windows.Forms.View.Details;
            this.buildCommandsListView.DoubleClick += new System.EventHandler(this.buildCommandsListView_DoubleClick_1);
            // 
            // BuildCommandNameColumn
            // 
            this.BuildCommandNameColumn.AspectName = "Name";
            this.BuildCommandNameColumn.CellPadding = null;
            // 
            // BuildCommandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(543, 320);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BuildCommandsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Commands";
            this.Load += new System.EventHandler(this.BuildCommandsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPage1)).EndInit();
            this.kryptonPage1.ResumeLayout(false);
            this.kryptonPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buildCommandsListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Krypton.Toolkit.KryptonListBox kryptonListBox1;
        private Krypton.Navigator.KryptonPage kryptonPage1;
        private Krypton.Toolkit.KryptonLabel buildCommandsLabel;
        private Krypton.Toolkit.KryptonButton removeButton;
        private Krypton.Toolkit.KryptonButton addButton;
        private Krypton.Toolkit.KryptonButton applyButton;
        private Krypton.Toolkit.KryptonPanel mainPanel;
        private BrightIdeasSoftware.ObjectListView buildCommandsListView;
        private BrightIdeasSoftware.OLVColumn BuildCommandNameColumn;
    }
}