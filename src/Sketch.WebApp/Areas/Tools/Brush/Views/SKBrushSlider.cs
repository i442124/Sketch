using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKBrushSlider : SKBrushSliderComponent
    {
        private async Task OnValueChanged(ChangeEventArgs e)
        {
            await SetBrushSizeAsync(Convert.ToSingle(e.Value));
        }
    }
}
