/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Enums;

/**
 * AutocompleteMenuNS is a namespace that comes from AutoCompleteMenu-ScintillaNet. It is used for various Autocomplete suggestions while writing code.
 * 
 * AutoCompleteMenu-ScintillaNet is licensed under the GNU Lesser General Public License (LGPLv3).
 * For more information related to the license, see https://www.gnu.org/licenses/lgpl-3.0.html.
 * 
 */
using AutocompleteMenuNS;

namespace pie.Classes
{
    public class TabInfo
    {
        private string openedFilePath;
        private bool openedFileChanges;
        private TabType tabType;
        private AutocompleteMenu autocompleteMenu;

        public TabInfo(string openedFilePath, bool openedFileChanges, TabType tabType, AutocompleteMenu autocompleteMenu)
        {
            this.openedFilePath = openedFilePath;
            this.openedFileChanges = openedFileChanges;
            this.tabType = tabType;
            this.autocompleteMenu = autocompleteMenu;
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

        public AutocompleteMenu getAutocompleteMenu()
        {
            return autocompleteMenu;
        }

        public void setAutocompleteMenu(AutocompleteMenu autocompleteMenu)
        {
            this.autocompleteMenu = autocompleteMenu;
        }
    }
}
