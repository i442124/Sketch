using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public abstract class SKBrushComponent : ComponentBase
    {
        public async Task UseAsync()
        {
            await Stylus.UseBrushAsync(Brush);
        }

        public float Size => Brush.Size;

        public Color Color => Brush.Color;

        public float Opacity => Brush.Opacity;

        [Inject]
        private IBrushModel Brush { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
