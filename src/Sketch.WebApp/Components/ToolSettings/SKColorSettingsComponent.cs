using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKColorSettingsComponent : ComponentBase
    {
        public Color Color
        {
            get { return ColorSettings.Color; }
        }

        protected void SetColor(Color color)
        {
            ColorSettings.SetColor(color);
        }

        protected Task SetColorAsync(Color color)
        {
            return ColorSettings.SetColorAsync(color);
        }

        protected void SetHue(float hue)
        {
            ColorSettings.SetHue(hue);
        }

        protected Task SetHueAsync(float hue)
        {
            return ColorSettings.SetHueAsync(hue);
        }

        protected void SetSaturation(float saturation)
        {
            ColorSettings.SetSaturation(saturation);
        }

        protected Task SetSaturationAsync(float saturation)
        {
            return ColorSettings.SetSaturationAsync(saturation);
        }

        protected void SetLuminosity(float luminosity)
        {
            ColorSettings.SetLuminosity(luminosity);
        }

        protected Task SetLuminosityAsync(float luminosity)
        {
            return ColorSettings.SetLuminosityAsync(luminosity);
        }

        [Inject]
        private IColorSettings ColorSettings { get; set; }
    }
}
