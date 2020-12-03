﻿using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;
using Sketch.Shared.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponent : SKCanvasComponentBase
    {
        private SKCanvas2DContext _context;

        protected override void OnInitialized()
        {
            Whiteboard.OnStroke(async stroke =>
            {
                await _context.StrokeAsync(stroke);
                await _context.FlushAsync();
            });

            Whiteboard.OnWipe(async wipe =>
            {
                await _context.WipeAsync(wipe);
                await _context.FlushAsync();
            });

            Whiteboard.OnFill(async fill =>
            {
                await _context.FillAsync(fill);
                await _context.FlushAsync();
            });
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await new SKCanvas2DContext(this).InitializeAsync();
            }
        }

        protected Task InvokeActionChanged()
        {
            return Whiteboard.InvokeActionChanged();
        }

        protected Task StrokeAsync(Stroke stroke)
        {
            return Whiteboard.StrokeAsync(stroke);
        }

        protected Task WipeAsync(Wipe wipe)
        {
            return Whiteboard.WipeAsync(wipe);
        }

        protected Task FillAsync(Fill fill)
        {
            return Whiteboard.FillAsync(fill);
        }

        [Inject]
        private IWhiteboardClient Whiteboard { get; set; }
    }
}
