using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

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
            Color = Color.Red;
        }

        public void UseBlueBrush()
        {
            Color = Color.DodgerBlue;
        }

        public void UseGreenBrush()
        {
            Color = Color.ForestGreen;
        }
    }
}
