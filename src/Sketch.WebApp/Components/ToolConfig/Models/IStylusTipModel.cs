using System.Threading.Tasks;

namespace Sketch.WebApp.Components
{
    public interface IStylusTipModel
    {
        float Size { get; }

        Task SetSizeAsync(float size);
    }
}
