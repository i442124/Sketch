using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKColor : SKColorComponent
    {
        private Color Tint => Color.FromHSL(Hue, Saturation, Luminosity + 0.1);

        private Color Shade => Color.FromHSL(Hue, Saturation, Luminosity - 0.1);

        private double Hue => Color.Hue;

        private double Saturation => Color.Saturation;

        private double Luminosity => Color.Luminosity;

        private async Task OnClickAsync(MouseEventArgs e)
        {
            await ColorSelected.InvokeAsync(this);
        }
    }
}
