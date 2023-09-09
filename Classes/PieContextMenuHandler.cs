using CefSharp;
using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
