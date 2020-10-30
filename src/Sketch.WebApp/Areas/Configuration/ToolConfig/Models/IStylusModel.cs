using System.Threading;
using System.Threading.Tasks;

using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Configuration
{
    public interface IStylusModel
    {
        StylusMode Mode { get; }

        Task UseBrushAsync(IBrushModel brush);

        Task UseBucketAsync(IBucketModel bucket);

        Task UseEraserAsync(IEraserModel eraser);
    }
}
