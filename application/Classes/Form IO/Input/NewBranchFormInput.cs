﻿/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using System.Collections.Generic;

/** 
 * Krypton Suite's Standard Toolkit was often used in order to design the .NET controls found inside this application.
 * 
 * Copyright (c) 2017 - 2022, Krypton Suite
*/
using Krypton.Toolkit;

namespace pie.Classes
{
    public class NewBranchFormInput
    {
        public ThemeInfo ActiveTheme { get; set; }
        public KryptonCustomPaletteBase Palette { get; set; }
        public EditorProperties EditorProperties { get; set; }
    }
}
