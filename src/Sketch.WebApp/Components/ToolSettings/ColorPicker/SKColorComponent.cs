using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKColorComponent : ComponentBase
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<SKColorComponent> ColorSelected { get; set; }
    }
}
