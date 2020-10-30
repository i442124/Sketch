using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Areas.Configuration
{
    public partial class SKSizeSlider : SKSizeComponent
    {
        [Parameter]
        public float MinValue { get; set; }

        [Parameter]
        public float MaxValue { get; set; }

        private async Task OnValueChanged(ChangeEventArgs e)
        {
            await SetStylusSizeAsync(Convert.ToSingle(e.Value));
        }
    }
}
