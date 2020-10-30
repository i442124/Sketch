using System.Threading;
using System.Threading.Tasks;

using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Configuration
{
    public class StylusModel : IStylusModel
    {
        public StylusMode Mode { get; private set; }

        public Task UseBrushAsync(IBrushModel brush)
        {
            return Task.Run(() => Mode = StylusMode.Brush);
        }

        public Task UseBucketAsync(IBucketModel bucket)
        {
            return Task.Run(() => Mode = StylusMode.Fill);
        }

        public Task UseEraserAsync(IEraserModel eraser)
        {
            return Task.Run(() => Mode = StylusMode.Erase);
        }
    }
}
