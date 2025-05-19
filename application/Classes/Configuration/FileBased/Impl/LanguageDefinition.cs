/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class LanguageDefinition : MultiFileConfigurationEntity
    {
        public string Lexer { get; set; }
        public string Keywords { get; set; }
        public string WordChars { get; set; }
        public LanguageProperties Properties { get; set; }
    }
}
