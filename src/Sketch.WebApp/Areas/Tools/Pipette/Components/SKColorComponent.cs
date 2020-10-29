using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKColorComponent : ComponentBase
    {
        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public EventCallback<SKColorComponent> ColorSelected { get; set; }
    }
}
