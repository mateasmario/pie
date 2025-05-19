/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie.Classes.Configuration;
using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class CodeTemplate : ConfigurationEntity, ICloneable<CodeTemplate>
    {
        public int Index { get; set; }
        public string Content { get; set; }

        public CodeTemplate() { }
        public CodeTemplate(int index, string content)
        {
            Index = index;
            Content = content;
        }

        public CodeTemplate Clone()
        {
            CodeTemplate codeTemplateClone = new CodeTemplate();

            codeTemplateClone.Index = this.Index;
            codeTemplateClone.Content = this.Content;

            return codeTemplateClone;
        }
    }
}
