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
            get { return Stylus.Size; }
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
            await Stylus.SetSizeAsync(newValue);
            await InvokeAsync(StateHasChanged);
        }

        [Inject]
        private IStylusModel Stylus { get; set; }
    }
}
