/** Copyright (C) 2023  Mario-Mihai Mateas
 * 
 * This file is part of pie.
 * 
 * pie is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * 
 * along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/

using pie.Enums;
using System;

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
