﻿using System;
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

        public SKCanvasComponent()
        {
        }

        protected override void OnInitialized()
        {
            Whiteboard.OnReceive((Func<FillEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<WipeEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<ClearEvent, Task>)ReceiveAsync);
            Whiteboard.OnReceive((Func<StrokeEvent, Task>)ReceiveAsync);
        }

        protected async Task SendAsync(Fill fill)
        {
            await _context.FillAsync(fill, fill.Style);
            await Whiteboard.SendAsync(fill);
        }

        protected async Task SendAsync(Wipe wipe)
        {
            await _context.WipeAsync(wipe, wipe.Style);
            await Whiteboard.SendAsync(wipe);
        }

        protected async Task SendAsync(Stroke stroke)
        {
            await _context.StrokeAsync(stroke, stroke.Style);
            await Whiteboard.SendAsync(stroke);
        }

        protected async Task ReceiveAsync(FillEvent e)
        {
            await _context.FillAsync(e.Fill, e.Fill.Style);
        }

        protected async Task ReceiveAsync(WipeEvent e)
        {
            await _context.WipeAsync(e.Wipe, e.Wipe.Style);
        }

        protected async Task ReceiveAsync(ClearEvent e)
        {
            await _context.ClearAsync(e.Clear);
        }

        protected async Task ReceiveAsync(StrokeEvent e)
        {
            await _context.StrokeAsync(e.Stroke, e.Stroke.Style);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await new SKCanvas2DContext(this).InitializeAsync();
            }
        }

        [Inject]
        private IWhiteboardModel Whiteboard { get; set; }
    }
}