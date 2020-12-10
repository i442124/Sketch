using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Views
{
    public partial class SKColor
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<SKColor> ColorSelected { get; set; }

        private double Hue => Color.Hue;

        private double Saturation => Color.Saturation;

        private double Luminosity => Color.Luminosity;

        private Color Tint => Color.FromHSL(Hue, Saturation, Luminosity + 0.1);

        private Color Shade => Color.FromHSL(Hue, Saturation, Luminosity - 0.1);

        private Task OnClickAsync(MouseEventArgs e)
        {
            return ColorSelected.InvokeAsync(this);
        }
    }
}
