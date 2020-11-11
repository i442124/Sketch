using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKUndoComponent : ComponentBase
    {
        protected async Task UndoAsync()
        {
            await Whiteboard.UndoAsync();
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}
