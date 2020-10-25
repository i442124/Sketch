using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKColorComponent : ComponentBase
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<Color> ColorSelected { get; set; }

        public SKColorComponent()
        {
        }

        public Color Tint => new Color(Hue, Saturation, Luminosity + 0.1);

        public Color Shade => new Color(Hue, Saturation, Luminosity - 0.1);

        private double Hue => Color.Hue;

        private double Saturation => Color.Saturation;

        private double Luminosity => Color.Luminosity;
    }
}
