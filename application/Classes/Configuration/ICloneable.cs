﻿/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

namespace pie.Classes.Configuration
{
    public interface ICloneable<T>
    {
        T Clone();
    }
}
