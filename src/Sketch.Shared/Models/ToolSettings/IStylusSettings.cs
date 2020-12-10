using System.Collections.Generic;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.Shared.Data;

namespace Sketch.Shared.Models
{
    public interface IStylusSettings
    {
        StylusMode Mode { get; }

        void UseBrush(IBrushTool brush);

        Task UseBrushAsync(IBrushTool brush);

        void UseEraser(IEraserTool eraser);

        Task UseEraserAsync(IEraserTool eraser);

        void UsePaintBucket(IPaintBucketTool paintBucket);

        Task UsePaintBucketAsync(IPaintBucketTool paintBucket);
    }
}
