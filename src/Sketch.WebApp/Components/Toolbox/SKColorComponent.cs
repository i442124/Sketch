using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components.Toolbox
{
    public abstract class SKColorComponent : ComponentBase
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<Color> ColorSelected { get; set; }

        public SKColorComponent()
        {
        }

        protected Color Tint => new Color(Hue, Saturation, Luminosity + 0.1);

        protected Color Shade => new Color(Hue, Saturation, Luminosity - 0.1);

        private double Hue => Color.Hue;

        private double Saturation => Color.Saturation;

        private double Luminosity => Color.Luminosity;
    }
}
