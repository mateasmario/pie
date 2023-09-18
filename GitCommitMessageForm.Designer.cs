namespace pie
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
            this.kryptonRichTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonRichTextBox1
            // 
            this.kryptonRichTextBox1.Location = new System.Drawing.Point(10, 10);
            this.kryptonRichTextBox1.Name = "kryptonRichTextBox1";
            this.kryptonRichTextBox1.Size = new System.Drawing.Size(450, 85);
            this.kryptonRichTextBox1.TabIndex = 0;
            this.kryptonRichTextBox1.Text = "Commit message";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(371, 105);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.OverrideDefault.Back.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideDefault.Back.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideDefault.Border.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideDefault.Border.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.OverrideFocus.Back.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideFocus.Back.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideFocus.Border.Color1 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideFocus.Border.Color2 = System.Drawing.Color.DarkGray;
            this.kryptonButton1.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 1;
            this.kryptonButton1.Values.Text = "Commit";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonRichTextBox1);
            this.kryptonPanel1.Controls.Add(this.kryptonButton1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(474, 138);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // GitCommitMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(474, 138);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GitCommitMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Git Commit";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox kryptonRichTextBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
    }
}