using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

namespace Sketch.WebApp.Areas.Tools
{
    public partial class SKStylusSizeSlider : SKStylusSizeComponent
    {
        private async Task OnValueChanged(ChangeEventArgs e)
        {
            await SetStylusSizeAsync(Convert.ToSingle(e.Value));
        }
    }
}
