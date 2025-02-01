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

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using ComponentFactory.Krypton.Toolkit;

/**
 * LibGit2Sharp is used for integrating several advanced Git functionalities into pie.
 * 
 * Copyright (c) LibGit2Sharp contributors
 */
using LibGit2Sharp;

namespace pie.Classes
{
    public class GitCommitLogFormInput
    {
        public ThemeInfo ActiveTheme { get; set; }
        public Repository Repository { get; set; }
        public KryptonPalette Palette { get; set; }
        public EditorProperties EditorProperties { get; set; }
    }
}
