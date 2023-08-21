namespace pie
{
    partial class AddBuildCommandForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBuildCommandForm));
            this.kryptonTextBox1 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonTextBox2 = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SuspendLayout();
            // 
            // kryptonTextBox1
            // 
            this.kryptonTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonTextBox1.Location = new System.Drawing.Point(12, 33);
            this.kryptonTextBox1.Name = "kryptonTextBox1";
            this.kryptonTextBox1.Size = new System.Drawing.Size(387, 23);
            this.kryptonTextBox1.TabIndex = 0;
            // 
            // kryptonTextBox2
            // 
            this.kryptonTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonTextBox2.Location = new System.Drawing.Point(12, 83);
            this.kryptonTextBox2.Name = "kryptonTextBox2";
            this.kryptonTextBox2.Size = new System.Drawing.Size(387, 23);
            this.kryptonTextBox2.TabIndex = 1;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(7, 12);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(86, 20);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "Display Name";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(7, 62);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(67, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Command";
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(309, 138);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(90, 25);
            this.kryptonButton1.TabIndex = 4;
            this.kryptonButton1.Values.Text = "Add";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.kryptonLabel3.Location = new System.Drawing.Point(7, 108);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(349, 20);
            this.kryptonLabel3.TabIndex = 5;
            this.kryptonLabel3.Values.Text = "$FILE can be used as a placeholder for the currently opened file.";
            // 
            // AddBuildCommandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(411, 171);
            this.Controls.Add(this.kryptonLabel3);
            this.Controls.Add(this.kryptonButton1);
            this.Controls.Add(this.kryptonLabel2);
            this.Controls.Add(this.kryptonLabel1);
            this.Controls.Add(this.kryptonTextBox2);
            this.Controls.Add(this.kryptonTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBuildCommandForm";
            this.Text = "Add Build Command";
            this.Load += new System.EventHandler(this.AddBuildCommandForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox kryptonTextBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
    }
}