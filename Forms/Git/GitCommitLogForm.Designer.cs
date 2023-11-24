namespace pie
{
    partial class GitCommitLogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GitCommitLogForm));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.gitCommitLogListView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gitCommitLogListView)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.gitCommitLogListView);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(794, 365);
            this.kryptonPanel1.TabIndex = 3;
            // 
            // gitCommitLogListView
            // 
            this.gitCommitLogListView.AllColumns.Add(this.olvColumn1);
            this.gitCommitLogListView.AllColumns.Add(this.olvColumn3);
            this.gitCommitLogListView.AllColumns.Add(this.olvColumn2);
            this.gitCommitLogListView.AllColumns.Add(this.olvColumn4);
            this.gitCommitLogListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gitCommitLogListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn2,
            this.olvColumn4});
            this.gitCommitLogListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gitCommitLogListView.HeaderUsesThemes = false;
            this.gitCommitLogListView.HideSelection = false;
            this.gitCommitLogListView.Location = new System.Drawing.Point(0, 0);
            this.gitCommitLogListView.Name = "gitCommitLogListView";
            this.gitCommitLogListView.Size = new System.Drawing.Size(794, 365);
            this.gitCommitLogListView.TabIndex = 10;
            this.gitCommitLogListView.UseCompatibleStateImageBehavior = false;
            this.gitCommitLogListView.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Id";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Text = "Id";
            this.olvColumn1.Width = 74;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Date";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Text = "Date";
            this.olvColumn3.Width = 155;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Author";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Text = "Author";
            this.olvColumn2.Width = 284;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Message";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.Text = "Message";
            // 
            // GitCommitLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 365);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GitCommitLogForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commit Log";
            this.Load += new System.EventHandler(this.GitCommitLogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gitCommitLogListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private BrightIdeasSoftware.ObjectListView gitCommitLogListView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}