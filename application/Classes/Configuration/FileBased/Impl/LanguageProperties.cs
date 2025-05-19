/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class LanguageProperties : ConfigurationEntity
    {
        public string LexerJsonAllowComments { get; set; }
        public string LexerJsonEscapeSequence{ get; set; }
        public string TabTimmyWhingeLevel { get; set; }
        public string FoldHtml { get; set; }
    }
}
