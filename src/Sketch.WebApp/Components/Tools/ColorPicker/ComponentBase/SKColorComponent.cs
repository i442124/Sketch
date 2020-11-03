using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public class SKColorComponent : ComponentBase
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<SKColorComponent> ColorSelected { get; set; }
    }
}
