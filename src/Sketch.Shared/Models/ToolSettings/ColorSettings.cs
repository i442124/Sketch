using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;

namespace Sketch.Shared.Models
{
    public class ColorSettings : IColorSettings
    {
        public Color Color { get; private set; }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public Task SetColorAsync(Color color)
        {
            return Task.Run(() => SetColor(color));
        }

        public void SetHue(float hue)
        {
            Color = Color.FromHSL(hue, Color.Saturation, Color.Luminosity);
        }

        public Task SetHueAsync(float hue)
        {
            return Task.Run(() => SetHue(hue));
        }

        public void SetSaturation(float saturation)
        {
            Color = Color.FromHSL(Color.Hue, saturation, Color.Luminosity);
        }

        public Task SetSaturationAsync(float saturation)
        {
            return Task.Run(() => SetSaturation(saturation));
        }

        public void SetLuminosity(float luminosity)
        {
            Color = Color.FromHSL(Color.Hue, Color.Saturation, luminosity);
        }

        public Task SetLuminosityAsync(float luminosity)
        {
            return Task.Run(() => SetLuminosity(luminosity));
        }
    }
}
