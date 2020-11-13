using System;
using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor;
using Excubo.Blazor.Canvas;
using Microsoft.AspNetCore.Components;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponent : SKCanvasComponentBase
    {
        private SKCanvas2DContext _context;

        protected override void OnInitialized()
        {
            Whiteboard.OnReceive((Func<StrokeEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<WipeEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<FillEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<ClearEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<UndoEvent, Task>)ReceiveAsync);
        }

        protected async Task StrokeAsync(Stroke stroke)
        {
            await StrokeAsync(stroke, stroke.Style);
        }

        protected async Task StrokeAsync(Stroke stroke, StrokeStyle style)
        {
            await _context.StrokeAsync(stroke, style);
            await _context.FlushAsync();
        }

        protected async Task WipeAsync(Wipe wipe)
        {
            await WipeAsync(wipe, wipe.Style);
        }

        protected async Task WipeAsync(Wipe wipe, WipeStyle style)
        {
            await _context.WipeAsync(wipe, style);
            await _context.FlushAsync();
        }

        protected async Task FillAsync(Fill fill)
        {
            await FillAsync(fill, fill.Style);
        }

        protected async Task FillAsync(Fill fill, FillStyle style)
        {
            await _context.FillAsync(fill, style);
            await _context.FlushAsync();
        }

        protected async Task ClearAsync()
        {
            await ClearAsync(Clear.All);
        }

        protected async Task ClearAsync(Clear clear)
        {
            await _context.ClearAsync();
            await _context.FlushAsync();
        }

        protected async Task SendAsync(Stroke stroke)
        {
            await Whiteboard.SendAsync(stroke);
        }

        protected async Task SendAsync(Wipe wipe)
        {
            await Whiteboard.SendAsync(wipe);
        }

        protected async Task SendAsync(Fill fill)
        {
            await Whiteboard.SendAsync(fill);
        }

        protected async Task InvokeWhiteboardActionChanged()
        {
            await Whiteboard.InvokeActionChanged();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await new SKCanvas2DContext(this).InitializeAsync();
            }
        }

        private async Task ReceiveAsync(StrokeEvent e)
        {
            await StrokeAsync(e.Stroke, e.Stroke.Style);
            WhiteboardStorage.Push(e.Stroke, _context.StrokeAsync);
        }

        private async Task ReceiveAsync(WipeEvent e)
        {
            await WipeAsync(e.Wipe, e.Wipe.Style);
            WhiteboardStorage.Push(e.Wipe, _context.WipeAsync);
        }

        private async Task ReceiveAsync(FillEvent e)
        {
            await FillAsync(e.Fill, e.Fill.Style);
            WhiteboardStorage.Push(e.Fill, _context.FillAsync);
        }

        private async Task ReceiveAsync(ClearEvent e)
        {
            await ClearAsync(e.Clear);
            WhiteboardStorage.Push(e.Clear, _context.ClearAsync);
        }

        private async Task ReceiveAsync(UndoEvent e)
        {
            WhiteboardStorage.Pop();
            await _context.ClearAsync(Clear.All);
            foreach (var whiteboardAction in WhiteboardStorage)
            {
                await whiteboardAction.DrawAsync();
            }

            await _context.FlushAsync();
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }

        [Inject]
        private IWhiteboardStorage WhiteboardStorage { get; set; }
    }
}
