using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public interface IBrushModel
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }
    }
}
