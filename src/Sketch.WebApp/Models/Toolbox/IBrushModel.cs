using System.Threading.Tasks;
using Sketch.Shared;

namespace Sketch.WebApp.Models.Toolbox
{
    public interface IBrushModel
    {
        float Size { get; }

        Color Color { get; }

        float Opacity { get; }

        Task SetSizeAsync(float size);

        Task SetColorAsync(Color color);

        Task SetOpacityAsync(float opacity);
    }
}
