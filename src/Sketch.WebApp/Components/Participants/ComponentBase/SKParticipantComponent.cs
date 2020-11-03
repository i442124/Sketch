using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKParticipantComponent : ComponentBase
    {
        [Parameter]
        public User User { get; set; }
    }
}
