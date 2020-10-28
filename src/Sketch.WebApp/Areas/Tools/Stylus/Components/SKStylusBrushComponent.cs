using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKStylusBrushComponent : ComponentBase
    {
        public float Size => Brush.Size;

        public Color Color => Brush.Color;

        protected async Task UseBrushAsync()
        {
            await Stylus.UseBrushAsync();
        }

        [Inject]
        private IBrushModel Brush { get; set; }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
