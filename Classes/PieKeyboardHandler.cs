using CefSharp;
using pie.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
