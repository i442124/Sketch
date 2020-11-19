using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public interface IStylusModel
    {
        StylusMode Mode { get; }

        Task UseBrushAsync(IBrushModel brush);

        Task UseBucketAsync(IBucketModel bucket);

        Task UseEraserAsync(IEraserModel eraser);
    }
}
