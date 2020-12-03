using System.Collections.Generic;
using System.Threading.Tasks;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.Shared.Models
{
    public class StylusSettings : IStylusSettings
    {
        public object Tool { get; private set; }

        public StylusMode Mode { get; private set; }

        public void UseBrush(IBrushTool brush)
        {
            Tool = brush;
            Mode = StylusMode.Brush;
        }

        public Task UseBrushAsync(IBrushTool brush)
        {
            return Task.Run(() => UseBrush(brush));
        }

        public void UseEraser(IEraserTool eraser)
        {
            Tool = eraser;
            Mode = StylusMode.Eraser;
        }

        public Task UseEraserAsync(IEraserTool eraser)
        {
            return Task.Run(() => UseEraser(eraser));
        }

        public void UsePaintBucket(IPaintBucketTool paintBucket)
        {
            Tool = paintBucket;
            Mode = StylusMode.PaintBucket;
        }

        public Task UsePaintBucketAsync(IPaintBucketTool paintBucket)
        {
            return Task.Run(() => UsePaintBucket(paintBucket));
        }
    }
}
