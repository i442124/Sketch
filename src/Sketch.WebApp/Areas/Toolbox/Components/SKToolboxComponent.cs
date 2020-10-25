using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using Sketch.Shared;
using Sketch.WebApp.Areas.Tools;

namespace Sketch.WebApp.Areas.Toolbox
{
    public class SKToolboxComponent : ComponentBase
    {
        public Color Color
        {
            get { return Brush.Color; }
        }

        public float Size
        {
            get { return Brush.Size; }
        }

        [Parameter]
        public EventCallback UpdateState { get; set; }

        public SKToolboxComponent()
        {
        }

        [Inject]
        private IBrushModel Brush { get; set; }
    }
}
