namespace pie
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.versionLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.titleLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.descriptionLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.mainPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.websiteButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.repoButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // versionLabel
            // 
            this.versionLabel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.versionLabel.Location = new System.Drawing.Point(5, 44);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(58, 22);
            this.versionLabel.TabIndex = 2;
            this.versionLabel.Values.Text = "Version x";
            // 
            // titleLabel
            // 
            this.titleLabel.Location = new System.Drawing.Point(2, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(105, 33);
            this.titleLabel.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.TabIndex = 5;
            this.titleLabel.Values.Text = "pie Beta";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.descriptionLabel.Location = new System.Drawing.Point(5, 70);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(349, 118);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Values.Text = resources.GetString("kryptonLabel2.Values.Text");
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.websiteButton);
            this.mainPanel.Controls.Add(this.repoButton);
            this.mainPanel.Controls.Add(this.iconPictureBox);
            this.mainPanel.Controls.Add(this.titleLabel);
            this.mainPanel.Controls.Add(this.versionLabel);
            this.mainPanel.Controls.Add(this.descriptionLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(346, 253);
            this.mainPanel.TabIndex = 6;
            // 
            // websiteButton
            // 
            this.websiteButton.Location = new System.Drawing.Point(176, 215);
            this.websiteButton.Name = "websiteButton";
            this.websiteButton.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.websiteButton.Size = new System.Drawing.Size(165, 25);
            this.websiteButton.TabIndex = 8;
            this.websiteButton.Values.Text = "Author\'s Personal Website";
            this.websiteButton.Click += new System.EventHandler(this.kryptonButton2_Click);
            // 
            // repoButton
            // 
            this.repoButton.Location = new System.Drawing.Point(5, 215);
            this.repoButton.Name = "repoButton";
            this.repoButton.OverrideFocus.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.repoButton.Size = new System.Drawing.Size(165, 25);
            this.repoButton.TabIndex = 7;
            this.repoButton.Values.Text = "GitHub Repository";
            this.repoButton.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.iconPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("iconPictureBox.BackgroundImage")));
            this.iconPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.iconPictureBox.Location = new System.Drawing.Point(288, 6);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(51, 48);
            this.iconPictureBox.TabIndex = 6;
            this.iconPictureBox.TabStop = false;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(346, 253);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonLabel versionLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel titleLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel descriptionLabel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel mainPanel;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private ComponentFactory.Krypton.Toolkit.KryptonButton repoButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton websiteButton;
    }
}