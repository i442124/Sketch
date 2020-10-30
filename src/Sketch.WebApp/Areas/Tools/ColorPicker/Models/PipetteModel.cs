using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
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
