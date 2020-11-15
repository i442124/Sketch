using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor;
using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Components
{
    public class SKCanvas2DContext
    {
        private Batch2D _batch;
        private Context2D _context;

        public SKCanvasComponent Component { get; }

        public SKCanvas2DContext(SKCanvasComponent component)
        {
            Component = component;
        }

        public async Task<SKCanvas2DContext> InitializeAsync()
        {
            _context = await Component.CreateCanvas2DAsync();
            _batch = await _context.CreateBatchAsync();
            await _batch.LineCapAsync(LineCap.Round);
            return this;
        }

        public async Task FlushAsync()
        {
            await _batch.DisposeAsync();
            _batch = await _context.CreateBatchAsync();
        }

        public async Task ClearAsync()
        {
            await ClearAsync(Clear.All);
        }

        public async Task ClearAsync(Clear clear)
        {
            await _batch.ClearRectAsync(clear.X, clear.Y, clear.Width, clear.Height);
        }

        public async Task FillAsync(Fill fill)
        {
            await FillAsync(fill, fill.Style);
        }

        public async Task FillAsync(Fill fill, FillStyle style)
        {
            await SetFillStyleAsync(style);
            await _batch.FillRectAsync(0, 0, int.MaxValue, int.MaxValue);
            await _batch.FillAsync(FillRule.EvenOdd);
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
            await _batch.FillStyleAsync(style.Color.ToHexString());
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task SetWipeStyleAsync(WipeStyle style)
        {
            await _batch.LineWidthAsync(style.Thickness);
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Destination_Out);
        }

        protected async Task SetStrokeStyleAsync(StrokeStyle style)
        {
            await _batch.LineWidthAsync(style.Thickness);
            await _batch.StrokeStyleAsync(style.Color.ToHexString());
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task DrawAsync(StylusPointCollection points)
        {
            var enumerator = points.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var point = enumerator.Current;

                await _batch.BeginPathAsync();
                await _batch.MoveToAsync(point.X, point.Y);

                while (enumerator.MoveNext())
                {
                    point = enumerator.Current;
                    await _batch.LineToAsync(point.X, point.Y);
                }

                await _batch.StrokeAsync();
            }
        }
    }
}
