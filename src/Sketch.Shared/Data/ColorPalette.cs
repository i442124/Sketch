using System.Collections.Generic;

namespace Sketch.Shared.Data
{
    public static class ColorPalette
    {
        public static IEnumerable<Color> GetMixtureOfTints(Color color, int count)
        {
            var hue = color.Hue;
            var sat = color.Saturation;
            var lum = color.Luminosity;

            var steps = count + 1;
            var stepSize = (1 - color.Luminosity) / steps;

            for (int i = 1; i < steps; i++)
            {
                lum += stepSize;
                yield return Color.FromHSL(hue, sat, lum);
            }
        }

        public static IEnumerable<Color> GetMixturesOfShades(Color color, int count)
        {
            var hue = color.Hue;
            var sat = color.Saturation;
            var lum = color.Luminosity;

            var steps = count + 1;
            var stepSize = color.Luminosity / steps;

            for (int i = 1; i < steps; i++)
            {
                lum -= stepSize;
                yield return Color.FromHSL(hue, sat, lum);
            }
        }
    }
}
