using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.WebApp.Models;
using Sketch.WebApp.Models.Toolbox;

namespace Sketch.WebApp.Components.Toolbox
{
    public class SKBrushSizeSliderComponent : ComponentBase
    {
        [Parameter]
        public float MinValue { get; set; }

        [Parameter]
        public float MaxValue { get; set; }

        [Parameter]
        public EventCallback<float> SizeHasChanged { get; set; }

        public SKBrushSizeSliderComponent()
        {
        }

        protected async Task SetBrushSizeAsync(float size)
        {
            await Brush.SetSizeAsync(size);
            await SizeHasChanged.InvokeAsync(size);
        }

        [Inject]
        public IBrushModel Brush { get; set; }
    }
}
