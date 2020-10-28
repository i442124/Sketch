using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public interface IStylusModel
    {
        StylusMode Mode { get; }

        Task UseBrushAsync();

        Task UseEraserAsync();

        Task UseBucketAsync();
    }
}
