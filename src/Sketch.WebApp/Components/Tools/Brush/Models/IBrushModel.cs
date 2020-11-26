using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IBrushModel
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }
    }
}
