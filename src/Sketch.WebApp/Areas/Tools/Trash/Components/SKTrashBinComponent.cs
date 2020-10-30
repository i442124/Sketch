using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Whiteboard;

namespace Sketch.WebApp.Areas.Tools
{
    public class SKTrashBinComponent : ComponentBase
    {
        protected async Task ClearWhiteboardAsync()
        {
            await Whiteboard.SendAsync(new Clear
            {
                Width = int.MaxValue, Height = int.MaxValue
            });
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}
