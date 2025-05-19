/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class LanguageMapping : ConfigurationEntity
    {
        public string Extension { get; set; }
        public string Language { get; set; }
    }
}
