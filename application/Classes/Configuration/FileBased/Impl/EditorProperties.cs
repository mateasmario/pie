/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class EditorProperties : ConfigurationEntity
    {
        public bool Wordwrap { get; set; }
        public bool Autosave { get; set; }
        public bool Glass { get; set; }

        public EditorProperties()
        {

        }

        public EditorProperties(bool wordwrap, bool autosave, bool glass)
        {
            Wordwrap = wordwrap;
            Autosave = autosave;
            Glass = glass;
        }
    }
}
