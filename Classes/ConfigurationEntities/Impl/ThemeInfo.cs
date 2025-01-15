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

using System.Drawing;
using pie.Classes.ConfigurationEntities;

namespace pie.Classes
{
    public class ThemeInfo : MultiFileConfigurationEntity
    {
        public Color Primary { get; set; }
        public Color Secondary { get; set; }
        public Color Button { get; set; }
        public Color ButtonFrame { get; set; }
        public Color ButtonHover { get; set; }
        public Color Fore { get; set; }
        public Color FormBorder { get; set; }
        public Color Selection { get; set; }
        public Color CaretLineBack { get; set; }
        public Color NumberMargin { get; set; }
        public Color Folding { get; set; }
        public Color Comment { get; set; }
        public Color CommentLine { get; set; }
        public Color Number { get; set; }
        public Color Word { get; set; }
        public Color String { get; set; }
        public Color Operator { get; set; }
        public Color Preprocessor { get; set; }
        public Color Triple { get; set; }
        public Color CommentBlock{ get; set; }
        public Color Decorator { get; set; }
        public Color Attribute { get; set; }
        public Color Entity { get; set; }
        public Color User1 { get; set; }
        public Color User2 { get; set; }
        public string IconType { get; set; }
    }
}
