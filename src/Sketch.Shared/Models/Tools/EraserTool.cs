using Sketch.Shared;
using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public class EraserTool : IEraserTool
    {
        private readonly IBrushSettings _brushSettings;
        private readonly IColorSettings _colorSettings;

        public EraserTool(
            IBrushSettings brushSettings,
            IColorSettings colorSettings)
        {
            _brushSettings = brushSettings;
            _colorSettings = colorSettings;
        }

        public float Size => _brushSettings.Size;

        public Color Color => _colorSettings.Color;

        public float Opacity => _brushSettings.Opacity;
    }
}
