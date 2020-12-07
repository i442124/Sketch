using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;

namespace Sketch.Shared.Models
{
    public class PaintBucketTool : IPaintBucketTool
    {
        private readonly IBrushSettings _brushSettings;
        private readonly IColorSettings _colorSettings;

        public PaintBucketTool(
            IBrushSettings brushSettings,
            IColorSettings colorSettings)
        {
            _brushSettings = brushSettings;
            _colorSettings = colorSettings;
        }

        public Color Color => _colorSettings.Color;

        public float Opacity => _brushSettings.Opacity;
    }
}
