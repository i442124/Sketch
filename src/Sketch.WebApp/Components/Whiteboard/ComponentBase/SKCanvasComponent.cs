using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp.Components;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponent : SKCanvasComponentBase
    {
        private SKCanvas2DContext _context;

        protected override void OnInitialized()
        {
            Whiteboard.OnReceive(StrokeAsync);
            Whiteboard.OnReceive(WipeAsync);
            Whiteboard.OnReceive(FillAsync);
            Whiteboard.OnReceive(ClearAsync);
            Whiteboard.OnReceive(UndoAsync);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await new SKCanvas2DContext(this).InitializeAsync();
            }
        }

        protected async Task InvokeStrokeAsync(Stroke stroke)
        {
            await Whiteboard.StrokeAsync(stroke);
        }

        protected async Task InvokeWipeAsync(Wipe wipe)
        {
            await Whiteboard.WipeAsync(wipe);
        }

        protected async Task InvokeFillAsync(Fill fill)
        {
            await Whiteboard.FillAsync(fill);
        }

        protected async Task InvokeActionChanged()
        {
            await Whiteboard.InvokeActionChanged();
        }

        private async Task StrokeAsync(Stroke stroke)
        {
            WhiteboardStorage.Push(stroke, _context.StrokeAsync);
            await _context.StrokeAsync(stroke);
            await _context.FlushAsync();
        }

        private async Task WipeAsync(Wipe wipe)
        {
            WhiteboardStorage.Push(wipe, _context.WipeAsync);
            await _context.WipeAsync(wipe);
            await _context.FlushAsync();
        }

        private async Task FillAsync(Fill fill)
        {
            WhiteboardStorage.Push(fill, _context.FillAsync);
            await _context.FillAsync(fill);
            await _context.FlushAsync();
        }

        private async Task ClearAsync(Clear clear)
        {
            WhiteboardStorage.Push(clear, _context.ClearAsync);
            await _context.ClearAsync(clear);
            await _context.FlushAsync();
        }

        private async Task UndoAsync(Undo undo)
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
