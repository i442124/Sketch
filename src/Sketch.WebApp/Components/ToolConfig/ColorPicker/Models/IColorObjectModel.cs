using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public interface IColorObjectModel
    {
        Color Color { get; }

        Task SetColorAsync(Color color);
    }
}
