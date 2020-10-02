using System;
using System.Threading;
using System.Threading.Tasks;

using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;

using Sketch.Shared;
using Sketch.WebApp;
using Sketch.WebApp.Models;

namespace Sketch.WebApp.Components
{
    public abstract class SKCanvasComponent : BECanvasComponent
    {
        private Canvas2DContext _context;

        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public float Thickness { get; set; }

        [Inject]
        public IWhiteboardModel Whiteboard { get; set; }

        protected async Task SendAsync(Line line)
        {
            await Whiteboard.SendAsync(new Stroke
            {
                Line = line, Options = new StrokeOptions
                {
                    Color = Color, Thickness = Thickness
                }
            });
        }

        protected async Task DrawAsync(Line line)
        {
            var endX = line.End.X;
            var endY = line.End.Y;

            var startX = line.Start.X;
            var startY = line.Start.Y;

            await _context.BeginPathAsync();
            await _context.MoveToAsync(startX, startY);

            await _context.LineToAsync(endX, endY);
            await _context.StrokeAsync();
        }

        protected async Task ReceiveAsync(StrokeEvent e)
        {
            await SetLineOptionsAsync(e.Stroke.Options);
            await DrawAsync(e.Stroke.Line);
        }

        protected async Task SetLineOptionsAsync(StrokeOptions options)
        {
            var color = options.Color;
            await _context.SetStrokeStyleAsync(
                $"#{color.R:X2}{color.G:X2}{color.B:X2}");

            var thickness = options.Thickness;
            await _context.SetLineWidthAsync(thickness);
            await _context.SetLineCapAsync(LineCap.Round);
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _context = await this.CreateCanvas2DAsync();
            }
        }

        protected override void OnInitialized()
        {
            Whiteboard.OnReceive(ReceiveAsync);
        }
    }
}
