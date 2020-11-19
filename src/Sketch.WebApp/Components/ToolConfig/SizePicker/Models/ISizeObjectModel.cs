using System.Threading;
using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public interface ISizeObjectModel
    {
        float Size { get; }

        Task SetSizeAsync(float newSize);
    }
}
