using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class PipetteModel : IPipetteModel
    {
        public Color Color { get; set; }

        public Task SetColorAsync(Color color)
        {
            return Task.Run(() => Color = color);
        }
    }
}
