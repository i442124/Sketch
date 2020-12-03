using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Models
{
    public interface IStylusSettings
    {
        StylusMode StylusMode { get; }

        void UseBrush(IBrushTool brush);

        Task UseBrushAsync(IBrushTool brush);

        void UseEraser(IEraserTool eraser);

        Task UseEraserAsync(IEraserTool eraser);

        void UsePaintBucket(IPaintBucketTool paintBucket);

        Task UsePaintBucketAsync(IPaintBucketTool paintBucket);
    }
}
