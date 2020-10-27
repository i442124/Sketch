using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Toolbox
{
    public interface IBrushModel
    {
        public float Size { get; }

        public Color Color { get; }

        public float Opacity { get; }
    }
}
