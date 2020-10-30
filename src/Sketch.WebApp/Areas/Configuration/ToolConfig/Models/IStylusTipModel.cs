using System.Threading.Tasks;

namespace Sketch.WebApp.Areas.Configuration
{
    public interface IStylusTipModel
    {
        float Size { get; }

        Task SetSizeAsync(float size);
    }
}
