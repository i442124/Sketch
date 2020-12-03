using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Models
{
    public class StylusSettings : IStylusSettings
    {
        public object StylusTool { get; private set; }

        public StylusMode StylusMode { get; private set; }

        public void UseBrush(IBrushTool brush)
        {
            StylusTool = brush;
            StylusMode = StylusMode.Brush;
        }

        public Task UseBrushAsync(IBrushTool brush)
        {
            throw new NotImplementedException();
        }

        public void UseEraser(IEraserTool eraser)
        {
            StylusTool = eraser;
            StylusMode = StylusMode.Eraser;
        }

        public Task UseEraserAsync(IEraserTool eraser)
        {
            throw new NotImplementedException();
        }

        public void UsePaintBucket(IPaintBucketTool paintBucket)
        {
            StylusTool = paintBucket;
            StylusMode = StylusMode.PaintBucket;
        }

        public Task UsePaintBucketAsync(IPaintBucketTool paintBucket)
        {
            throw new NotImplementedException();
        }
    }
}
