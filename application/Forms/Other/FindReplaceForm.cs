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
using ScintillaNET;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace pie.Forms.Other
{
    public partial class FindReplaceForm : KryptonForm
    {
        private ThemingService themeService = new ThemingService();

        public FindReplaceFormInput Input { get; set; }

        const int WM_CONTEXTMENU = 0x007B;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CONTEXTMENU)
            {
                m.Result = IntPtr.Zero;
                //Close();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public FindReplaceForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            this.BringToFront();
            base.OnShown(e);
        }

        private void FindReplaceForm_Load(object sender, EventArgs e)
        {
            themeService.SetPaletteToObjects(this, Input.Palette);

            regularExpressionCheckBox.StateNormal.ShortText.Color1 = Input.ActiveTheme.Fore;
            regularExpressionCheckBox.StateNormal.ShortText.Color2 = Input.ActiveTheme.Fore;

            matchCaseCheckBox.StateNormal.ShortText.Color1 = Input.ActiveTheme.Fore;
            matchCaseCheckBox.StateNormal.ShortText.Color2 = Input.ActiveTheme.Fore;

            matchWholeWordCheckBox.StateNormal.ShortText.Color1 = Input.ActiveTheme.Fore;
            matchWholeWordCheckBox.StateNormal.ShortText.Color2 = Input.ActiveTheme.Fore;

            regularExpressionCheckBox.StateDisabled.ShortText.Color1 = Input.ActiveTheme.Fore;
            regularExpressionCheckBox.StateDisabled.ShortText.Color2 = Input.ActiveTheme.Fore;

            matchCaseCheckBox.StateDisabled.ShortText.Color1 = Input.ActiveTheme.Fore;
            matchCaseCheckBox.StateDisabled.ShortText.Color2 = Input.ActiveTheme.Fore;

            matchWholeWordCheckBox.StateDisabled.ShortText.Color1 = Input.ActiveTheme.Fore;
            matchWholeWordCheckBox.StateDisabled.ShortText.Color2 = Input.ActiveTheme.Fore;

            if (Input.EditorProperties.Glass)
            {
                this.Opacity = 0.875;
            }
            else
            {
                this.Opacity = 1;
            }

            this.BringToFront();
            findTextBox.SelectAll();
        }

        private void regularExpressionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            KryptonCheckBox kryptonCheckBox = (KryptonCheckBox)sender;

            if (kryptonCheckBox.Checked)
            {
                matchWholeWordCheckBox.Checked = false;
                matchWholeWordCheckBox.Enabled = false;
            }
            else
            {
                matchWholeWordCheckBox.Enabled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Input.MainForm.StartFind(findTextBox.Text, regularExpressionCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            bool status = Input.MainForm.Replace(findTextBox.Text, replaceTextBox.Text, regularExpressionCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);

            if (!status)
            {
                Input.MainForm.ShowNotification("No occurences found.");
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Input.MainForm.ReplaceAll(findTextBox.Text, replaceTextBox.Text, regularExpressionCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
        }

        private void findTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Input.MainForm.StartFind(findTextBox.Text, regularExpressionCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                || e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.Close();
            }
        }

        private void replaceTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool status = Input.MainForm.Replace(findTextBox.Text, replaceTextBox.Text, regularExpressionCheckBox.Checked, matchCaseCheckBox.Checked, matchWholeWordCheckBox.Checked);

                if (!status)
                {
                    Input.MainForm.ShowNotification("No occurences found.");
                }
            }
            else if ((e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FindReplaceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
                || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FindReplaceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Input.MainForm.ClearHighlights();
        }

        private void findTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
