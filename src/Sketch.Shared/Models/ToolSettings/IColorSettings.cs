using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public interface IColorSettings
    {
        Color Color { get; }

        void SetColor(Color color);

        Task SetColorAsync(Color color);

        void SetHue(float hue);

        Task SetHueAsync(float hue);

        void SetSaturation(float saturation);

        Task SetSaturationAsync(float saturation);

        void SetLuminosity(float luminosity);

        Task SetLuminosityAsync(float luminosity);
    }
}
