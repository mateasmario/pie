namespace pie.Forms.Format
{
    partial class FormatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormatForm));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.formatOptionsListView = new BrightIdeasSoftware.ObjectListView();
            this.FormatOptionNameColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FormatOptionCategoryColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FormatOptionDescriptionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formatOptionsListView)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonTextBox1);
            this.kryptonPanel1.Controls.Add(this.formatOptionsListView);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(714, 372);
            this.kryptonPanel1.TabIndex = 7;
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonTextBox1.Location = new System.Drawing.Point(10, 10);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(694, 23);
            this.kryptonTextBox1.TabIndex = 6;
            this.kryptonTextBox1.Text = "Search...";
            this.kryptonTextBox1.TextChanged += new System.EventHandler(this.kryptonTextBox1_TextChanged);
            // 
            // formatOptionsListView
            // 
            this.formatOptionsListView.AllColumns.Add(this.FormatOptionNameColumn);
            this.formatOptionsListView.AllColumns.Add(this.FormatOptionCategoryColumn);
            this.formatOptionsListView.AllColumns.Add(this.FormatOptionDescriptionColumn);
            this.formatOptionsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formatOptionsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.formatOptionsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FormatOptionNameColumn,
            this.FormatOptionCategoryColumn,
            this.FormatOptionDescriptionColumn});
            this.formatOptionsListView.HeaderUsesThemes = false;
            this.formatOptionsListView.HideSelection = false;
            this.formatOptionsListView.Location = new System.Drawing.Point(10, 42);
            this.formatOptionsListView.MultiSelect = false;
            this.formatOptionsListView.Name = "formatOptionsListView";
            this.formatOptionsListView.SelectColumnsOnRightClick = false;
            this.formatOptionsListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            this.formatOptionsListView.Size = new System.Drawing.Size(694, 318);
            this.formatOptionsListView.TabIndex = 5;
            this.formatOptionsListView.UseCompatibleStateImageBehavior = false;
            this.formatOptionsListView.View = System.Windows.Forms.View.Details;
            this.formatOptionsListView.DoubleClick += new System.EventHandler(this.formatOptionsListView_DoubleClick);
            // 
            // FormatOptionNameColumn
            // 
            this.FormatOptionNameColumn.AspectName = "Name";
            this.FormatOptionNameColumn.CellPadding = null;
            this.FormatOptionNameColumn.Text = "Name";
            this.FormatOptionNameColumn.Width = 199;
            // 
            // FormatOptionCategoryColumn
            // 
            this.FormatOptionCategoryColumn.AspectName = "Category";
            this.FormatOptionCategoryColumn.CellPadding = null;
            this.FormatOptionCategoryColumn.Text = "Category";
            // 
            // FormatOptionDescriptionColumn
            // 
            this.FormatOptionDescriptionColumn.AspectName = "Description";
            this.FormatOptionDescriptionColumn.CellPadding = null;
            this.FormatOptionDescriptionColumn.Text = "Description";
            // 
            // FormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(714, 372);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormatForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Format";
            this.Load += new System.EventHandler(this.FormatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.formatOptionsListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private BrightIdeasSoftware.ObjectListView formatOptionsListView;
        private BrightIdeasSoftware.OLVColumn FormatOptionNameColumn;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private BrightIdeasSoftware.OLVColumn FormatOptionCategoryColumn;
        private BrightIdeasSoftware.OLVColumn FormatOptionDescriptionColumn;
    }
}