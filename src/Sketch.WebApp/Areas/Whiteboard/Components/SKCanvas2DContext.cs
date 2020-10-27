using System.Threading;
using System.Threading.Tasks;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using Sketch;
using Sketch.Shared;

namespace Sketch.WebApp.Areas.Whiteboard
{
    public class SKCanvas2DContext
    {
        private Canvas2DContext _context;

        public SKCanvasComponent Component { get; }

        public SKCanvas2DContext(SKCanvasComponent component)
        {
            Component = component;
        }

        public async Task<SKCanvas2DContext> InitializeAsync()
        {
            _context = await Component.CreateCanvas2DAsync();
            return this;
        }

        public async Task StrokeAsync(Stroke stroke)
        {
            await StrokeAsync(stroke, stroke.Style);
        }

        public async Task StrokeAsync(Stroke stroke, StrokeStyle style)
        {
            await _context.SetLineCapAsync(LineCap.Round);
            await _context.SetLineWidthAsync(style.Thickness);
            await _context.SetStrokeStyleAsync(style.Color.ToHexString());

            await _context.BeginBatchAsync();
            var enumerator = stroke.StylusPoints.GetEnumerator();

            if (enumerator.MoveNext())
            {
                var point = enumerator.Current;
                await _context.BeginPathAsync();
                await _context.MoveToAsync(point.X, point.Y);

                while (enumerator.MoveNext())
                {
                    point = enumerator.Current;
                    await _context.LineToAsync(point.X, point.Y);
                    await _context.StrokeAsync();
                }
            }

            await _context.EndBatchAsync();
        }
    }
}
