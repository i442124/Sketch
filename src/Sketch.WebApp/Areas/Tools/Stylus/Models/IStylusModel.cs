using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Tools
{
    public interface IStylusModel
    {
        float Size { get; }

        Task SetSizeAsync(float size);
    }
}
