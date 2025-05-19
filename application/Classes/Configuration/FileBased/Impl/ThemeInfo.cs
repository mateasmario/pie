/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

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
