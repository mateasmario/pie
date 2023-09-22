using System;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie
{
    public partial class NotificationOK : KryptonForm
    {
        public NotificationOK()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonRichTextBox1.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            kryptonRichTextBox1.Text = Globals.notificationText;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
