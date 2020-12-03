using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public interface IBrushTool
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }
    }
}
