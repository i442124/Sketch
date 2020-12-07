using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Views
{
    public partial class SKBrushSizeSlider
    {
        [Parameter]
        public float MinValue { get; set; }

        [Parameter]
        public float MaxValue { get; set; }

        private async Task OnValueChanged(ChangeEventArgs e)
        {
            await SetBrushSizeAsync(Convert.ToSingle(e.Value));
        }
    }
}
