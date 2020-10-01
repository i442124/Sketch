using System;
using System.Threading;
using System.Threading.Tasks;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponent : BECanvasComponent
    {
        private Canvas2DContext _context;

        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public float Thickness { get; set; }

        [Parameter]
        public EventCallback<LineEvent> OnAfterDraw { get; set; }

        public async Task DrawAsync(Line line)
        {
            await _context.BeginPathAsync();
            await _context.SetStrokeStyleAsync(
                $"#{Color.R:X2}{Color.G:X2}{Color.B:X2}");

            await _context.SetLineWidthAsync(Thickness);
            await _context.SetLineCapAsync(LineCap.Round);

            await _context.MoveToAsync(line.Start.X, line.Start.Y);
            await _context.LineToAsync(line.End.X, line.End.Y);

            await _context.StrokeAsync();
        }

        protected async virtual Task OnAfterDrawAsync(Line line)
        {
            await OnAfterDraw.InvokeAsync(new LineEvent
            {
                Line = line, TimeStamp = DateTime.Now
            });
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await this.CreateCanvas2DAsync();
            }
        }
    }
}
