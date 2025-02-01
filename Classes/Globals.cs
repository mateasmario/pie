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

using System.Collections.Generic;
using System.Drawing;
using pie.Classes;
using pie.Enums;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

namespace pie
{
    public class Globals
    {
        public static int lastSelectedTabIndex;
        public static int maxLineNumberCharLength;
        public static int lastSelectedIndex;
        public static bool canUpdateUI;
        public static bool firstBrowserTab;
        public static bool deletesLastTab;
        public static bool deletesTab;

        // Find & Replace Panel
        public static bool mouseDown;
        public static Point lastLocation;

        // Git
        public static Branch selectedBranch;
        public static bool gitTabOpened;
        public static int selectedBranchIndex;
        public static bool doNotTriggerBranchChangeEvent;
        public static bool doNotShowBranchChangeNotification;
        public static bool showGitTabPressed;

        // Scintilla preferences
        public static List<LanguageDefinition> languageDefinitions;
        public static List<LanguageMapping> languageMappings;

        // Format
        public static List<Formatter> formatters;
    }
}
