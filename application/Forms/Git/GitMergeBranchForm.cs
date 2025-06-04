/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;
using pie.Classes;
using pie.Services;
using System;
using System.Windows.Forms;

namespace pie.Forms.Git
{
    public partial class GitMergeBranchForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();
        public GitMergeBranchFormInput Input { get; set; }
        public GitMergeBranchFormOutput Output { get; set; }

        public GitMergeBranchForm()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Output = new GitMergeBranchFormOutput();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        private void GitMergeBranchForm_Load(object sender, EventArgs e)
        {
            foreach (string branch in Input.Branches)
            {
                if (!branch.Equals(Input.CurrentBranch))
                {
                    branchComboBox.Items.Add(branch);
                }
            }

            branchComboBox.SelectedIndex = 0;

            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            Output.SelectedBranch = null;
        }

        private void mergeButton_Click(object sender, EventArgs e)
        {
            Output.SelectedBranch = branchComboBox.SelectedItem.ToString();
            this.Close();
        }
    }
}
