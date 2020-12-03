using System.Threading;
using System.Threading.Tasks;

namespace Sketch.Shared.Models
{
    public interface IBrushSettings
    {
        float Size { get; }

        float Opacity { get; }

        void SetSize(float size);

        Task SetSizeAsync(float size);

        void SetOpacity(float opacity);

        Task SetOpacityAsync(float opacity);
    }
}
