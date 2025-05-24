/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace pie.Services
{
    public class Win32APIService
    {
        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;

        public void HideScrollBars(Control control)
        {
            ShowScrollBar(control.Handle, SB_HORZ, false);
            ShowScrollBar(control.Handle, SB_VERT, false);
        }
    }
}
