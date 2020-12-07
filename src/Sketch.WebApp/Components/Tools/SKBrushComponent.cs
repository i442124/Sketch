using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Data.Ink.Colors;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKBrushComponent : ComponentBase
    {
        public float Size
        {
            get { return Brush.Size; }
        }

        public Color Color
        {
            get { return Brush.Color; }
        }

        public float Opacity
        {
            get { return Brush.Opacity; }
        }

        public void UseBrush()
        {
            StylusSettings.UseBrush(Brush);
        }

        public async Task UseBrushAsync()
        {
            await StylusSettings.UseBrushAsync(Brush);
        }

        [Inject]
        private IBrushTool Brush { get; set; }

        [Inject]
        private IStylusSettings StylusSettings { get; set; }
    }
}
