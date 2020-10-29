using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKStylusSizeComponent : ComponentBase
    {
        public float Value
        {
            get { return StylusTip.Size; }
        }

        [Parameter]
        public float MaxValue { get; set; }

        [Parameter]
        public float MinValue { get; set; }

        public SKStylusSizeComponent()
        {
        }

        protected async Task SetStylusSizeAsync(float newValue)
        {
            await StylusTip.SetSizeAsync(newValue);
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IStylusTipModel StylusTip { get; set; }
    }
}
