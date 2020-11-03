using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public interface IBrushModel
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }
    }
}
