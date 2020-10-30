using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Configuration;

namespace Sketch.WebApp.Areas.Tools
{
    public abstract class SKBrushComponent : ComponentBase
    {
        public float Size => Brush.Size;

        public Color Color => Brush.Color;

        public float Opacity => Brush.Opacity;

        protected Task UseBrushAsync()
        {
            return Stylus.UseBrushAsync(Brush);
        }

        [Inject]
        private IBrushModel Brush { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
