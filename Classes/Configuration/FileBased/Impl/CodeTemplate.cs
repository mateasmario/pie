﻿/** Copyright (C) 2023  Mario-Mihai Mateas
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
