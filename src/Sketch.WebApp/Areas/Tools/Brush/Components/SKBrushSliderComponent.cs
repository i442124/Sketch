using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKBrushSliderComponent : ComponentBase
    {
        public float Value { get; set; }

        [Parameter]
        public float MinValue { get; set; }

        [Parameter]
        public float MaxValue { get; set; }

        [Parameter]
        public EventCallback<float> SizeHasChanged { get; set; }

        public SKBrushSliderComponent()
        {
        }

        protected async Task SetBrushSizeAsync(float size)
        {
            await Brush.SetSizeAsync(size);
            await SizeHasChanged.InvokeAsync(size);

            Value = size;
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IBrushModel Brush { get; set; }
    }
}
