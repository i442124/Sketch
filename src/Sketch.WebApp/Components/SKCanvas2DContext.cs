using System.Threading;
using System.Threading.Tasks;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using Sketch.Shared;

namespace Sketch.WebApp.Components
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
            await StrokeAsync(stroke, stroke.Options);
        }

        public async Task StrokeAsync(Stroke stroke, StrokeOptions options)
        {
            await _context.SetLineCapAsync(LineCap.Round);
            await _context.SetLineWidthAsync(options.Thickness);
            await _context.SetStrokeStyleAsync(options.Color.ToHexString());

            await _context.BeginPathAsync();
            await _context.MoveToAsync(stroke.StartX, stroke.StartY);

            await _context.LineToAsync(stroke.EndX, stroke.EndY);
            await _context.StrokeAsync();
        }
    }
}
