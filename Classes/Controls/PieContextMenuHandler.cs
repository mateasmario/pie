/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System;
using System.Windows.Forms;

/**
 * CefSharp is used in order to integrate Chromium-Based Web Browsers inside pie.
 * 
 * Copyright © The CefSharp Authors. All rights reserved.
 */
using CefSharp;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

namespace pie.Classes
{
    internal class PieContextMenuHandler : IContextMenuHandler
    {
        private MainForm mainForm;
        private KryptonContextMenu kryptonContextMenu;

        public PieContextMenuHandler(MainForm mainForm, KryptonContextMenu kryptonContextMenu)
        {
            this.mainForm = mainForm;
            this.kryptonContextMenu = kryptonContextMenu;
        }

        void IContextMenuHandler.OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            mainForm.Invoke(new Action(() =>
            {
                kryptonContextMenu.Close();
            }));

            model.Clear();
        }

        bool IContextMenuHandler.OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            MessageBox.Show("OnContextMenuCommand");
            return true;
        }

        void IContextMenuHandler.OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {
            mainForm.Invoke(new Action(() =>
            {
                kryptonContextMenu.Show(this);
            }));
        }

        bool IContextMenuHandler.RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {

            MessageBox.Show("RunContextMenu");

            return true;
        }
    }
}
