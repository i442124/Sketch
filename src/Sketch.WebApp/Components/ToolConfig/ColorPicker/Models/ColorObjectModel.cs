using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class ColorObjectModel : IColorObjectModel
    {
        public Color Color { get; private set; }

        public async Task SetColorAsync(Color color)
        {
            await Task.Run(() => Color = color);
        }
    }
}
