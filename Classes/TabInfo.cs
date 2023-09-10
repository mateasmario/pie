using pie.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pie.Classes
{
    public class TabInfo
    {
        private string openedFilePath;
        private bool openedFileChanges;
        private TabType tabType;

        public TabInfo(string openedFilePath, bool openedFileChanges, TabType tabType)
        {
            this.openedFilePath = openedFilePath;
            this.openedFileChanges = openedFileChanges;
            this.tabType = tabType;
        }

        public string getOpenedFilePath()
        {
            return openedFilePath;
        }

        public void setOpenedFilePath(string openedFilePath)
        {
            this.openedFilePath = openedFilePath;
        }

        public bool getOpenedFileChanges()
        {
            return openedFileChanges;
        }

        public void setOpenedFileChanges(bool openedFileChanges)
        {
            this.openedFileChanges = openedFileChanges;
        }

        public TabType getTabType()
        {
            return tabType;
        }

        public void setTabType(TabType tabType)
        {
            this.tabType = tabType;
        }
    }
}
