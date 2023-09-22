﻿using ComponentFactory.Krypton.Toolkit;
using pie.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pie
{
    public partial class NotificationYesNoCancel : KryptonForm
    {
        public NotificationYesNoCancel()
        {
            InitializeComponent();

            this.Palette = Globals.kryptonPalette;
            kryptonPanel1.Palette = Globals.kryptonPalette;
            kryptonRichTextBox1.Palette = Globals.kryptonPalette;
            kryptonButton1.Palette = Globals.kryptonPalette;
            kryptonButton2.Palette = Globals.kryptonPalette;
            kryptonButton3.Palette = Globals.kryptonPalette;
        }

        private void NotificationYesNoCancel_Load(object sender, EventArgs e)
        {
            Globals.notificationButtonPressed = NotificationButton.CANCEL;
            kryptonRichTextBox1.Text = Globals.notificationText;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Globals.notificationButtonPressed = NotificationButton.YES;
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Globals.notificationButtonPressed = NotificationButton.NO;
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Globals.notificationButtonPressed = NotificationButton.CANCEL;
            this.Close();
        }
    }
}
