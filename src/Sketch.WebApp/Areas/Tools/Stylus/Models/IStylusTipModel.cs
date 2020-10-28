using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Tools
{
    public interface IStylusTipModel
    {
        float Size { get; }

        Task SetSizeAsync(float size);
    }
}
