/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using pie.Enums;

/**
 * CefSharp is used in order to integrate Chromium-Based Web Browsers inside pie.
 * 
 * Copyright © The CefSharp Authors. All rights reserved.
 */
using CefSharp;

namespace pie.Classes
{
    internal class PieKeyboardHandler : IKeyboardHandler
    {
        private MainForm mainForm;
        private bool blockEvent;

        public PieKeyboardHandler(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        bool IKeyboardHandler.OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            if (type == KeyType.RawKeyDown && modifiers == CefEventFlags.ControlDown)
            {
                    if (windowsKeyCode == 84)
                    {
                        mainForm.Invoke(new Action(() =>
                        {
                            mainForm.NewTab(TabType.CODE, null);
                        }));
                    }
                    else if (windowsKeyCode == 87)
                    {
                        mainForm.Invoke(new Action(() =>
                        {
                            mainForm.CloseTab();
                        }));
                    }
            }
            else if (type == KeyType.KeyUp && (windowsKeyCode == 17 || windowsKeyCode == 87 || windowsKeyCode == 84))
            {
            }

            return true;
        }

        bool IKeyboardHandler.OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            return false;
        }
    }
}
