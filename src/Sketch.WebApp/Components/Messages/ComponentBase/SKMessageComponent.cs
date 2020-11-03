using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKMessageComponent : ComponentBase
    {
        [Parameter]
        public Message Message { get; set; }
    }
}
