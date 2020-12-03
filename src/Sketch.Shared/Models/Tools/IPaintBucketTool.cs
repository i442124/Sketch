using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public interface IPaintBucketTool
    {
        Color Color { get; }

        float Opacity { get; }
    }
}
