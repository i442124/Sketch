using System;
using System.Threading;
using System.Threading.Tasks;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;
using Sketch.WebApp.Areas;
using Sketch.WebApp.Areas.Subscriptions;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public abstract class SKCanvasComponent : BECanvasComponent
    {
        private SKCanvas2DContext _context;

        public SKCanvasComponent()
        {
        }

        protected override void OnInitialized()
        {
            Whiteboard.OnReceive(ReceiveAsync);
        }

        protected async Task SendAsync(Stroke stroke)
        {
            await _context.StrokeAsync(stroke, stroke.Options);
            await Whiteboard.SendAsync(stroke);
        }

        protected async Task ReceiveAsync(StrokeEvent e)
        {
            await _context.StrokeAsync(e.Stroke, e.Stroke.Options);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            _context = await new SKCanvas2DContext(this).InitializeAsync();
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}
