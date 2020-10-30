using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public class SKCanvas2DContext
    {
        private Context2D _context;

        public SKCanvasComponent Component { get; }

        public SKCanvas2DContext(SKCanvasComponent component)
        {
            Component = component;
        }

        public async Task<SKCanvas2DContext> InitializeAsync()
        {
            _context = await Component.CreateCanvas2DAsync();
            await _context.LineCapAsync(LineCap.Round);
            return this;
        }

        public async Task ClearAsync(Clear clear)
        {
            await _context.ClearRectAsync(0, 0, Component.Width, Component.Height);
        }

        public async Task FillAsync(Fill fill)
        {
            await FillAsync(fill, fill.Style);
        }

        public async Task FillAsync(Fill fill, FillStyle style)
        {
            await SetFillStyleAsync(style);

            await _context.FillRectAsync(0, 0, Component.Width, Component.Height);
            await _context.FillAsync(FillRule.EvenOdd);
        }

        public async Task WipeAsync(Wipe wipe)
        {
            await WipeAsync(wipe, wipe.Style);
        }

        public async Task WipeAsync(Wipe wipe, WipeStyle style)
        {
            await SetWipeStyleAsync(style);
            await DrawAsync(wipe.StylusPoints);
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            await StrokeAsync(stroke, stroke.Style);
        }

        public async Task StrokeAsync(Stroke stroke, StrokeStyle style)
        {
            await SetStrokeStyleAsync(style);
            await DrawAsync(stroke.StylusPoints);
        }

        protected async Task SetFillStyleAsync(FillStyle style)
        {
            await _context.FillStyleAsync(style.Color.ToHexString());
            await _context.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task SetWipeStyleAsync(WipeStyle style)
        {
            await _context.LineWidthAsync(style.Thickness);
            await _context.GlobalCompositeOperationAsync(CompositeOperation.Destination_Out);
        }

        protected async Task SetStrokeStyleAsync(StrokeStyle style)
        {
            await _context.LineWidthAsync(style.Thickness);
            await _context.StrokeStyleAsync(style.Color.ToHexString());
            await _context.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task DrawAsync(StylusPointCollection points)
        {
            var enumerator = points.GetEnumerator();
            await using (var batch = await _context.CreateBatchAsync())
            {
                if (enumerator.MoveNext())
                {
                    var point = enumerator.Current;
                    await batch.BeginPathAsync();
                    await batch.MoveToAsync(point.X, point.Y);

                    while (enumerator.MoveNext())
                    {
                        point = enumerator.Current;
                        await batch.LineToAsync(point.X, point.Y);
                    }

                    await batch.StrokeAsync();
                }
            }
        }
    }
}
