using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKColorPickerComponent : ComponentBase
    {
        [Parameter]
        public int Shades { get; set; }

        [Parameter]
        public int Tints { get; set; }

        [Parameter]
        public List<Color> Colorants { get; set; }

        [Parameter]
        public EventCallback<Color> ColorHasChanged { get; set; }

        public SKColorPickerComponent()
        {
        }

        protected async Task SetPippeteColorAsync(Color color)
        {
            await Pipette.SetColorAsync(color);
            await ColorHasChanged.InvokeAsync(color);
        }

        protected IEnumerable<Color> GetMixtureOfTints(Color color)
        {
            var hue = color.Hue;
            var sat = color.Saturation;
            var lum = color.Luminosity;

            var steps = Tints + 1;
            var stepSize = (1 - color.Luminosity) / steps;

            for (int i = 1; i < steps; i++)
            {
                lum += stepSize;
                yield return Color.FromHSL(hue, sat, lum);
            }
        }

        protected IEnumerable<Color> GetMixtureOfShades(Color color)
        {
            var hue = color.Hue;
            var sat = color.Saturation;
            var lum = color.Luminosity;

            var steps = Shades + 1;
            var stepSize = color.Luminosity / steps;

            for (int i = 1; i < steps; i++)
            {
                lum -= stepSize;
                yield return Color.FromHSL(hue, sat, lum);
            }
        }

        protected IEnumerable<Color> GetColorPallette(Color baseColor)
        {
            foreach (var mixture in GetMixtureOfTints(baseColor).Reverse())
            {
                yield return mixture;
            }

            yield return baseColor;

            foreach (var mixture in GetMixtureOfShades(baseColor))
            {
                yield return mixture;
            }
        }

        [Inject]
        private IPipetteModel Pipette { get; set; }
    }
}
