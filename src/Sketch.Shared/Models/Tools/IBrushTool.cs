using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;

namespace Sketch.Shared.Models
{
    public interface IBrushTool
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }
    }
}
