namespace pie.Forms.Other
{
    partial class FindReplaceForm
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
            mainPanel = new Krypton.Toolkit.KryptonPanel();
            matchWholeWordCheckBox = new Krypton.Toolkit.KryptonCheckBox();
            matchCaseCheckBox = new Krypton.Toolkit.KryptonCheckBox();
            regularExpressionCheckBox = new Krypton.Toolkit.KryptonCheckBox();
            findTextBox = new Krypton.Toolkit.KryptonTextBox();
            replaceTextBox = new Krypton.Toolkit.KryptonTextBox();
            kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            kryptonButton2 = new Krypton.Toolkit.KryptonButton();
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)mainPanel).BeginInit();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(matchWholeWordCheckBox);
            mainPanel.Controls.Add(matchCaseCheckBox);
            mainPanel.Controls.Add(regularExpressionCheckBox);
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new System.Drawing.Size(484, 96);
            mainPanel.TabIndex = 8;
            // 
            // matchWholeWordCheckBox
            // 
            matchWholeWordCheckBox.Location = new System.Drawing.Point(213, 66);
            matchWholeWordCheckBox.Name = "matchWholeWordCheckBox";
            matchWholeWordCheckBox.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            matchWholeWordCheckBox.Size = new System.Drawing.Size(125, 22);
            matchWholeWordCheckBox.TabIndex = 7;
            matchWholeWordCheckBox.Values.Text = "Match whole word";
            // 
            // matchCaseCheckBox
            // 
            matchCaseCheckBox.Location = new System.Drawing.Point(128, 66);
            matchCaseCheckBox.Name = "matchCaseCheckBox";
            matchCaseCheckBox.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            matchCaseCheckBox.Size = new System.Drawing.Size(85, 22);
            matchCaseCheckBox.TabIndex = 6;
            matchCaseCheckBox.Values.Text = "Match case";
            // 
            // regularExpressionCheckBox
            // 
            regularExpressionCheckBox.Location = new System.Drawing.Point(5, 66);
            regularExpressionCheckBox.Name = "regularExpressionCheckBox";
            regularExpressionCheckBox.PaletteMode = Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            regularExpressionCheckBox.Size = new System.Drawing.Size(126, 22);
            regularExpressionCheckBox.TabIndex = 5;
            regularExpressionCheckBox.Values.Text = "Regular Expression";
            regularExpressionCheckBox.CheckedChanged += regularExpressionCheckBox_CheckedChanged;
            // 
            // findTextBox
            // 
            findTextBox.Location = new System.Drawing.Point(3, 11);
            findTextBox.Name = "findTextBox";
            findTextBox.Size = new System.Drawing.Size(358, 23);
            findTextBox.TabIndex = 1;
            findTextBox.Text = "Find...";
            findTextBox.KeyDown += findTextBox_KeyDown;
            findTextBox.KeyPress += findTextBox_KeyPress;
            // 
            // replaceTextBox
            // 
            replaceTextBox.Location = new System.Drawing.Point(3, 38);
            replaceTextBox.Name = "replaceTextBox";
            replaceTextBox.Size = new System.Drawing.Size(358, 23);
            replaceTextBox.TabIndex = 3;
            replaceTextBox.Text = "Replace...";
            replaceTextBox.KeyDown += replaceTextBox_KeyDown;
            replaceTextBox.KeyPress += findTextBox_KeyPress;
            // 
            // kryptonButton3
            // 
            kryptonButton3.Location = new System.Drawing.Point(366, 65);
            kryptonButton3.Name = "kryptonButton3";
            kryptonButton3.Size = new System.Drawing.Size(114, 25);
            kryptonButton3.TabIndex = 15;
            kryptonButton3.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            kryptonButton3.Values.Text = "Replace all";
            kryptonButton3.Click += kryptonButton3_Click;
            kryptonButton3.KeyDown += FindReplaceForm_KeyDown;
            // 
            // kryptonButton2
            // 
            kryptonButton2.Location = new System.Drawing.Point(366, 38);
            kryptonButton2.Name = "kryptonButton2";
            kryptonButton2.Size = new System.Drawing.Size(114, 25);
            kryptonButton2.TabIndex = 4;
            kryptonButton2.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            kryptonButton2.Values.Text = "Find and Replace";
            kryptonButton2.Click += kryptonButton2_Click;
            kryptonButton2.KeyDown += FindReplaceForm_KeyDown;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Location = new System.Drawing.Point(366, 11);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.Size = new System.Drawing.Size(114, 25);
            kryptonButton1.StateTracking.Border.DrawBorders = Krypton.Toolkit.PaletteDrawBorders.Top | Krypton.Toolkit.PaletteDrawBorders.Bottom | Krypton.Toolkit.PaletteDrawBorders.Left | Krypton.Toolkit.PaletteDrawBorders.Right;
            kryptonButton1.TabIndex = 2;
            kryptonButton1.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            kryptonButton1.Values.Text = "Find";
            kryptonButton1.Click += kryptonButton1_Click;
            kryptonButton1.KeyDown += FindReplaceForm_KeyDown;
            // 
            // FindReplaceForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(484, 96);
            Controls.Add(findTextBox);
            Controls.Add(replaceTextBox);
            Controls.Add(kryptonButton3);
            Controls.Add(kryptonButton2);
            Controls.Add(kryptonButton1);
            Controls.Add(mainPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FindReplaceForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Find and Replace";
            FormClosing += FindReplaceForm_FormClosing;
            Load += FindReplaceForm_Load;
            KeyDown += FindReplaceForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)mainPanel).EndInit();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonPanel mainPanel;
        private Krypton.Toolkit.KryptonTextBox findTextBox;
        private Krypton.Toolkit.KryptonTextBox replaceTextBox;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonButton kryptonButton2;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Krypton.Toolkit.KryptonCheckBox regularExpressionCheckBox;
        private Krypton.Toolkit.KryptonCheckBox matchCaseCheckBox;
        private Krypton.Toolkit.KryptonCheckBox matchWholeWordCheckBox;
    }
}