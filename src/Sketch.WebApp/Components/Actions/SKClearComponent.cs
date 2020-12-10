using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKClearComponent : ComponentBase
    {
        protected Task ClearAsync()
        {
            return ClearAsync(Clear.All);
        }

        protected Task ClearAsync(Clear clear)
        {
            return Whiteboard.ClearAsync(clear);
        }

        [Inject]
        private IWhiteboardClient Whiteboard { get; set; }
    }
}
