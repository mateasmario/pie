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

namespace pie.Forms.Databases
{
    public partial class DatabaseOutputForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public DatabaseOutputFormInput Input { get; set; }

        public DatabaseOutputForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
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

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DatabaseOutputForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }

            outputDataGridView.DataSource = Input.DataTable;
        }
    }
}
