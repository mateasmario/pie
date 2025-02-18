/* SPDX-FileCopyrightText: 2023-2025 Mario-Mihai Mateas <mateasmario@aol.com> */
/* SPDX-License-Identifier: GPL-3.0-or-later */

using pie;
using pie.Classes;

namespace System.Windows.Forms
{
    internal class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        public ThemeInfo InputThemeInfo;

        public CustomToolStripRenderer(ProfessionalColorTable professionalColorTable) : base(professionalColorTable)
        {
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var tsMenuItem = e.Item as ToolStripMenuItem;
            if (tsMenuItem != null)
                e.ArrowColor = InputThemeInfo.Fore;
            base.OnRenderArrow(e);
        }
    }
}
