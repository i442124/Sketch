using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;

namespace Sketch.Shared.Models
{
    public interface IPaintBucketTool
    {
        Color Color { get; }

        float Opacity { get; }
    }
}
