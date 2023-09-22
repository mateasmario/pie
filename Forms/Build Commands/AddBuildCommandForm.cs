﻿using System;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class AddBuildCommandForm : KryptonForm
    {
        public AddBuildCommandForm()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonLabel3.Palette = Globals.kryptonPalette;
            kryptonTextBox1.Palette = Globals.kryptonPalette;
            kryptonTextBox2.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text == "" || kryptonTextBox2.Text == "")
            {
                MainForm.ShowNotification("Display Name and Command cannot be empty.");
            }
            else
            {
                Globals.addBuildCommandName = kryptonTextBox1.Text;
                Globals.addBuildCommandCmd = kryptonTextBox2.Text;

                this.Close();
            }
        }

        private void AddBuildCommandForm_Load(object sender, EventArgs e)
        {
            Globals.addBuildCommandName = null;
            Globals.addBuildCommandCmd = null;
        }
    }
}