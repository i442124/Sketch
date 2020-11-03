using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public class SKColorPickerComponent : ComponentBase
    {
        [Parameter]
        public int Tints { get; set; }

        [Parameter]
        public int Shades { get; set; }

        [Parameter]
        public IList<Color> Colorants { get; set; }

        public SKColorPickerComponent()
        {
            Tints = 2;
            Shades = 2;
            Colorants = new Collection<Color>
            {
                Colors.Grey,
                Colors.Red,
                Colors.Orange,
                Colors.Yellow,
                Colors.Green,
                Colors.Turquoise,
                Colors.Aqua,
                Colors.Blue,
                Colors.Purple,
                Colors.Pink,
                Colors.Beige,
                Colors.Brown
            };
        }

        protected IEnumerable<Color> GetPalette(Color color)
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

        protected IEnumerable<Color> GetMixturesOfTints(Color color)
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

        protected IEnumerable<Color> GetMixturesOfShades(Color color)
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

        protected async Task SetPipetteColorAsync(Color color)
        {
            await Pipette.SetColorAsync(color);
        }

        [Inject]
        private IPipetteModel Pipette { get; set; }
    }
}
