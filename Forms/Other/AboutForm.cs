using System;
using System.Diagnostics;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class AboutForm : KryptonForm
    {
        public AboutForm()
        {
            InitializeComponent();
            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonLabel1.Palette = Globals.kryptonPalette;
            kryptonLabel2.Palette = Globals.kryptonPalette;
            kryptonLabel3.Palette = Globals.kryptonPalette;
        }

        private void kryptonLinkLabel1_LinkClicked(object sender, EventArgs e)
        {
            Process.Start("https://github.com/mateasmario/pie");
        }

        private void kryptonLinkLabel2_LinkClicked(object sender, EventArgs e)
        {
            Process.Start("https://mateasmario.com");
        }
    }
}
