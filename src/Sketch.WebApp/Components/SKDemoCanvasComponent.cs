using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp;
using Sketch.WebApp.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKDemoCanvasComponent : ComponentBase
    {
        protected Color Color { get; set; }

        protected float Thickness { get; set; }

        public SKDemoCanvasComponent()
        {
        }

        public void UseRedBrush()
        {
            Color = Colors.Red;
        }

        public void UseBlueBrush()
        {
            Color = Colors.Aqua;
        }

        public void UseGreenBrush()
        {
            Color = Colors.Green;
        }
    }
}
