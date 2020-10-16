using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Models.Toolbox
{
    public class PipetteModel : IPipetteModel
    {
        public Color Color { get; private set; }

        public Task SetColorAsync(Color color)
        {
            return Task.Run(() => Color = color);
        }
    }
}
