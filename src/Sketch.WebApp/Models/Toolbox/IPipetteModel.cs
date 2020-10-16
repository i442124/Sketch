using System.Threading;
using System.Threading.Tasks;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Models.Toolbox
{
    public interface IPipetteModel
    {
        Color Color { get; }

        Task SetColorAsync(Color color);
    }
}
