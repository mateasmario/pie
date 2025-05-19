/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;

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
        public KryptonCustomPaletteBase Palette { get; set; }
        public EditorProperties EditorProperties { get; set; }
    }
}
