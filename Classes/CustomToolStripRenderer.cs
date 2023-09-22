using pie.Services;

namespace System.Windows.Forms
{
    internal class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            base.OnRenderArrow(e);
            e.ArrowColor = ThemeService.GetForeColor();
        }
    }
}
