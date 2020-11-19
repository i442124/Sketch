using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Sketch.WebApp.Components
{
    public partial class SKSizeSlider : SKSizePickerComponent
    {
        [Parameter]
        public float MinValue { get; set; }

        [Parameter]
        public float MaxValue { get; set; }

        private async Task OnValueChanged(ChangeEventArgs e)
        {
            await SetValueAsync(Convert.ToSingle(e.Value));
        }
    }
}
