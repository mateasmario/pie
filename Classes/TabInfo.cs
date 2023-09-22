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
