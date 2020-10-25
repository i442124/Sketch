using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public interface IBrushModel
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }

        Task SetSizeAsync(float size);

        Task SetColorAsync(Color color);

        Task SetOpacityAsync(float opacity);
    }
}
