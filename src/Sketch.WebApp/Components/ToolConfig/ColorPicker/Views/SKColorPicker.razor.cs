using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public partial class SKColorPicker : SKColorPickerComponent
    {
        [Parameter]
        public int Tints { get; set; }

        [Parameter]
        public int Shades { get; set; }

        [Parameter]
        public IList<Color> Colorants { get; set; }

        private IEnumerable<Color> GetPalette(Color color)
        {
            foreach (var mixture in GetMixturesOfTints(color).Reverse())
            {
                yield return mixture;
            }

            yield return color;

            foreach (var mixture in GetMixturesOfShades(color))
            {
                yield return mixture;
            }
        }

        private IEnumerable<Color> GetMixturesOfTints(Color color)
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

        private IEnumerable<Color> GetMixturesOfShades(Color color)
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

        private Task OnColorSelectedAsync(SKColorComponent component)
        {
            return SetValueAsync(component.Color);
        }
    }
}
