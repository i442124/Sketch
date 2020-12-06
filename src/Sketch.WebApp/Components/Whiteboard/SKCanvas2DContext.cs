using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;

using Sketch.Shared.Data;
using Sketch.Shared.Data.Ink;

namespace Sketch.WebApp.Components
{
    public class SKCanvas2DContext
    {
        private readonly SemaphoreSlim _semaphore =
        new SemaphoreSlim(initialCount: 1, maxCount: 1);

        private Batch2D _batch;
        private Context2D _context;

        public SKCanvasComponentBase Component { get; }

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
            if (await _semaphore.WaitAsync(TimeSpan.Zero))
            {
                await _batch.DisposeAsync();
                _batch = await _context.CreateBatchAsync();
                _semaphore.Release();
            }
        }

        public async Task FlushAsync(Func<Task> batch)
        {
            await _semaphore.WaitAsync();
            await batch.Invoke();
            _semaphore.Release();
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            await StrokeAsync(stroke, stroke.Style);
        }

        public async Task StrokeAsync(Stroke stroke, StrokeStyle style)
        {
            await SetStrokeStyleAsync(style);
            await LineAsync(stroke.StylusPoints);
        }

        public async Task WipeAsync(Wipe wipe)
        {
            await WipeAsync(wipe, wipe.Style);
        }

        public async Task WipeAsync(Wipe wipe, WipeStyle style)
        {
            await SetWipeStyleAsync(style);
            await LineAsync(wipe.StylusPoints);
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

        protected async Task SetStrokeStyleAsync(StrokeStyle style)
        {
            await _batch.LineWidthAsync(style.Thickness);
            await _batch.StrokeStyleAsync(style.Color.ToHexString());
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task SetWipeStyleAsync(WipeStyle style)
        {
            await _batch.LineWidthAsync(style.Thickness);
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Destination_Out);
        }

        protected async Task SetFillStyleAsync(FillStyle style)
        {
            await _batch.FillStyleAsync(style.Color.ToHexString());
            await _batch.GlobalCompositeOperationAsync(CompositeOperation.Source_Over);
        }

        protected async Task LineAsync(StylusPointCollection points)
        {
            await LineAsync((IEnumerable<StylusPoint>)points);
        }

        protected async Task LineAsync(IEnumerable<StylusPoint> points)
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
