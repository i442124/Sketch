using System;
using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor;
using Excubo.Blazor.Canvas;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public abstract class SKCanvasComponent : SKCanvasComponentBase
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
            await _context.StrokeAsync(stroke, stroke.Style);
            await Whiteboard.SendAsync(stroke);
        }

        protected async Task ReceiveAsync(StrokeEvent e)
        {
            await _context.StrokeAsync(e.Stroke, e.Stroke.Style);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            _context = await new SKCanvas2DContext(this).InitializeAsync();
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}
